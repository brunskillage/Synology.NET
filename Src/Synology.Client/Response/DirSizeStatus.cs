namespace SynologyClient.Response
{
    public class DirSizeStatus
    {
        public bool finished { get; set; }
        public int  num_dir { get; set; }
        public int  num_file { get; set; }
        public long  total_size { get; set; }
    }
}