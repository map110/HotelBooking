using BookingManagement;
using HotelBooking.Domain;

namespace HotelBooking.Application.Services;

public class BookingService
{
    public BookingService()
    {
    }

    public Booking CreateBooking(BookingRequest bookingRequest)
    {
        if (bookingRequest.CheckOutDate <= bookingRequest.CheckInDate)
        {
            throw new ArgumentException(BookingErrorMessages.CheckOutBeforeCheckIn);
        }
        return new Booking(
            bookingRequest.CustomerId,
            bookingRequest.HotelId,
            bookingRequest.RoomId,
            bookingRequest.CheckInDate,
            bookingRequest.CheckOutDate
        );
    }
}