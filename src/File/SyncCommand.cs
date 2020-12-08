﻿using System.IO;
using System.Linq;

namespace Microsoft.DotNet
{
    class SyncCommand : UpdateCommand
    {
        public SyncCommand(Config configuration) : base(configuration) { }

        protected override bool OnRemoteUrlMissing(FileSpec spec)
        {
            // If the file exists locally, delete it. Remove the config entry.
            if (File.Exists(spec.Path))
                File.Delete(spec.Path);

            // Clear empty directories
            if (!Directory.EnumerateFiles(new FileInfo(spec.Path).DirectoryName).Any())
                Directory.Delete(Path.GetDirectoryName(spec.Path));

            Configuration.RemoveSection("file", spec.Path);

            return true;
        }
    }
}
