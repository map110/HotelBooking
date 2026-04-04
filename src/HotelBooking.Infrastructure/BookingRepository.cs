using BookingManagement;
using HotelBooking.Domain;
using HotelBooking.Infrastructure.Contexts;

namespace HotelBooking.Infrastructure;

public class BookingRepository : IBookingRepository
{
    private readonly HotelBookingDbContext _context;

    public BookingRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public Task<Booking?> GetByIdAsync(int bookingId)
    {
        throw new NotImplementedException();
    }

    public bool IsRoomBooked(int roomId, DateOnly checkIn, DateOnly checkOut)
    {
        throw new NotImplementedException();
    }


    public void Save(Booking booking)
    {
        _context.Add(booking);
        _context.SaveChanges();
    }
}