namespace SynologyClient.Response
{
    public class Permission
    {
        public int posix { get; set; }
        public bool is_acl_mode { get; set; }
        public Acl acl { get; set; }
    }
}