using Landfill.Common.Helpers;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.DataAccess.Models.Enums;
using Microsoft.Extensions.Hosting;
using System.Linq;
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
            var (login, password) = ("admin", "admin");

            var userAccount = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == login);
            if (userAccount != null)
            {
                return Task.CompletedTask;
            }

            var salt = PasswordHelper.GenerateSalt();

            userAccount = new UserAccount
            {
                Login = login,
                Salt = salt,
                PasswordHash = password.GetHash(salt),
                Employee = new Employee
                {
                    FirstName = "Олег",
                    LastName = "Кирпиченко",
                    MiddleName = "Петрович",
                    Phone = "+77778888888"
                }
            };
            userAccount.Roles.Add(new RoleToUser { Role = RoleEnum.Admin });

            _dbContext.Add(userAccount);
            _dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
