using SMS.Application.DTOs.Student;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    public class StudentService(IGenericRepository genericRepository) : IStudentService
    {
        public void ActivateDeactivateStudent(Guid studentId)
        {
                var studentModel = genericRepository.GetById<Student>(studentId)
                 ?? throw new NotFoundException("The student was not found.");

                studentModel.IsActive = !studentModel.IsActive;

                genericRepository.Update(studentModel);
            }

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

        public void UpdateStudentDetails(UpdateStudentRequestDto student)
        {
            var studentModel = genericRepository.GetById<Student>(student.Id)
                        ?? throw new NotFoundException("The Subject was not found.");

            studentModel.Name = student.Name;

            genericRepository.Update(studentModel);
        }
    }
}