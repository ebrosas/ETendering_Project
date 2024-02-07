namespace Tendering.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Telerik.Web.UI;
    using Tendering.Application;
    using Tendering.Persistence;
    using Attachment = GARMCO.AMS.B2B.eTendering.DAL.SupplierOrderAttachItem;

    public partial class ViewTender : GARMCO.AMS.Common.Web.BaseWebForm
    {
        protected override void OnInitComplete(EventArgs e)
        {
            this.responses.DetailTableDataBind += new GridDetailTableDataBindEventHandler(this.BindOptions);
            this.responses.ItemCommand += new GridCommandEventHandler(this.HandleResponsesItemCommand);
            this.responses.ItemDataBound += new GridItemEventHandler(this.HandleResponsesItemDataBound);

            AjaxSetting ajaxSetting = new AjaxSetting(this.Master.AjaxMngr.ClientID);
            ajaxSetting.UpdatedControls.Add(new AjaxUpdatedControl(this.panEntry.ID, this.ajaxLoadingPanel.ID));
            this.Master.AjaxMngr.AjaxSettings.Add(ajaxSetting);

            base.OnPreInit(e);
        }

        private void BindOptions(object sender, GridDetailTableDataBindEventArgs e)
        {
            var parentKeyName = e.DetailTableView.ParentItem.OwnerTableView.DataKeyNames.Single();
            var requirementNumber = (int)e.DetailTableView.ParentItem.GetDataKeyValue(parentKeyName);

            e.DetailTableView.DataSource = this.OptionsDataSource[requirementNumber].Values;
        }
        private void HandleResponsesItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "offers")
            {
                if (e.CommandName == "ViewSupplierAttachments")
                {
                    var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
                    var requirementNumber = (int)((GridDataItem)e.Item).GetDataKeyValue(keyName);

                    this.Session["ItemListSupplierOrderAttach"] =
                        this
                        .OffersDataSource[requirementNumber]
                        .Attachments;

                    var parameters = new
                    {
                        AjaxManagerId = this.Master.AjaxMngr.ClientID,
                        AttachmentType = 2,
                        IsOption = "false",
                        ViewOnly = "true"
                    };

                    var script =
                        string.Format(
                            "ShowSupplierOrderAttachmentLookup('{0}', {1}, {2}, {3});",
                            parameters.AjaxManagerId,
                            parameters.AttachmentType,
                            parameters.IsOption,
                            parameters.ViewOnly);

                    ScriptManager
                        .RegisterClientScriptBlock(
                            this,
                            this.GetType(),
                            "ViewSupplierAttachments",
                            script,
                            true);
                }
            }

            if (e.Item.OwnerTableView.Name == "options")
            {
                if (e.CommandName == "ViewSupplierAttachments")
                {
                    var parentKeyName = e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyNames.Single();
                    var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
                    var requirementNumber = (int)e.Item.OwnerTableView.ParentItem.GetDataKeyValue(parentKeyName);
                    var description = (string)((GridDataItem)e.Item).GetDataKeyValue(keyName);

                    this.Session["ItemListSupplierOrderAttach"] =
                        this
                        .OptionsDataSource[requirementNumber][description]
                        .Attachments;

                    var parameters = new
                    {
                        AjaxManagerId = this.Master.AjaxMngr.ClientID,
                        AttachmentType = 2,
                        IsOption = "true",
                        ViewOnly = "true"
                    };

                    var script =
                        string.Format(
                            "ShowSupplierOrderAttachmentLookup('{0}', {1}, {2}, {3});",
                            parameters.AjaxManagerId,
                            parameters.AttachmentType,
                            parameters.IsOption,
                            parameters.ViewOnly);

                    ScriptManager
                        .RegisterClientScriptBlock(
                            this,
                            this.GetType(),
                            "ViewSupplierAttachments",
                            script,
                            true);
                }
            }
        }
        private void HandleResponsesItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.OwnerTableView.Name == "offers")
            {
                if (e.Item is GridDataItem)
                {
                    var item = (GridDataItem)e.Item;
                    var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
                    var requirementNumber = (int)item.GetDataKeyValue(keyName);
                    var offer = (Offer)e.Item.DataItem;

                    this.SetViewAttachmentsButton(
                        item["TextAttachments"]
                            .Controls
                            .Cast<ImageButton>()
                            .Single(),
                        new
                        {
                            requirementNumber,
                            Count = offer.Requirement.TextAttachments,
                            Type = 0
                        });

                    this.SetViewAttachmentsButton(
                        item["FileAttachments"]
                            .Controls
                            .Cast<ImageButton>()
                            .Single(),
                        new
                        {
                            requirementNumber,
                            Count = offer.Requirement.FileAttachments,
                            Type = 1
                        });
                }
            }
        }
        private void Initialize(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.panEntry.GroupingText = string.Format(this.panEntry.GroupingText, this.CallForTendersId);

                var tender = this.service.FindTender(this.TenderId);

                this.currency.Text = this.currencyRepository.FindByKey(tender.Currency).Value;
                this.deliveryPoint.Text = tender.DeliveryPoint;
                this.deliveryTerms.Text = this.deliveryTermsRepository.FindByKey(tender.DeliveryTerms).Value;
                this.deliveryTime.Text = tender.DeliveryTime.ToString();
                this.expiryDate.Text = tender.ExpiryDate.ToString("dd/MM/yyyy");
                this.paymentTerms.Text = tender.PaymentTerms;

                tender
                    .Charges
                    .Select(charge =>
                    {
                        this.ChargesDataSource.Add(charge.Description, this.chargeDisassembler.Disassemble(charge));
                        return true;
                    })
                    .ToArray();

                this.OffersDataSource = new Dictionary<int, Offer>();
                this.OptionsDataSource = new Dictionary<int, Dictionary<string, Option>>();

                tender
                    .Offers
                    .Select(offer =>
                    {
                        this.OffersDataSource.Add(offer.Requirement.Number, this.offerDisassembler.Disassemble(offer));
                        this.OptionsDataSource.Add(offer.Requirement.Number, new Dictionary<string, Option>());

                        this
                            .optionDisassembler
                            .Disassemble(offer.Requirement.Number, offer.Options)
                            .Select(option =>
                            {
                                this.OptionsDataSource[option.RequirementNumber].Add(option.Description, option);
                                return true;
                            })
                            .ToArray();
                        return true;
                    })
                    .ToArray();
            }

            this.responses.MasterTableView.DataSource = this.OffersDataSource.Values;
            this.charges.MasterTableView.DataSource = this.ChargesDataSource.Values;
        }

        private void SetViewAttachmentsButton(ImageButton viewAttachments, dynamic attachments)
        {
            if (attachments.Count > 0)
            {
                viewAttachments.Enabled = true;
                viewAttachments.ImageUrl = "~/includes/images/attach_small_f1.gif";

                viewAttachments.OnClientClick =
                    string.Format(
                    "ShowRFQDetailAttachmentLookup('', {0}, {1}, -1, -1, false, {2}, true); return false;",
                    this.CallForTendersId,
                    attachments.requirementNumber,
                    attachments.Type);
            }
            else
            {
                viewAttachments.ImageUrl = "~/includes/images/attach_small_f3.gif";
                viewAttachments.Enabled = false;
            }
        }

        private int CallForTendersId
        {
            get
            {
                return (int)Session["callForTendersId"];
            }
        }
        private int TenderId
        {
            get
            {
                return (int)Session["tenderId"];
            }
        }

        private Dictionary<string, Charge> ChargesDataSource
        {
            get
            {
                return (Dictionary<string, Charge>)
                (
                    this.ViewState["charges"] == null ?
                    this.ViewState["charges"] = new Dictionary<string, Charge>() :
                    this.ViewState["charges"]
                );
            }
            set
            {
                this.ViewState["charges"] = value;
            }
        }
        private Dictionary<int, Offer> OffersDataSource
        {
            get
            {
                return (Dictionary<int, Offer>)this.Session["offers"];
            }
            set
            {
                this.Session["offers"] = value;
            }
        }
        private Dictionary<int, Dictionary<string, Option>> OptionsDataSource
        {
            get
            {
                return (Dictionary<int, Dictionary<string, Option>>)this.Session["options"];
            }
            set
            {
                this.Session["options"] = value;
            }
        }

        protected ViewTender()
        {
            this.chargeAssembler = new ChargeAssembler();

            this.chargeDisassembler =
                new ChargeDisassembler(
                    new CurrencyRepository(Configuration.ConnectionString));

            this.currencyRepository = new CurrencyRepository(Configuration.ConnectionString);

            this.deliveryTermsRepository = new DeliveryTermsRepository(Configuration.ConnectionString);
            this.offerAssembler = new OfferAssembler();

            this.offerDisassembler =
                new OfferDisassembler(
                    new UnitOfMeasureRepository(Configuration.ConnectionString));

            this.optionDisassembler =
                new OptionDisassembler(
                    new CurrencyRepository(Configuration.ConnectionString),
                    new DeliveryTermsRepository(Configuration.ConnectionString),
                    new UnitOfMeasureRepository(Configuration.ConnectionString));

            this.service =
                new Service(
                    new ApplicationUserRepository(Configuration.ConnectionString),
                    new CallForTendersRepository(Configuration.ConnectionString),
                    new TenderRepository(Configuration.ConnectionString));

            this.unitOfMeasureRepository = new UnitOfMeasureRepository(Configuration.ConnectionString);

            this.Load += new EventHandler(this.Initialize);
        }

        private readonly ChargeAssembler chargeAssembler;
        private readonly ChargeDisassembler chargeDisassembler;
        private readonly CurrencyRepository currencyRepository;
        private readonly DeliveryTermsRepository deliveryTermsRepository;
        private readonly OfferAssembler offerAssembler;
        private readonly OfferDisassembler offerDisassembler;
        private readonly OptionDisassembler optionDisassembler;
        private readonly Service service;
        private readonly UnitOfMeasureRepository unitOfMeasureRepository;
    }
}