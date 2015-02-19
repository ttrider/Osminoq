using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTRider.Osminoq.TypeHandlers
{
    internal class TypeHandlerFactory
    {
        [ImportMany(typeof(IDataItemTypeHandler))]
        #pragma warning disable 649
        private List<IDataItemTypeHandler> handlers;
        #pragma warning restore 649

        private readonly Lazy<Dictionary<string, MethodInfo>> typeHandlers;

        internal TypeHandlerFactory()
        {
            this.typeHandlers = new Lazy<Dictionary<string, MethodInfo>>(GetTypeHandlers, LazyThreadSafetyMode.ExecutionAndPublication);    
        }
            

        private Dictionary<string, MethodInfo> GetTypeHandlers()
        {
            var di = new Dictionary<string, MethodInfo>(StringComparer.OrdinalIgnoreCase);

            if (this.handlers != null)
            {
                foreach (var handler in this.handlers)
                {
                    var handlerType = handler.GetType();
                    foreach (var attr in handlerType.GetCustomAttributes<TypeHandlerAttribute>())
                    {
                        di[attr.TypeName] = handler.ConverterInfo;
                    }
                }
            }
            return di;
        }



        public bool TryGetTypeHandler(string name, out MethodInfo mi)
        {
            mi = null;
            if (handlers == null) return false;

            return this.typeHandlers.Value.TryGetValue(name, out mi);
        }
    }
}
