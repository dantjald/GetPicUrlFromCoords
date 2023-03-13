using System.Text.Json;

string api_key = "AIzaSyCvnx9gwPUxl6mKIl-8S_YDZYR974Oxab8";


string placeName = "Aalborg Dinosaurs Disc Golf";

string location = "57.02816615872709, 9.989194227228772";

string photoUrl = GetPhotoUrl(placeName, location);

Console.WriteLine(photoUrl);

string GetPhotoUrl(string placeName, string location)
{
    string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
    string parameters = "&key=" + api_key +
                        "&keyword=" + placeName +
                        "&location=" + location +
                        "&radius=500";

    string idCall = url + parameters;

    var client = new HttpClient();
    
    var result = client.GetAsync(idCall).Result;

    var jsonString = result.Content.ReadAsStringAsync().Result;
    var placeResult = JsonSerializer.Deserialize<PlaceDetails>(jsonString);
    var photoRef = placeResult.results[0].photos[0].photo_reference;


    string photoUrl = "https://maps.googleapis.com/maps/api/place/photo?";

    string photoParameters = "&key=" + api_key + "&photo_reference=" + photoRef + "&maxwidth=3000";

    return photoUrl + photoParameters;

}


public class PlaceDetails
{
    public Result[] results { get; set; }
}

public class Result
{
    public string name { get; set; }
    public Photo[] photos { get; set; }
    public string place_id { get; set; }
    public string[] types { get; set; }
    public string vicinity { get; set; }
}

public class Geometry
{
    public Location location { get; set; }
}

public class Location
{
    public float lat { get; set; }
    public float lng { get; set; }
}

public class Photo
{
    public int height { get; set; }
    public string[] html_attributions { get; set; }
    public string photo_reference { get; set; }
    public int width { get; set; }
}
