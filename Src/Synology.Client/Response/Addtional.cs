namespace SynologyClient.Response
{
    public class Addtional
    {
        public string real_path { get; set; }
        public Owner owner { get; set; }
        public Time time { get; set; }
        public SharedFolderPerm perm { get; set; }
        public string mount_point_type { get; set; }
        //public VolumeStatus VolumeStatus { get; set; }
    }
}