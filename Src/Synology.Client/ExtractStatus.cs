namespace SynologyClient
{
    public class ExtractStatus
    {
        public bool finished { get; set; }

        public long progress { get; set; }

        public string dest_folder_path { get; set; }
    }
}
