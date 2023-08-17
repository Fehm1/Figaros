using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Shared.Utilities.Results.Abstract;

namespace Figaros.Services.Abstract
{
    public interface IBookingService
    {
        Task<IDataResult<BookingDto>> Get(int bookingId);
        Task<IDataResult<BookingUpdateDto>> GetUpdateDto(int BookingId);
        Task<IDataResult<BookingListDto>> GetAll();
        Task<IDataResult<BookingListDto>> GetAllByNonDeleted();
        Task<IDataResult<BookingListDto>> GetAllByDeleted();
        Task<IDataResult<BookingDto>> Add(BookingPostDto bookingPostDto);
        Task<IDataResult<BookingDto>> Update(BookingUpdateDto bookingUpdateDto);
        Task<IDataResult<BookingDto>> Restore(int bookingId);
        Task<IDataResult<BookingDto>> Delete(int bookingId);
        Task<IDataResult<BookingDto>> HardDelete(int bookingId);
    }
}
