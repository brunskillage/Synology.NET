using System.Collections.Generic;

namespace SynologyClient.Response
{
    public class FavoriteList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public List<Favorite> favorites { get; set; }
    }
}