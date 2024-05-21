using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Landfill.Services
{
    public class SeedService : IHostedService
    {
        private readonly IDbContext _dbContext;
        private const string Admin = "admin";

        public SeedService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SeedUserAccounts();
            SeedBuildProjects();

            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        private void SeedBuildProjects()
        {
            var projectName = "Строительный проект детской площадки";

            var employee = _dbContext.QuerySet<UserAccount>().First(x => x.Login == Admin).Employee;
            var project = _dbContext.QuerySet<BuildProject>().FirstOrDefault(x => x.Name == projectName);
            if (project == null)
            {
                project = new BuildProject
                {
                    Name = "Строительный проект детской площадки",
                    Address = "г. Симферополь, ул. Рандомная, д. 1",
                    Price = 5000000,
                    Customer = "ООО \"КрымМегаСтрой\"",
                    PlanningCompletionDate = DateTime.Now.AddYears(1),
                    State = ProjectStateEnum.Created,
                    Members = [
                    new ProjectMember { FirstName = "Василий", LastName = "Морозов", MiddleName = "Сергеевич", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Владислав", LastName = "Владов", MiddleName = "Дмитриевич", Phone = "+72222222222" }
                    ],
                    EmployeeId = employee.Id
                };

                _dbContext.Add(project);
            }
        }

        private void SeedUserAccounts()
        {
            var userAccount = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == Admin);
            if (userAccount == null)
            {
                var salt = PasswordHelper.GenerateSalt();

                userAccount = new UserAccount
                {
                    Login = Admin,
                    Salt = salt,
                    PasswordHash = Admin.GetHash(salt),
                    Employee = new Employee
                    {
                        FirstName = "Олег",
                        LastName = "Иванов",
                        MiddleName = "Петрович",
                        Phone = "+77778888888",
                        Position = "Директор"
                    }
                };
                userAccount.Roles.Add(new RoleToUser { Role = RoleEnum.Admin });

                _dbContext.Add(userAccount);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
