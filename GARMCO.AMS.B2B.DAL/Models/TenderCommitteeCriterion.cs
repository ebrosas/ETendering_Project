namespace GARMCO.AMS.B2B.DAL
{
    public class TenderCommitteeCriterion
    {
        private readonly decimal mINBID;
        private readonly decimal mINQUOTED;

        public decimal MINBID { get { return this.mINBID; } }
        public decimal MINQUOTED { get { return this.mINQUOTED; } }

        public TenderCommitteeCriterion(decimal mINBID, decimal mINQUOTED)
        {
            this.mINBID = mINBID;
            this.mINQUOTED = mINQUOTED;
        }

        public static bool operator==(TenderCommitteeCriterion x, TenderCommitteeCriterion y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(TenderCommitteeCriterion x, TenderCommitteeCriterion y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TenderCommitteeCriterion);
        }
        public override int GetHashCode()
        {
            return this.MINBID.GetHashCode() ^ this.MINQUOTED.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.MINBID, this.MINQUOTED);
        }

        public bool Equals(TenderCommitteeCriterion other)
        {
            return other != null && this.MINBID == other.MINBID && this.MINQUOTED == other.MINQUOTED;
        }
    }
}