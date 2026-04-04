using HotelBooking.Domain;
using HotelBooking.Infrastructure;
using HotelBooking.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookingManagement;

public class BookingRepositoryTests
{
    [Test]
    public async Task Save_Booking_ShouldSaveToDatabase()
    {
        // Arrange
        var booking = new Booking(
            customerId: 1,
            roomId: 101,
            hotelId: 1,
            checkInDate: DateOnly.FromDateTime(DateTime.Today),
            checkOutDate: DateOnly.FromDateTime(DateTime.Today.AddDays(2))
        );

        // Act
        var options = new DbContextOptionsBuilder<HotelBookingDbContext>()
            .UseInMemoryDatabase(databaseName: $"HotelBookingTestDb_{Guid.NewGuid()}")
            .Options;

        var context = new HotelBookingDbContext(options);
        var bookingRepository = new BookingRepository(context);
        await bookingRepository.SaveAsync(booking);


        // Assert
        var savedBooking = context.Bookings.Find(booking.Id);
        Assert.That(savedBooking, Is.Not.Null);
        Assert.That(savedBooking.CustomerId, Is.EqualTo(booking.CustomerId));
        Assert.That(savedBooking.HotelId, Is.EqualTo(booking.HotelId));
        Assert.That(savedBooking.RoomId, Is.EqualTo(booking.RoomId));
        Assert.That(savedBooking.CheckInDate, Is.EqualTo(booking.CheckInDate));
        Assert.That(savedBooking.CheckOutDate, Is.EqualTo(booking.CheckOutDate));
    }
}