using System;
using System.IO;

namespace SynologyClient
{
    public interface ISynologyApi
    {
        InfoGetInfoResponse GetDiskstationInfo();

        ListListShareResponse GetShares(int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection, bool onlywritable, SynologyApi.FileListAddtionalOptions additional);

        FileSystemListResponse GetFileSystemEntries(string folderPath, int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection, string pattern, SynologyApi.FileTypeFilter fileType,
            string gotoPath, SynologyApi.FileListAddtionalOptions additional);
        
        ListGetInfoResponse GetFileSystemInfo(string[] paths, SynologyApi.FileGetInfoAddtionalOptions additional);


        ////Note: Linux timestamp in second, defined as the number of seconds that have elapsed since 00:00:00 Coordinated Universal Time (UTC), Thursday, 1 January 1970.
        SearchStartResponse SearchStart(string folderPath, bool recursive, string[] globPatterns,
            string[] extentionPatterns, SynologyApi.FileTypeFilter fileType, long minSizeBytes, long maxSizeBytes,
            DateTime? modifiedTimeFrom, DateTime? modifiedTimeTo, DateTime? createdTimeFrom, DateTime? createdTimeTo,
            DateTime? accessedTimeTo, DateTime? accessedTimeFrom, string owner, string group);

        SearchListResponse Searches(string taskId, int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection direction, string[] pattern, SynologyApi.FileTypeFilter fileType,
            SynologyApi.FileSearchListAddtionalOptions additional);


        SearchStopResponse SearchStop(string taskId);

        SearchCleanResponse SearchClean(string taskId);

        VirtualFolderListResponse VirtualFolderList(SynologyApi.FileSystemType fileSystemType, int? offset, int? limit,
            SynologyApi.SortBy sortBy, SynologyApi.SortDirection sortDirection,
            SynologyApi.VirtualFolderListAddtionalOptions additional);

        FavoriteListResponse FavoriteList(int? offset, int? limit, SynologyApi.StatusFilter statusFilter,
            SynologyApi.FileStationFavoriteAddtionalOptions additional);

        FavoriteAddResponse FavoriteAdd(string path, string name, int index);

        FavoriteDeleteResponse FavoriteDelete(string path);

        FavoriteClearBrokenResponse FavoriteClearBroken(string path, string name);

        FavoriteEditResponse FavoriteEdit(string path, string name);

        FavoritReplaceAllResponse FavoriteReplaceAll(string path, string name);

        RawSynologyResponse Upload(FileInfo fileName, string destinationFilePath, bool createParents,
            bool? overwrite);


        CompressStartResponse CompressStart(string path, string destinationFilePath,
            SynologyApi.CompressionLevel level, SynologyApi.CompressionMode mode, SynologyApi.CompressionFormat format,
            string password);

        CompressStatusResponse CompressStatus(string taskId);

        CompressStopResponse CompressStop(string taskId);

        byte[] GetThumb(string path, SynologyApi.ThumbnailSizeOption size,
            SynologyApi.ThumbnailRotateOptions rotate);

        DirSizeStartResponse GetDirectorySizeStart(string path);

        DirSizeStatusResponse GetDirectorySizeStatus(string taskId);

        DirSizeStopResponse GetDirectorySizeStop(string taskId);

        Md5StartResponse GetFileMd5Start(string filePath);

        Md5StatusResponse GetFileMd5Status(string taskId);

        Md5StopResponse GetFileMd5Stop(string taskId);

        RawSynologyResponse CheckWritePermission(string path, bool? createOnly);

        byte[] Download(string filePath, SynologyApi.DownloadMode mode);

        SharingGetInfoResponse GetSharingInfo(string id);

        SharingListResponse GetUserShares(int? offest, int? limit, SynologyApi.SharingSortBy sortBy,
            SynologyApi.SortDirection sortDirection, bool? forceClean);

        SharingCreateResponse CreateShare(string path, string password, DateTime? dateExpires,
            DateTime? dateAvailable);

        SharingDeleteResponse DeleteShare(string id);

        SharingClearInvalidResponse ClearInvalidShares(string id);

        SharingEditResponse EditShare(string id, string password, DateTime? dateExpires,
            DateTime? dateAvailable);

        CreateFolderResponse CreateFolder(string folderPath, string name, bool? forceParent,
            SynologyApi.FileSearchListAddtionalOptions additional);

        RenameResponse FIleSystemRename(string path, string name,
            SynologyApi.FileSearchListAddtionalOptions additional, string searchTaskId);

        CopyMoveStartResponse CopyMoveStart(string path, string destinationPath, bool? overwrite = false,
            bool? removeSrc = false, bool? accurateProgress = false, string taskId = null);

        CopyMoveStatusResponse CopyMoveStatus(string taskId);

        CopyMoveStopResponse CopyMoveStop(string taskId);

        DeleteStartResponse DeleteStart(string path, bool? accurateProgress, bool? recursive,
            string searchTaskId);

        DeleteStatusResponse DeleteStatus(string taskId);

        DeleteStopResponse DeleteStop(string taskId);

        DeleteSyncResponse Delete(string path, bool? recursive, string searchTaskId);

        ExtractStartResponse ExtractStart(string archivePath, string destFolderPath, bool? overwrite,
            bool? keepDir, bool? createSubFolder, string codePage, string password, string itemId);

        ExtractStatusResponse ExtractStatus(string taskId);

        ExtractStopResponse ExtractStop(string taskId);

        ExtractListResponse ExtractListFiles(string archivePath, int? offset, int? limit,
            SynologyApi.ExtractSortBy sortBy, SynologyApi.SortDirection sortDirection, string codePage, string password,
            string itemId);


        BackgroundTaskListResponse GetBackgroundTasks(int? offset,
            int? limit,
            SynologyApi.BackgroundTaskSortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            string apiFilterNamespace
            );

        BackgroundTaskClearFinishedResponse ClearFinishedBackgroundTasks(string taskId);
    }
}