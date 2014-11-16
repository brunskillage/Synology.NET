using System;
using System.IO;

namespace SynologyClient
{
    public interface ISynologyApi
    {
        // 19
        SynologyResponse SynoFilestationInfo();

        // p21
        SynologyResponse SynoFileStationListShare(
            int? offset,
            int? limit,
            SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            bool onlywritable,
            SynologyApi.FileListAddtionalOptions additional);

        //p26
        SynologyResponse SynoFileStationList(
            string folderPath,
            int? offset,
            int? limit,
            SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            string pattern,
            SynologyApi.FileTypeFilter fileType,
            string gotoPath,
            SynologyApi.FileListAddtionalOptions additional);

        //p32
        SynologyResponse SynoFileStationListGetInfo(string[] paths, SynologyApi.FileGetInfoAddtionalOptions addtional);

        //p35
        //Note: Linux timestamp in second, defined as the number of seconds that have elapsed since 00:00:00 Coordinated Universal Time (UTC), Thursday, 1 January 1970.
        SynologyResponse SynoFileStationSearchStart(
            string folderPath,
            bool recursive,
            string[] globPatterns,
            string[] extentionPatterns,
            SynologyApi.FileTypeFilter fileType,
            long minSizeBytes,
            long maxSizeBytes,
            DateTime? modifiedTimeFrom,
            DateTime? modifiedTimeTo,
            DateTime? createdTimeFrom,
            DateTime? createdTimeTo,
            DateTime? accessedTimeTo,
            DateTime? accessedTimeFrom,
            string owner,
            string group);

        SynologyResponse SynoFileStationSearchList(
            string taskId,
            int? offset,
            int? limit,
            SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection direction, string[] pattern,
            SynologyApi.FileTypeFilter fileType,
            SynologyApi.FileSearchListAddtionalOptions additional);

        //42
        SynologyResponse SynoFileStationSearchStop(string taskId);

        //42
        SynologyResponse SynoFileStationSearchClean(string taskId);

        SynologyResponse SynoFileStationVirtualFolderList(
            SynologyApi.FileSystemType fileSystemType,
            int? offset,
            int? limit,
            SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            SynologyApi.VirtualFolderListAddtionalOptions additional);

        SynologyResponse SynoFileStationFavoriteList(
            int? offset,
            int? limit,
            SynologyApi.StatusFilter statusFilter,
            SynologyApi.FileStationFavoriteAddtionalOptions additional);

        SynologyResponse SynoFileStationFavoriteAdd(string path, string name, int index);

        SynologyResponse SynoFileStationFavoriteDelete(string path);

        SynologyResponse SynoFileStationFavoriteClearBroken(string path, string name);

        SynologyResponse SynoFileStationFavoriteReplaceAll(string path, string name);

        byte[] SynoFileStationThumbGet(
            string path,
            SynologyApi.ThumbnailSizeOption size,
            SynologyApi.ThumbnailRotateOptions rotate);

        SynologyResponse SynoFileStationDirsizeStart(string path);

        SynologyResponse SynoFileStationDirsizeStatus(string taskId);

        SynologyResponse SynoFileStationDirsizeStop(string taskId);

        SynologyResponse SynoFileStationMd5Start(string filePath);

        SynologyResponse SynoFileStationMd5Status(string taskId);

        SynologyResponse SynoFileStationMd5Stop(string taskId);

        SynologyResponse SynoFileStationCheckPermission(string path, bool? createOnly);

        SynologyResponse SynoFileStationUpload(
            FileInfo fileName,
            string destinationFilePath,
            bool createParents,
            bool? overwrite);

        byte[] SynoFileStationDownload(string filePath, SynologyApi.DownloadMode mode);

        SynologyResponse SynoFileStationSharingGetInfo(string id);

        SynologyResponse SynoFileStationSharingList(int? offest,
            int? limit,
            SynologyApi.SharingSortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            bool? forceClean);

        SynologyResponse SynoFileStationSharingCreate(string path,
            string password,
            DateTime? dateExpires,
            DateTime? dateAvailable
            );

        SynologyResponse SynoFileStationSharingDelete(string id);

        SynologyResponse SynoFileStationSharingClearInvalid(string id);

        SynologyResponse SynoFileStationSharingEdit(string id,
            string password,
            DateTime? dateExpires,
            DateTime? dateAvailable
            );

        SynologyResponse SynoFileStationCreateFolder(string folderPath,
            string name,
            bool? forceParent,
            SynologyApi.FileSearchListAddtionalOptions additional);

        SynologyResponse SynoFileStationRename(string path,
            string name,
            SynologyApi.FileSearchListAddtionalOptions additional, string searchTaskId);

        SynologyResponse SynoFileStationCopyMoveStart(string path,
            string destinationPath,
            bool? overwrite = false,
            bool? removeSrc = false,
            bool? accurateProgress = false,
            string taskId = null);

        SynologyResponse SynoFileStationCopyMoveStatus(string taskId);

        SynologyResponse SynoFileStationCopyMoveStop(string taskId);

        SynologyResponse SynoFileStationDeleteStart(string path,
            bool? accurateProgress,
            bool? recursive,
            string searchTaskId);

        SynologyResponse SynoFileStationDeleteStatus(string taskId);

        SynologyResponse SynoFileStationDeleteStop(string taskId);

        SynologyResponse SynoFileStationDeleteSync(string path, bool? recursive, string searchTaskId);

        SynologyResponse SynoFileStationExtractStart(string archivePath,
            string destFolderPath,
            bool? overwrite,
            bool? keepDir,
            bool? createSubFolder,
            string codePage,
            string password,
            string itemId);

        SynologyResponse SynoFileStationExtractStatus(string taskId);

        SynologyResponse SynoFileStationExtractStop(string taskId);

        SynologyResponse SynoFileStationExtractList(string archivePath,
            int? offset,
            int? limit,
            SynologyApi.ExtractSortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            string codePage,
            string password,
            string itemId
            );

        SynologyResponse SynoFileStationCompressStart(string path,
            string destinationFilePath,
            SynologyApi.CompressionLevel level,
            SynologyApi.CompressionMode mode,
            SynologyApi.CompressionFormat format,
            string password);

        SynologyResponse SynoFileStationCompressStatus(string taskId);

        SynologyResponse SynoFileStationCompressStop(string taskId);

        SynologyResponse SynoFileStationBackgroundTaskList(int? offset,
            int? limit,
            SynologyApi.BackgroundTaskSortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            string apiFilterNamespace
            );

        SynologyResponse SynoFileStationBackgroundTaskClearFinished(string taskId);
    }
}