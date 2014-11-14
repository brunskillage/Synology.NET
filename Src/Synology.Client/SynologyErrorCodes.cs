using System.Collections.Generic;

namespace SynologyClient
{
    public static class SynologyErrorCodes
    {
        public static readonly Dictionary<int, string> Dic = new Dictionary<int, string>();

        static SynologyErrorCodes()
        {
            Dic.Add(100, "Unknown error");
            Dic.Add(101, "No parameter of API, method or version");
            Dic.Add(102, "The requested API does not exist");
            Dic.Add(103, "The requested method does not exist");
            Dic.Add(104, "The requested version does not support the functionality");
            Dic.Add(105, "The logged in session does not have permission");
            Dic.Add(106, "Session timeout");
            Dic.Add(107, "Session interrupted by duplicate login");
            Dic.Add(400, "Invalid parameter of file operation");
            Dic.Add(401, "Unknown error of file operation");
            Dic.Add(402, "System is too busy");
            Dic.Add(403, "Invalid user does this file operation");
            Dic.Add(404, "Invalid group does this file operation");
            Dic.Add(405, "Invalid user and group does this file operation");
            Dic.Add(406, "Can’t get user/group information from the account server");
            Dic.Add(407, "Operation not permitted");
            Dic.Add(408, "No such file or directory");
            Dic.Add(409, "Non-supported file system");
            Dic.Add(410, "Failed to connect internet-based file system (ex: CIFS)");
            Dic.Add(411, "Read-only file system");
            Dic.Add(412, "Filename too long in the non-encrypted file system");
            Dic.Add(413, "Filename too long in the encrypted file system");
            Dic.Add(414, "File already exists");
            Dic.Add(415, "Disk quota exceeded");
            Dic.Add(416, "No space left on device");
            Dic.Add(417, "Input/output error");
            Dic.Add(418, "Illegal name or path");
            Dic.Add(419, "Illegal file name");
            Dic.Add(420, "Illegal file name on FAT file system");
            Dic.Add(421, "Device or resource busy");
            Dic.Add(599, "No such task of the file operation");
        }
    }
}