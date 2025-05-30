namespace BookCatalog.Models;

public class MyExtent
{
    public double MinLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MaxLongitude { get; set; }

    public void ReadLatLon(double lat, double lon, bool isFirstPoint)
    {
        if (isFirstPoint)
        {
            MinLongitude = lon;
            MaxLongitude = lon;
            MinLatitude = lat;
            MaxLatitude = lat;
        }
        else
        {
            if (lon < MinLongitude)
                MinLongitude = lon;
            if (lat < MinLatitude)
                MinLatitude = lat;
            if (lon > MaxLongitude)
                MaxLongitude = lon;
            if (lat > MaxLatitude)
                MaxLatitude = lat;
        }
    }

    // Update the extent with on a single point
    public void ReadLatLon(Location location, bool isFirstPoint)
    {
        ReadLatLon(location.Latitude, location.Longitude, isFirstPoint);
    }

    // Populate the extent with a list of points
    public void ReadLatLon(List<Location> locations)
    {
        for (int i = 0; i < locations.Count; i++)
        {
            ReadLatLon(locations[i], i == 0);
        }
    }

    public MyExtent() { }

    // Create an extent from a single point
    public MyExtent(double latitude, double longitude)
    {
        ReadLatLon(latitude, longitude, true);
    }

    // Create an extent from a list of points
    public MyExtent(List<Location> locations)
    {
        ReadLatLon(locations);
    }

    // Simple way of getting center, reasonably accurate for short distances
    public Location GetCenter()
    {
        var Latitude = (MaxLatitude + MinLatitude) / 2.0;
        var Longitude = (MaxLongitude + MinLongitude) / 2.0;

        return new Location(Latitude, Longitude);
    }

    // Get the maximum distance between two points in the extent
    public double GetDistance()
    {
        // https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.devices.sensors.location?view=net-maui-8.0
        Location start = new(MinLatitude, MinLongitude);
        Location end = new(MaxLatitude, MaxLongitude);

        return Location.CalculateDistance(start, end, DistanceUnits.Kilometers);
    }
}