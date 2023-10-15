﻿using QuanLib.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftUpdater
{
    public class ConfigFileIndex : FileIndex
    {
        public ConfigFileIndex(string filePath, string assetPath, byte[] bytes)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException($"“{nameof(filePath)}”不能为 null 或空。", nameof(filePath));
            if (string.IsNullOrEmpty(assetPath))
                throw new ArgumentException($"“{nameof(assetPath)}”不能为 null 或空。", nameof(assetPath));
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));

            _bytes = bytes;
            FilePath = filePath;
            AssetPath = assetPath;
            Hash = HashUtil.GetHashString(bytes, HashType.SHA1);
            Size = bytes.Length;
        }

        private readonly byte[] _bytes;

        public override string FilePath { get; }

        public override string AssetPath { get; }

        public override string Hash { get; }

        public override int Size { get; }

        public string Extension => Path.GetExtension(FilePath);

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[_bytes.Length];
            _bytes.CopyTo(bytes, 0);
            return bytes;
        }


        public static bool Create(string filePath, string assetPath, [MaybeNullWhen(false)] out ConfigFileIndex result)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(assetPath))
                goto fail;

            try
            {
                using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read);
                fileStream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                result = new(filePath, assetPath, bytes);
                return true;
            }
            catch
            {
                goto fail;
            }

            fail:
            result = null;
            return false;
        }
    }
}
