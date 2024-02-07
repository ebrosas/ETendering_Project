using System;

namespace GARMCO.AMS.B2B.DAL
{
    sealed public class Country : IEquatable<Country>
    {
        private readonly TrimmedString dRKY;
        private readonly TrimmedString dRDL01;

        public string DRKY { get { return dRKY; } }
        public string DRDL01 { get { return dRDL01; } }

        public Country(string dRKY, string dRDL01)
        {
            this.dRKY = dRKY;
            this.dRDL01 = dRDL01;
        }

        public static bool operator==(Country x, Country y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(Country x, Country y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Country);
        }
        public override int GetHashCode()
        {
            return this.DRKY.GetHashCode() ^ this.DRDL01.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.DRKY, this.DRDL01);
        }

        public bool Equals(Country other)
        {
            return other != null && this.DRKY == other.DRKY && this.DRDL01 == other.DRDL01;
        }
    }
}