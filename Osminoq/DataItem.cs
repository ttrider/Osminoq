using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItem : IDataItem, IDataItemInternal
    {



        void IDataItemInternal.Initialize(string[] values)
        {
            this.Initialize(values);
        }


        protected virtual void Initialize(string[] values)
        {
        }
    }

    internal class TestTabularTextDataItem : TabularTextDataItem
    {
        private string _v1;
        private string _v2;
        private string _v3;

        static TestTabularTextDataItem()
        {
            patterns = new []
            {
                new Regex("pattern1"),
                new Regex("pattern1"),
                new Regex("pattern1"),
            };
            validators = new Regex[]
            {
                null,
                null,
                null,
            };
        }

        protected override void Initialize(string[] values)
        {
            var vs = base.PreInitialize(values);

            _v1 = vs[0];
            _v2 = vs[1];
            _v3 = vs[2];
        }
    }

    internal class TabularTextDataItem : DataItem
    {
        protected static Regex[] patterns;
        protected static Regex[] validators;


        protected string[] PreInitialize(string[] values)
        {
            if (patterns != null)
            {
                var length = Math.Min(values.Length, patterns.Length);
                for (var i = 0; i < length; i++)
                {
                    values[i] = ProcessPattern(values[i], patterns[i]);
                }
            }

            if (validators != null)
            {
                var length = Math.Min(values.Length, validators.Length);
                for (var i = 0; i < length; i++)
                {
                    if (!Validate(values[i], validators[i]))
                    {
                        throw new InvalidDataException();
                    }
                }
            }
            return values;
        }

        public static bool Validate(string value, Regex pattern)
        {
            if (pattern == null)
            {
                return true;
            }
            return pattern.IsMatch(value);
        }

        public static string ProcessPattern(string value, Regex pattern)
        {
            if (pattern == null)
            {
                return value;
            }
            var match = pattern.Match(value);
            if (!match.Success)
            {
                return value;
            }

            var group = match.Groups["value"];
            return group.Success ? group.Value : value;
        }
    }
}


/*
 * .method private hidebysig specialname rtspecialname static 
        void  .cctor() cil managed
{
  // Code size       67 (0x43)
  .maxstack  3
  .locals init ([0] class [System]System.Text.RegularExpressions.Regex[] CS$0$0000)
  IL_0000:  nop
  IL_0001:  ldc.i4.3
  IL_0002:  newarr     [System]System.Text.RegularExpressions.Regex
  IL_0007:  stloc.0
  IL_0008:  ldloc.0
  IL_0009:  ldc.i4.0
  IL_000a:  ldstr      "pattern1"
  IL_000f:  newobj     instance void [System]System.Text.RegularExpressions.Regex::.ctor(string)
  IL_0014:  stelem.ref
  IL_0015:  ldloc.0
  IL_0016:  ldc.i4.1
  IL_0017:  ldstr      "pattern1"
  IL_001c:  newobj     instance void [System]System.Text.RegularExpressions.Regex::.ctor(string)
  IL_0021:  stelem.ref
  IL_0022:  ldloc.0
  IL_0023:  ldc.i4.2
  IL_0024:  ldstr      "pattern1"
  IL_0029:  newobj     instance void [System]System.Text.RegularExpressions.Regex::.ctor(string)
  IL_002e:  stelem.ref
  IL_002f:  ldloc.0
  IL_0030:  stsfld     class [System]System.Text.RegularExpressions.Regex[] TTRider.Osminoq.TabularTextDataItem::patterns
  IL_0035:  ldc.i4.3
  IL_0036:  newarr     [System]System.Text.RegularExpressions.Regex
  IL_003b:  stloc.0
  IL_003c:  ldloc.0
  IL_003d:  stsfld     class [System]System.Text.RegularExpressions.Regex[] TTRider.Osminoq.TabularTextDataItem::validators
  IL_0042:  ret
} // end of method TestTabularTextDataItem::.cctor

.method family hidebysig virtual instance void 
        Initialize(string[] values) cil managed
{
  // Code size       37 (0x25)
  .maxstack  3
  .locals init ([0] string[] vs)
  IL_0000:  nop
  IL_0001:  ldarg.0
  IL_0002:  ldarg.1
  IL_0003:  call       instance string[] TTRider.Osminoq.TabularTextDataItem::PreInitialize(string[])
  IL_0008:  stloc.0
  IL_0009:  ldarg.0
  IL_000a:  ldloc.0
  IL_000b:  ldc.i4.0
  IL_000c:  ldelem.ref
  IL_000d:  stfld      string TTRider.Osminoq.TestTabularTextDataItem::_v1
  IL_0012:  ldarg.0
  IL_0013:  ldloc.0
  IL_0014:  ldc.i4.1
  IL_0015:  ldelem.ref
  IL_0016:  stfld      string TTRider.Osminoq.TestTabularTextDataItem::_v2
  IL_001b:  ldarg.0
  IL_001c:  ldloc.0
  IL_001d:  ldc.i4.2
  IL_001e:  ldelem.ref
  IL_001f:  stfld      string TTRider.Osminoq.TestTabularTextDataItem::_v3
  IL_0024:  ret
} // end of method TestTabularTextDataItem::Initialize


 */