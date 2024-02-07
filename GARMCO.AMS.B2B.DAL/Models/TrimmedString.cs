using System;

namespace GARMCO.AMS.B2B.DAL
{
    sealed public class TrimmedString : IEquatable<TrimmedString>
    {
        private readonly string value;

        public TrimmedString(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this.value = value.Trim('.', ' ');
        }

        public static bool operator==(TrimmedString x, TrimmedString y)
        {
            return (ReferenceEquals(x, null) && ReferenceEquals(y, null)) || (!ReferenceEquals(x, null) && x.Equals(y));
        }
        public static bool operator!=(TrimmedString x, TrimmedString y)
        {
            return !(x == y);
        }

        public static implicit operator string(TrimmedString trimmedString)
        {
            return trimmedString.value;
        }
        public static implicit operator TrimmedString(string value)
        {
            return new TrimmedString(value);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TrimmedString);
        }
        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }
        public override string ToString()
        {
            return this.value;
        }

        public bool Equals(TrimmedString other)
        {
            return other != null && this.value == other.value;
        }
    }
}