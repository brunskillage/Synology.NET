namespace SynologyClient
{
    public class SharedFolderPerm
    {
        public string share_right { get; set; }
        public int posix { get; set; }
        public AdvancedRights adv_right { get; set; }
        public bool acl_enable { get; set; }
        public bool is_acl_mode { get; set; }
        public Acl Acl { get; set; }
    }
}