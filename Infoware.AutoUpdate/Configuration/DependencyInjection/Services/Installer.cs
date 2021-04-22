using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoware.AutoUpdate.Services
{
    public class Installer
    {
        public string FileName { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Architecture { get; set; }
        public string Configuration { get; set; }
    }
}
