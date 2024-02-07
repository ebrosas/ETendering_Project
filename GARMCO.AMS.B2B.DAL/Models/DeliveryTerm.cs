using System;

namespace GARMCO.AMS.B2B.DAL
{
    public class DeliveryTerm : IEquatable<DeliveryTerm>
    {
        private readonly int uDCID;
        private readonly string uDCDesc1;

        public int UDCID { get { return this.uDCID; } }
        public string UDCDesc1 { get { return this.uDCDesc1; } }

        public DeliveryTerm(int uDCID, string uDCDesc1)
        {
            this.uDCID = uDCID;
            this.uDCDesc1 = uDCDesc1;
        }

        public static bool operator==(DeliveryTerm x, DeliveryTerm y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(DeliveryTerm x, DeliveryTerm y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DeliveryTerm);
        }
        public override int GetHashCode()
        {
            return this.UDCID.GetHashCode() ^ this.UDCDesc1.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", this.UDCID, this.UDCDesc1);
        }

        public bool Equals(DeliveryTerm other)
        {
            return other != null && this.UDCID == other.UDCID && this.UDCDesc1 == other.UDCDesc1;
        }
    }
}