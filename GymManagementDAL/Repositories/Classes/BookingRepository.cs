using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class BookingRepository : IBookingRepository
{
    private readonly GymContext _context;

    public BookingRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Booking entity)
    {
        _context.Bookings.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var booking = _context.Bookings.Find(id);
        if (booking is null)
            return 0;

        _context.Bookings.Remove(booking);
        return _context.SaveChanges();
    }

    public int Update(Booking entity)
    {
        _context.Bookings.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Booking> GetAll() => _context.Bookings.ToList();

    public Booking? GetById(int id) => _context.Bookings.Find(id);
}
