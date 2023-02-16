using EmployeeAppCore.Core.IRepository;
using EmployeeAppCore.Core.IServices;
using EmployeeAppCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAppCore.Services.Services
{
    public class EmployeeSevices : IServices
    {
        private IRepository _repository;
        public EmployeeSevices(IRepository repository)
        {
            _repository = repository;
        }
        public LogIn_Details LoginDetails(LogIn_Details userName)
        {
            return _repository.LoginDetails(userName);
        }
        public List<WorkerLocation> GetLocations()
        {
            return _repository.GetLocations();
        }
        public List<LogIn_Details> GetUser()
        {
            return _repository.GetUser();
        }
        public void AddandEditEmpolyeeDetails(EmployeeDetails Empdata)
        {
            _repository.AddandEditEmpolyeeDetails(Empdata);
        }
        public List<EmployeeDetails> ListEmpolyeeDetails()
        {
            return _repository.ListEmpolyeeDetails();
        }
        public EmployeeDetails EnterDataInEdit(int EmpId)
        {
            return _repository.EnterDataInEdit(EmpId);
        }
        public EmployeeDetails DeleteEmpolyeeDetails(int EmpId)
        {
            return _repository.DeleteEmpolyeeDetails(EmpId);
        }
        public List<EmployeeDetails> ListDetails(int userId)
        {
            return _repository.ListDetails(userId);
        }
        public EmployeeDetails UpdateOption(int EmpId)
        {
            return _repository.UpdateOption(EmpId);
        }
        public List<EmployeeDetails> ViewInfo(int EmpId)
        {
            return _repository.ViewInfo(EmpId);
        }


    }
}
