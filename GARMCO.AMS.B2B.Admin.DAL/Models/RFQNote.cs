using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class RFQNote
    {
        public int NoteCreatedBy { get; set; }
        public DateTime NoteCreatedDate { get; set; }
        public string NoteCreatedName { get; set; }
        public int NoteID { get; set; }
        public int NoteModifiedBy { get; set; }
        public DateTime NoteModifiedDate { get; set; }
        public string NoteModifiedName { get; set; }
        public string NoteRemark { get; set; }
        public double NoteRFQNo { get; set; }
    }
}