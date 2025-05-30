namespace BookCatalog.DAL.DTO
{
    public class BookStoreDTO
    {
        public DateTime CreatedAt { get; set; }
        public Guid Id { get; set; }
        public DateTime UpdatedAt { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string FirstImageUrl => Pictures?.FirstOrDefault()?.ImgUrl ?? "https://cdn1.iconfinder.com/data/icons/business-company-1/500/image-1024.png";
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<PictureDTO> Pictures { get; set; }

        //public List<Location>? GetDrivingPath()
        //{
        //    if (string.IsNullOrWhiteSpace(DrivingPath))
        //        return null;

        //    var points = PolyLineEncoder.Decode(DrivingPath);
        //    List<Location> locations = new List<Location>();
        //    foreach (var item in path)
        //    {
        //        var location = item.Split(',');
        //        if (location.Length == 2)
        //        {
        //            locations.Add(new Location
        //            {
        //                Latitude = double.Parse(location[0]),
        //                Longitude = double.Parse(location[1])
        //            });
        //        }
        //    }
        //    return locations;
        //    return null;
        //}
    }
}
