using HotelBooking.Application.Services;
using HotelBooking.Unit.Tests;

namespace BookingManagement;

public class BookingServiceTests
{
    private BookingService _bookingService;

    [SetUp]
    public void Setup()
    {
        _bookingService = new BookingService();
    }

    [Test]
    public void CreateBooking_WithValidData_ShouldCreateBooking()
    {
        //Arrange
        var bookingRequest = new BookingRequest
        {
            CustomerId = 1,
            HotelId = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            CheckOutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
            RoomId = 1
        };
        //Act
        var result = _bookingService.CreateBooking(bookingRequest);
        //Assert
        Assert.That(result, Is.Not.Null);

        Assert.That(result.CustomerId, Is.EqualTo(bookingRequest.CustomerId));
        Assert.That(result.HotelId, Is.EqualTo(bookingRequest.HotelId));
        Assert.That(result.RoomId, Is.EqualTo(bookingRequest.RoomId));
        Assert.That(result.CheckInDate, Is.EqualTo(bookingRequest.CheckInDate));
        Assert.That(result.CheckOutDate, Is.EqualTo(bookingRequest.CheckOutDate));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void CreateBooking_WhenCheckOutIsBeforeCheckIn_ShouldThrowException(int daysDiff)
    {
        var checkInDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5));
        //Arrange
        var bookingRequest = new BookingRequest
        {
            CustomerId = 1,
            HotelId = 1,
            CheckInDate = checkInDate,
            CheckOutDate = checkInDate.AddDays(daysDiff),
            RoomId = 1
        };
        //Act && Assert
        var ex = Assert.Throws<ArgumentException>(() => _bookingService.CreateBooking(bookingRequest));
        Assert.That(ex.Message, Is.EqualTo(BookingErrorMessages.CheckOutBeforeCheckIn));
        //_bookingRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<Booking>()), Times.Never);
    }
}