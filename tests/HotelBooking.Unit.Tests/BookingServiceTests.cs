using HotelBooking.Application.Services;
using HotelBooking.Domain;
using HotelBooking.Unit.Tests;
using Moq;

namespace BookingManagement;

public class BookingServiceTests
{
    private Mock<IBookingRepository> _bookingRepositoryMock;
    private BookingService _bookingService;

    [SetUp]
    public void Setup()
    {
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _bookingService = new BookingService(_bookingRepositoryMock.Object);
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
        _bookingRepositoryMock.Verify(repo => repo.Save(It.IsAny<Booking>()), Times.Never);
    }

    [Test]
    public void CreateBooking_withValidData_ShouldSaveTheBooking()
    {
        // Arrange
        var bookingRequest = new BookingRequest
        {
            CustomerId = 1,
            RoomId = 101,
            HotelId = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Today),
            CheckOutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(2))
        };
        // Act
        var booking = _bookingService.CreateBooking(bookingRequest);

        // Assert
        _bookingRepositoryMock.Verify(repo => repo.Save(It.Is<Booking>(b =>
            b.CustomerId == bookingRequest.CustomerId &&
            b.HotelId == bookingRequest.HotelId &&
            b.RoomId == bookingRequest.RoomId &&
            b.CheckInDate == bookingRequest.CheckInDate &&
            b.CheckOutDate == bookingRequest.CheckOutDate
        )), Times.Once);
    }

    [Test]
    public void CreateBooking_WhenRoomIsAlreadyBooked_ShouldThrowException()
    {
        // Arrange
        var bookingRequest = new BookingRequest
        {
            CustomerId = 1,
            HotelId = 1,
            RoomId = 1,
            CheckInDate = DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            CheckOutDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5))
        };
        _bookingRepositoryMock
            .Setup(repo => repo.IsRoomBooked(bookingRequest.RoomId, bookingRequest.CheckInDate,
                bookingRequest.CheckOutDate)).Returns(true);
        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() =>
            _bookingService.CreateBooking(bookingRequest));
        Assert.That(ex.Message, Is.EqualTo(BookingErrorMessages.RoomAlreadyBooked));
        _bookingRepositoryMock.Verify(repo => repo.Save(It.IsAny<Booking>()), Times.Never);
    }
}