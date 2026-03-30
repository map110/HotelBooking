using HotelBooking.Domain;
using HotelBooking.Unit.Tests;

namespace HotelBooking.Application.Services;

public class BookingService
{
    public BookingService()
    {
    }

    public Booking CreateBooking(BookingRequest bookingRequest)
    {
        return new Booking(
            bookingRequest.CustomerId,
            bookingRequest.HotelId,
            bookingRequest.RoomId,
            bookingRequest.CheckInDate,
            bookingRequest.CheckOutDate
        );
    }
}