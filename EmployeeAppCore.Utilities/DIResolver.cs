using EmployeeAppCore.Core.IRepository;
using EmployeeAppCore.Core.IServices;
using EmployeeAppCore.Resources.Repository;
using EmployeeAppCore.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAppCore.Utilities
{
    public class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IServices, EmployeeSevices>();

            services.AddScoped<IRepository, EmpolyeeRepository>();
        }
    }
}
