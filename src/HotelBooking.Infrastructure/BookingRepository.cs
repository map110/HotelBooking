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

    public async Task<bool> IsRoomBookedAsync(int roomId, DateOnly checkIn, DateOnly checkOut)
    {
        
        return await _context.Bookings.AnyAsync(b =>
            b.RoomId == roomId &&
            (b.CheckInDate >= checkIn && b.CheckOutDate >= checkIn) ||
            (b.CheckInDate <= checkOut && b.CheckOutDate <= checkOut));
    }

    public async Task SaveAsync(Booking booking)
    {
        await _context.AddAsync(booking);
        await _context.SaveChangesAsync();
    }
}