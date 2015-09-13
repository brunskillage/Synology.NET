namespace SynologyClient
{
    public class ArchiveItem
    {
        public int itemid { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public int pack_size { get; set; }
        public string mtime { get; set; }
        public string path { get; set; }
        public bool is_dir { get; set; }
    }
}