using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkExtensions
{
    public static class AssemblyWrapper
    {
        /// <summary>
        /// Gets the assembly custom attribute value.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static string GetCustomAttributeValue(string attributeName)
        {
            return Assembly.GetCallingAssembly().CustomAttributes.
                FirstOrDefault(a => a.AttributeType.Name.Contains(attributeName))?.ConstructorArguments.First().Value as string;
        }

        /// <summary>
        /// Gets the last modification date of the assembly
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastModificationDate()
        {
            var filePath = Assembly.GetExecutingAssembly().Location;
            return new FileInfo(filePath).LastWriteTime;
        }
    }
}
