﻿using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace SynologyClient
{
    public class SynologyApi : ISynologyApi
    {
        // ReSharper disable InconsistentNaming
        public enum BackgroundTaskSortBy
        {
            crtime,
            finished
        }

        public enum CompressionFormat
        {
            formatZip,
            format7z
        }

        public enum CompressionLevel
        {
            moderate,
            fast,
            best,
            store
        }

        public enum CompressionMode
        {
            add,
            update,
            refreshen,
            synchronize
        }

        public enum DownloadMode
        {
            open,
            download
        }

        public enum ExtractSortBy
        {
            name,
            size,
            pack_size,
            mtime
        }

        public enum FileSystemType
        {
            cifs,
            iso
        }

        public enum FileTypeFilter
        {
            file,
            dir,
            all
        }

        public enum SharingSortBy
        {
            id,
            isFolder,
            path,
            date_expired,
            date_available,
            status,
            has_password,
            url,
            link_owner
        }

        public enum SortBy
        {
            name,
            user,
            group,
            mtime,
            atime,
            ctime,
            posix,
            size
        }

        public enum SortDirection
        {
            asc,
            desc
        }

        public enum StatusFilter
        {
            valid,
            broken,
            all
        }

        public enum ThumbnailRotateOptions
        {
            none,
            rotate90,
            rotate180,
            rotate270,
            rotate360
        }

        public enum ThumbnailSizeOption
        {
            small,
            medium,
            large,
            original
        }
        // ReSharper restore InconsistentNaming

        private readonly ISynologySession _session;

        public SynologyApi(ISynologySession session)
        {
            if (session == null)
                throw new ArgumentNullException("session");
            _session = session;

            if (string.IsNullOrEmpty(_session.sid))
                throw new SynologyClientException("Session Id is empty");
        }

        public InfoGetInfoResponse GetDiskstationInfo()
        {
            var proc = new FuncProcessor<InfoGetInfoResponse>("/FileStation/info.cgi", _session.sid, new
            {
                api = "SYNO.FileStation.Info",
                version = 1,
                method = "getinfo"
            });

            return proc.Run();
        }


        public FileSystemListResponse GetFileSystemEntries(string folderPath, int? offset = null, int? limit = null,
            SortBy sortBy = SortBy.ctime, SortDirection sortDirection = SortDirection.asc, string pattern = null,
            FileTypeFilter fileType = FileTypeFilter.all, string gotoPath = null,
            FileListAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.List",
                version = 1,
                method = "list",
                folder_path = folderPath,
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection,
                pattern,
                filetype = fileType,
                goto_path = gotoPath
            };

            var proc = new FuncProcessor<FileSystemListResponse>("/FileStation/file_share.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });

            return proc.Run();
        }

        public ListGetInfoResponse GetFileSystemInfo(string[] paths, FileGetInfoAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.List",
                version = 1,
                method = "getinfo",
                path = string.Join(",", paths)
            };

            var proc = new FuncProcessor<ListGetInfoResponse>("/FileStation/file_share.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });

            return proc.Run();
        }





        public SearchStartResponse SearchStart(string folderPath, bool recursive = true,
            string[] globPatterns = null, string[] extentionPatterns = null,
            FileTypeFilter fileType = FileTypeFilter.file,
            long minSizeBytes = 0, long maxSizeBytes = Int64.MaxValue, DateTime? modifiedTimeFrom = null,
            DateTime? modifiedTimeTo = null, DateTime? createdTimeFrom = null, DateTime? createdTimeTo = null,
            DateTime? accessedTimeTo = null, DateTime? accessedTimeFrom = null, string owner = null, string group = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Search",
                version = 1,
                method = "start",
                folder_path = folderPath,
                recursive,
                pattern = string.Join(",", globPatterns ?? new[] { "" }),
                extension = string.Join(",", extentionPatterns ?? new[] { "" }),
                filetype = fileType,
                size_from = minSizeBytes,
                size_to = maxSizeBytes,
                mtime_from = modifiedTimeFrom,
                mtime_to = modifiedTimeTo,
                crtime_from = createdTimeFrom,
                crtime_to = createdTimeTo,
                atime_from = accessedTimeFrom,
                atiime_to = accessedTimeTo,
                owner,
                group
            };

            var proc = new FuncProcessor<SearchStartResponse>("/FileStation/file_find.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SearchListResponse Searches(string taskId, int? offset = null, int? limit = 100,
            SortBy sortBy = SortBy.name, SortDirection sortDirection = SortDirection.asc, string[] pattern = null,
            FileTypeFilter fileType = FileTypeFilter.file, FileSearchListAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Search",
                version = 1,
                method = "list",
                taskid = taskId,
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection,
                pattern = string.Join(",", pattern ?? new[] { "" }),
                filetype = fileType
            };

            var proc = new FuncProcessor<SearchListResponse>("/FileStation/file_find.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });

            return proc.Run();
        }

        public SearchStopResponse SearchStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Search",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<SearchStopResponse>("/FileStation/file_find.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SearchCleanResponse SearchClean(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Search",
                version = 1,
                method = "clean",
                taskid = taskId
            };

            var proc = new FuncProcessor<SearchCleanResponse>("/FileStation/file_find.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public VirtualFolderListResponse VirtualFolderList(FileSystemType fileSystemType = FileSystemType.cifs,
            int? offset = null, int? limit = null, SortBy sortBy = SortBy.ctime,
            SortDirection sortDirection = SortDirection.asc, VirtualFolderListAddtionalOptions additional = null)
        {
            // string systype = fileSystemType.ToString() == FileSystemType.none.ToString() ? null : fileSystemType.ToString();

            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.VirtualFolder",
                version = 1,
                method = "list",
                type = fileSystemType,
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection
            };

            var proc = new FuncProcessor<VirtualFolderListResponse>("/FileStation/file_virtual.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });
            return proc.Run();
        }

        public FavoriteListResponse FavoriteList(int? offset = null, int? limit = null,
            StatusFilter statusFilter = StatusFilter.all, FileStationFavoriteAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "list",
                offset,
                limit,
                status_filter = statusFilter
            };

            var proc = new FuncProcessor<FavoriteListResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });
            return proc.Run();
        }

        public FavoriteAddResponse FavoriteAdd(string path, string name, int index = -1)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "add",
                path,
                name
            };

            var proc = new FuncProcessor<FavoriteAddResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public FavoriteDeleteResponse FavoriteDelete(string path)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "delete",
                path
            };

            var proc = new FuncProcessor<FavoriteDeleteResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public FavoriteClearBrokenResponse FavoriteClearBroken(string path, string name)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "clear_broken",
                path,
                name
            };

            var proc = new FuncProcessor<FavoriteClearBrokenResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public FavoriteEditResponse FavoriteEdit(string path, string name)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "edit",
                path,
                name
            };

            var proc = new FuncProcessor<FavoriteEditResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public FavoritReplaceAllResponse FavoriteReplaceAll(string path, string name)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Favorite",
                version = 1,
                method = "replace_all",
                path,
                name
            };

            var proc = new FuncProcessor<FavoritReplaceAllResponse>("/FileStation/file_favorite.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public RawSynologyResponse Upload(FileInfo fileName, string destinationFilePath,
            bool createParents = true, bool? overwrite = false)
        {
            var request = new SynologyRestRequest(Method.POST);

            request.AddParameter("_sid", _session.sid);
            request.AddParameter("api", "SYNO.FileStation.Upload");
            request.AddParameter("version", "1");
            request.AddParameter("method", "upload");
            request.AddParameter("dest_folder_path", destinationFilePath);
            request.AddParameter("create_parents", createParents);
            request.AddParameter("mtime", DateTimeExtender.GetUnixTimeFromDate(fileName.LastWriteTimeUtc).ToString());
            request.AddParameter("crtime", DateTimeExtender.GetUnixTimeFromDate(fileName.CreationTimeUtc).ToString());
            request.AddParameter("atime", DateTimeExtender.GetUnixTimeFromDate(fileName.LastAccessTimeUtc).ToString());
            request.AddParameter("overwrite", overwrite);

            request.AddFile(fileName.Name, fileName.FullName);

            var config = new SynologyClientConfig();
            var client = new RestClient(config.ApiBaseAddressAndPathNoTrailingSlash + "/FileStation/api_upload.cgi");

            IRestResponse<RawSynologyResponse> response = client.Execute<RawSynologyResponse>(request);
            return response.Data;
        }


        public CompressStartResponse CompressStart(string path, string destinationFilePath,
            CompressionLevel level = CompressionLevel.moderate, CompressionMode mode = CompressionMode.add,
            CompressionFormat format = CompressionFormat.formatZip,
            string password = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Compress",
                version = 1,
                method = "start",
                path,
                dest_file_path = destinationFilePath,
                level,
                mode,
                format,
                password
            };

            var proc = new FuncProcessor<CompressStartResponse>("/FileStation/file_compress.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public CompressStatusResponse CompressStatus(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Compress",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<CompressStatusResponse>("/FileStation/file_compress.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public CompressStopResponse CompressStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Compress",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<CompressStopResponse>("/FileStation/file_compress.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public byte[] GetThumb(string path, ThumbnailSizeOption size = ThumbnailSizeOption.small,
            ThumbnailRotateOptions rotate = ThumbnailRotateOptions.none)
        {
            var request = new SynologyRestRequest();
            request.AddParameter("api", "SYNO.FileStation.Thumb");
            request.AddParameter("version", "1");
            request.AddParameter("method", "get");
            request.AddParameter("path", path);
            request.AddParameter("size", size);
            request.AddParameter("rotate", rotate);
            request.AddParameter("_sid", _session.sid);
            var config = new SynologyClientConfig();
            var client = new RestClient(config.ApiBaseAddressAndPathNoTrailingSlash + "/FileStation/file_thumb.cgi");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new SynologyClientException("Errored with http status code " + response.StatusCode);
            return response.RawBytes;
        }

        public DirSizeStartResponse GetDirectorySizeStart(string path)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.DirSize",
                version = 1,
                method = "start",
                path
            };

            var proc = new FuncProcessor<DirSizeStartResponse>("/FileStation/file_dirSize.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DirSizeStatusResponse GetDirectorySizeStatus(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.DirSize",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<DirSizeStatusResponse>("/FileStation/file_dirSize.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DirSizeStopResponse GetDirectorySizeStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.DirSize",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<DirSizeStopResponse>("/FileStation/file_dirSize.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public Md5StartResponse GetFileMd5Start(string filePath)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.MD5",
                version = 1,
                method = "start",
                file_path = filePath
            };

            var proc = new FuncProcessor<Md5StartResponse>("/FileStation/file_md5.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public Md5StatusResponse GetFileMd5Status(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.MD5",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<Md5StatusResponse>("/FileStation/file_md5.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public Md5StopResponse GetFileMd5Stop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.MD5",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<Md5StopResponse>("/FileStation/file_md5.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public RawSynologyResponse CheckWritePermission(string path, bool? createOnly = true)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.CheckPermission",
                version = 1,
                method = "write",
                path,
                create_only = createOnly
            };

            var proc = new FuncProcessor<RawSynologyResponse>("/FileStation/file_permission.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public byte[] Download(string filePath, DownloadMode mode = DownloadMode.download)
        {
            var request = new SynologyRestRequest();

            request.AddParameter("api", "SYNO.FileStation.Download");
            request.AddParameter("version", "1");
            request.AddParameter("method", "download");
            request.AddParameter("path", filePath);
            request.AddParameter("mode", mode);
            request.AddParameter("_sid", _session.sid);
            var config = new SynologyClientConfig();
            var client = new RestClient(config.ApiBaseAddressAndPathNoTrailingSlash + "/FileStation/file_download.cgi");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new SynologyClientException("Errored with http status code " + response.StatusCode);
            return response.RawBytes;
        }

        public SharingGetInfoResponse GetSharingInfo(string id)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "getinfo",
                id
            };

            var proc = new FuncProcessor<SharingGetInfoResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public ListListShareResponse GetShares(int? offset = null, int? limit = null,
                    SortBy sortBy = SortBy.ctime, SortDirection sortDirection = SortDirection.asc,
                            bool onlywritable = false, FileListAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.List",
                version = 1,
                method = "list_share",
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection,
                onlywritable
            };

            var proc = new FuncProcessor<ListListShareResponse>("/FileStation/file_share.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });

            return proc.Run();
        }


        public SharingListResponse GetUserShares(int? offset, int? limit, SharingSortBy sortBy,
            SortDirection sortDirection = SortDirection.asc, bool? forceClean = true)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "list",
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection,
                force_clean = forceClean
            };

            var proc = new FuncProcessor<SharingListResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SharingCreateResponse CreateShare(string path, string password = null,
            DateTime? dateExpires = null,
            DateTime? dateAvailable = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "create",
                path,
                password,
                date_expired = dateExpires.HasValue ? dateExpires.Value.ToString("yyyy-MM-dd") : "0",
                date_available = dateAvailable.HasValue ? dateAvailable.Value.ToString("yyyy-MM-dd") : "0"
            };

            var proc = new FuncProcessor<SharingCreateResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SharingDeleteResponse DeleteShare(string id)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "delete",
                id
            };

            var proc = new FuncProcessor<SharingDeleteResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SharingClearInvalidResponse ClearInvalidShares(string id)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "clear_invalid",
                id
            };

            var proc = new FuncProcessor<SharingClearInvalidResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public SharingEditResponse EditShare(string id, string password = null,
            DateTime? dateExpires = null, DateTime? dateAvailable = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Sharing",
                version = 1,
                method = "edit",
                id,
                password,
                date_expired = dateExpires.HasValue ? dateExpires.Value.ToString("yyyy-MM-dd") : "0",
                date_available = dateAvailable.HasValue ? dateAvailable.Value.ToString("yyyy-MM-dd") : "0"
            };

            var proc = new FuncProcessor<SharingEditResponse>("/FileStation/file_sharing.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public CreateFolderResponse CreateFolder(string folderPath, string name, bool? forceParent = true,
            FileSearchListAddtionalOptions additional = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.CreateFolder",
                version = 1,
                method = "create",
                folder_path = folderPath,
                name,
                force_parent = forceParent
            };

            var proc = new FuncProcessor<CreateFolderResponse>("/FileStation/file_crtfdr.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });

            return proc.Run();
        }

        public RenameResponse FIleSystemRename(string path, string name,
            FileSearchListAddtionalOptions additional = null, string searchTaskId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Rename",
                version = 1,
                method = "rename",
                path,
                name,
                additional,
                search_taskid = searchTaskId
            };

            var proc = new FuncProcessor<RenameResponse>("/FileStation/file_rename.cgi", _session.sid, requiredParams, new
            {
                additional = TrueBooleanValuesFromObjectToCommaSeparatedList(additional)
            });
            return proc.Run();
        }

        public CopyMoveStartResponse CopyMoveStart(string path, string destinationPath,
            bool? overwrite = false, bool? removeSrc = false, bool? accurateProgress = false, string taskId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.CopyMove",
                version = 1,
                method = "start",
                path,
                dest_folder_path = destinationPath,
                overwrite,
                accurate_progress = accurateProgress,
                remove_src = removeSrc,
                taskid = taskId
            };

            var proc = new FuncProcessor<CopyMoveStartResponse>("/FileStation/file_MVCP.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public CopyMoveStatusResponse CopyMoveStatus(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.CopyMove",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<CopyMoveStatusResponse>("/FileStation/file_MVCP.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public CopyMoveStopResponse CopyMoveStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.CopyMove",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<CopyMoveStopResponse>("/FileStation/file_MVCP.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DeleteStartResponse DeleteStart(string path, bool? accurateProgress = true,
            bool? recursive = true, string searchTaskId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Delete",
                version = 1,
                method = "start",
                path,
                accurate_progress = accurateProgress,
                recursive,
                search_taskid = searchTaskId
            };

            var proc = new FuncProcessor<DeleteStartResponse>("/FileStation/file_delete.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DeleteStatusResponse DeleteStatus(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Delete",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<DeleteStatusResponse>("/FileStation/file_delete.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DeleteStopResponse DeleteStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Delete",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<DeleteStopResponse>("/FileStation/file_delete.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public DeleteSyncResponse Delete(string path, bool? recursive = true,
            string searchTaskId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Delete",
                version = 1,
                method = "delete",
                path,
                recursive,
                search_taskid = searchTaskId
            };

            var proc = new FuncProcessor<DeleteSyncResponse>("/FileStation/file_delete.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public ExtractStartResponse ExtractStart(string archivePath,
            string destFolderPath,
            bool? overwrite = false,
            bool? keepDir = true,
            bool? createSubFolder = false,
            string codePage = null,
            string password = null,
            string itemId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Extract",
                version = 1,
                method = "start",
                file_path = archivePath,
                dest_folder_path = destFolderPath,
                overwrite,
                keep_dir = keepDir,
                create_subfolder = createSubFolder,
                codepage = codePage,
                password,
                item_id = itemId
            };

            var proc = new FuncProcessor<ExtractStartResponse>("/FileStation/file_extract.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public ExtractStatusResponse ExtractStatus(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Extract",
                version = 1,
                method = "status",
                taskid = taskId
            };

            var proc = new FuncProcessor<ExtractStatusResponse>("/FileStation/file_extract.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public ExtractStopResponse ExtractStop(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Extract",
                version = 1,
                method = "stop",
                taskid = taskId
            };

            var proc = new FuncProcessor<ExtractStopResponse>("/FileStation/file_extract.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public ExtractListResponse ExtractListFiles(string archivePath, int? offset = 0, int? limit = -1,
            ExtractSortBy sortBy = ExtractSortBy.name, SortDirection sortDirection = SortDirection.asc,
            string codePage = "enu", string password = null, string itemId = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.Extract",
                version = 1,
                method = "list",
                file_path = archivePath,
                offset,
                limit,
                sortby = sortBy,
                sort_direction = sortDirection,
                codepage = codePage,
                password,
                itemId
            };

            var proc = new FuncProcessor<ExtractListResponse>("/FileStation/file_extract.cgi", _session.sid, requiredParams);
            return proc.Run();
        }


        public BackgroundTaskListResponse GetBackgroundTasks(int? offset = 0, int? limit = 0,
            BackgroundTaskSortBy sortBy = BackgroundTaskSortBy.crtime,
            SortDirection sortDirection = SortDirection.asc, string apiFilterNamespace = null)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.BackgroundTask",
                version = 1,
                method = "list",
                offset,
                limit,
                sort_by = sortBy,
                sort_direction = sortDirection,
                api_filter = apiFilterNamespace
            };

            var proc = new FuncProcessor<BackgroundTaskListResponse>("/FileStation/background_task.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public BackgroundTaskClearFinishedResponse ClearFinishedBackgroundTasks(string taskId)
        {
            dynamic requiredParams = new
            {
                api = "SYNO.FileStation.BackgroundTask",
                version = 1,
                method = "clear_finished",
                taskid = taskId
            };

            var proc = new FuncProcessor<BackgroundTaskClearFinishedResponse>("/FileStation/background_task.cgi", _session.sid, requiredParams);
            return proc.Run();
        }

        public string TrueBooleanValuesFromObjectToCommaSeparatedList<T>(T instance) where T : class
        {
            // creates comma delimited list of only boolean public property names set as true

            if (instance == null)
                return null;

            string[] selected = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => (bool)p.GetValue(instance, null))
                .Select(p => p.Name).ToArray();

            return selected.Any() ? string.Join(",", selected) : null;
        }

        // ReSharper disable InconsistentNaming
        // To directly map from synology specified Addtional parameters list paremeter naming conventions
        public class FileGetInfoAddtionalOptions
        {
            public bool real_path { get; set; }

            public bool size { get; set; }

            public bool owner { get; set; }

            public bool time { get; set; }

            public bool perm { get; set; }

            public bool mount_point_type { get; set; }

            public bool type { get; set; }
        }

        public class FileListAddtionalOptions
        {
            public bool real_path { get; set; }

            public bool size { get; set; }

            public bool owner { get; set; }

            public bool time { get; set; }

            public bool perm { get; set; }

            public bool mount_point_type { get; set; }

            public bool volume_status { get; set; }
        }

        public class FileSearchListAddtionalOptions
        {
            public bool real_path { get; set; }

            public bool size { get; set; }

            public bool owner { get; set; }

            public bool time { get; set; }

            public bool perm { get; set; }

            public bool type { get; set; }
        }

        public class FileStationFavoriteAddtionalOptions
        {
            public bool real_path { get; set; }

            public bool size { get; set; }

            public bool owner { get; set; }

            public bool time { get; set; }

            public bool perm { get; set; }

            public bool mount_point_type { get; set; }
        }

        public class VirtualFolderListAddtionalOptions
        {
            public bool real_path { get; set; }

            public bool size { get; set; }

            public bool owner { get; set; }

            public bool time { get; set; }

            public bool perm { get; set; }

            public bool volume_status { get; set; }
        }
        // ReSharper restore InconsistentNaming
    }
}