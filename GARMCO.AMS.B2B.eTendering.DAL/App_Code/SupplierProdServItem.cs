using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GARMCO.Common.Object;
using GARMCO.AMS.B2B.eTendering.DAL;
using GARMCO.AMS.B2B.Utility;

namespace GARMCO.AMS.B2B.eTendering.DAL
{
	[Serializable]
	sealed public class SupplierProdServItem : ObjectItem
	{
		#region Properties
		public int ProdServSupplierNo { get; set; }
		public string ProdServCode { get; set; }
		public string ProdServCodeDesc { get; set; }
		public B2BConstants.CheckState ProdServCheckState { get; set; }
		#endregion

		#region Constructors
		public SupplierProdServItem() :
			base()
		{
			this.ProdServSupplierNo = 0;
			this.ProdServCode = String.Empty;
			this.ProdServCodeDesc = String.Empty;
			this.ProdServCheckState = B2BConstants.CheckState.Uncheck;
		}

		public SupplierProdServItem(DataRow row) :
			this()
		{
			this.AssignItem(row);
		}

		public SupplierProdServItem(SupplierProdServItem item)
		{
			this.Added = item.Added;
			this.Modified = item.Modified;
			this.MarkForDeletion = item.MarkForDeletion;

			this.ProdServSupplierNo = item.ProdServSupplierNo;
			this.ProdServCode = item.ProdServCode;
			this.ProdServCodeDesc = item.ProdServCodeDesc;
			this.ProdServCheckState = item.ProdServCheckState;
		}

		public SupplierProdServItem(SupplierProductService supplierProductService) :
			this()
		{
			this.ProdServSupplierNo = supplierProductService.ProdServSupplierNo;
			this.ProdServCode = supplierProductService.ProdServCode;
			this.ProdServCodeDesc = supplierProductService.ProdServCodeDesc;
			this.ProdServCheckState = (B2BConstants.CheckState)Enum.Parse(typeof(B2BConstants.CheckState), supplierProductService.ProdServCheckState.ToString());
		}
		#endregion

		#region Override Methods
		public override void AssignItem(DataRow row)
		{
		}
		#endregion
	}
}
