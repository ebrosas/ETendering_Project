using System;

namespace GARMCO.AMS.B2B.DAL
{
    sealed public class UppercaseString : IEquatable<UppercaseString>
    {
        private readonly string value;

        public UppercaseString(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = value.Trim('.', ' ').ToUpper();
        }

        public static bool operator==(UppercaseString x, UppercaseString y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(UppercaseString x, UppercaseString y)
        {
            return !(x == y);
        }

        public static implicit operator string(UppercaseString uppercaseString)
        {
            return uppercaseString.value;
        }
        public static implicit operator UppercaseString(string value)
        {
            return new UppercaseString(value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as UppercaseString);
        }
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
        public override string ToString()
        {
            return this.value;
        }

        public bool Equals(UppercaseString other)
        {
            return other != null && this.value == other.value;
        }
    }
}