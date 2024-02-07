using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSessionHistory
    {
        public int TSHistCreatedBy { get; set; }
        public DateTime TSHistCreatedDate { get; set; }
        public string TSHistCreatedName { get; set; }
        public string TSHistDesc { get; set; }
        public int TSHistID { get; set; }
        public int TSHistTSID { get; set; }
    }
}