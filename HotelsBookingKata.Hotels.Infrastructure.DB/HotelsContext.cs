using Microsoft.EntityFrameworkCore;

namespace HotelsBookingKata.Hotels.Infrastructure.DB;

public class HotelsContext(DbContextOptions<HotelsContext> options) : DbContext(options)
{

}
