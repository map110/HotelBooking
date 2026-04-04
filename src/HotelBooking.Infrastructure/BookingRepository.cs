using BookingManagement;
using HotelBooking.Domain;
using HotelBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

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
        return _context.Bookings.FirstOrDefaultAsync(p => p.Id == bookingId);
    }

    public bool IsRoomBooked(int roomId, DateOnly checkIn, DateOnly checkOut)
    {
        throw new NotImplementedException();
    }


    public async Task SaveAsync(Booking booking)
    {
        await _context.AddAsync(booking);
        await _context.SaveChangesAsync();
    }
}