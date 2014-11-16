namespace SynologyClient.Response
{
    public class MethodStatus
    {
        public string taskid { get; set; }
    }    
    
    public class DirSizeStatus
    {
        public bool finished { get; set; }
        public int  num_dir { get; set; }
        public int  num_file { get; set; }
        public long  total_size { get; set; }
    }

    public class MD5Status
    {
        public bool finished { get; set; }
        public string md5 { get; set; }
    }

    public class CheckPermissionWrite
    {
         // no specific response
    }

    public class DownloadDownload
    {
         // binary no response
    }
}