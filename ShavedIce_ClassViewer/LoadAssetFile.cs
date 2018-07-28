using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ShavedIce_ClassViewer
{
    static class LoadAssetFile
    {
        public static Type[] GetAssemblyTypes(string filename)
        {
            return Assembly.LoadFrom(filename).GetTypes();
        }
    }
}
