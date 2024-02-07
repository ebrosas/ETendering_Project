namespace Tendering
{
    using System.Configuration;
    using SautinSoft;

    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                return
                    ConfigurationManager
                        .ConnectionStrings["DefaultConnection"]
                        .ConnectionString;
            }
        }
        public static string HtmlToRftKey
        {
            get
            {
                return
                    ConfigurationManager
                        .AppSettings["HTML_RTF_KEY"];
            }
        }
        public static string RftToHtmlKey
        {
            get
            {
                return
                    ConfigurationManager
                        .AppSettings["RTF_HTML_KEY"];
            }
        }
    }

    static class StringExtensions
    {
        public static string ConvertHtmlToRtf(this string @this)
        {
            HtmlToRtf htmlToRft = new HtmlToRtf();
            htmlToRft.Serial = Configuration.HtmlToRftKey;

            return htmlToRft.ConvertString(@this);
        }

        public static string ConvertRtfToHtml(this string @this)
        {
            RtfToHtml rtfToHtml = new RtfToHtml();
            rtfToHtml.Serial = Configuration.RftToHtmlKey;

            return rtfToHtml.ConvertString(@this);
        }
    }
}
namespace Tendering.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Attachment
    {
        public int Sequence
        {
            get
            {
                return this.sequence;
            }
        }
        public byte[] Content
        {
            get
            {
                return this.content;
            }
        }
        public string Display
        {
            get
            {
                return this.display;
            }
        }
        public string Filename
        {
            get
            {
                return this.filename;
            }
        }
        public int Type
        {
            get
            {
                return this.type;
            }
        }

        public Attachment(int sequence, byte[] content, string display, string filename, int type)
        {
            this.sequence = sequence;
            this.content = content;
            this.display = display;
            this.filename = filename;
            this.type = type;
        }

        private readonly int sequence;
        private readonly byte[] content;
        private readonly string display;
        private readonly string filename;
        private readonly int type;
    }

    class ApplicationUser
    {
        public string Currency
        {
            get
            {
                return this.currency;
            }
        }
        public string DeliveryTerms
        {
            get
            {
                return this.deliveryTerms;
            }
        }
        public int SupplierId
        {
            get
            {
                return this.supplierId;
            }
        }

        public ApplicationUser(string currency, string deliveryTerms, int supplierId)
        {
            this.currency = currency;
            this.deliveryTerms = deliveryTerms;
            this.supplierId = supplierId;
        }

        private readonly string currency;
        private readonly string deliveryTerms;
        private readonly int supplierId;
    }
    class Requirement
    {
        public int Number
        {
            get
            {
                return this.number;
            }
        }
        public int FileAttachments
        {
            get
            {
                return this.fileAttachments;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }
        public int TextAttachments
        {
            get
            {
                return this.textAttachments;
            }
        }
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasure;
            }
        }

        public Requirement(int number, string description, int fileAttachments, int quantity, int textAttachments, string unitOfMeasure)
        {
            this.number = number;
            this.description = description;
            this.fileAttachments = fileAttachments;
            this.quantity = quantity;
            this.textAttachments = textAttachments;
            this.unitOfMeasure = unitOfMeasure;
        }

        private readonly int number;
        private readonly string description;
        private readonly int fileAttachments;
        private readonly int quantity;
        private readonly int textAttachments;
        private readonly string unitOfMeasure;
    }
    class CallForTenders
    {
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public DateTime ClosingDate
        {
            get
            {
                return this.closingDate;
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
        }
        public Requirement[] Requirements
        {
            get
            {
                return this.requirements.ToArray();
            }
        }

        public CallForTenders(int id, DateTime closingDate, IEnumerable<Requirement> requirements, string status)
        {
            this.id = id;
            this.closingDate = closingDate;
            this.requirements = requirements;
            this.status = status;
        }

        private readonly int id;
        private readonly DateTime closingDate;
        private readonly IEnumerable<Requirement> requirements;
        private readonly string status;
    }
    class Charge
    {
        public double Amount
        {
            get
            {
                return this.amount;
            }
        }
        public string Currency
        {
            get
            {
                return this.currency;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        public Charge(double amount, string currency, string description)
        {
            this.description = description;
            this.amount = amount;
            this.currency = currency;
        }

        private readonly double amount;
        private readonly string currency;
        private readonly string description;
    }
    class Option
    {
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public IEnumerable<Attachment> Attachments
        {
            get
            {
                return this.attachments;
            }
        }
        public string Currency
        {
            get
            {
                return this.currency;
            }
        }
        public string DeliveryPoint
        {
            get
            {
                return this.deliveryPoint;
            }
        }
        public string DeliveryTerms
        {
            get
            {
                return this.deliveryTerms;
            }
        }
        public int DeliveryTime
        {
            get
            {
                return this.deliveryTime;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public DateTime ExpiryDate
        {
            get
            {
                return this.expiryDate;
            }
        }
        public string ManufacturerCode
        {
            get
            {
                return this.manufacturerCode;
            }
        }
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }
        public string SupplierCode
        {
            get
            {
                return this.supplierCode;
            }
        }
        public double UnitCost
        {
            get
            {
                return this.unitCost;
            }
        }
        public string UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasure;
            }
        }

        public Option(
            IEnumerable<Attachment> attachments,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            string description,
            DateTime expiryDate,
            string manufacturerCode,
            int quantity,
            string supplierCode,
            double unitCost,
            string unitOfMeasure) :
            this(
            0,
            attachments,
            currency,
            deliveryPoint,
            deliveryTerms,
            deliveryTime,
            description,
            expiryDate,
            manufacturerCode,
            quantity,
            supplierCode,
            unitCost,
            unitOfMeasure)
        {
        }

        public Option(
            int id,
            IEnumerable<Attachment> attachments,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            string description,
            DateTime expiryDate,
            string manufacturerCode,
            int quantity,
            string supplierCode,
            double unitCost,
            string unitOfMeasure)
        {
            this.id = id;
            this.attachments = attachments;
            this.currency = currency;
            this.deliveryPoint = deliveryPoint;
            this.deliveryTerms = deliveryTerms;
            this.deliveryTime = deliveryTime;
            this.description = description;
            this.expiryDate = expiryDate;
            this.manufacturerCode = manufacturerCode;
            this.quantity = quantity;
            this.supplierCode = supplierCode;
            this.unitCost = unitCost;
            this.unitOfMeasure = unitOfMeasure;
        }

        private int id;
        private readonly IEnumerable<Attachment> attachments;
        private readonly string currency;
        private readonly string deliveryPoint;
        private readonly string deliveryTerms;
        private readonly int deliveryTime;
        private readonly string description;
        private readonly DateTime expiryDate;
        private readonly string manufacturerCode;
        private readonly int quantity;
        private readonly string supplierCode;
        private readonly double unitCost;
        private readonly string unitOfMeasure;
    }
    class Offer
    {
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public IEnumerable<Attachment> Attachments
        {
            get
            {
                return this.attachments;
            }
        }
        public string ManufacturerCode
        {
            get
            {
                return this.manufacturerCode;
            }
        }
        public Option[] Options
        {
            get
            {
                return this.options.ToArray();
            }
        }
        public string Remarks
        {
            get
            {
                return this.remarks;
            }
        }
        public Requirement Requirement
        {
            get
            {
                return this.requirement;
            }
        }
        public string SupplierCode
        {
            get
            {
                return this.supplierCode;
            }
        }
        public double UnitCost
        {
            get
            {
                return this.unitCost;
            }
        }

        public Offer(
            IEnumerable<Attachment> attachments,
            string manufacturerCode,
            IEnumerable<Option> options,
            string remarks,
            Requirement requirement,
            string supplierCode,
            double unitCost) :
            this(
                0,
                attachments,
                manufacturerCode,
                options,
                remarks,
                requirement,
                supplierCode,
                unitCost)
        {
        }

        public Offer(
            int id,
            IEnumerable<Attachment> attachments,
            string manufacturerCode,
            IEnumerable<Option> options,
            string remarks,
            Requirement requirement,
            string supplierCode,
            double unitCost)
        {
            this.id = id;
            this.attachments = attachments;
            this.manufacturerCode = manufacturerCode;
            this.options = options;
            this.remarks = remarks;
            this.requirement = requirement;
            this.supplierCode = supplierCode;
            this.unitCost = unitCost;
        }

        private int id;
        private readonly IEnumerable<Attachment> attachments;
        private readonly string manufacturerCode;
        private readonly IEnumerable<Option> options;
        private readonly string remarks;
        private readonly Requirement requirement;
        private readonly string supplierCode;
        private readonly double unitCost;
    }
    class Tender
    {
        public static Tuple<string, Tender> Create(
            int callForTendersId,
            IEnumerable<Charge> charges,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            DateTime expiryDate,
            IEnumerable<Offer> offers,
            string paymentTerms,
            int supplierId)
        {
            return
                Tender.Create(
                    0,
                    callForTendersId,
                    charges,
                    currency,
                    deliveryPoint,
                    deliveryTerms,
                    deliveryTime,
                    expiryDate,
                    offers,
                    paymentTerms,
                    supplierId);
        }

        public static Tuple<string, Tender> Create(
            int id,
            int callForTendersId,
            IEnumerable<Charge> charges,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            DateTime expiryDate,
            IEnumerable<Offer> offers,
            string paymentTerms,
            int supplierId)
        {
            if (offers.Sum(offer => offer.UnitCost + offer.Options.Sum(option => option.UnitCost)) == 0)
                return Tuple.Create("Please respond to this call for tenders by making an offer or providing an option to one of its requirements.", (Tender)null);

            if (!offers.SelectMany(offer => offer.Options).All(option => option.UnitCost > 0))
                return Tuple.Create("Please provide options with nonzero unit cost.", (Tender)null);

            if (!charges.All(charge => charge.Amount > 0))
                return Tuple.Create("Please add charges with nonzero amount.", (Tender)null);

            return
                Tuple.Create(
                    string.Empty,
                    new Tender(
                        id,
                        callForTendersId,
                        charges,
                        currency,
                        deliveryPoint,
                        deliveryTerms,
                        deliveryTime,
                        expiryDate,
                        offers,
                        paymentTerms,
                        supplierId));
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public int CallForTendersId
        {
            get
            {
                return this.callForTendersId;
            }
        }
        public Charge[] Charges
        {
            get
            {
                return this.charges.ToArray();
            }
        }
        public string Currency
        {
            get
            {
                return this.currency;
            }
        }
        public string DeliveryPoint
        {
            get
            {
                return this.deliveryPoint;
            }
        }
        public string DeliveryTerms
        {
            get
            {
                return this.deliveryTerms;
            }
        }
        public int DeliveryTime
        {
            get
            {
                return this.deliveryTime;
            }
        }
        public DateTime ExpiryDate
        {
            get
            {
                return this.expiryDate;
            }
        }
        public Offer[] Offers
        {
            get
            {
                return this.offers.ToArray();
            }
        }
        public string PaymentTerms
        {
            get
            {
                return this.paymentTerms;
            }
        }
        public int SupplierId
        {
            get
            {
                return this.supplierId;
            }
        }

        public Tender(
            int id,
            int callForTendersId,
            IEnumerable<Charge> charges,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            DateTime expiryDate,
            IEnumerable<Offer> offers,
            string paymentTerms,
            int supplierId)
        {
            this.id = id;
            this.callForTendersId = callForTendersId;
            this.charges = charges;
            this.currency = currency;
            this.deliveryPoint = deliveryPoint;
            this.deliveryTerms = deliveryTerms;
            this.deliveryTime = deliveryTime;
            this.expiryDate = expiryDate;
            this.offers = offers;
            this.paymentTerms = paymentTerms;
            this.supplierId = supplierId;
        }


        private int id;
        private readonly int callForTendersId;
        private readonly IEnumerable<Charge> charges;
        private readonly string currency;
        private readonly string deliveryPoint;
        private readonly string deliveryTerms;
        private readonly int deliveryTime;
        private readonly DateTime expiryDate;
        private readonly IEnumerable<Offer> offers;
        private readonly string paymentTerms;
        private readonly int supplierId;
    }
}
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

    public partial class PlaceTender : GARMCO.AMS.Common.Web.BaseWebForm
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

                var supplier = this.applicationUserRepository.GetByUsername(this.Username);

                this.currency.SelectedValue = supplier.Currency;
                this.deliveryTerms.SelectedValue = supplier.DeliveryTerms;

                this.OffersDataSource = new Dictionary<int, Offer>();
                this.OptionsDataSource = new Dictionary<int, Dictionary<string, Option>>();

                foreach (var requirement in this.service.GetRequirementsOf(this.CallForTendersId))
                {
                    var unitOfMeasure = this.unitOfMeasureRepository.FindByKey(requirement.UnitOfMeasure);

                    var offer =
                        new Offer(
                            new Attachment[0],
                            string.Empty,
                            string.Empty,
                        new Requirement(
                            requirement.Number,
                            requirement.Description,
                            requirement.FileAttachments,
                            requirement.Quantity,
                            requirement.TextAttachments,
                            new KeyValuePair<string, string>(unitOfMeasure.Key, unitOfMeasure.Value)),
                        string.Empty,
                            0d);

                    this.OffersDataSource.Add(requirement.Number, offer);
                    this.OptionsDataSource.Add(requirement.Number, new Dictionary<string, Option>());
                }
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
                    .SaveTender(
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

        protected PlaceTender()
        {
            this.applicationUserRepository = new ApplicationUserRepository(Configuration.ConnectionString);
            this.chargeAssembler = new ChargeAssembler();
            this.currencyRepository = new CurrencyRepository(Configuration.ConnectionString);
            this.deliveryTermsRepository = new DeliveryTermsRepository(Configuration.ConnectionString);
            this.offerAssembler = new OfferAssembler();

            this.service =
                new Service(
                    new ApplicationUserRepository(Configuration.ConnectionString),
                    new CallForTendersRepository(Configuration.ConnectionString),
                    new TenderRepository(Configuration.ConnectionString));

            this.unitOfMeasureRepository = new UnitOfMeasureRepository(Configuration.ConnectionString);

            this.Load += new EventHandler(this.Initialize);
        }

        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly ChargeAssembler chargeAssembler;
        private readonly CurrencyRepository currencyRepository;
        private readonly DeliveryTermsRepository deliveryTermsRepository;
        private readonly OfferAssembler offerAssembler;
        private readonly Service service;
        private readonly UnitOfMeasureRepository unitOfMeasureRepository;
    }
}
namespace Tendering.Presentation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Attachment = GARMCO.AMS.B2B.eTendering.DAL.SupplierOrderAttachItem;

    [Serializable]
    class Charge
    {
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public KeyValuePair<string, string> Currency
        {
            get
            {
                return this.currency;
            }
        }
        public double Amount
        {
            get
            {
                return this.amount;
            }
        }

        public Charge(double amount, KeyValuePair<string, string> currency, string description)
        {
            this.description = description;
            this.amount = amount;
            this.currency = currency;
        }

        private readonly string description;
        private readonly double amount;
        private readonly KeyValuePair<string, string> currency;
    }
    [Serializable]
    class Requirement
    {
        public int Number
        {
            get
            {
                return this.number;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public int FileAttachments
        {
            get
            {
                return this.fileAttachments;
            }
        }
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }
        public int TextAttachments
        {
            get
            {
                return this.textAttachments;
            }
        }
        public KeyValuePair<string, string> UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasure;
            }
        }

        public Requirement(int number, string description, int fileAttachments, int quantity, int textAttachments, KeyValuePair<string, string> unitOfMeasure)
        {
            this.number = number;
            this.description = description;
            this.fileAttachments = fileAttachments;
            this.quantity = quantity;
            this.textAttachments = textAttachments;
            this.unitOfMeasure = unitOfMeasure;
        }

        private readonly int number;
        private readonly string description;
        private readonly int fileAttachments;
        private readonly int quantity;
        private readonly int textAttachments;
        private readonly KeyValuePair<string, string> unitOfMeasure;
    }
    [Serializable]
    class Offer
    {
        public List<Attachment> Attachments
        {
            get
            {
                return this.attachments;
            }
        }
        public Requirement Requirement
        {
            get
            {
                return this.requirement;
            }
        }
        public string ManufacturerCode
        {
            get
            {
                return this.manufacturerCode;
            }
        }
        public string Remarks
        {
            get
            {
                return this.remarks;
            }
        }
        public string SupplierCode
        {
            get
            {
                return this.supplierCode;
            }
        }
        public double UnitCost
        {
            get
            {
                return this.unitCost;
            }
        }

        public Offer(IEnumerable<Attachment> attachments, string manufacturerCode, string remarks, Requirement requirement, string supplierCode, double unitCost)
        {
            this.attachments = new List<Attachment>();

            foreach (var attachment in attachments)
                this.attachments.Add(attachment);

            this.requirement = requirement;
            this.manufacturerCode = manufacturerCode;
            this.remarks = remarks;
            this.supplierCode = supplierCode;
            this.unitCost = unitCost;
        }

        private readonly List<Attachment> attachments;
        private readonly Requirement requirement;
        private readonly string manufacturerCode;
        private readonly string remarks;
        private readonly string supplierCode;
        private readonly double unitCost;
    }
    [Serializable]
    class Option
    {
        public List<Attachment> Attachments
        {
            get
            {
                return this.attachments;
            }
        }
        public KeyValuePair<string, string> Currency
        {
            get
            {
                return this.currency;
            }
        }
        public string DeliveryPoint
        {
            get
            {
                return this.deliveryPoint;
            }
        }
        public KeyValuePair<string, string> DeliveryTerms
        {
            get
            {
                return this.deliveryTerms;
            }
        }
        public int DeliveryTime
        {
            get
            {
                return this.deliveryTime;
            }
        }
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public DateTime ExpiryDate
        {
            get
            {
                return this.expiryDate;
            }
        }
        public string ManufacturerCode
        {
            get
            {
                return this.manufacturerCode;
            }
        }
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }
        public int RequirementNumber
        {
            get
            {
                return this.requirementNumber;
            }
        }
        public string SupplierCode
        {
            get
            {
                return this.supplierCode;
            }
        }
        public double UnitCost
        {
            get
            {
                return this.unitCost;
            }
        }
        public KeyValuePair<string, string> UnitOfMeasure
        {
            get
            {
                return this.unitOfMeasure;
            }
        }

        public Option(
            IEnumerable<Attachment> attachments,
            KeyValuePair<string, string> currency,
            string deliveryPoint,
            KeyValuePair<string, string> deliveryTerms,
            int deliveryTime,
            string description,
            DateTime expiryDate,
            string manufacturerCode,
            int quantity,
            int requirementNumber,
            string supplierCode,
            double unitCost,
            KeyValuePair<string, string> unitOfMeasure)
        {
            this.attachments = new List<Attachment>();

            foreach (var attachment in attachments)
                this.attachments.Add(attachment);

            this.currency = currency;
            this.deliveryPoint = deliveryPoint;
            this.deliveryTerms = deliveryTerms;
            this.deliveryTime = deliveryTime;
            this.description = description;
            this.expiryDate = expiryDate;
            this.manufacturerCode = manufacturerCode;
            this.quantity = quantity;
            this.requirementNumber = requirementNumber;
            this.supplierCode = supplierCode;
            this.unitCost = unitCost;
            this.unitOfMeasure = unitOfMeasure;
        }

        private readonly List<Attachment> attachments;
        private readonly KeyValuePair<string, string> currency;
        private readonly string deliveryPoint;
        private readonly KeyValuePair<string, string> deliveryTerms;
        private readonly int deliveryTime;
        private readonly string description;
        private readonly DateTime expiryDate;
        private readonly string manufacturerCode;
        private readonly int quantity;
        private readonly int requirementNumber;
        private readonly string supplierCode;
        private readonly double unitCost;
        private readonly KeyValuePair<string, string> unitOfMeasure;
    }
    class ChargeAssembler
    {
        public Domain.Charge[] Assemble(Charge[] charges)
        {
            return Array.ConvertAll(charges, this.Assemble);
        }

        private Domain.Charge Assemble(Charge charge)
        {
            return new Domain.Charge(charge.Amount, charge.Currency.Key, charge.Description);
        }
    }
    class OfferAssembler
    {
        public Domain.Offer[] Assemble(Offer[] offers, Dictionary<int, Dictionary<string, Option>> options)
        {
            return
                offers
                .Select(offer => this.Assemble(offer, options[offer.Requirement.Number].Values.ToArray()))
                .ToArray();
        }

        private Domain.Offer Assemble(Offer offer, Option[] options)
        {
            return
                new Domain.Offer(
                    offer
                        .Attachments
                            .Where(attachment => !attachment.MarkForDeletion)
                            .Select(attachment => new Domain.Attachment(
                                (int)attachment.OrderAttachSeq.Value,
                                Encoding.Unicode.GetBytes(attachment.OrderAttachContentHtml.ConvertHtmlToRtf()),
                                attachment.OrderAttachDisplayName,
                                attachment.OrderAttachFilename,
                                (int)attachment.OrderAttachType)),
                    offer.ManufacturerCode,
                    options
                        .Select(option => new Domain.Option(
                            option
                                .Attachments
                                .Where(attachment => !attachment.MarkForDeletion)
                                .Select(attachment => new Domain.Attachment(
                                    (int)attachment.OrderAttachSeq.Value,
                                    Encoding.Unicode.GetBytes(attachment.OrderAttachContentHtml.ConvertHtmlToRtf()),
                                    attachment.OrderAttachDisplayName,
                                    attachment.OrderAttachFilename,
                                    (int)attachment.OrderAttachType)),
                            option.Currency.Key,
                            option.DeliveryPoint,
                            option.DeliveryTerms.Key,
                            option.DeliveryTime,
                            option.Description,
                            option.ExpiryDate,
                            option.ManufacturerCode,
                            option.Quantity,
                            option.SupplierCode,
                            option.UnitCost,
                            option.UnitOfMeasure.Key)),
                    offer.Remarks,
                    new Domain.Requirement(
                        offer.Requirement.Number,
                        offer.Requirement.Description,
                        offer.Requirement.FileAttachments,
                        offer.Requirement.Quantity,
                        offer.Requirement.TextAttachments,
                        offer.Requirement.UnitOfMeasure.Key),
                    offer.SupplierCode,
                    offer.UnitCost);
        }
    }
}
namespace Tendering.Persistence
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    using Dapper;
    using Tendering.Domain;

    class ApplicationUserRepository
    {
        public ApplicationUser GetByUsername(string username)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .QuerySingle<ApplicationUser>(
                            new StringBuilder()
                                .AppendLine("SELECT")
                                .AppendLine("b.SupplierCurrency Currency,")
                                .AppendLine("b.SupplierDelTerm DeliveryTerms,")
                                .AppendLine("b.SupplierNo SupplierId")
                                .AppendLine("FROM b2badminuser.SupplierContact a")
                                .AppendLine("INNER JOIN b2badminuser.Supplier b ON")
                                .AppendLine("b.SupplierNo = a.ContactSupplierNo")
                                .AppendLine("WHERE a.ContactActive = 1")
                                .AppendLine("AND a.ContactReviewed = 1")
                                .AppendLine("AND a.ContactEmail = @Username;")
                                .ToString(),
                            new { username });
            }
        }

        public ApplicationUserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
    class CallForTendersRepository
    {
        private static CallForTenders Create(dynamic arguments, IEnumerable<Requirement> requirements)
        {
            return
                new CallForTenders(
                    arguments.Id,
                    arguments.ClosingDate,
                    requirements,
                    arguments.Status);
        }

        public CallForTenders Find(int id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var gridReader =
                    connection
                        .QueryMultiple(
                            new StringBuilder()
                                .AppendLine("SELECT")
                                .AppendLine("CAST(a.OrderNo AS INT) Id,")
                                .AppendLine("a.OrderClosingDate ClosingDate,")
                                .AppendLine("a.OrderStatus Status")
                                .AppendLine("FROM b2badminuser.OrderRequisition a")
                                .AppendLine("WHERE a.OrderNo = @Id;")
                                .AppendLine("SELECT")
                                .AppendLine("CAST(a.OrderDetLineNo AS INT) Number,")
                                .AppendLine("a.OrderDetDesc Description,")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("COUNT(1)")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment x")
                                .AppendLine("WHERE x.OrderAttachNo = a.OrderDetNo")
                                .AppendLine("AND x.OrderAttachLineNo = a.OrderDetLineNo")
                                .AppendLine("AND x.OrderAttachSupplier = 0")
                                .AppendLine("AND x.OrderAttachSODID = -1")
                                .AppendLine("AND x.OrderAttachSODAltID = -1")
                                .AppendLine("AND x.OrderAttachType <> 0")
                                .AppendLine(") FileAttachments,")
                                .AppendLine("CAST(a.OrderDetQuantity AS INT) Quantity,")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("COUNT(1)")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment x")
                                .AppendLine("WHERE x.OrderAttachNo = a.OrderDetNo")
                                .AppendLine("AND x.OrderAttachLineNo = a.OrderDetLineNo")
                                .AppendLine("AND x.OrderAttachSupplier = 0")
                                .AppendLine("AND x.OrderAttachSODID = -1")
                                .AppendLine("AND x.OrderAttachSODAltID = -1")
                                .AppendLine("AND x.OrderAttachType = 0")
                                .AppendLine(") TextAttachments,")
                                .AppendLine("a.OrderDetUM UnitOfMeasure")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetail a")
                                .AppendLine("WHERE a.OrderDetNo = @Id;")
                                .ToString(),
                            new { id });

                return
                    Create(
                        gridReader.ReadSingle(),
                        gridReader.Read<Requirement>());
            }
        }

        public CallForTendersRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
    class CurrencyRepository
    {
        const string Sql =
@"
SELECT
a.CVCRCD [Key],
RTRIM(a.CVDL01) Value
FROM b2badminuser.F0013 a
";

        public KeyValuePair<string, string> FindByKey(string key)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .QuerySingle<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("WHERE a.CVCRCD = @Key;")
                                .ToString(),
                            new { key });
            }
        }
        public KeyValuePair<string, string>[] GetAll()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .Query<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("ORDER BY a.CVDL01;")
                                .ToString())
                        .ToArray();
            }
        }

        public CurrencyRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
    class DeliveryTermsRepository
    {
        const string Sql =
@"
SELECT
b.UDCID [Key],
b.UDCDesc1 Value
FROM b2badminuser.UserDefinedCodeGroup a
INNER JOIN b2badminuser.UserDefinedCode b
ON b.UDCUDCGID = a.UDCGID
WHERE a.UDCGCode = 'DELTERM'
";

        public KeyValuePair<string, string> FindByKey(string key)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .QuerySingle<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("AND b.UDCID = @Key;")
                                .ToString(),
                            new { key });
            }
        }
        public KeyValuePair<string, string>[] GetAll()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .Query<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("ORDER BY b.UDCDesc1;")
                                .ToString())
                        .ToArray();
            }
        }

        public DeliveryTermsRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
    class TenderRepository
    {
        public static Tender Create(
            dynamic tender,
            IEnumerable<Charge> charges,
            IEnumerable<dynamic> offerAttachments,
            IEnumerable<dynamic> offers,
            IEnumerable<dynamic> optionAttachments,
            IEnumerable<dynamic> options,
            IEnumerable<dynamic> requirements)
        {
            return
                new Tender(
                    tender.Id,
                    tender.CallForTendersId,
                    charges,
                    tender.Currency,
                    tender.DeliverPoint,
                    tender.DeliveryTerms,
                    tender.DeliveryTime,
                    tender.ExpiryDate,
                    offers
                        .Select(offer => new Offer(
                            offer.Id,
                            offerAttachments
                                .Where(attachment => attachment.OfferId == offer.Id)
                                .Select(attachment => new Attachment(
                                    attachment.Sequence,
                                    attachment.Content,
                                    attachment.Display,
                                    attachment.Filename,
                                    attachment.Type)),
                            offer.ManufacturerCode,
                            options
                                .Where(option => option.RequirementNumber == offer.RequirementNumber)
                                .Select(option => new Option(
                                    option.Id,
                                    optionAttachments
                                        .Where(attachment => attachment.OptionId == option.Id)
                                        .Select(attachment => new Attachment(
                                            attachment.Sequence,
                                            attachment.Content,
                                            attachment.Display,
                                            attachment.Filename,
                                            attachment.Type)),
                                    option.Currency,
                                    option.DeliveryPoint,
                                    option.DeliveryTerms,
                                    option.DeliveryTime,
                                    option.Description,
                                    option.ExpiryDate,
                                    option.ManufacturerCode,
                                    option.Quantity,
                                    option.SupplierCode,
                                    option.UnitCost,
                                    option.UnitOfMeasure)),
                                offer.Remarks,
                                requirements
                                    .Where(requirement => requirement.Number == offer.RequirementNumber)
                                    .Select(requirement => new Requirement(
                                        requirement.Number,
                                        requirement.Description,
                                        requirement.FileAttachments,
                                        requirement.Quantity,
                                        requirement.TextAttachments,
                                        requirement.UnitOfMeasure))
                                    .Single(),
                                offer.SupplierCode,
                                offer.UnitCost)),
                    tender.PaymentTerms,
                    tender.SupplierId);
        }

        public void Add(Tender tender)
        {
            using (var scope = new TransactionScope())
            using (var connection = new SqlConnection(this.connectionString))
            {
                tender.Id =
                connection
                .QuerySingle<int>(
                    new StringBuilder()
                    .AppendLine("INSERT")
                    .AppendLine("b2badminuser.SupplierOrder")
                    .AppendLine("(")
                    .AppendLine("SupOrderOrderNo,")
                    .AppendLine("SupOrderSupplierNo,")
                    .AppendLine("SupOrderCurrencyCode,")
                    .AppendLine("SupOrderFXRate,")
                    .AppendLine("SupOrderValidityPeriod,")
                    .AppendLine("SupOrderDeliveryTime,")
                    .AppendLine("SupOrderDelTerm,")
                    .AppendLine("SupOrderShipVia,")
                    .AppendLine("SupOrderDeliveryPt,")
                    .AppendLine("SupOrderPaymentTerm,")
                    .AppendLine("SupOrderStatus")
                    .AppendLine(")")
                    .AppendLine("VALUES")
                    .AppendLine("(")
                    .AppendLine("@CallForTendersId,")
                    .AppendLine("@SupplierId,")
                    .AppendLine("@Currency,")
                    .AppendLine("0,")
                    .AppendLine("@ExpiryDate,")
                    .AppendLine("@DeliveryTime,")
                    .AppendLine("@DeliveryTerms,")
                    .AppendLine("0,")
                    .AppendLine("@DeliveryPoint,")
                    .AppendLine("@PaymentTerms,")
                    .AppendLine("NULL")
                    .AppendLine(");")
                    .AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);")
                    .ToString(),
                    new
                    {
                        tender.CallForTendersId,
                        tender.Currency,
                        tender.DeliveryPoint,
                        tender.DeliveryTerms,
                        tender.DeliveryTime,
                        tender.ExpiryDate,
                        tender.PaymentTerms,
                        tender.SupplierId
                    });

                connection
                .Execute(
                    new StringBuilder()
                    .AppendLine("INSERT")
                    .AppendLine("b2badminuser.SupplierOrderOtherCharge")
                    .AppendLine("(")
                    .AppendLine("OtherChgSupOrderID,")
                    .AppendLine("OtherChgDesc,")
                    .AppendLine("OtherChgCurrencyCode,")
                    .AppendLine("OtherChgAmount,")
                    .AppendLine("OtherChgAmountBD,")
                    .AppendLine("OtherChgFXRate,")
                    .AppendLine("OtherChgSelected")
                    .AppendLine(")")
                    .AppendLine("VALUES")
                    .AppendLine("(")
                    .AppendLine("@Id,")
                    .AppendLine("@Description,")
                    .AppendLine("@Currency,")
                    .AppendLine("@Amount,")
                    .AppendLine("0,")
                    .AppendLine("0,")
                    .AppendLine("0")
                    .AppendLine(");")
                    .ToString(),
                    tender
                    .Charges
                    .Select(charge => new
                    {
                        charge.Amount,
                        charge.Currency,
                        charge.Description,
                        tender.Id
                    }));

                foreach (var offer in tender.Offers)
                {
                    offer.Id =
                    connection
                    .QuerySingle<int>(
                        new StringBuilder()
                        .AppendLine("INSERT")
                        .AppendLine("b2badminuser.SupplierOrderDetail")
                        .AppendLine("(")
                        .AppendLine("SODSupOrderID,")
                        .AppendLine("SODOrderNo,")
                        .AppendLine("SODOrderLineNo,")
                        .AppendLine("SODSupplierCode,")
                        .AppendLine("SODManufacturerCode,")
                        .AppendLine("SODQuantity,")
                        .AppendLine("SODUM,")
                        .AppendLine("SODUnitCost,")
                        .AppendLine("SODExtPrice,")
                        .AppendLine("SODUnitCostBD,")
                        .AppendLine("SODRemarks,")
                        .AppendLine("SODSelected,")
                        .AppendLine("SODLowest")
                        .AppendLine(")")
                        .AppendLine("VALUES")
                        .AppendLine("(")
                        .AppendLine("@Id,")
                        .AppendLine("@CallForTendersId,")
                        .AppendLine("@Number,")
                        .AppendLine("@SupplierCode,")
                        .AppendLine("@ManufacturerCode,")
                        .AppendLine("@Quantity,")
                        .AppendLine("@UnitOfMeasure,")
                        .AppendLine("@UnitCost,")
                        .AppendLine("0,")
                        .AppendLine("0,")
                        .AppendLine("@Remarks,")
                        .AppendLine("0,")
                        .AppendLine("NULL")
                        .AppendLine(");")
                        .AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);")
                        .ToString(),
                        new
                        {
                            offer.Remarks,
                            offer.Requirement.Number,
                            offer.Requirement.Quantity,
                            offer.Requirement.UnitOfMeasure,
                            offer.ManufacturerCode,
                            offer.SupplierCode,
                            offer.UnitCost,
                            tender.CallForTendersId,
                            tender.Id
                        });

                    connection
                    .Execute(
                        new StringBuilder()
                        .AppendLine("INSERT")
                        .AppendLine("b2badminuser.OrderRequisitionDetailAttachment")
                        .AppendLine("(")
                        .AppendLine("OrderAttachNo,")
                        .AppendLine("OrderAttachLineNo,")
                        .AppendLine("OrderAttachSeq,")
                        .AppendLine("OrderAttachSupplier,")
                        .AppendLine("OrderAttachSODID,")
                        .AppendLine("OrderAttachSODAltID,")
                        .AppendLine("OrderAttachType,")
                        .AppendLine("OrderAttachDisplayName,")
                        .AppendLine("OrderAttachFilename,")
                        .AppendLine("OrderAttachContent")
                        .AppendLine(")")
                        .AppendLine("VALUES")
                        .AppendLine("(")
                        .AppendLine("0,")
                        .AppendLine("0,")
                        .AppendLine("@Sequence,")
                        .AppendLine("1,")
                        .AppendLine("@Id,")
                        .AppendLine("-1,")
                        .AppendLine("@Type,")
                        .AppendLine("@Display,")
                        .AppendLine("@Filename,")
                        .AppendLine("@Content")
                        .AppendLine(");")
                        .ToString(),
                        offer
                        .Attachments
                        .Select(attachment => new
                        {
                            attachment.Content,
                            attachment.Display,
                            attachment.Filename,
                            attachment.Sequence,
                            attachment.Type,
                            offer.Id
                        }));

                    foreach (var option in offer.Options)
                    {
                        option.Id =
                        connection
                        .QuerySingle<int>(
                            new StringBuilder()
                            .AppendLine("INSERT")
                            .AppendLine("b2badminuser.SupplierOrderDetailAlternative")
                            .AppendLine("(")
                            .AppendLine("SODAltSODID,")
                            .AppendLine("SODAltLineNo,")
                            .AppendLine("SODAltDesc,")
                            .AppendLine("SODAltSupplierCode,")
                            .AppendLine("SODAltManufacturerCode,")
                            .AppendLine("SODAltQuantity,")
                            .AppendLine("SODAltUM,")
                            .AppendLine("SODAltUnitCost,")
                            .AppendLine("SODAltExtPrice,")
                            .AppendLine("SODAltUnitCostBD,")
                            .AppendLine("SODAltCurrencyCode,")
                            .AppendLine("SODAltFXRate,")
                            .AppendLine("SODAltValidityPeriod,")
                            .AppendLine("SODAltDeliveryTime,")
                            .AppendLine("SODAltDelTerm,")
                            .AppendLine("SODAltShipVia,")
                            .AppendLine("SODAltDeliveryPt,")
                            .AppendLine("SODAltSelected,")
                            .AppendLine("SODAltLowest")
                            .AppendLine(")")
                            .AppendLine("VALUES")
                            .AppendLine("(")
                            .AppendLine("@Id,")
                            .AppendLine("@Number,")
                            .AppendLine("@Description,")
                            .AppendLine("@SupplierCode,")
                            .AppendLine("@ManufacturerCode,")
                            .AppendLine("@Quantity,")
                            .AppendLine("@UnitOfMeasure,")
                            .AppendLine("@UnitCost,")
                            .AppendLine("0,")
                            .AppendLine("0,")
                            .AppendLine("@Currency,")
                            .AppendLine("0,")
                            .AppendLine("@ExpiryDate,")
                            .AppendLine("@DeliveryTime,")
                            .AppendLine("@DeliveryTerms,")
                            .AppendLine("0,")
                            .AppendLine("@DeliveryPoint,")
                            .AppendLine("0,")
                            .AppendLine("NULL")
                            .AppendLine(");")
                            .AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);")
                            .ToString(),
                            new
                            {
                                offer.Id,
                                offer.Requirement.Number,
                                option.Currency,
                                option.DeliveryPoint,
                                option.DeliveryTerms,
                                option.DeliveryTime,
                                option.Description,
                                option.ExpiryDate,
                                option.ManufacturerCode,
                                option.Quantity,
                                option.SupplierCode,
                                option.UnitCost,
                                option.UnitOfMeasure
                            });

                        connection
                        .Execute(
                            new StringBuilder()
                            .AppendLine("INSERT")
                            .AppendLine("b2badminuser.OrderRequisitionDetailAttachment")
                            .AppendLine("(")
                            .AppendLine("OrderAttachNo,")
                            .AppendLine("OrderAttachLineNo,")
                            .AppendLine("OrderAttachSeq,")
                            .AppendLine("OrderAttachSupplier,")
                            .AppendLine("OrderAttachSODID,")
                            .AppendLine("OrderAttachSODAltID,")
                            .AppendLine("OrderAttachType,")
                            .AppendLine("OrderAttachDisplayName,")
                            .AppendLine("OrderAttachFilename,")
                            .AppendLine("OrderAttachContent")
                            .AppendLine(")")
                            .AppendLine("VALUES")
                            .AppendLine("(")
                            .AppendLine("0,")
                            .AppendLine("0,")
                            .AppendLine("@Sequence,")
                            .AppendLine("1,")
                            .AppendLine("-1,")
                            .AppendLine("@Id,")
                            .AppendLine("@Type,")
                            .AppendLine("@Display,")
                            .AppendLine("@Filename,")
                            .AppendLine("@Content")
                            .AppendLine(");")
                            .ToString(),
                            option
                            .Attachments
                            .Select(attachment => new
                            {
                                attachment.Content,
                                attachment.Display,
                                attachment.Filename,
                                attachment.Sequence,
                                attachment.Type,
                                option.Id
                            }));
                    }
                }

                scope.Complete();
            }
        }
        public void Update(Tender tender)
        {
            using (var scope = new TransactionScope())
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection
                .Execute(
                    new StringBuilder()
                    .AppendLine("UPDATE")
                    .AppendLine("b2badminuser.SupplierOrder")
                    .AppendLine("SET")
                    .AppendLine("SupOrderCurrencyCode = @Currency,")
                    .AppendLine("SupOrderValidityPeriod = @ExpiryDate,")
                    .AppendLine("SupOrderDeliveryTime = @DeliveryTime,")
                    .AppendLine("SupOrderDelTerm = @DeliveryTerms,")
                    .AppendLine("SupOrderDeliveryPt = @DeliveryPoint,")
                    .AppendLine("SupOrderPaymentTerm = @PaymentTerms")
                    .AppendLine("WHERE SupOrderID = @Id;")
                    .ToString(),
                    new
                    {
                        tender.Id,
                        tender.Currency,
                        tender.DeliveryPoint,
                        tender.DeliveryTerms,
                        tender.DeliveryTime,
                        tender.ExpiryDate,
                        tender.PaymentTerms,
                    });

                connection.Execute("DELETE b2badminuser.OrderRequisitionDetailAttachment WHERE OrderAttachSODAltID IN (SELECT a.SODAltID FROM b2badminuser.SupplierOrderDetailAlternative a WHERE a.SODAltSODID IN (SELECT a.SODID FROM b2badminuser.SupplierOrderDetail a WHERE a.SODSupOrderID = @Id));", new { tender.Id });
                connection.Execute("DELETE b2badminuser.SupplierOrderDetailAlternative WHERE SODAltSODID IN (SELECT a.SODID FROM b2badminuser.SupplierOrderDetail a WHERE a.SODSupOrderID = @Id);", new { tender.Id });
                connection.Execute("DELETE b2badminuser.OrderRequisitionDetailAttachment WHERE OrderAttachSODID IN (SELECT a.SODID FROM b2badminuser.SupplierOrderDetail a WHERE a.SODSupOrderID = @Id);", new { tender.Id });
                connection.Execute("DELETE b2badminuser.SupplierOrderDetail WHERE SODSupOrderID = @Id;", new { tender.Id });
                connection.Execute("DELETE b2badminuser.SupplierOrderOtherCharge WHERE OtherChgSupOrderID = @Id;", new { tender.Id });

                connection
                .Execute(
                    new StringBuilder()
                    .AppendLine("INSERT")
                    .AppendLine("b2badminuser.SupplierOrderOtherCharge")
                    .AppendLine("(")
                    .AppendLine("OtherChgSupOrderID,")
                    .AppendLine("OtherChgDesc,")
                    .AppendLine("OtherChgCurrencyCode,")
                    .AppendLine("OtherChgAmount,")
                    .AppendLine("OtherChgAmountBD,")
                    .AppendLine("OtherChgFXRate,")
                    .AppendLine("OtherChgSelected")
                    .AppendLine(")")
                    .AppendLine("VALUES")
                    .AppendLine("(")
                    .AppendLine("@Id,")
                    .AppendLine("@Description,")
                    .AppendLine("@Currency,")
                    .AppendLine("@Amount,")
                    .AppendLine("0,")
                    .AppendLine("0,")
                    .AppendLine("0")
                    .AppendLine(");")
                    .ToString(),
                    tender
                    .Charges
                    .Select(charge => new
                    {
                        charge.Amount,
                        charge.Currency,
                        charge.Description,
                        tender.Id
                    }));

                foreach (var offer in tender.Offers)
                {
                    offer.Id =
                    connection
                    .QuerySingle<int>(
                        new StringBuilder()
                        .AppendLine("INSERT")
                        .AppendLine("b2badminuser.SupplierOrderDetail")
                        .AppendLine("(")
                        .AppendLine("SODSupOrderID,")
                        .AppendLine("SODOrderNo,")
                        .AppendLine("SODOrderLineNo,")
                        .AppendLine("SODSupplierCode,")
                        .AppendLine("SODManufacturerCode,")
                        .AppendLine("SODQuantity,")
                        .AppendLine("SODUM,")
                        .AppendLine("SODUnitCost,")
                        .AppendLine("SODExtPrice,")
                        .AppendLine("SODUnitCostBD,")
                        .AppendLine("SODRemarks,")
                        .AppendLine("SODSelected,")
                        .AppendLine("SODLowest")
                        .AppendLine(")")
                        .AppendLine("VALUES")
                        .AppendLine("(")
                        .AppendLine("@Id,")
                        .AppendLine("@CallForTendersId,")
                        .AppendLine("@Number,")
                        .AppendLine("@SupplierCode,")
                        .AppendLine("@ManufacturerCode,")
                        .AppendLine("@Quantity,")
                        .AppendLine("@UnitOfMeasure,")
                        .AppendLine("@UnitCost,")
                        .AppendLine("0,")
                        .AppendLine("0,")
                        .AppendLine("@Remarks,")
                        .AppendLine("0,")
                        .AppendLine("NULL")
                        .AppendLine(");")
                        .AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);")
                        .ToString(),
                        new
                        {
                            offer.Remarks,
                            offer.Requirement.Number,
                            offer.Requirement.Quantity,
                            offer.Requirement.UnitOfMeasure,
                            offer.ManufacturerCode,
                            offer.SupplierCode,
                            offer.UnitCost,
                            tender.CallForTendersId,
                            tender.Id
                        });

                    connection
                    .Execute(
                        new StringBuilder()
                        .AppendLine("INSERT")
                        .AppendLine("b2badminuser.OrderRequisitionDetailAttachment")
                        .AppendLine("(")
                        .AppendLine("OrderAttachNo,")
                        .AppendLine("OrderAttachLineNo,")
                        .AppendLine("OrderAttachSeq,")
                        .AppendLine("OrderAttachSupplier,")
                        .AppendLine("OrderAttachSODID,")
                        .AppendLine("OrderAttachSODAltID,")
                        .AppendLine("OrderAttachType,")
                        .AppendLine("OrderAttachDisplayName,")
                        .AppendLine("OrderAttachFilename,")
                        .AppendLine("OrderAttachContent")
                        .AppendLine(")")
                        .AppendLine("VALUES")
                        .AppendLine("(")
                        .AppendLine("0,")
                        .AppendLine("0,")
                        .AppendLine("@Sequence,")
                        .AppendLine("1,")
                        .AppendLine("@Id,")
                        .AppendLine("-1,")
                        .AppendLine("@Type,")
                        .AppendLine("@Display,")
                        .AppendLine("@Filename,")
                        .AppendLine("@Content")
                        .AppendLine(");")
                        .ToString(),
                        offer
                        .Attachments
                        .Select(attachment => new
                        {
                            attachment.Content,
                            attachment.Display,
                            attachment.Filename,
                            attachment.Sequence,
                            attachment.Type,
                            offer.Id
                        }));

                    foreach (var option in offer.Options)
                    {
                        option.Id =
                        connection
                        .QuerySingle<int>(
                            new StringBuilder()
                            .AppendLine("INSERT")
                            .AppendLine("b2badminuser.SupplierOrderDetailAlternative")
                            .AppendLine("(")
                            .AppendLine("SODAltSODID,")
                            .AppendLine("SODAltLineNo,")
                            .AppendLine("SODAltDesc,")
                            .AppendLine("SODAltSupplierCode,")
                            .AppendLine("SODAltManufacturerCode,")
                            .AppendLine("SODAltQuantity,")
                            .AppendLine("SODAltUM,")
                            .AppendLine("SODAltUnitCost,")
                            .AppendLine("SODAltExtPrice,")
                            .AppendLine("SODAltUnitCostBD,")
                            .AppendLine("SODAltCurrencyCode,")
                            .AppendLine("SODAltFXRate,")
                            .AppendLine("SODAltValidityPeriod,")
                            .AppendLine("SODAltDeliveryTime,")
                            .AppendLine("SODAltDelTerm,")
                            .AppendLine("SODAltShipVia,")
                            .AppendLine("SODAltDeliveryPt,")
                            .AppendLine("SODAltSelected,")
                            .AppendLine("SODAltLowest")
                            .AppendLine(")")
                            .AppendLine("VALUES")
                            .AppendLine("(")
                            .AppendLine("@Id,")
                            .AppendLine("@Number,")
                            .AppendLine("@Description,")
                            .AppendLine("@SupplierCode,")
                            .AppendLine("@ManufacturerCode,")
                            .AppendLine("@Quantity,")
                            .AppendLine("@UnitOfMeasure,")
                            .AppendLine("@UnitCost,")
                            .AppendLine("0,")
                            .AppendLine("0,")
                            .AppendLine("@Currency,")
                            .AppendLine("0,")
                            .AppendLine("@ExpiryDate,")
                            .AppendLine("@DeliveryTime,")
                            .AppendLine("@DeliveryTerms,")
                            .AppendLine("0,")
                            .AppendLine("@DeliveryPoint,")
                            .AppendLine("0,")
                            .AppendLine("NULL")
                            .AppendLine(");")
                            .AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);")
                            .ToString(),
                            new
                            {
                                offer.Id,
                                offer.Requirement.Number,
                                option.Currency,
                                option.DeliveryPoint,
                                option.DeliveryTerms,
                                option.DeliveryTime,
                                option.Description,
                                option.ExpiryDate,
                                option.ManufacturerCode,
                                option.Quantity,
                                option.SupplierCode,
                                option.UnitCost,
                                option.UnitOfMeasure
                            });

                        connection
                        .Execute(
                            new StringBuilder()
                            .AppendLine("INSERT")
                            .AppendLine("b2badminuser.OrderRequisitionDetailAttachment")
                            .AppendLine("(")
                            .AppendLine("OrderAttachNo,")
                            .AppendLine("OrderAttachLineNo,")
                            .AppendLine("OrderAttachSeq,")
                            .AppendLine("OrderAttachSupplier,")
                            .AppendLine("OrderAttachSODID,")
                            .AppendLine("OrderAttachSODAltID,")
                            .AppendLine("OrderAttachType,")
                            .AppendLine("OrderAttachDisplayName,")
                            .AppendLine("OrderAttachFilename,")
                            .AppendLine("OrderAttachContent")
                            .AppendLine(")")
                            .AppendLine("VALUES")
                            .AppendLine("(")
                            .AppendLine("0,")
                            .AppendLine("0,")
                            .AppendLine("@Sequence,")
                            .AppendLine("1,")
                            .AppendLine("-1,")
                            .AppendLine("@Id,")
                            .AppendLine("@Type,")
                            .AppendLine("@Display,")
                            .AppendLine("@Filename,")
                            .AppendLine("@Content")
                            .AppendLine(");")
                            .ToString(),
                            option
                            .Attachments
                            .Select(attachment => new
                            {
                                attachment.Content,
                                attachment.Display,
                                attachment.Filename,
                                attachment.Sequence,
                                attachment.Type,
                                option.Id
                            }));
                    }
                }

                scope.Complete();
            }
        }

        public Tender Find(int tenderId)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var gridReader =
                    connection
                        .QueryMultiple(
                            new StringBuilder()
                                .AppendLine("SELECT")
                                .AppendLine("a.SupOrderID Id,")
                                .AppendLine("CAST(a.SupOrderOrderNo AS INT) CallForTendersId,")
                                .AppendLine("a.SupOrderCurrencyCode Currency,")
                                .AppendLine("a.SupOrderDeliveryPt DeliverPoint,")
                                .AppendLine("a.SupOrderDelTerm DeliveryTerms,")
                                .AppendLine("a.SupOrderDeliveryTime DeliveryTime,")
                                .AppendLine("a.SupOrderValidityPeriod ExpiryDate,")
                                .AppendLine("a.SupOrderPaymentTerm PaymentTerms,")
                                .AppendLine("CAST(a.SupOrderSupplierNo AS INT) SupplierId")
                                .AppendLine("FROM b2badminuser.SupplierOrder a")
                                .AppendLine("WHERE a.SupOrderID = @TenderId;")
                                .AppendLine("SELECT")
                                .AppendLine("a.OtherChgAmount Amount,")
                                .AppendLine("a.OtherChgCurrencyCode Currency,")
                                .AppendLine("a.OtherChgDesc Description")
                                .AppendLine("FROM b2badminuser.SupplierOrderOtherCharge a")
                                .AppendLine("WHERE a.OtherChgSupOrderID = @TenderId;")
                                .AppendLine("SELECT")
                                .AppendLine("a.OrderAttachContent Content,")
                                .AppendLine("a.OrderAttachDisplayName Display,")
                                .AppendLine("a.OrderAttachFilename [Filename],")
                                .AppendLine("a.OrderAttachSODID OfferId,")
                                .AppendLine("CAST(a.OrderAttachSeq AS INT) Sequence,")
                                .AppendLine("CAST(a.OrderAttachType AS INT) [Type]")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment a")
                                .AppendLine("WHERE a.OrderAttachSupplier = 1")
                                .AppendLine("AND a.OrderAttachSODID IN")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("SODID")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetail")
                                .AppendLine("WHERE SODSupOrderID = @TenderId")
                                .AppendLine(");")
                                .AppendLine("SELECT")
                                .AppendLine("a.SODID Id,")
                                .AppendLine("a.SODManufacturerCode ManufacturerCode,")
                                .AppendLine("a.SODRemarks Remarks,")
                                .AppendLine("CAST(a.SODOrderLineNo AS INT) RequirementNumber,")
                                .AppendLine("a.SODSupplierCode SupplierCode,")
                                .AppendLine("a.SODUnitCost UnitCost")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetail a")
                                .AppendLine("WHERE a.SODSupOrderID = @TenderId;")
                                .AppendLine("SELECT")
                                .AppendLine("a.OrderAttachContent Content,")
                                .AppendLine("a.OrderAttachDisplayName Display,")
                                .AppendLine("a.OrderAttachFilename [Filename],")
                                .AppendLine("a.OrderAttachSODAltID OptionId,")
                                .AppendLine("CAST(a.OrderAttachSeq AS INT) Sequence,")
                                .AppendLine("CAST(a.OrderAttachType AS INT) [Type]")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment a")
                                .AppendLine("WHERE a.OrderAttachSupplier = 1")
                                .AppendLine("AND a.OrderAttachSODAltID IN")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("x.SODAltID")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetailAlternative x")
                                .AppendLine("WHERE x.SODAltSODID IN")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("y.SODID")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetail y")
                                .AppendLine("WHERE y.SODSupOrderID = @TenderId")
                                .AppendLine(")")
                                .AppendLine(");")
                                .AppendLine("SELECT")
                                .AppendLine("a.SODAltID Id,")
                                .AppendLine("a.SODAltCurrencyCode Currency,")
                                .AppendLine("a.SODAltDeliveryPt DeliveryPoint,")
                                .AppendLine("a.SODAltDelTerm DeliveryTerms,")
                                .AppendLine("a.SODAltDeliveryTime DeliveryTime,")
                                .AppendLine("a.SODAltDesc Description,")
                                .AppendLine("a.SODAltValidityPeriod ExpiryDate,")
                                .AppendLine("a.SODAltManufacturerCode ManufacturerCode,")
                                .AppendLine("CAST(a.SODAltQuantity AS INT) Quantity,")
                                .AppendLine("CAST(a.SODAltLineNo AS INT) RequirementNumber,")
                                .AppendLine("a.SODAltSupplierCode SupplierCode,")
                                .AppendLine("a.SODAltUnitCost UnitCost,")
                                .AppendLine("a.SODAltUM UnitOfMeasure")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetailAlternative a")
                                .AppendLine("WHERE a.SODAltSODID IN")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("a.SODID")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetail a")
                                .AppendLine("WHERE a.SODSupOrderID = @TenderId")
                                .AppendLine(");")
                                .AppendLine("SELECT")
                                .AppendLine("CAST(a.OrderDetLineNo AS INT) Number,")
                                .AppendLine("a.OrderDetDesc Description,")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("COUNT(1)")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment x")
                                .AppendLine("WHERE x.OrderAttachNo = a.OrderDetNo")
                                .AppendLine("AND x.OrderAttachLineNo = a.OrderDetLineNo")
                                .AppendLine("AND x.OrderAttachSupplier = 0")
                                .AppendLine("AND x.OrderAttachSODID = -1")
                                .AppendLine("AND x.OrderAttachSODAltID = -1")
                                .AppendLine("AND x.OrderAttachType <> 0")
                                .AppendLine(") FileAttachments,")
                                .AppendLine("CAST(a.OrderDetQuantity AS INT) Quantity,")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("COUNT(1)")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetailAttachment x")
                                .AppendLine("WHERE x.OrderAttachNo = a.OrderDetNo")
                                .AppendLine("AND x.OrderAttachLineNo = a.OrderDetLineNo")
                                .AppendLine("AND x.OrderAttachSupplier = 0")
                                .AppendLine("AND x.OrderAttachSODID = -1")
                                .AppendLine("AND x.OrderAttachSODAltID = -1")
                                .AppendLine("AND x.OrderAttachType = 0")
                                .AppendLine(") TextAttachments,")
                                .AppendLine("a.OrderDetUM UnitOfMeasure")
                                .AppendLine("FROM b2badminuser.OrderRequisitionDetail a")
                                .AppendLine("WHERE a.OrderDetNo IN")
                                .AppendLine("(")
                                .AppendLine("SELECT")
                                .AppendLine("a.SODOrderNo")
                                .AppendLine("FROM b2badminuser.SupplierOrderDetail a")
                                .AppendLine("WHERE a.SODSupOrderID = @TenderId")
                                .AppendLine(");")
                                .ToString(),
                            new { tenderId });

                return
                    Create(
                        gridReader.ReadSingle(),
                        gridReader.Read<Charge>(),
                        gridReader.Read(),
                        gridReader.Read(),
                        gridReader.Read(),
                        gridReader.Read(),
                        gridReader.Read());
            }
        }

        public TenderRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
    class UnitOfMeasureRepository
    {
        const string Sql =
@"
SELECT
RTRIM(LTRIM(a.DRKY)) [Key],
RTRIM(a.DRDL01) Value
FROM b2badminuser.F0005 a
WHERE a.DRSY = '00'
AND a.DRRT = 'UM'
";
        public KeyValuePair<string, string> FindByKey(string key)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .QuerySingle<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("AND LTRIM(a.DRKY) = @Key;")
                                .ToString(),
                            new { key });
            }
        }
        public KeyValuePair<string, string>[] GetAll()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return
                    connection
                        .Query<KeyValuePair<string, string>>(
                            new StringBuilder(Sql)
                                .Append("ORDER BY RTRIM(a.DRDL01);")
                                .ToString())
                        .ToArray();
            }
        }

        public UnitOfMeasureRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private readonly string connectionString;
    }
}
namespace Tendering.Application
{
    using System;
    using System.Linq;
    using Tendering.Domain;
    using Tendering.Persistence;

    class Service
    {
        public Tuple<string, int> SaveTender(
            int callForTendersId,
            Charge[] charges,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            DateTime expiryDate,
            Offer[] offers,
            string paymentTerms,
            string username)
        {
            var supplierId = this.applicationUserRepository.GetByUsername(username).SupplierId;

            var result =
                Tender.Create(
                    callForTendersId,
                    charges,
                    currency,
                    deliveryPoint,
                    deliveryTerms,
                    deliveryTime,
                    expiryDate,
                    offers,
                    paymentTerms,
                    supplierId);

            if (!string.IsNullOrWhiteSpace(result.Item1))
                return
                    Tuple.Create(
                        result.Item1,
                        0);

            this.tenderRepository.Add(result.Item2);

            return
                Tuple.Create(
                    result.Item1,
                    result.Item2.Id);
        }

        public Tuple<string, int> UpdateTender(
            int id,
            int callForTendersId,
            Charge[] charges,
            string currency,
            string deliveryPoint,
            string deliveryTerms,
            int deliveryTime,
            DateTime expiryDate,
            Offer[] offers,
            string paymentTerms,
            string username)
        {
            var supplierId = this.applicationUserRepository.GetByUsername(username).SupplierId;

            var result =
                Tender.Create(
                    id,
                    callForTendersId,
                    charges,
                    currency,
                    deliveryPoint,
                    deliveryTerms,
                    deliveryTime,
                    expiryDate,
                    offers,
                    paymentTerms,
                    supplierId);

            if (!string.IsNullOrWhiteSpace(result.Item1))
                return
                    Tuple.Create(
                        result.Item1,
                        0);

            this.tenderRepository.Update(result.Item2);

            return
                Tuple.Create(
                    result.Item1,
                    result.Item2.Id);
        }

        public Tender FindTender(int tenderId)
        {
            return this.tenderRepository.Find(tenderId);
        }
        public Requirement[] GetRequirementsOf(int callForTendersId)
        {
            return
                this
                .callForTendersRepository
                .Find(callForTendersId)
                .Requirements
                .ToArray();
        }

        public Service(ApplicationUserRepository applicationUserRepository, CallForTendersRepository callForTendersRepository, TenderRepository tenderRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.callForTendersRepository = callForTendersRepository;
            this.tenderRepository = tenderRepository;
        }

        private readonly ApplicationUserRepository applicationUserRepository;
        private readonly CallForTendersRepository callForTendersRepository;
        private readonly TenderRepository tenderRepository;
    }
}