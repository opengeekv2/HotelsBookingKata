namespace HotelsBookingKata.Hotel.Domain;

public class Hotel
{
    public string Id { get; }
    public string Name { get; }
    public IList<Room> Rooms { get; set; }

    public Hotel(string id, string name)
    {
        Id = id;
        Name = name;
        Rooms = new List<Room>();
    }
}

public class Room
{
    public int Number { get; }
    public string Type { get; }

    public Room(int number, string type)
    {
        Number = number;
        Type = type;
    }
}
