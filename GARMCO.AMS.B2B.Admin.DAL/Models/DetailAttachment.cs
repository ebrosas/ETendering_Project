using System;

namespace GARMCO.AMS.B2B.Admin.DAL
{
    public class DetailAttachment
    {
        public byte[] OrderAttachContent { get; set; }
        public int OrderAttachCreatedBy { get; set; }
        public DateTime OrderAttachCreatedDate { get; set; }
        public string OrderAttachCreatedName { get; set; }
        public string OrderAttachDisplayName { get; set; }
        public string OrderAttachFilename { get; set; }
        public double OrderAttachLineNo { get; set; }
        public int OrderAttachModifiedBy { get; set; }
        public DateTime OrderAttachModifiedDate { get; set; }
        public string OrderAttachModifiedName { get; set; }
        public double OrderAttachNo { get; set; }
        public decimal OrderAttachSeq { get; set; }
        public int OrderAttachSODAltID { get; set; }
        public int OrderAttachSODID { get; set; }
        public bool OrderAttachSupplier { get; set; }
        public decimal OrderAttachType { get; set; }
    }
}