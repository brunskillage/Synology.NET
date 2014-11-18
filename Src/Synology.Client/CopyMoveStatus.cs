namespace SynologyClient
{
    public class CopyMoveStatus
    {
        public int processed_size { get; set; }
        public int total { get; set; }
        public string path { get; set; }
        public bool finished { get; set; }
        public long progress { get; set; }
        public string dest_folder_path { get; set; }
    }
}