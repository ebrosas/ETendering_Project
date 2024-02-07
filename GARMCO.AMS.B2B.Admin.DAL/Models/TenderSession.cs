using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class TenderSession
    {
        public int TotalRFQOpened { get; set; }
        public int TotalRFQProcessed { get; set; }
        public int TotalRFQToProcess { get; set; }
        public int TotalTSAttendees { get; set; }
        public int TotalTSAttendeesPresent { get; set; }
        public int TSCreatedBy { get; set; }
        public DateTime TSCreatedDate { get; set; }
        public string TSCreatedName { get; set; }
        public DateTime? TSEndDate { get; set; }
        public int TSID { get; set; }
        public int TSModifiedBy { get; set; }
        public DateTime TSModifiedDate { get; set; }
        public string TSModifiedName { get; set; }
        public bool TSOpen { get; set; }
        public int TSRFQOpen { get; set; }
        public int TSRFQProcessed { get; set; }
        public DateTime TSStartDate { get; set; }
    }
}