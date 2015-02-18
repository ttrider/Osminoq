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
        [Import(typeof (IDataItemTypeHandler))] 
        private List<IDataItemTypeHandler> handlers;

        public MethodInfo GetTypeHandler(string name)
        {
            if (handlers != null)
            {
                
            }
            return null;
        }
    }
}
