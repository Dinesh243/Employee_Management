using EmployeeAppCore.Core.IRepository;
using EmployeeAppCore.Core.Models;
using EmployeeAppCore.Entity.Employee_Details.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeAppCore.Resources.Repository
{

    public class EmpolyeeRepository : IRepository
    {
        #region Employee And Admin Login
        public LogIn_Details LoginDetails(LogIn_Details userName)
        {
            var login = new LogIn_Details();
            if (userName != null)
            {
                using (var Entity = new Employee_ManagementContext())
                {
                    var Dbdata = Entity.Employee_LogIn.Where(x => x.Username == userName.UserName && x.Password== userName.Password && !x.Is_Deleted).Select(x=>x).SingleOrDefault();

                    if (Dbdata != null)
                    {
                        //if (Dbdata.Password == userName.Password)
                        {
                            login.UserId = Dbdata.User_Id;
                            login.IsAdminUser = Dbdata.Is_Admin_User;
                            //return userName;
                        }
                    }
                    //else
                    //{
                    //    if (Dbdata.Password == userName.Password)
                    //    {
                    //        userName.UserId = Dbdata.User_Id;
                    //        userName.IsAdminUser = Dbdata.Is_Admin_User;
                    //        return userName;
                    //    }
                    //}
                }
            }
            return login;
        }
        #endregion

        #region Update Data to DropDown 
        public List<WorkerLocation> GetLocations()
        {
            List<WorkerLocation> data = new List<WorkerLocation>();
            using (var Entity = new Employee_ManagementContext())
            {
                data = (from b in Entity.Employee_Location
                        where !b.Is_Deleted
                        select new WorkerLocation
                        {
                            Location_Id = b.Location_Id,
                            Location = b.Location
                        }).ToList();
            }
            return data;
        }
        

        public List<LogIn_Details> GetUser()
        {
            List<LogIn_Details> data = new List<LogIn_Details>();
            using (var Entity = new Employee_ManagementContext())
            {
                data = (from b in Entity.Employee_LogIn
                        where !b.Is_Deleted
                        select new LogIn_Details
                        {
                            UserId = b.User_Id,
                            UserName = b.Username
                        }).ToList();
            }
            return data;
        }
        #endregion

        #region Save And Update Empolyee Details
        public void AddandEditEmpolyeeDetails(EmployeeDetails Empdata)
        {
            try
            {
                if (Empdata != null)
                {
                    using (var Entity = new Employee_ManagementContext())
                    {
                        Employee_Details AddDetail = null;
                        bool isRecordExist = false;
                        AddDetail = Entity.Employee_Details.Where(x => x.Worker_Id == Empdata.WorkerId && !x.Is_Deleted).SingleOrDefault();
                        if (AddDetail != null)
                        {
                            isRecordExist = true;
                        }
                        else
                        {
                            AddDetail = new Employee_Details();
                        }

                        AddDetail.User_Id = Empdata.UserId;
                        AddDetail.First_Name = Empdata.FirstName;
                        AddDetail.Second_Name = Empdata.SecondName;
                        AddDetail.Date_Of_Joining = Empdata.DateOfJoining;
                        AddDetail.Age = Empdata.Age;
                        AddDetail.Experience = Empdata.Experience;
                        AddDetail.Contact_Number = Empdata.ContactNumber;
                        AddDetail.Adress = Empdata.Adress;
                        AddDetail.Location_Id = Empdata.LocationId;
                        AddDetail.Updated_Time_Stamp = DateTime.Now;
                        if (!isRecordExist)
                        {
                            AddDetail.Created_Time_Stamp = DateTime.Now;
                            Entity.Employee_Details.Add(AddDetail);
                        }
                        Entity.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region List Empolyee Details
        public List<EmployeeDetails> ListEmpolyeeDetails()
        {
            List<EmployeeDetails> ListData = new List<EmployeeDetails>();
            using (var Entity = new Employee_ManagementContext())
            {
                var DBdata = Entity.Employee_Details.Where(x => !x.Is_Deleted).ToList();
                if (DBdata.Count > 0)
                {
                    foreach (var item in DBdata)
                    {
                        EmployeeDetails Empdata = new EmployeeDetails();
                        Empdata.UserId = item.User_Id;
                        Empdata.WorkerId = item.Worker_Id;
                        Empdata.FirstName = item.First_Name;
                        Empdata.SecondName = item.Second_Name;
                        Empdata.DateOfJoining = item.Date_Of_Joining.Date;
                        Empdata.Age = item.Age;
                        Empdata.Experience = item.Experience;
                        Empdata.ContactNumber = item.Contact_Number;
                        Empdata.Adress = item.Adress;
                        Empdata.LocationId = item.Location_Id;
                        if (Empdata.LocationId > 0)
                        {
                            Empdata.Location = GetLoctionName(Empdata.LocationId);
                        }
                        ListData.Add(Empdata);
                    }
                }
            }
            return ListData.ToList();
        }

        public List<EmployeeDetails> ListDetails(int userId)
        {
            List<EmployeeDetails> ListData = new List<EmployeeDetails>();
            using (var Entity = new Employee_ManagementContext())
            {
                var DBdata = Entity.Employee_Details.Where(x => x.User_Id == userId && !x.Is_Deleted).ToList();

                if (DBdata.Count > 0 && userId > 0)
                {
                    foreach (var item in DBdata)
                    {
                        EmployeeDetails Empdata = new EmployeeDetails();
                        Empdata.WorkerId = item.Worker_Id;
                        Empdata.FirstName = item.First_Name;
                        Empdata.SecondName = item.Second_Name;
                        Empdata.DateOfJoining = item.Date_Of_Joining.Date;
                        Empdata.Age = item.Age;
                        Empdata.Experience = item.Experience;
                        Empdata.ContactNumber = item.Contact_Number;
                        Empdata.Adress = item.Adress;
                        Empdata.LocationId = item.Location_Id;
                        Empdata.EnableEditButton = item.Enable_Edit_Button;
                        if (Empdata.LocationId > 0)
                        {
                            Empdata.Location = GetLoctionName(Empdata.LocationId);
                        }
                        ListData.Add(Empdata);
                    }
                }
            }
            return ListData.ToList();
        }
        public string GetLoctionName(int LoctionId)
        {
            string Loction = string.Empty;
            if (LoctionId > 0)
            {
                using (var Entity = new Employee_ManagementContext())
                {
                    var workerdata = Entity.Employee_Location.Where(x => x.Location_Id == LoctionId && !x.Is_Deleted).SingleOrDefault();
                    if (workerdata != null)
                    {
                        Loction = workerdata.Location;
                    }
                }
            }
            return Loction;
        }
        #endregion

        #region Given data for Edit 
        public EmployeeDetails EnterDataInEdit(int EmpId)
        {
            EmployeeDetails Data = new EmployeeDetails();
            if (EmpId > 0)
            {
                using (var Entity = new Employee_ManagementContext())
                {
                    var DBdata = Entity.Employee_Details.Where(x => x.Worker_Id == EmpId && !x.Is_Deleted).SingleOrDefault();
                    if (DBdata != null)
                    {
                        Data.UserId = DBdata.User_Id;
                        Data.WorkerId = DBdata.Worker_Id;
                        Data.FirstName = DBdata.First_Name;
                        Data.SecondName = DBdata.Second_Name;
                        Data.DateOfJoining = DBdata.Date_Of_Joining.Date;
                        Data.Age = DBdata.Age;
                        Data.Experience = DBdata.Experience;
                        Data.Adress = DBdata.Adress;
                        Data.ContactNumber = DBdata.Contact_Number;
                        Data.LocationId = DBdata.Location_Id;
                    }
                }
            }
            return Data;
        }
        #endregion

        #region Delete Worker Details
        public EmployeeDetails DeleteEmpolyeeDetails(int EmpId)
        {
            EmployeeDetails EmpData = new EmployeeDetails();
            if (EmpId > 0)
            {
                using (var Entity = new Employee_ManagementContext())
                {
                    var DBdata = Entity.Employee_Details.Where(x => x.Worker_Id == EmpId && !x.Is_Deleted).SingleOrDefault();
                    if (DBdata != null)
                    {
                        EmpData.WorkerId = DBdata.Worker_Id;
                        DBdata.Is_Deleted = true;
                        Entity.SaveChanges();
                    }
                }
            }
            return EmpData;
        }
        #endregion

        #region Flag for Edit button
        public EmployeeDetails UpdateOption(int EmpId)
        {
            EmployeeDetails EmpData = new EmployeeDetails();
            if (EmpId > 0)
            {
                using (var Entity = new Employee_ManagementContext())
                {
                    var DBdata = Entity.Employee_Details.Where(x => x.Worker_Id == EmpId && !x.Is_Deleted).SingleOrDefault();
                    if (DBdata != null)
                    {
                        EmpData.UserId = DBdata.User_Id;
                        EmpData.WorkerId = DBdata.Worker_Id;
                        DBdata.Enable_Edit_Button = true;
                        EmpData.EnableEditButton = DBdata.Enable_Edit_Button;
                        Entity.SaveChanges();
                        return EmpData;
                    }
                }
            }
            return EmpData;
        }
        #endregion

        #region Details for Partial View
        public List<EmployeeDetails> ViewInfo(int EmpId)
        {
            List<EmployeeDetails> ListData = new List<EmployeeDetails>();
            using (var Entity = new Employee_ManagementContext())
            {
                var DBdata = Entity.Employee_Details.Where(x => x.Worker_Id == EmpId && !x.Is_Deleted).ToList();

                if (DBdata.Count > 0 && EmpId > 0)
                {
                    foreach (var item in DBdata)
                    {
                        EmployeeDetails Empdata = new EmployeeDetails();
                        Empdata.WorkerId = item.Worker_Id;
                        Empdata.FirstName = item.First_Name;
                        Empdata.SecondName = item.Second_Name;
                        Empdata.DateOfJoining = item.Date_Of_Joining;
                        Empdata.Age = item.Age;
                        Empdata.Experience = item.Experience;
                        Empdata.ContactNumber = item.Contact_Number;
                        Empdata.Adress = item.Adress;
                        Empdata.LocationId = item.Location_Id;
                        Empdata.EnableEditButton = item.Enable_Edit_Button;
                        if (Empdata.LocationId > 0)
                        {
                            Empdata.Location = GetLoctionName(Empdata.LocationId);
                        }
                        ListData.Add(Empdata);
                    }
                }
            }
            return ListData.ToList();
        }
        #endregion
    }
}
