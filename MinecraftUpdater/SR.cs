using log4net.Core;
using MinecraftUpdater.Directorys;
using MinecraftUpdater.Namespaces;
using QuanLib.Minecraft.Directorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater
{
    public static class SR
    {
        private static LogImpl LOGGER => LogUtil.GetLogger();

        static SR()
        {
            SystemResourceNamespace = new("MinecraftUpdater.SystemResource");
            MinecraftUpdaterDirectory = new(Path.GetFullPath("MinecraftUpdater"));
            MinecraftUpdaterDirectory.BuildDirectoryTree();
        }

        public static SystemResourceNamespace SystemResourceNamespace { get; }

        public static MinecraftUpdaterDirectory MinecraftUpdaterDirectory { get; }

        public static MinecraftDirectory MinecraftDirectory
        {
            get
            {
                if (_MinecraftDirectory is null)
                    throw new InvalidOperationException();
                return _MinecraftDirectory;
            }
        }
        private static MinecraftDirectory? _MinecraftDirectory;

        public static MinecraftFileManager MinecraftFileManager
        {
            get
            {
                if (_MinecraftFileManager is null)
                    throw new InvalidOperationException();
                return _MinecraftFileManager;
            }
        }
        private static MinecraftFileManager? _MinecraftFileManager;

        public static CoreConfig CoreConfig
        {
            get
            {
                if (_CoreConfig is null)
                    throw new InvalidOperationException();
                return _CoreConfig;
            }
        }
        private static CoreConfig? _CoreConfig;

        public static void LoadAll()
        {
            _CoreConfig = CoreConfig.Load(MinecraftUpdaterDirectory.CoreConfigFile);
            _MinecraftDirectory = new(CoreConfig.MinecraftPath);
            _MinecraftFileManager = new();
        }

        public static void CreateIfNotExists()
        {
            CreateIfNotExists(MinecraftUpdaterDirectory.LogConfigFile, SystemResourceNamespace.LogConfigFile);
            CreateIfNotExists(MinecraftUpdaterDirectory.CoreConfigFile, SystemResourceNamespace.CoreConfigFile);
        }

        private static void CreateIfNotExists(string path, string resource)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException($"“{nameof(path)}”不能为 null 或空。", nameof(path));
            if (string.IsNullOrEmpty(resource))
                throw new ArgumentException($"“{nameof(resource)}”不能为 null 或空。", nameof(resource));

            if (!File.Exists(path))
            {
                using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource) ?? throw new InvalidOperationException();
                FileStream fileStream = new(path, FileMode.Create);
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
                LOGGER.Warn($"配置文件“{path}”不存在，已创建默认配置文件");
            }
        }
    }
}
