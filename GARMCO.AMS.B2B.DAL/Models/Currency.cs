using System;

namespace GARMCO.AMS.B2B.DAL
{
    public class Currency : IEquatable<Currency>
    {
        private readonly TrimmedString cVCRCD;
        private readonly UppercaseString cVDL01;

        public string CVCRCD { get { return this.cVCRCD; } }
        public string CVDL01 { get { return this.cVDL01; } }

        public Currency(string cVCRCD, string cVDL01)
        {
            this.cVCRCD = cVCRCD;
            this.cVDL01 = cVDL01;
        }

        public static bool operator==(Currency x, Currency y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(Currency x, Currency y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Currency);
        }
        public override int GetHashCode()
        {
            return this.CVCRCD.GetHashCode() ^ this.CVDL01.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.CVCRCD, this.CVDL01);
        }

        public bool Equals(Currency other)
        {
            return other != null && this.CVCRCD == other.CVDL01 && this.CVCRCD == other.CVDL01;
        }
    }
}