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

    public partial class ReviseTender : GARMCO.AMS.Common.Web.BaseWebForm
    {
        protected override void OnInitComplete(EventArgs e)
        {
            this.charges.InsertCommand += new GridCommandEventHandler(this.InsertCharge);
            this.charges.UpdateCommand += new GridCommandEventHandler(this.UpdateCharge);
            this.charges.DeleteCommand += new GridCommandEventHandler(this.DeleteCharge);
            this.charges.ItemDataBound += new GridItemEventHandler(this.HandleChargesItemDataBound);

            this.responses.DetailTableDataBind += new GridDetailTableDataBindEventHandler(this.BindOptions);
            this.responses.InsertCommand += new GridCommandEventHandler(this.InsertOption);
            this.responses.UpdateCommand += new GridCommandEventHandler(this.UpdateResponse);
            this.responses.DeleteCommand += new GridCommandEventHandler(this.DeleteOption);
            this.responses.ItemCommand += new GridCommandEventHandler(this.HandleResponsesItemCommand);
            this.responses.ItemDataBound += new GridItemEventHandler(this.HandleResponsesItemDataBound);

            this.save.Click += new EventHandler(this.SaveTender);

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
        private void DeleteCharge(object sender, GridCommandEventArgs e)
        {
            var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
            var description = (string)((GridDataItem)e.Item).GetDataKeyValue(keyName);

            this.ChargesDataSource.Remove(description);
            this.charges.MasterTableView.DataSource = this.ChargesDataSource.Values;
        }
        private void DeleteOption(object sender, GridCommandEventArgs e)
        {
            var parentKeyName = e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyNames.Single();
            var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
            var requirementNumber = (int)e.Item.OwnerTableView.ParentItem.GetDataKeyValue(parentKeyName);
            var description = (string)((GridDataItem)e.Item).GetDataKeyValue(keyName);

            this.OptionsDataSource[requirementNumber].Remove(description);
            this.responses.MasterTableView.DataSource = this.OffersDataSource.Values;
        }
        private void HandleChargesItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                var currency = (DropDownList)e.Item.FindControl("currency");
                currency.DataSource = this.currencyRepository.GetAll();
                currency.DataTextField = "Value";
                currency.DataValueField = "Key";
                currency.DataBind();

                if (e.Item.ItemIndex != -1)
                {
                    var charge = (Charge)e.Item.DataItem;
                    currency.SelectedValue = charge.Currency.Key;
                }
                else
                {
                    currency.SelectedValue = this.Currency;
                }
            }
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
                        ViewOnly = "false"
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
                        ViewOnly = "false"
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

            if (e.Item.OwnerTableView.Name == "options")
            {
                if (e.Item.IsInEditMode)
                {
                    var currency = (DropDownList)e.Item.FindControl("currency");
                    currency.DataSource = this.currencyRepository.GetAll();
                    currency.DataTextField = "Value";
                    currency.DataValueField = "Key";
                    currency.DataBind();

                    var deliveryTerms = (DropDownList)e.Item.FindControl("deliveryTerms");
                    deliveryTerms.DataSource = this.deliveryTermsRepository.GetAll();
                    deliveryTerms.DataTextField = "Value";
                    deliveryTerms.DataValueField = "Key";
                    deliveryTerms.DataBind();

                    var unitOfMeasure = (DropDownList)e.Item.FindControl("unitOfMeasure");
                    unitOfMeasure.DataSource = this.unitOfMeasureRepository.GetAll();
                    unitOfMeasure.DataTextField = "Value";
                    unitOfMeasure.DataValueField = "Key";
                    unitOfMeasure.DataBind();

                    if (e.Item.ItemIndex != -1)
                    {
                        var option = ((Option)e.Item.DataItem);
                        currency.SelectedValue = option.Currency.Key;
                        deliveryTerms.SelectedValue = option.DeliveryTerms.Key;
                        unitOfMeasure.SelectedValue = option.UnitOfMeasure.Key;
                    }
                    else
                    {
                        currency.SelectedValue = this.currency.SelectedValue; ;
                        ((TextBox)e.Item.FindControl("deliveryPoint")).Text = this.deliveryPoint.Text;
                        deliveryTerms.SelectedValue = this.DeliveryTerms;
                        ((TextBox)e.Item.FindControl("deliveryTime")).Text = this.deliveryTime.Text;
                        ((RadDatePicker)e.Item.FindControl("expiryDate")).SelectedDate = this.expiryDate.SelectedDate;
                    }
                }
            }
        }
        private void InsertCharge(object sender, GridCommandEventArgs e)
        {
            var description = ((TextBox)e.Item.FindControl("description")).Text;
            var amount = double.Parse(((TextBox)e.Item.FindControl("amount")).Text);
            var currency = ((DropDownList)e.Item.FindControl("currency")).SelectedItem;
            var charge = new Charge(amount, new KeyValuePair<string, string>(currency.Value, currency.Text), description);

            this.ChargesDataSource.Add(description, charge);
            this.charges.MasterTableView.DataSource = this.ChargesDataSource.Values;
        }
        private void Initialize(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.message.Visible = false;

                this.currency.DataSource = this.currencyRepository.GetAll();
                this.currency.DataTextField = "Value";
                this.currency.DataValueField = "Key";
                this.currency.DataBind();

                this.deliveryTerms.DataSource = this.deliveryTermsRepository.GetAll();
                this.deliveryTerms.DataTextField = "Value";
                this.deliveryTerms.DataValueField = "Key";
                this.deliveryTerms.DataBind();

                this.panEntry.GroupingText = string.Format(this.panEntry.GroupingText, this.CallForTendersId);

                var tender = this.service.FindTender(this.TenderId);

                this.currency.SelectedValue = tender.Currency;
                this.deliveryPoint.Text = tender.DeliveryPoint;
                this.deliveryTerms.SelectedValue = tender.DeliveryTerms;
                this.deliveryTime.Text = tender.DeliveryTime.ToString();
                this.expiryDate.SelectedDate = tender.ExpiryDate;
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
        private void InsertOption(object sender, GridCommandEventArgs e)
        {
            var parentKeyName = e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyNames.Single();
            var requirementNumber = (int)e.Item.OwnerTableView.ParentItem.GetDataKeyValue(parentKeyName);
            var description = ((TextBox)e.Item.FindControl("description")).Text;
            var manufacturerCode = ((TextBox)e.Item.FindControl("manufacturerCode")).Text;
            var quantity = int.Parse(((TextBox)e.Item.FindControl("quantity")).Text);
            var supplierCode = ((TextBox)e.Item.FindControl("supplierCode")).Text;
            var unitCost = double.Parse(((TextBox)e.Item.FindControl("unitCost")).Text);
            var unitOfMeasure = ((DropDownList)e.Item.FindControl("unitOfMeasure")).SelectedItem;
            var currency = ((DropDownList)e.Item.FindControl("currency")).SelectedItem;
            var deliveryPoint = ((TextBox)e.Item.FindControl("deliveryPoint")).Text;
            var deliveryTerms = ((DropDownList)e.Item.FindControl("deliveryTerms")).SelectedItem;
            var deliveryTime = int.Parse(((TextBox)e.Item.FindControl("deliveryTime")).Text);
            var expiryDate = ((RadDatePicker)e.Item.FindControl("expiryDate")).SelectedDate.Value;

            var option =
                new Option(
                    new Attachment[0],
                    new KeyValuePair<string, string>(currency.Value, currency.Text),
                    deliveryPoint,
                    new KeyValuePair<string, string>(deliveryTerms.Value, deliveryTerms.Text),
                    deliveryTime,
                    description,
                    expiryDate,
                    manufacturerCode,
                    quantity,
                    requirementNumber,
                    supplierCode,
                    unitCost,
                    new KeyValuePair<string, string>(unitOfMeasure.Value, unitOfMeasure.Text));

            this.OptionsDataSource[requirementNumber].Add(description, option);
            this.responses.MasterTableView.DataSource = this.OffersDataSource.Values;
        }
        private void SaveTender(object sender, EventArgs e)
        {
            if (!this.IsValid) return;

            var result =
                this
                    .service
                    .UpdateTender(
                        this.TenderId,
                        this.CallForTendersId,
                        this.chargeAssembler.Assemble(this.Charges),
                        this.Currency,
                        this.DeliveryPoint,
                        this.DeliveryTerms,
                        this.DeliveryTime,
                        this.ExpiryDate,
                        this.offerAssembler.Assemble(this.Offers, this.OptionsDataSource),
                        this.PaymentTerms,
                        this.Username);

            if (!string.IsNullOrWhiteSpace(result.Item1))
            {
                this.message.InnerText = result.Item1;
                this.message.Visible = true;
                return;
            }

            Session["tenderId"] = result.Item2;
            Response.Redirect("ViewTender.aspx");
        }
        private void UpdateCharge(object sender, GridCommandEventArgs e)
        {
            var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
            var description = (string)((GridDataItem)e.Item).GetDataKeyValue(keyName);
            var amount = double.Parse(((TextBox)e.Item.FindControl("amount")).Text);
            var currency = ((DropDownList)e.Item.FindControl("currency")).SelectedItem;
            var charge = new Charge(amount, new KeyValuePair<string, string>(currency.Value, currency.Text), description);

            this.ChargesDataSource.Remove(description);
            this.ChargesDataSource.Add(description, charge);
            this.charges.MasterTableView.DataSource = this.ChargesDataSource.Values;
        }
        private void UpdateResponse(object sender, GridCommandEventArgs e)
        {
            switch (e.Item.OwnerTableView.Name)
            {
                case "offers":
                    this.UpdateOffer(e);
                    break;
                case "options":
                    this.UpdateOption(e);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            this.responses.MasterTableView.DataSource = this.OffersDataSource.Values;
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
        private void UpdateOffer(GridCommandEventArgs e)
        {
            var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
            var requirementNumber = (int)((GridDataItem)e.Item).GetDataKeyValue(keyName);
            var oldOffer = this.OffersDataSource[requirementNumber];
            var manufacturerCode = ((TextBox)e.Item.FindControl("manufacturerCode")).Text;
            var remarks = ((TextBox)e.Item.FindControl("remarks")).Text;
            var supplierCode = ((TextBox)e.Item.FindControl("supplierCode")).Text;
            var unitCost = double.Parse(((TextBox)e.Item.FindControl("unitCost")).Text);
            var newOffer = new Offer(oldOffer.Attachments, manufacturerCode, remarks, oldOffer.Requirement, supplierCode, unitCost);

            this.OffersDataSource.Remove(requirementNumber);
            this.OffersDataSource.Add(requirementNumber, newOffer);
        }
        private void UpdateOption(GridCommandEventArgs e)
        {
            var parentKeyName = e.Item.OwnerTableView.ParentItem.OwnerTableView.DataKeyNames.Single();
            var keyName = e.Item.OwnerTableView.DataKeyNames.Single();
            var requirementNumber = (int)e.Item.OwnerTableView.ParentItem.GetDataKeyValue(parentKeyName);
            var description = (string)((GridDataItem)e.Item).GetDataKeyValue(keyName);
            var oldOption = this.OptionsDataSource[requirementNumber][description];
            var manufacturerCode = ((TextBox)e.Item.FindControl("manufacturerCode")).Text;
            var quantity = int.Parse(((TextBox)e.Item.FindControl("quantity")).Text);
            var supplierCode = ((TextBox)e.Item.FindControl("supplierCode")).Text;
            var unitCost = double.Parse(((TextBox)e.Item.FindControl("unitCost")).Text);
            var unitOfMeasure = ((DropDownList)e.Item.FindControl("unitOfMeasure")).SelectedItem;
            var currency = ((DropDownList)e.Item.FindControl("currency")).SelectedItem;
            var deliveryPoint = ((TextBox)e.Item.FindControl("deliveryPoint")).Text;
            var deliveryTerms = ((DropDownList)e.Item.FindControl("deliveryTerms")).SelectedItem;
            var deliveryTime = int.Parse(((TextBox)e.Item.FindControl("deliveryTime")).Text);
            var expiryDate = ((RadDatePicker)e.Item.FindControl("expiryDate")).SelectedDate.Value;

            var option =
                new Option(
                    oldOption.Attachments,
                    new KeyValuePair<string, string>(currency.Value, currency.Text),
                    deliveryPoint,
                    new KeyValuePair<string, string>(deliveryTerms.Value, deliveryTerms.Text),
                    deliveryTime,
                    description,
                    expiryDate,
                    manufacturerCode,
                    quantity,
                    requirementNumber,
                    supplierCode,
                    unitCost,
                    new KeyValuePair<string, string>(unitOfMeasure.Value, unitOfMeasure.Text));

            this.OptionsDataSource[requirementNumber].Remove(description);
            this.OptionsDataSource[requirementNumber].Add(description, option);
        }

        private int CallForTendersId
        {
            get
            {
                return (int)Session["callForTendersId"];
            }
        }
        private Charge[] Charges
        {
            get
            {
                return
                    this
                    .ChargesDataSource
                    .Values
                    .ToArray();
            }
        }
        private string Currency
        {
            get
            {
                return this.currency.SelectedValue;
            }
        }
        private string DeliveryPoint
        {
            get
            {
                return this.deliveryPoint.Text;
            }
        }
        private string DeliveryTerms
        {
            get
            {
                return this.deliveryTerms.SelectedValue;
            }
        }
        private int DeliveryTime
        {
            get
            {
                return int.Parse(this.deliveryTime.Text);
            }
        }
        private DateTime ExpiryDate
        {
            get
            {
                return this.expiryDate.SelectedDate.Value;
            }
        }
        private Offer[] Offers
        {
            get
            {
                return
                    this
                    .OffersDataSource
                    .Values
                    .ToArray();
            }
        }
        private string PaymentTerms
        {
            get
            {
                return this.paymentTerms.Text;
            }
        }
        private int TenderId
        {
            get
            {
                return (int)Session["tenderId"];
            }
        }
        private string Username
        {
            get
            {
                return this.User.Identity.Name;
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

        protected ReviseTender()
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
namespace Tendering.Presentation
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Tendering.Persistence;
    using Attachment = GARMCO.AMS.B2B.eTendering.DAL.SupplierOrderAttachItem;

    class ChargeDisassembler
    {
        public Charge Disassemble(Domain.Charge charge)
        {
            var currency = this.currencyRepository.FindByKey(charge.Currency);

            return
                new Charge(
                    charge.Amount,
                    new KeyValuePair<string, string>(currency.Key, currency.Value),
                    charge.Description);
        }

        public ChargeDisassembler(CurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        private readonly CurrencyRepository currencyRepository;
    }
    class OfferDisassembler
    {
        public Offer Disassemble(Domain.Offer offer)
        {
            var unitOfMeasure = this.unitOfMeasureRepository.FindByKey(offer.Requirement.UnitOfMeasure);

            return
                new Offer(
                    offer
                        .Attachments
                        .Select(attachment => new Attachment
                        {
                            OrderAttachSODID = offer.Id,
                            OrderAttachContentHtml = (attachment.Content.Length > 0) ? Encoding.Unicode.GetString(attachment.Content).ConvertRtfToHtml() : string.Empty,
                            OrderAttachContentRtf = (attachment.Content.Length > 0) ? Encoding.Unicode.GetString(attachment.Content) : string.Empty,
                            OrderAttachDisplayName = attachment.Display,
                            OrderAttachFilename = attachment.Filename,
                            OrderAttachSeq = attachment.Sequence,
                            OrderAttachType = (GARMCO.AMS.B2B.Utility.B2BConstants.MediaObjectType)attachment.Type

                        }),
                    offer.ManufacturerCode,
                    offer.Remarks,
                new Requirement(
                    offer.Requirement.Number,
                    offer.Requirement.Description,
                    offer.Requirement.FileAttachments,
                    offer.Requirement.Quantity,
                    offer.Requirement.TextAttachments,
                    new KeyValuePair<string, string>(unitOfMeasure.Key, unitOfMeasure.Value)),
                offer.SupplierCode,
                    offer.UnitCost);
        }

        public OfferDisassembler(UnitOfMeasureRepository unitOfMeasureRepository)
        {
            this.unitOfMeasureRepository = unitOfMeasureRepository;
        }

        private readonly UnitOfMeasureRepository unitOfMeasureRepository;
    }
    class OptionDisassembler
    {
        public Option[] Disassemble(int requirementNumber, Domain.Option[] options)
        {
            return
                options
                .Select(option => this.Disassemble(requirementNumber, option))
                .ToArray();
        }

        private Option Disassemble(int requirementNumber, Domain.Option option)
        {
            var currency = this.currencyRepository.FindByKey(option.Currency);
            var deliveryTerms = this.deliveryTermsRepository.FindByKey(option.DeliveryTerms);
            var unitOfMeasure = this.unitOfMeasureRepository.FindByKey(option.UnitOfMeasure);

            return
                new Option(
                    option
                        .Attachments
                        .Select(attachment => new Attachment
                        {
                            OrderAttachSODID = option.Id,
                            OrderAttachContentHtml = (attachment.Content.Length > 0) ? Encoding.Unicode.GetString(attachment.Content).ConvertRtfToHtml() : string.Empty,
                            OrderAttachContentRtf = (attachment.Content.Length > 0) ? Encoding.Unicode.GetString(attachment.Content) : string.Empty,
                            OrderAttachDisplayName = attachment.Display,
                            OrderAttachFilename = attachment.Filename,
                            OrderAttachSeq = attachment.Sequence,
                            OrderAttachType = (GARMCO.AMS.B2B.Utility.B2BConstants.MediaObjectType)attachment.Type

                        }),
                    new KeyValuePair<string, string>(currency.Key, currency.Value),
                    option.DeliveryPoint,
                    new KeyValuePair<string, string>(deliveryTerms.Key, deliveryTerms.Value),
                    option.DeliveryTime,
                    option.Description,
                    option.ExpiryDate,
                    option.ManufacturerCode,
                    option.Quantity,
                    requirementNumber,
                    option.SupplierCode,
                    option.UnitCost,
                    new KeyValuePair<string, string>(unitOfMeasure.Key, unitOfMeasure.Value));
        }

        public OptionDisassembler(CurrencyRepository currencyRepository, DeliveryTermsRepository deliveryTermsRepository, UnitOfMeasureRepository unitOfMeasureRepository)
        {
            this.currencyRepository = currencyRepository;
            this.deliveryTermsRepository = deliveryTermsRepository;
            this.unitOfMeasureRepository = unitOfMeasureRepository;
        }

        private readonly CurrencyRepository currencyRepository;
        private readonly DeliveryTermsRepository deliveryTermsRepository;
        private readonly UnitOfMeasureRepository unitOfMeasureRepository;
    }
}