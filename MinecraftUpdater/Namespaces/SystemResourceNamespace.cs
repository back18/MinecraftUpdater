using QuanLib.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater.Namespaces
{
    public class SystemResourceNamespace : NamespaceManager
    {
        public SystemResourceNamespace(string @namespace) : base(@namespace)
        {
            CoreConfigFile = Combine("CoreConfig.toml");
            LogConfigFile = Combine("log4net.xml");
        }

        public string CoreConfigFile { get; }

        public string LogConfigFile { get; }
    }
}
