using System;
using System.IO;

namespace SynologyClient
{
    public interface ISynologyApi
    {
        GetDiskstationInfoResponse GetDiskstationInfo();

        GetSharesResponse GetShares(int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection, bool onlywritable, SynologyApi.FileListAddtionalOptions additional);

        GetFileSystemEntriesResponse GetFileSystemEntries(string folderPath, int? offset, int? limit,
            SynologyApi.SortBy sortBy, SynologyApi.SortDirection sortDirection, string pattern,
            SynologyApi.FileTypeFilter fileType, string gotoPath, SynologyApi.FileListAddtionalOptions additional);

        GetFileSystemInfoResponse GetFileSystemInfo(string[] paths, SynologyApi.FileGetInfoAddtionalOptions additional);

        ////Note: Linux timestamp in second, defined as the number of seconds that have elapsed since 00:00:00 Coordinated Universal Time (UTC), Thursday, 1 January 1970.
        SearchStartResponse SearchStartAsync(string folderPath, bool recursive, string[] globPatterns,
            string[] extentionPatterns, SynologyApi.FileTypeFilter fileType, long minSizeBytes, long maxSizeBytes,
            DateTime? modifiedTimeFrom, DateTime? modifiedTimeTo, DateTime? createdTimeFrom, DateTime? createdTimeTo,
            DateTime? accessedTimeTo, DateTime? accessedTimeFrom, string owner, string group);

        SearchStatusResponse SearchStatus(string taskId, int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection direction, string[] pattern, SynologyApi.FileTypeFilter fileType,
            SynologyApi.FileSearchListAddtionalOptions additional);

        SearchStopResponse SearchStop(string taskId);

        SearchCleanResponse SearchClean(string taskId);

        GetVirtualFoldersResponse GetVirtualFolders(SynologyApi.FileSystemType fileSystemType, int? offset, int? limit,
            SynologyApi.SortBy sortBy, SynologyApi.SortDirection sortDirection,
            SynologyApi.VirtualFolderListAddtionalOptions additional);

        FavoriteListResponse FavoriteList(int? offset, int? limit, SynologyApi.StatusFilter statusFilter,
            SynologyApi.FileStationFavoriteAddtionalOptions additional);

        AddFavoriteResponse AddFavorite(string path, string name, int index);

        DeleteFavoriteResponse DeleteFavorite(string path);

        ClearBrokenFavoritesResponse ClearBrokenFavorites(string path, string name);

        EditFavoriteResponse EditFavorite(string path, string name);

        ReplaceFavoriteResponse ReplaceFavorite(string path, string name);

        RawSynologyResponse Upload(FileInfo fileName, string destinationFilePath, bool createParents, bool? overwrite);

        CompressAsyncResposne CompressAsync(string path, string destinationFilePath, SynologyApi.CompressionLevel level,
            SynologyApi.CompressionMode mode, SynologyApi.CompressionFormat format, string password);

        CompressStatusResponse CompressStatus(string taskId);

        CompressStopResponse CompressStop(string taskId);

        byte[] GetThumb(string path, SynologyApi.ThumbnailSizeOption size, SynologyApi.ThumbnailRotateOptions rotate);

        GetDirectorySizeAsyncResponse GetDirectorySizeAsync(string path);

        DirSizeStatusResponse GetDirectorySizeStatus(string taskId);

        DirSizeStopResponse GetDirectorySizeStop(string taskId);

        GetFileMd5AsyncResponse GetFileMd5Async(string filePath);

        GetFileMd5StatusResponse GetFileMd5Status(string taskId);

        GetFileMd5StopResponse GetFileMd5Stop(string taskId);

        RawSynologyResponse CheckWritePermission(string path, bool? createOnly);

        byte[] Download(string filePath, SynologyApi.DownloadMode mode);

        GetSharingInfoResponse GetSharingInfo(string id);

        GetUserSharesResponse GetUserShares(int? offest, int? limit, SynologyApi.SharingSortBy sortBy,
            SynologyApi.SortDirection sortDirection, bool? forceClean);

        AddShareResponse AddShare(string path, string password, DateTime? dateExpires, DateTime? dateAvailable);

        DeleteShareResponse DeleteShare(string id);

        ClearInvalidSharesResponse ClearInvalidShares(string id);

        EditShareResponse EditShare(string id, string password, DateTime? dateExpires, DateTime? dateAvailable);

        AddFolderResponse AddFolder(string folderPath, string name, bool? forceParent,
            SynologyApi.FileSearchListAddtionalOptions additional);

        RenameResponse FileSystemRename(string path, string name, SynologyApi.FileSearchListAddtionalOptions additional,
            string searchTaskId);

        CopyMoveAsyncResponse CopyMoveAsync(string path, string destinationPath, bool? overwrite = false,
            bool? removeSrc = false, bool? accurateProgress = false, string taskId = null);

        CopyMoveStatusResponse CopyMoveStatus(string taskId);

        CopyMoveStopResponse CopyMoveStop(string taskId);

        DeleteAsyncResponse DeleteAsync(string path, bool? accurateProgress, bool? recursive, string searchTaskId);

        DeleteStatusResponse DeleteStatus(string taskId);

        DeleteStopResponse DeleteStop(string taskId);

        DeleteResponse Delete(string path, bool? recursive, string searchTaskId);

        ExtractAsyncResponse ExtractAsync(string archivePath, string destFolderPath, bool? overwrite, bool? keepDir,
            bool? createSubFolder, string codePage, string password, string itemId);

        ExtractStatusResponse ExtractStatus(string taskId);

        ExtractStopResponse ExtractStop(string taskId);

        ExtractListResponse ExtractListFiles(string archivePath, int? offset, int? limit,
            SynologyApi.ExtractSortBy sortBy, SynologyApi.SortDirection sortDirection, string codePage, string password,
            string itemId);

        GetBackgroundTasksResponse GetBackgroundTasks(int? offset, int? limit, SynologyApi.BackgroundTaskSortBy sortBy,
            SynologyApi.SortDirection sortDirection, string apiFilterNamespace);

        ClearFinishedBackgroundTasksResponse ClearFinishedBackgroundTasks(string taskId);
    }
}