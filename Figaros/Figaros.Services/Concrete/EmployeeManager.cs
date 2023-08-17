using AutoMapper;
using Figaros.Data.Abstract;
using Figaros.Entities.Concrete;
using Figaros.Entities.DTOs.AboutDtos;
using Figaros.Entities.DTOs.BookingDtos;
using Figaros.Entities.DTOs.EmployeeDtos;
using Figaros.Services.Abstract;
using Figaros.Shared.Extentions;
using Figaros.Shared.Utilities.Results.Abstract;
using Figaros.Shared.Utilities.Results.ComplexTypes;
using Figaros.Shared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace Figaros.Services.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;
        }

        public async Task<IDataResult<EmployeeDto>> Add(EmployeePostDto employeePostDto)
        {
            if (employeePostDto != null)
            {
                var employee = _mapper.Map<Employee>(employeePostDto);

                if (employeePostDto.ImageFile != null)
                {
                    if (!employeePostDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<EmployeeDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new EmployeeDto
                        {
                            Employee = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!employeePostDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<EmployeeDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new EmployeeDto
                        {
                            Employee = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = employeePostDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Employees");
                    employee.ImageString = newImage;
                }
                else
                {
                    return new DataResult<EmployeeDto>(ResultStatus.Error, "Şəkil daxil edin!", new EmployeeDto
                    {
                        Employee = null,
                        ResultStatus = ResultStatus.Error,
                        Message = "Şəkil daxil edin!"
                    });
                }


                var addedEmployee = await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.SaveAsync();
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);

                return new DataResult<EmployeeDto>(ResultStatus.Success, "Əməkdaş uğurla əlavə olundu!", new EmployeeDto
                {
                    Employee = addedEmployee,
                    ResultStatus = ResultStatus.Success,
                    Message = "Əməkdaş uğurla əlavə olundu!"
                });
            }

            return new DataResult<EmployeeDto>(ResultStatus.Error, "Əməkdaş əlavə olunmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaş əlavə olunmadı!"
            });
        }

        public async Task<IDataResult<EmployeeDto>> Delete(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetAsync(x => x.Id == employeeId);

            if (employee != null)
            {
                employee.IsDeleted = true;
                var deletedEmployee = await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveAsync();

                return new DataResult<EmployeeDto>(ResultStatus.Success, "Əməkdaş uğurla silindi!", new EmployeeDto
                {
                    Employee = deletedEmployee,
                    ResultStatus = ResultStatus.Success,
                    Message = "Əməkdaş uğurla silindi!"
                });
            }

            return new DataResult<EmployeeDto>(ResultStatus.Error, "Əməkdaş tapılmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaş tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeDto>> Get(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetAsync(x => x.Id == employeeId);

            if (employee != null)
            {
                return new DataResult<EmployeeDto>(ResultStatus.Success, new EmployeeDto
                {
                    Employee = employee,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<EmployeeDto>(ResultStatus.Error, "Əməkdaş tapılmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaş tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeListDto>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();

            if (employees.Count >= 0)
            {
                return new DataResult<EmployeeListDto>(ResultStatus.Success, new EmployeeListDto
                {
                    Employees = employees,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<EmployeeListDto>(ResultStatus.Error, "Əməkdaşlar tapılmadı!", new EmployeeListDto
            {
                Employees = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaşlar tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeListDto>> GetAllByDeleted()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(x => x.IsDeleted);

            if (employees.Count >= 0)
            {
                return new DataResult<EmployeeListDto>(ResultStatus.Success, new EmployeeListDto
                {
                    Employees = employees,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<EmployeeListDto>(ResultStatus.Error, "Əməkdaşlar tapılmadı!", new EmployeeListDto
            {
                Employees = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaşlar tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeListDto>> GetAllByNonDeleted()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(x => !x.IsDeleted);

            if (employees.Count >= 0)
            {
                return new DataResult<EmployeeListDto>(ResultStatus.Success, new EmployeeListDto
                {
                    Employees = employees,
                    ResultStatus = ResultStatus.Success
                });
            }

            return new DataResult<EmployeeListDto>(ResultStatus.Error, "Əməkdaşlar tapılmadı!", new EmployeeListDto
            {
                Employees = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaşlar tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeUpdateDto>> GetUpdateDto(int EmployeeId)
        {
            var result = await _unitOfWork.Employees.AnyAsync(c => c.Id == EmployeeId);
            if (result)
            {
                var booking = await _unitOfWork.Employees.GetAsync(c => c.Id == EmployeeId);
                EmployeeUpdateDto bookingUpdateDto = _mapper.Map<EmployeeUpdateDto>(booking);
                return new DataResult<EmployeeUpdateDto>(ResultStatus.Success, bookingUpdateDto);
            }
            else
            {
                return new DataResult<EmployeeUpdateDto>(ResultStatus.Error, "Nəticə tapılmadı!", null);
            }
        }

        public async Task<IDataResult<EmployeeDto>> HardDelete(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetAsync(x => x.Id == employeeId);

            if (employee != null)
            {
                var deletedEmployee = await _unitOfWork.Employees.DeleteAsync(employee);
                employee.ImageString.DeleteImage(_env.WebRootPath, "uploads/Employees");
                await _unitOfWork.SaveAsync();

                return new DataResult<EmployeeDto>(ResultStatus.Success, "Əməkdaş uğurla silindi!", new EmployeeDto
                {
                    Employee = deletedEmployee,
                    ResultStatus = ResultStatus.Success,
                    Message = "Əməkdaş uğurla silindi!"
                });
            }

            return new DataResult<EmployeeDto>(ResultStatus.Error, "Əməkdaş tapılmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaş tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeDto>> Restore(int employeeId)
        {
            var employee = await _unitOfWork.Employees.GetAsync(x => x.Id == employeeId);

            if (employee != null)
            {
                employee.IsDeleted = false;
                var restoredEmployee = await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveAsync();

                return new DataResult<EmployeeDto>(ResultStatus.Success, "Əməkdaş uğurla yeniləndi!", new EmployeeDto
                {
                    Employee = restoredEmployee,
                    ResultStatus = ResultStatus.Success,
                    Message = "Əməkdaş uğurla yeniləndi!"
                });
            }

            return new DataResult<EmployeeDto>(ResultStatus.Error, "Əməkdaş tapılmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Əməkdaş tapılmadı!"
            });
        }

        public async Task<IDataResult<EmployeeDto>> Update(EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = await _unitOfWork.Employees.GetAsync(x => x.Id == employeeUpdateDto.Id);

            if (employee != null)
            {
                if (employeeUpdateDto.ImageFile != null)
                {
                    if (!employeeUpdateDto.ImageFile.IsImageContent())
                    {
                        return new DataResult<EmployeeDto>(ResultStatus.Error, "Şəkil formatı daxil edin!", new EmployeeDto
                        {
                            Employee = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkil formatı daxil edin!"
                        });
                    }

                    if (!employeeUpdateDto.ImageFile.IsValidImageLength())
                    {
                        return new DataResult<EmployeeDto>(ResultStatus.Error, "Şəkilin həcmi böyükdür!", new EmployeeDto
                        {
                            Employee = null,
                            ResultStatus = ResultStatus.Error,
                            Message = "Şəkilin həcmi böyükdür!"
                        });
                    }

                    string newImage = employeeUpdateDto.ImageFile.SaveImage(_env.WebRootPath, "uploads/Employees");
                    employee.ImageString.DeleteImage(_env.WebRootPath, "uploads/Employees");

                    employee.ImageString = newImage;
                }

                employee.ProfessionId = employeeUpdateDto.ProfessionId;
                employee.FullName = employeeUpdateDto.FullName;
                employee.Description = employeeUpdateDto.Description;
                employee.InstagramUrl = employeeUpdateDto.InstagramUrl;
                employee.TiktokUrl = employeeUpdateDto.TiktokUrl;
                employee.FacebookUrl = employeeUpdateDto.FacebookUrl;
                employee.WhatsAppUrl = employeeUpdateDto.WhatsAppUrl;
                employee.ModifiedDate = DateTime.Now;

                var updatedEmployee = await _unitOfWork.Employees.UpdateAsync(employee);
                await _unitOfWork.SaveAsync();

                return new DataResult<EmployeeDto>(ResultStatus.Success, "Əməkdaş uğurla yeniləndi!", new EmployeeDto
                {
                    Employee = updatedEmployee,
                    ResultStatus = ResultStatus.Error,
                    Message = "Əməkdaş uğurla yeniləndi!"
                });
            }

            return new DataResult<EmployeeDto>(ResultStatus.Error, "Məlumatlar tapılmadı!", new EmployeeDto
            {
                Employee = null,
                ResultStatus = ResultStatus.Error,
                Message = "Məlumatlar tapılmadı!"
            });
        }
    }
}
