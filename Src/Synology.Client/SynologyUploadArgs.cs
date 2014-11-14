using System;
using System.IO;

namespace SynologyClient
{
    public class SynologyUploadArgs
    {
        // returns 1003 error if same named file exists
        public SynologyUploadArgs(FileInfo fileInfo, string destFolderPath, bool? overwriteFile = null,
            bool createParents = true)
        {
            TheFile = fileInfo;
            mtime = fileInfo.LastWriteTimeUtc;
            crtime = fileInfo.CreationTimeUtc;
            atime = fileInfo.LastAccessTimeUtc;
            dest_folder_path = destFolderPath;
            create_parents = createParents;
            overwrite = overwriteFile;
            file = fileInfo.Name;
            boundary = "--" + Guid.NewGuid().ToString().Substring(0, 5);
        }

        public string dest_folder_path { get; private set; }

        public string _sid { get; set; }

        public string file { get; private set; }

        public string boundary { get; private set; }

        public bool create_parents { get; private set; }

        public bool? overwrite { get; set; }

        public DateTime mtime { get; private set; }

        public DateTime crtime { get; private set; }

        public DateTime atime { get; private set; }

        public FileInfo TheFile { get; private set; }
    }
}