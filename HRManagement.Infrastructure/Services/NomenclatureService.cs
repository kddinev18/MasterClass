﻿using HRManagement.DAL.Data.Entities;
using HRManagement.DAL.Repositories.Base;
using HRManagement.Domain.DTO.Common;
using HRManagement.Infrastructure.Contracts;

namespace HRManagement.Infrastructure.Services
{
    public class NomenclatureService : BaseService, INomenclatureService
    {
        private readonly BaseRepository<Job> _jobRepository;
        private readonly BaseRepository<Department> _departmentRepository;
        private readonly BaseRepository<Employee> _employeeRepository;
        public NomenclatureService(UnitOfWork unitOfWork, ICurrentUserService currentUserService) : base(currentUserService)
        {
            _jobRepository = unitOfWork.GetRepository<Job>(true);
            _departmentRepository = unitOfWork.GetRepository<Department>(true);
            _employeeRepository = unitOfWork.GetRepository<Employee>(true);
        }

        public IQueryable<NomenclatureDTO<int>> GetJobs()
        {
            return _jobRepository.GetAll()
                .Select(x =>
                    new NomenclatureDTO<int>()
                    {
                        Id = x.Id,
                        Code = x.Title.ToUpper(),
                        Value = x.Title
                    }
                );
        }

        public IQueryable<NomenclatureDTO<int>> GetDepartments()
        {
            return _departmentRepository.GetAll()
                .Select(x =>
                    new NomenclatureDTO<int>()
                    {
                        Id = x.Id,
                        Code = x.Name.ToUpper(),
                        Value = x.Name
                    }
                );
        }

        public IQueryable<NomenclatureDTO<int>> GetManagers()
        {
            return _employeeRepository.GetAll()
                .Select(x =>
                    new NomenclatureDTO<int>()
                    {
                        Id = x.Id,
                        Code = x.Email.ToUpper(),
                        Value = x.FirstName + " " + x.LastName
                    }
                );
        }
    }
}
