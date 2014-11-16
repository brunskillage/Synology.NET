namespace SynologyClient.Response
{
    public class VolumeStatus
    {
        public long freespace { get; set; }
        public long totalspace { get; set; }
        public bool @readonly { get; set; }
    }
}