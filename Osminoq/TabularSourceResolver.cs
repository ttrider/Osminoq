using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TTRider.Osminoq
{
    class TabularSourceResolver
    {
        private readonly Dictionary<string, int> fieldNames;
        private readonly int fieldCount;

        public TabularSourceResolver(int fieldCount)
        {
            if (fieldCount <= 0) throw new ArgumentNullException("fieldCount");
            this.fieldCount = fieldCount;
        }

        public TabularSourceResolver(IEnumerable<string> fieldNames)
        {
            if (fieldNames == null) throw new ArgumentNullException("fieldNames");

            this.fieldNames = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var fieldName in fieldNames)
            {
                this.fieldNames[fieldName] = this.fieldNames.Count;
            }
            this.fieldCount = this.fieldNames.Count;
        }

        public int ResolveSource(string source)
        {
            // source can be either @name or just a number
            // in case of @name - we can't proceed - recordset is without names
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new InvalidDataException("Field's 'Source' property can't be empty");
            }
            source = source.Trim();

            int index;
            if (source.StartsWith("@"))
            {
                if (this.fieldNames == null)
                {
                    throw new InvalidDataException("Can't use named fields without list of names");
                }
                if (source.Length <= 1)
                {
                    throw new InvalidDataException("Field name is missing");
                }
                index = this.fieldNames[source.Substring(1)];
            }
            else
            {
                index = int.Parse(source, NumberStyles.Any);
            }

            if (index < 0 || index >= this.fieldCount)
            {
                throw new InvalidDataException("Field index '" + index + "' is out of bounds");
            }
            return index;
        }
    }
}
