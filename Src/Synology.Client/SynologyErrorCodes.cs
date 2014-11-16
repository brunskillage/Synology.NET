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
            Dic.Add(800, "A folder path of favorite folder is already added to user’s favorites");
            Dic.Add(801, "A name of favorite folder conflicts with an existing folder path in the user’s favorites");
            Dic.Add(802, "There are too many favorites to be added");

            Dic.Add(1000, "Failed to copy files/folders. More information in <errors> object");
            Dic.Add(1001, "Failed to copy files/folders. More information in <errors> object");
            Dic.Add(1002, "An error occurred at the destination. More information in <errors> object");
            Dic.Add(1003, "Cannot overwrite or skip the existing file because no overwrite parameter is given");
            Dic.Add(1004,
                "File cannot overwrite a folder with the same name, or folder cannot overwrite a file with the same name");
            Dic.Add(1006, "Cannot copy/move file/folder with special characters to a FAT32 file system");
            Dic.Add(1007, "Cannot copy/move a file bigger than 4G to a FAT32 file system");

            Dic.Add(1100, "Failed to create a folder. More information in <errors> object");
            Dic.Add(1101, "The number of folders to the parent folder would exceed the system limitation");
            Dic.Add(1200, "Failed to rename it. More information in <errors> object");

            Dic.Add(1300, "Failed to compress files/folders");
            Dic.Add(1301, "Cannot create the archive because the given archive name is too long");

            Dic.Add(1400, "Failed to extract files.");
            Dic.Add(1401, "Cannot open the file as archive");
            Dic.Add(1402, "Failed to read archive data error");
            Dic.Add(1403, "Wrong password");
            Dic.Add(1404, "Failed to get the file and dir list in an archive");
            Dic.Add(1405, "Failed to find the item ID in an archive file");
            



            Dic.Add(2000, "Sharing link does not exist");
            Dic.Add(2001, "Cannot generate sharing link because too many sharing links exist");
            Dic.Add(2002, "Failed to access sharing links");
        }
    }
}