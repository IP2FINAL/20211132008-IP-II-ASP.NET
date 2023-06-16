namespace News_Asp_Net_Api.Models
{
    public class news
    {
        public int id { get; set; }
        public string newsTitle { get; set; }
        public string newsDetail { get; set; }
        public int newsCategoryId { get; set; }
        public string newsPhoto { get; set; }
        
    }
}
