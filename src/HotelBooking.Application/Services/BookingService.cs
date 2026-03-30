using BookingManagement;
using HotelBooking.Domain;

namespace HotelBooking.Application.Services;

public class BookingService(IBookingRepository bookingRepository)
{
    // private readonly IBookingRepository _bookingRepository;
    // public BookingService(IBookingRepository bookingRepository)
    // {
    //     _bookingRepository = bookingRepository;
    // }

    public Booking CreateBooking(BookingRequest bookingRequest)
    {
        if (bookingRequest.CheckOutDate <= bookingRequest.CheckInDate)
        {
            throw new ArgumentException(BookingErrorMessages.CheckOutBeforeCheckIn);
        }

        if (bookingRepository.IsRoomBooked(bookingRequest.RoomId, bookingRequest.CheckInDate,
                bookingRequest.CheckOutDate))
        {
            throw new InvalidOperationException(BookingErrorMessages.RoomAlreadyBooked);
        }

        var booking = new Booking(bookingRequest.CustomerId,
            bookingRequest.HotelId,
            bookingRequest.RoomId,
            bookingRequest.CheckInDate,
            bookingRequest.CheckOutDate);
        bookingRepository.Save(booking);
        return booking;
    }
}