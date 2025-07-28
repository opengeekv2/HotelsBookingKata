using HotelsBookingKata.Hotel.Domain;

namespace HotelsBookingKata.Hotels.Infrastructure.DB;

class HotelRepository(HotelsContext context) : IHotelRepository
{
    public void Add(Hotel.Domain.Hotel hotel)
    {
        throw new NotImplementedException();
    }

    public Hotel.Domain.Hotel? Get(string id)
    {
        throw new NotImplementedException();
    }

    public void Save(Hotel.Domain.Hotel hotel)
    {
        throw new NotImplementedException();
    }
}