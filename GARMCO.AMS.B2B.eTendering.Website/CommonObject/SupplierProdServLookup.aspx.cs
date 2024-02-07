using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Utility;
using GARMCO.AMS.Common.Web;
using Telerik.Web.UI;

namespace GARMCO.AMS.B2B.eTendering.Website.CommonObject
{
	public partial class SupplierProdServLookup : BaseWebForm
	{
		#region Properties
		public List<SupplierProdServItem> SupplierProductServiceList
		{
			get
			{
				List<SupplierProdServItem> list = new List<SupplierProdServItem>();
				if (Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] != null)
					list = Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] as List<SupplierProdServItem>;

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE] = value;
			}
		}

		public List<SupplierProdServItem> SupplierProductServiceListTemp
		{
			get
			{
				List<SupplierProdServItem> list = new List<SupplierProdServItem>();
				if (Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE_TEMP] != null)
					list = Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE_TEMP] as List<SupplierProdServItem>;

				return list;
			}

			set
			{
				Session[B2BConstants.ITEM_LIST_SUPPLIER_PRODSERVICE_TEMP] = value;
			}
		}
		#endregion

		#region Private Data Members
		private IEnumerable<SupplierProductService> _prodServDataTable = null;
		private bool _nodeCheckedFound = false;
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{

				// Stores the parent ajax id
				this.hidAjaxID.Value = Request.QueryString["ajaxID"];

				// Sets the parent product/service
				this.LoadRootNodes(Request.QueryString["prodServCode"], Server.UrlDecode(Request.QueryString["prodServCodeDesc"]));

			}
		}

		#region Override Methods
		protected override void OnInit(EventArgs e)
		{
			// Don't check the session
			this.IsToCheckSession = false;

			base.OnInit(e);
		}
		#endregion

		#region Private Methods
		private void LoadRootNodes(string unspscCode, string unspscDesc)
		{
			#region Copy the products/services to the temporary list
			string nodePrefix = unspscCode.Substring(0, 2);
			this.SupplierProductServiceListTemp = new List<SupplierProdServItem>();

			if (this.SupplierProductServiceList.Exists(tempItem => tempItem.ProdServCode.IndexOf(nodePrefix) == 0))
			{

				foreach (SupplierProdServItem item in this.SupplierProductServiceList.FindAll(prodServ => prodServ.ProdServCode.IndexOf(nodePrefix) == 0))
					this.SupplierProductServiceListTemp.Add(new SupplierProdServItem(item));

			}

			else
			{

				// Retrieves from the database
				this.objProdServ.SelectParameters["prodServCode"].DefaultValue = unspscCode;
				this.objProdServ.Select();

				if (this._prodServDataTable != null)
				{

					foreach (var service in this._prodServDataTable)
						this.SupplierProductServiceListTemp.Add(new SupplierProdServItem(service));

				}
			}
			#endregion

			#region Add the parent node
			RadTreeNode node = new RadTreeNode(unspscDesc, unspscCode);
			node.ExpandMode = TreeNodeExpandMode.ServerSide;

			// Add to the tree
			this.treeSupplierProdServ.Nodes.Add(node);

			#region Set the state
			SupplierProdServItem prodServItem = this.SupplierProductServiceListTemp.Find(tempItem => tempItem.ProdServCode == unspscCode && !tempItem.MarkForDeletion);
			if (prodServItem != null)
			{

				if (prodServItem.ProdServCheckState == B2BConstants.CheckState.Indeterminate)
					this.PopulateChildNodes(new RadTreeNodeEventArgs(this.treeSupplierProdServ.Nodes[0]), prodServItem, false);

				else if (prodServItem.ProdServCheckState == B2BConstants.CheckState.Checked)
					this.treeSupplierProdServ.Nodes[0].Checked = true;

			}
			#endregion
			#endregion
		}

		private void PopulateChildNodes(RadTreeNodeEventArgs e, SupplierProdServItem parentItem, bool expand)
		{
			#region Retrieve the data
			if (e.Node.Value.IndexOf("00") > -1)
			{

				// Set the data source
				this.objProdServ.SelectParameters["prodServCode"].DefaultValue = e.Node.Value;
				this.objProdServ.Select();

			}
			#endregion

			#region Create the child nodes
			if (this._prodServDataTable != null)
			{

				foreach (var service in this._prodServDataTable)
				{

					RadTreeNode node = new RadTreeNode(service.ProdServCodeDesc, service.ProdServCode);
					node.ExpandMode = TreeNodeExpandMode.ServerSide;

					if (parentItem != null && parentItem.ProdServCheckState == B2BConstants.CheckState.Checked)
						node.Checked = true;

					e.Node.Nodes.Add(node);

					#region Set the state
					SupplierProdServItem prodServItem = this.SupplierProductServiceListTemp.Find(prodServ => prodServ.ProdServCode == service.ProdServCode && !prodServ.MarkForDeletion);
					if (prodServItem != null)
					{

						if (prodServItem.ProdServCheckState == B2BConstants.CheckState.Indeterminate)
							this.PopulateChildNodes(new RadTreeNodeEventArgs(e.Node.Nodes[e.Node.Nodes.Count - 1]), prodServItem, false);

						else if (prodServItem.ProdServCheckState == B2BConstants.CheckState.Checked)
						{

							node.Checked = true;

							// Set the flag
							this._nodeCheckedFound = true;

						}

						else
							node.Checked = false;

					}
					#endregion
				}

				if (this._prodServDataTable.Count() > 0)
					e.Node.Expanded = expand;

			}
			#endregion
		}

		private void SetNodeState(RadTreeNode parentNode)
		{
			// Retrieve the item from the list
			SupplierProdServItem item = this.SupplierProductServiceListTemp.Find(tempItem => tempItem.ProdServCode == parentNode.Value);

			#region Checks the state
			if (parentNode.CheckState == TreeNodeCheckState.Unchecked)
			{

				// Remove the item and child items
				if (item != null)
					this.RemoveFromList(item);

			}

			else
			{

				if (item == null)
				{

					item = new SupplierProdServItem()
					{
						Added = true,
						ProdServCheckState = (B2BConstants.CheckState)(int)parentNode.CheckState,
						ProdServSupplierNo = 0,
						ProdServCode = parentNode.Value,
						ProdServCodeDesc = parentNode.Text
					};

					this.SupplierProductServiceListTemp.Add(item);

				}

				else
				{

					item.MarkForDeletion = false;
					item.Modified = true;
					item.ProdServCheckState = (B2BConstants.CheckState)(int)parentNode.CheckState;

				}
			}
			#endregion

			// Checks the child nodes
			if (parentNode.Nodes.Count > 0 && parentNode.CheckState != TreeNodeCheckState.Unchecked)
			{

				foreach (RadTreeNode node in parentNode.Nodes)
					this.SetNodeState(node);

			}
		}

		private void RemoveFromList(SupplierProdServItem parentItem)
		{
			int index = parentItem.ProdServCode.IndexOf("00");
			if (index > -1)
			{

				if ((index % 2) != 0)
					index++;

				string unspscCode = parentItem.ProdServCode.Substring(0, index);

				List<SupplierProdServItem> list = this.SupplierProductServiceListTemp.FindAll(
					tempItem => tempItem.ProdServCode.IndexOf(unspscCode) == 0);
				foreach (SupplierProdServItem item in list)
				{

					// Mark for deletion
					if (item.ProdServSupplierNo > 0)
					{

						item.MarkForDeletion = true;
						item.ProdServCheckState = B2BConstants.CheckState.Uncheck;

					}

					// Remove physically
					else
						this.SupplierProductServiceListTemp.Remove(item);

				}
			}
		}
		#endregion

		#region Action Buttons
		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Retrieve all the selected products/services
			this.SetNodeState(this.treeSupplierProdServ.Nodes[0]);

			#region Remove first all the previous items
			int index = this.treeSupplierProdServ.Nodes[0].Value.IndexOf("00");
			if (index > -1)
			{

				if ((index % 2) != 0)
					index++;

				string unspscCode = this.treeSupplierProdServ.Nodes[0].Value.Substring(0, index);

				this.SupplierProductServiceList.RemoveAll(tempItem => tempItem.ProdServCode.IndexOf(unspscCode) == 0);

			}
			#endregion

			// Copies all the items to the master list
			foreach (SupplierProdServItem item in this.SupplierProductServiceListTemp)
				this.SupplierProductServiceList.Add(new SupplierProdServItem(item));

			// Builds the script
			ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UNSPSC",
				String.Format("OnCloseSupplierProdServLookUp('{0}', '{1}');", this.hidAjaxID.Value, this.treeSupplierProdServ.Nodes[0].Value), true);
		}
		#endregion

		#region Data Binding
		protected void treeSupplierProdServ_NodeDataBound(object sender, RadTreeNodeEventArgs e)
		{
			var row = e.Node.DataItem as SupplierProductService;
			if (row != null)
				e.Node.Text = String.Format("({0}) {1}", row.ProdServCode, row.ProdServCodeDesc);
		}

		protected void treeSupplierProdServ_NodeExpand(object sender, RadTreeNodeEventArgs e)
		{
			// Checks if node is not yet populated
			if (e.Node.Nodes.Count == 0)
			{

				SupplierProdServItem provServItem = new SupplierProdServItem()
				{
					ProdServSupplierNo = 0,
					ProdServCode = e.Node.Value,
					ProdServCodeDesc = e.Node.Text,
					ProdServCheckState = (B2BConstants.CheckState)(int)e.Node.CheckState
				};

				this.PopulateChildNodes(e, provServItem, true);

			}
		}
		#endregion

		#region Database Access
		protected void objProdServ_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			this._prodServDataTable = e.ReturnValue as IEnumerable<SupplierProductService>;
		}
		#endregion
	}
}