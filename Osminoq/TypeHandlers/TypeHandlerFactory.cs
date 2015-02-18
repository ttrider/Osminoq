using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq.TypeHandlers
{
    class TypeHandlerFactory
    {
        [ImportMany(typeof (IDataItemTypeHandler))] 
        private List<IDataItemTypeHandler> handlers;

        public bool TryGetTypeHandler(string name, out MethodInfo mi)
        {
            mi = null;
            if (handlers != null)
            {
                mi = (
                    from handler in handlers 
                    from attr in handler.GetType().GetCustomAttributes<TypeHandlerAttribute>() 
                    where string.Equals(name, attr.TypeName, StringComparison.OrdinalIgnoreCase) 
                    select handler.ConverterInfo).FirstOrDefault();
            }
            return mi!=null;
        }
    }
}
