namespace FirstWebApi.Models
{
    public class TestDataViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string? Error { get; set; }
    }
}
