namespace SynologyClient
{
    public class File
    {
        public string path { get; set; }

        public string name { get; set; }

        public bool isdir { get; set; }

        public Children children { get; set; }

        public FileAddtional additional { get; set; }
    }
}
