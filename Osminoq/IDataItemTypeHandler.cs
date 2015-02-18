using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    
    public interface IDataItemTypeHandler
    {
        Type TargetType { get; }

        MethodInfo ConverterInfo { get; }
    }
}
