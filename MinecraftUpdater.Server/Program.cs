using log4net.Core;

namespace MinecraftUpdater.Server
{
    public static class Program
    {
        private static readonly LogImpl LOGGER;

        static Program()
        {
            Thread.CurrentThread.Name = "Main Thread";
            SR.CreateIfNotExists();
            LOGGER = LogUtil.GetLogger();
        }

        private static void Main(string[] args)
        {
            SR.LoadAll();
        }
    }
}
