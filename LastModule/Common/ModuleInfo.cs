using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ModuleInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }
        public string Path { get; set; } = "lib";
        public Assembly Assembly { get; set; }
    }
}
