using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionAttendee
    {
        public int TSAttCreatedBy { get; set; }
        public DateTime? TSAttCreatedDate { get; set; }
        public string TSAttCreatedName { get; set; }
        public DateTime TSAttDate { get; set; }
        public string TSAttEmail { get; set; }
        public int TSAttEmpNo { get; set; }
        public string TSAttEmpPosition { get; set; }
        public int TSAttID { get; set; }
        public int TSAttModifiedBy { get; set; }
        public DateTime? TSAttModifiedDate { get; set; }
        public string TSAttModifiedName { get; set; }
        public string TSAttName { get; set; }
        public bool TSAttPresent { get; set; }
        public bool TSAttPrimary { get; set; }
        public bool TSAttSigned { get; set; }
        public bool TSAttTenderComm { get; set; }
        public int TSAttTSID { get; set; }
        public string TSAttUsername { get; set; }
    }
}