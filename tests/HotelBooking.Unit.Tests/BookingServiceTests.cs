using HotelBooking.Application.Services;
using HotelBooking.Unit.Tests;

namespace BookingManagement;

public class BookingServiceTests
{
    [Test]
    public void CreateBooking_WithValidData_ShouldCreateBooking()
    {
        //Arrange
        var bookingService = new BookingService();
        var bookingRequest = new BookingRequest
        {
            CustomerId = 1,
            HotelId = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            CheckOutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            RoomId = 1
        }; 
        //Act
        var result = bookingService.CreateBooking(bookingRequest);
        //Assert
        Assert.That(result,Is.Not.Null);

        Assert.That(result.CustomerId,Is.EqualTo(bookingRequest.CustomerId));
        Assert.That(result.HotelId,Is.EqualTo(bookingRequest.HotelId));
        Assert.That(result.RoomId,Is.EqualTo(bookingRequest.RoomId));
        Assert.That(result.CheckInDate,Is.EqualTo(bookingRequest.CheckInDate));
        Assert.That(result.CheckOutDate,Is.EqualTo(bookingRequest.CheckOutDate));
    }
}