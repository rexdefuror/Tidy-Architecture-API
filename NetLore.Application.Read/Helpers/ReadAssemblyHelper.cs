using System.Reflection;

namespace NetLore.Application.Read.Helpers
{
    public static class ReadAssemblyHelper
    {
        public static Assembly Get()
        {
            return Assembly.GetAssembly(typeof(ReadAssemblyHelper));
        }
    }
}
