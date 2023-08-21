using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;

namespace Figaros.Services.Concrete
{
    public class BookingManager : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<BookingDto>> Add(BookingPostDto bookingPostDto)
        {
            if (bookingPostDto != null)
            {
                var booking = _mapper.Map<Booking>(bookingPostDto);

                var addedBooking = await _unitOfWork.Bookings.AddAsync(booking);
                await _unitOfWork.SaveAsync();
                BookingDto bookingDto = _mapper.Map<BookingDto>(booking);
                return new DataResult<BookingDto>(ResultStatus.Success, "Rezervasiya uğurla əlavə edildi!", new BookingDto
                {
                    Booking = addedBooking,
                    ResultStatus = ResultStatus.Success,
                    Message = "Rezervasiya uğurla əlavə edildi!"
                });
            }

            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya əlavə edilmədi!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya əlavə edilmədi!"
            });
        }

        public async Task<IDataResult<BookingDto>> Delete(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(x => x.Id == bookingId);

            if (booking != null)
            {
                booking.IsDeleted = true;
                var deletedBooking = await _unitOfWork.Bookings.UpdateAsync(booking);
                await _unitOfWork.SaveAsync();

                return new DataResult<BookingDto>(ResultStatus.Success, "Rezervasiya uğurla silindi!", new BookingDto
                {
                    Booking = deletedBooking,
                    ResultStatus = ResultStatus.Success,
                    Message = "Rezervasiya uğurla silindi!"
                });
            }

            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya tapılmadı!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingDto>> Get(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(x => x.Id == bookingId);

            if (booking != null)
            {
                return new DataResult<BookingDto>(ResultStatus.Success, new BookingDto
                {
                    Booking = booking,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya tapılmadı!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingListDto>> GetAll()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();

            if (bookings.Count >= 0)
            {
                return new DataResult<BookingListDto>(ResultStatus.Success, new BookingListDto
                {
                    Bookings = bookings,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<BookingListDto>(ResultStatus.Error, "Rezervasiyalar tapılmadı!", new BookingListDto
            {
                Bookings = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiyalar tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingListDto>> GetAllByDeleted()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(x => x.IsDeleted);

            if (bookings.Count >= 0)
            {
                return new DataResult<BookingListDto>(ResultStatus.Success, new BookingListDto
                {
                    Bookings = bookings,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<BookingListDto>(ResultStatus.Error, "Rezervasiyalar tapılmadı!", new BookingListDto
            {
                Bookings = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiyalar tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingListDto>> GetAllByNonDeleted()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync(x => !x.IsDeleted);

            if (bookings.Count >= 0)
            {
                return new DataResult<BookingListDto>(ResultStatus.Success, new BookingListDto
                {
                    Bookings = bookings,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<BookingListDto>(ResultStatus.Error, "Rezervasiyalar tapılmadı!", new BookingListDto
            {
                Bookings = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiyalar tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingUpdateDto>> GetUpdateDto(int BookingId)
        {
            var result = await _unitOfWork.Bookings.AnyAsync(c => c.Id == BookingId);
            if (result)
            {
                var booking = await _unitOfWork.Bookings.GetAsync(c => c.Id == BookingId);
                BookingUpdateDto bookingUpdateDto = _mapper.Map<BookingUpdateDto>(booking);
                return new DataResult<BookingUpdateDto>(ResultStatus.Success, bookingUpdateDto);
            }
            else
            {
                return new DataResult<BookingUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<BookingDto>> HardDelete(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(x => x.Id == bookingId);

            if (booking != null)
            {
                var deletedBooking = await _unitOfWork.Bookings.DeleteAsync(booking);
                await _unitOfWork.SaveAsync();

                return new DataResult<BookingDto>(ResultStatus.Success, "Rezervasiya uğurla silindi!", new BookingDto
                {
                    Booking = deletedBooking,
                    ResultStatus = ResultStatus.Success,
                    Message = "Rezervasiya uğurla silindi!"
                });
            }

            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya tapılmadı!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingDto>> Restore(int bookingId)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(x => x.Id == bookingId);

            if (booking != null)
            {
                booking.IsDeleted = false;
                var restoredBooking = await _unitOfWork.Bookings.UpdateAsync(booking);
                await _unitOfWork.SaveAsync();

                return new DataResult<BookingDto>(ResultStatus.Success, "Rezervasiya uğurla geri qaytarıldı!", new BookingDto
                {
                    Booking = restoredBooking,
                    ResultStatus = ResultStatus.Success,
                    Message = "Rezervasiya uğurla geri qaytarıldı!"
                });
            }

            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya tapılmadı!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya tapılmadı!"
            });
        }

        public async Task<IDataResult<BookingDto>> Update(BookingUpdateDto bookingUpdateDto)
        {
            var booking = await _unitOfWork.Bookings.GetAsync(x => x.Id == bookingUpdateDto.Id);

            if (booking != null)
            {
                booking.EmployeeId = bookingUpdateDto.EmployeeId;
                booking.PriceId = bookingUpdateDto.PriceId;
                booking.TimeId = bookingUpdateDto.TimeId;
                booking.Fullname = bookingUpdateDto.Fullname;
                booking.Email = bookingUpdateDto.Email;
                booking.Phone = bookingUpdateDto.Phone;
                booking.Date = bookingUpdateDto.Date;
                booking.Message = bookingUpdateDto.Message;
                booking.IsActive = bookingUpdateDto.IsActive;
                booking.ModifiedDate = DateTime.Now;

                var updatedBooking = await _unitOfWork.Bookings.UpdateAsync(booking);
                await _unitOfWork.SaveAsync();

                return new DataResult<BookingDto>(ResultStatus.Success, "Rezervasiya uğurla yeniləndi!", new BookingDto
                {
                    Booking = updatedBooking,
                    ResultStatus = ResultStatus.Success,
                    Message = "Rezervasiya uğurla yeniləndi!"
                });
            }

            return new DataResult<BookingDto>(ResultStatus.Error, "Rezervasiya tapılmadı!", new BookingDto
            {
                Booking = null,
                ResultStatus = ResultStatus.Error,
                Message = "Rezervasiya tapılmadı!"
            });
        }
    }
}
