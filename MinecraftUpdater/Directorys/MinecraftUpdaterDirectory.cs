using QuanLib.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater.Directorys
{
    public class MinecraftUpdaterDirectory : DirectoryManager
    {
        public MinecraftUpdaterDirectory(string directory) : base(directory)
        {
            CoreConfigFile = Combine("CoreConfig.toml");
            LogConfigFile = Combine("log4net.xml");
            LogsDir = AddDirectory<LogsDirectory>("Logs");
        }

        public string CoreConfigFile { get; }

        public string LogConfigFile { get; }

        public LogsDirectory LogsDir { get; }
    }
}
