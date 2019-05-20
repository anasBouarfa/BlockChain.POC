using System;
using System.Linq;
using System.Reflection;
namespace Blockchain.POC.UI
{
    public static class TypeProvider
    {
        public static Type[] DefinedTypes
        {
            get
            {
                return Assembly.GetAssembly(typeof(TypeProvider)).DefinedTypes.ToArray();
            }
        }
    }
}
