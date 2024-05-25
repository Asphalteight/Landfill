using Landfill.Common.Enums;
using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Landfill.Services
{
    public class SeedService : IHostedService
    {
        private readonly IDbContext _dbContext;

        public SeedService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SeedUserAccounts();

            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        private Task SeedUserAccounts()
        {
            var salt = new[] { PasswordHelper.GenerateSalt(), PasswordHelper.GenerateSalt() };
            var login = new[] { "admin", "user1" };

            var userAccounts = new UserAccount[]
            {
                new()
                {
                    Login = login[0],
                    Salt = salt[0],
                    PasswordHash = login[0].GetHash(salt[0]),
                    Employee = new Employee
                    {
                        FirstName = "Олег",
                        LastName = "Иванов",
                        MiddleName = "Петрович",
                        Phone = "+77778888888",
                        Position = "Директор"
                    },
                    Roles = [
                        new() { Role = RoleEnum.Employee },
                        new() { Role = RoleEnum.Manager },
                        new() { Role = RoleEnum.Admin }
                    ]
                },
                new()
                {
                    Login = login[1],
                    Salt = salt[1],
                    PasswordHash = login[1].GetHash(salt[1]),
                    Employee = new Employee
                    {
                        FirstName = "Николай",
                        LastName = "Некрасов",
                        MiddleName = "Максимович",
                        Phone = "+77777777777",
                        Position = "Сотрудник"
                    },
                    Roles = [ new() { Role = RoleEnum.Employee } ]
                }
            };
            
            foreach (var account in userAccounts)
            {
                _dbContext.Add(account);
            }

            SeedBuildProjects(userAccounts);
            return Task.CompletedTask;
        }

        private Task SeedBuildProjects(UserAccount[] userAccounts)
        {
            var projects = new BuildProject[]
            {
                new() {
                    Name = "Строительный проект детской площадки",
                    Address = "г. Симферополь, ул. Рандомная, д. 1",
                    Price = 5000000,
                    Customer = "ООО \"КрымМегаСтрой\"",
                    PlanningCompletionDate = DateTime.Now.AddYears(1),
                    State = ProjectStateEnum.Created,
                    Members = [
                        new ProjectMember { FirstName = "Илья", LastName = "Киреев", MiddleName = "Петрович", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Владислав", LastName = "Владов", MiddleName = "Дмитриевич", Phone = "+72222222222" }
                    ],
                    Employee = userAccounts[0].Employee
                },
                new() {
                    Name = "Строительный проект по возведению здания государственного управления Республики Крым",
                    Address = "г. Севастополь, ул. Иванова, д. 10",
                    Price = 700000000,
                    Customer = "ООО \"Новое время\"",
                    PlanningCompletionDate = new DateTime(2026, 2, 5),
                    State = ProjectStateEnum.InProgress,
                    Members = [
                        new ProjectMember { FirstName = "Сергей", LastName = "Петров", MiddleName = "Иванович", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Владимир", LastName = "Гидров", MiddleName = "Николаевич", Phone = "+72222222222" },
                        new ProjectMember { FirstName = "Максим", LastName = "Цукунберг", MiddleName = "Джекович", Phone = "+72222222222" },
                        new ProjectMember { FirstName = "Виктор", LastName = "Петрович", MiddleName = "Баринов", Phone = "+72222222222" },
                    ],
                    Employee = userAccounts[0].Employee
                },
                new() {
                    Name = "Строительство жилого дома",
                    Address = "г. Алушта, ул. Гагарина, д. 131",
                    Price = 3000000,
                    Customer = "ИП \"Сергеев\"",
                    PlanningCompletionDate = new DateTime(2024, 9, 12),
                    State = ProjectStateEnum.Created,
                    Members = [
                        new ProjectMember { FirstName = "Егор", LastName = "Матвеев", MiddleName = "Владимирович", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Николай", LastName = "Андреев", MiddleName = "Андреевич", Phone = "+72222222222" },
                    ],
                    Employee = userAccounts[1].Employee
                },
                new() {
                    Name = "Строительный проект школы",
                    Address = "г. Симферополь, ул. Фрунзе, д. 142",
                    Price = 200000000,
                    Customer = "ООО \"ГлобалЗастройщик\"",
                    StartDate = new DateTime(2023, 1, 5),
                    PlanningCompletionDate = new DateTime(2024, 5, 22),
                    CompletionDate = new DateTime(2024, 5, 22),
                    State = ProjectStateEnum.Done,
                    Members = [
                        new ProjectMember { FirstName = "Василий", LastName = "Хлебов", MiddleName = "Сергеевич", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Тимофей", LastName = "Тимьянов", MiddleName = "Максимович", Phone = "+72222222222" },
                        new ProjectMember { FirstName = "Михаил", LastName = "Полевой", MiddleName = "Дмитриевич", Phone = "+72222222222" }
                    ],
                    Employee = userAccounts[0].Employee
                },
                new() {
                    Name = "Строительный проект детского садика",
                    Address = "г. Симферополь, ул. Сергеева Ценского, д. 41",
                    Price = 1000000000,
                    Customer = "ООО \"Строитель\"",
                    StartDate = new DateTime(2024, 3, 1),
                    PlanningCompletionDate = new DateTime(2025, 4, 5),
                    State = ProjectStateEnum.InProgress,
                    Members = [
                        new ProjectMember { FirstName = "Данил", LastName = "Никифиоров", MiddleName = "Егорович", Phone = "+79998888888" },
                        new ProjectMember { FirstName = "Евгений", LastName = "Зеленый", MiddleName = "Иванович", Phone = "+72222222222" }
                    ],
                    Employee = userAccounts[0].Employee
                }

            };

            foreach (var project in projects)
            {
                _dbContext.Add(project);
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
