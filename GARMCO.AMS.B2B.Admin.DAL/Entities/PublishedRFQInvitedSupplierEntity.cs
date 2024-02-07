using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GARMCO.AMS.B2B.Admin.DAL.Entities
{
    [Serializable]
    public class PublishedRFQInvitedSupplierEntity
    {
        #region Properties

        public double SupInvJDERefNo { get; set; }
        public string SupInvSupplierName { get; set; }
        public int? SupInvSupplierNo { get; set; }
        public string SupInvSupplierAddress { get; set; }
        public string SupInvSupplierCity { get; set; }
        public string SupInvSupplierState { get; set; }
        public string SupInvSupplierCountry { get; set; }
        public string SupInvSupplierPostalCode { get; set; }
        public bool? SupInvRegistered { get; set; }

        #endregion
    }
}
