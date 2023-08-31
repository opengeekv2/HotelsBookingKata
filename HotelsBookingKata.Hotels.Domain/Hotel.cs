namespace HotelsBookingKata.Hotels.Domain;

public class Hotel
{
    private readonly string _id;
    private readonly string _name;

    public Hotel(string id, string name)
    {
        _id = id;
        _name = name;
    }
}