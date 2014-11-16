namespace SynologyClient.Response
{
    public class FileAddtional
    {
        public string real_path { get; set; }
        public long size { get; set; }
        public Owner owner { get; set; }
        public Time time { get; set; }
        public Permission perm { get; set; }
        public string mount_point_type { get; set; }
        public string type { get; set; }
    }
}