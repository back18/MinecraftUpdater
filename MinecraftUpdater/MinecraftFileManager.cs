using log4net.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater
{
    public class MinecraftFileManager
    {
        private static LogImpl LOGGER = LogUtil.GetLogger();

        public MinecraftFileManager()
        {
            Mods = new(new Dictionary<string, ModFileIndex>());
            Configs = new(new Dictionary<string, ConfigFileIndex>());
            Reread();
        }

        public ReadOnlyDictionary<string, ModFileIndex> Mods { get; private set; }

        public ReadOnlyDictionary<string, ConfigFileIndex> Configs { get; private set; }

        public void Reread()
        {
            Mods = ReadMods();
            Configs = ReadConfigs();
        }

        public ReadOnlyDictionary<string, ModFileIndex> ReadMods()
        {
            string[] files = SR.MinecraftDirectory.ModsDir.GetFiles("*.jar");

            Dictionary<string, ModFileIndex> mods = new();
            foreach (string file in files)
            {
                if (ModFileIndex.Create(file, "mods/" + Path.GetFileName(file), out var modFileIndex))
                {
                    mods.Add(modFileIndex.AssetPath, modFileIndex);
                    LOGGER.Info("已读取模组: " + modFileIndex.ModInfo);
                }
                else
                {
                    LOGGER.Warn("无法读取模组: " + file);
                }
            }

            return new(mods);
        }

        public ReadOnlyDictionary<string, ConfigFileIndex> ReadConfigs()
        {
            throw new NotImplementedException();
        }
    }
}
