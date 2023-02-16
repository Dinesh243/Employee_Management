using EmployeeAppCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAppCore.Core.IServices
{
    public interface IServices
    {
        LogIn_Details LoginDetails(LogIn_Details userName);
        List<WorkerLocation> GetLocations();
        void AddandEditEmpolyeeDetails(EmployeeDetails Empdata);
        List<EmployeeDetails> ListEmpolyeeDetails();
        EmployeeDetails EnterDataInEdit(int EmpId);
        EmployeeDetails DeleteEmpolyeeDetails(int EmpId);
        List<EmployeeDetails> ListDetails(int userId);
        EmployeeDetails UpdateOption(int EmpId);
        List<EmployeeDetails> ViewInfo(int EmpId);
        List<LogIn_Details> GetUser();

    }
}
