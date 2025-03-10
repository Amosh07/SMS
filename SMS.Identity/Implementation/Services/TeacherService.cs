using SMS.Application.DTOs.Teacher;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Domain.Entities;

namespace SMS.Identity.Implementation.Services
{
    //public class TeacherService(IGenericRepository genericRepository) : ITeacherService
    //{
    //    public void ActivateDeactivateTeacher(Guid teacherId)
    //    {
    //            var teacherModel = genericRepository.GetById<Teacher>(teacherId)
    //             ?? throw new NotFoundException("The teacher was not found.");

    //            teacherModel.IsActive = !teacherModel.IsActive;

    //            genericRepository.Update(teacherModel);
    //        }

    //    public GetTeacherDetailDto GetTeacherDetails(Guid Id)
    //    {
    //        var teacherModel = genericRepository.GetById<Teacher>(Id)
    //             ?? throw new NotFoundException("The Teacher was not found.");

    //        var result = new GetTeacherDetailDto()
    //        {
    //            Name = teacherModel.Name,
    //            Address = teacherModel.Address,
    //            Email = teacherModel.Email,
    //            Number = teacherModel.Number
    //        };

    //        return result;

    //    }

        /*public void UpdateTeacherDetails(UpdateTeacherRequestDto teacher)
        {
            var teacherModel = genericRepository.GetById<Teacher>(teacher.Id)
                        ?? throw new NotFoundException("The Teacher was not found.");

            teacherModel.Name = teacher.Name;
            teacherModel.Number = teacher.PhoneNumber;
            teacherModel.Email = teacher.Email;
            teacherModel.Address = teacher.Address;
            genericRepository.Update(teacherModel);
        }*/
}
