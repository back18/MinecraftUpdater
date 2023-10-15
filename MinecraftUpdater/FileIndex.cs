using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater
{
    public abstract class FileIndex
    {
        public abstract string FilePath { get; }

        public abstract string AssetPath { get; }

        public abstract string Hash { get; }

        public abstract int Size { get; }

        public abstract byte[] GetBytes();
    }
}
