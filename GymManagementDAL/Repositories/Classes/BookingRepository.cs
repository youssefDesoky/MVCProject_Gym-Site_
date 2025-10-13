using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class BookingRepository : MainCRUDRepository<Booking>, IBookingRepository
{
    public BookingRepository(GymContext context) : base(context) {}
}
