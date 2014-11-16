namespace SynologyClient.Response
{
    public class VirtualFolderAddtional
    {
        public string real_path { get; set; }
        public Owner ownner { get; set; }
        public Time time { get; set; }
        public Permission perm { get; set; }
        public string mount_point_type { get; set; }
        public VolumeStatus volume_status { get; set; }
    }
}