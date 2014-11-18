using System;
using System.IO;
using SynologyClient.Response;

namespace SynologyClient
{
    public interface ISynologyApi
    {
        // 19
        InfoGetInfoResponse Info_GetInfo();

        //// p21
        ListListShareResponse List_ListShare(int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection, bool onlywritable, SynologyApi.FileListAddtionalOptions additional);

        ////p26
        ListListResponse List_List(string folderPath, int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection sortDirection, string pattern, SynologyApi.FileTypeFilter fileType,
            string gotoPath, SynologyApi.FileListAddtionalOptions additional);

        ////p32
        ListGetInfoResponse List_GetInfo(string[] paths, SynologyApi.FileGetInfoAddtionalOptions additional);

        ////p35
        ////Note: Linux timestamp in second, defined as the number of seconds that have elapsed since 00:00:00 Coordinated Universal Time (UTC), Thursday, 1 January 1970.
        SearchStartResponse Search_Start(string folderPath, bool recursive, string[] globPatterns,
            string[] extentionPatterns, SynologyApi.FileTypeFilter fileType, long minSizeBytes, long maxSizeBytes,
            DateTime? modifiedTimeFrom, DateTime? modifiedTimeTo, DateTime? createdTimeFrom, DateTime? createdTimeTo,
            DateTime? accessedTimeTo, DateTime? accessedTimeFrom, string owner, string group);

        SearchListResponse Search_List(string taskId, int? offset, int? limit, SynologyApi.SortBy sortBy,
            SynologyApi.SortDirection direction, string[] pattern, SynologyApi.FileTypeFilter fileType,
            SynologyApi.FileSearchListAddtionalOptions additional);

        //42
        SearchStopResponse Search_Stop(string taskId);

        //42
        SearchCleanResponse Search_Clean(string taskId);

        VirtualFolderListResponse VirtualFolder_List(SynologyApi.FileSystemType fileSystemType, int? offset, int? limit,
            SynologyApi.SortBy sortBy, SynologyApi.SortDirection sortDirection,
            SynologyApi.VirtualFolderListAddtionalOptions additional);

        FavoriteListResponse Favorite_List(int? offset, int? limit, SynologyApi.StatusFilter statusFilter,
            SynologyApi.FileStationFavoriteAddtionalOptions additional);

        FavoriteAddResponse SynoFileStationFavoriteAdd(string path, string name, int index);

        FavoriteDeleteResponse SynoFileStationFavoriteDelete(string path);

        FavoriteClearBrokenResponse SynoFileStationFavoriteClearBroken(string path, string name);

        FavoriteEditResponse SynoFileStationFavoriteEdit(string path, string name);

        FavoritReplaceAllResponse SynoFileStationFavoriteReplaceAll(string path, string name);

        RawSynologyResponse SynoFileStationUpload(
            FileInfo fileName,
            string destinationFilePath,
            bool createParents,
            bool? overwrite);


        CompressStartResponse SynoFileStationCompress_Start(string path,
            string destinationFilePath,
            SynologyApi.CompressionLevel level,
            SynologyApi.CompressionMode mode,
            SynologyApi.CompressionFormat format,
            string password);

        CompressStatusResponse SynoFileStationCompress_Status(string taskId);

        CompressStopResponse SynoFileStationCompress_Stop(string taskId);

        byte[] SynoFileStationThumbGet(
            string path,
            SynologyApi.ThumbnailSizeOption size,
            SynologyApi.ThumbnailRotateOptions rotate);

        DirSizeStartResponse SynoFileStationDirsizeStart(string path);

        DirSizeStatusResponse SynoFileStationDirsizeStatus(string taskId);

        DirSizeStopResponse SynoFileStationDirsizeStop(string taskId);

        Md5StartResponse SynoFileStationMd5Start(string filePath);

        Md5StatusResponse SynoFileStationMd5Status(string taskId);

        Md5StopResponse SynoFileStationMd5Stop(string taskId);

        RawSynologyResponse SynoFileStationCheckPermissionWrite(string path, bool? createOnly);



        byte[] SynoFileStationDownload(string filePath, SynologyApi.DownloadMode mode);

        SharingGetInfoResponse SynoFileStationSharingGetInfo(string id);

        SharingListResponse SynoFileStationSharingList(int? offest,
            int? limit,
            SynologyApi.SharingSortBy sortBy,
            SynologyApi.SortDirection sortDirection,
            bool? forceClean);

        SharingCreateResponse SynoFileStationSharingCreate(string path,
            string password,
            DateTime? dateExpires,
            DateTime? dateAvailable
            );

        SharingDeleteResponse SynoFileStationSharingDelete(string id);

        SharingClearInvalidResponse SynoFileStationSharingClearInvalid(string id);

        SharingEditResponse SynoFileStationSharingEdit(string id,
            string password,
            DateTime? dateExpires,
            DateTime? dateAvailable
            );

        //BaseSynologyResponse SynoFileStationCreateFolder(string folderPath,
        //    string name,
        //    bool? forceParent,
        //    SynologyApi.FileSearchListAddtionalOptions additional);

        //BaseSynologyResponse SynoFileStationRename(string path,
        //    string name,
        //    SynologyApi.FileSearchListAddtionalOptions additional, string searchTaskId);

        //BaseSynologyResponse SynoFileStationCopyMoveStart(string path,
        //    string destinationPath,
        //    bool? overwrite = false,
        //    bool? removeSrc = false,
        //    bool? accurateProgress = false,
        //    string taskId = null);

        //BaseSynologyResponse SynoFileStationCopyMoveStatus(string taskId);

        //BaseSynologyResponse SynoFileStationCopyMoveStop(string taskId);

        //BaseSynologyResponse SynoFileStationDeleteStart(string path,
        //    bool? accurateProgress,
        //    bool? recursive,
        //    string searchTaskId);

        //BaseSynologyResponse SynoFileStationDeleteStatus(string taskId);

        //BaseSynologyResponse SynoFileStationDeleteStop(string taskId);

        //BaseSynologyResponse SynoFileStationDeleteSync(string path, bool? recursive, string searchTaskId);

        //BaseSynologyResponse SynoFileStationExtractStart(string archivePath,
        //    string destFolderPath,
        //    bool? overwrite,
        //    bool? keepDir,
        //    bool? createSubFolder,
        //    string codePage,
        //    string password,
        //    string itemId);

        //BaseSynologyResponse SynoFileStationExtractStatus(string taskId);

        //BaseSynologyResponse SynoFileStationExtractStop(string taskId);

        //BaseSynologyResponse SynoFileStationExtractList(string archivePath,
        //    int? offset,
        //    int? limit,
        //    SynologyApi.ExtractSortBy sortBy,
        //    SynologyApi.SortDirection sortDirection,
        //    string codePage,
        //    string password,
        //    string itemId
        //    );


        //BaseSynologyResponse SynoFileStationBackgroundTaskList(int? offset,
        //    int? limit,
        //    SynologyApi.BackgroundTaskSortBy sortBy,
        //    SynologyApi.SortDirection sortDirection,
        //    string apiFilterNamespace
        //    );

        //BaseSynologyResponse SynoFileStationBackgroundTaskClearFinished(string taskId);
    }
}