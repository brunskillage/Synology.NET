namespace SynologyClient.Response
{
    public class FavoriteList
    {
        public int total { get; set; }
        public int offset { get; set; }
        public Favorite[] favourites { get; set; }
    }


}