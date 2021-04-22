using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infoware.AutoUpdate
{
    public static class Helpers
    {
        public static bool CompatibilityArchitecture(string architectureInstaller, Assembly assembly)
        {
            if (architectureInstaller.Equals("x86", StringComparison.InvariantCultureIgnoreCase))
            {
                if (assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.MSIL ||
                    assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.X86 ||
                    assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.Amd64)
                {
                    return true;
                }
            }
            if (architectureInstaller.Equals("x64", StringComparison.InvariantCultureIgnoreCase))
            {
                if (assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.MSIL ||
                    assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.Amd64)
                {
                    return Environment.Is64BitOperatingSystem;
                }
            }
            if (architectureInstaller.Equals("arm", StringComparison.InvariantCultureIgnoreCase))
            {
                if (assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.Arm)
                {
                    return true;
                }
            }
            if (architectureInstaller.Equals("IA64", StringComparison.InvariantCultureIgnoreCase))
            {
                if (assembly.GetName().ProcessorArchitecture == ProcessorArchitecture.IA64)
                {
                    return true;
                }
            }
            return false;
        }

        public static int ArchitectureWeight(string architectureInstaller)
        {
            if (architectureInstaller.Equals("x64", StringComparison.InvariantCultureIgnoreCase))
            {
                return 1;
            }
            return 0;
        }
    }
}
