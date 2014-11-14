using System;
using SynologyClient.Upload;

namespace SynologyClient
{
    public interface ISynologyApi
    {
        SynologyResponse Upload(SynologyUploadArgs args);

        // 19
        SynologyResponse SynoFilestationInfo();

        // p21
        SynologyResponse SynoFileStationListShare(
            int? offset, 
            int? limit,
            SynologyApi.sort_by sort_by,
            SynologyApi.sort_direction sort_direction, 
            bool onlywritable,
            SynologyApi.FileListAddtionalOptions additional);

        //p26
        SynologyResponse SynoFileStationList(
            string folderPath, 
            int? offset, 
            int? limit,
            SynologyApi.sort_by sortBy,
            SynologyApi.sort_direction sortDirection, 
            string pattern,
            SynologyApi.filetype fileType, 
            string goto_path,
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
            SynologyApi.filetype fileType, 
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
            SynologyApi.sort_by sortBy,
            SynologyApi.sort_direction direction, string[] pattern,
            SynologyApi.filetype fileType,
            SynologyApi.FileSearchListAddtionalOptions additional);

        //42
        SynologyResponse SynoFileStationSearchStop(string taskId);

        //42
        SynologyResponse SynoFileStationSearchClean(string taskId);

        SynologyResponse SynoFileStationVirtualFolderList(
            SynologyApi.FileSystemType fileSystemType, 
            int? offset,
            int? limit, 
            SynologyApi.sort_by sort_by,
            SynologyApi.sort_direction sort_direction,
            SynologyApi.VirtualFolderListAddtionalOptions additional);

        SynologyResponse SynoFileStationFavoriteList(
            int? offset, 
            int? limit,
            SynologyApi.status_filter statusFilter,
            SynologyApi.FileStationFavoriteAddtionalOptions additional);

        SynologyResponse SynoFileStationFavoriteAdd(string path, string name, int index);

        SynologyResponse SynoFileStationFavoriteDelete(string path);

        SynologyResponse SynoFileStationFavoriteClearBroken(string path, string name);
    }
}
