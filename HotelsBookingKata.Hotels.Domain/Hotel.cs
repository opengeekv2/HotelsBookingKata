namespace HotelsBookingKata.Hotels.Domain;

public class Hotel
{
    public string Id { get; }
    public string Name { get; }

    public Hotel(string id, string name)
    {
        Id = id;
        Name = name;
    }
}