using SMS.Application.DTOs.Student;
using SMS.Application.DTOs.Teacher;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class StudentService(IGenericRepository genericRepository) : IStudentService
    {
        public GetStudentDetailDto GetStudentDetails(Guid Id)
        {
            var studentModel = genericRepository.GetById<Student>(Id)
                ?? throw new NotFoundException("The Student was not found.");

            var result = new GetStudentDetailDto()
            {
                Name = studentModel.Name,
                Address = studentModel.Address,
                Number = studentModel.Number
            };

            return result;
        }

        public void InsertStudent(InsertStudentDto insertStudentDto)
        {
            throw new NotImplementedException();
        }
    }
}