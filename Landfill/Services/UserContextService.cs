using AutoMapper;
using Landfill.Abstractions;
using Landfill.Common.Enums;
using Landfill.DataAccess;
using Landfill.DataAccess.Models;
using Landfill.MVVM.Models;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;

namespace Landfill.Services
{
    public interface IUserContextService
    {
        public EmployeeInfoModel CurrentUser { get; set; }
        public PermissionsModel Permissions { get; set; }
        public void SetUserFromStored();
        public void SetUser(UserAccount userName);
        public void ResetStoredUser();
    }

    public class UserContextService : ViewModelBase, IUserContextService
    {
        private const string FileName = "cookie.txt";

        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        private EmployeeInfoModel _currentUser;
        private PermissionsModel _permissions = new();

        public EmployeeInfoModel CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(); } }
        public PermissionsModel Permissions { get => _permissions; set { _permissions = value; OnPropertyChanged(); } }

        public UserContextService(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void SetUserFromStored()
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            var reader = new StreamReader(new IsolatedStorageFileStream(FileName, FileMode.OpenOrCreate, isolatedStorage));

            UserAccount account = null;
            if (reader != null)
            {
                while (!reader.EndOfStream)
                {
                    var login = reader.ReadLine();
                    var passwordHash = reader.ReadLine();
                    reader.ReadLine();
                    account = new UserAccount
                    {
                        Login = login,
                        PasswordHash = passwordHash
                    };
                }
            }
            reader.Close();

            if (account != null)
            {
                SetCurrentUser(account);
            }
        }

        public void SetUser(UserAccount account)
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            var writer = new StreamWriter(new IsolatedStorageFileStream(FileName, FileMode.OpenOrCreate, isolatedStorage));

            writer.WriteLine(account.Login);
            writer.WriteLine(account.PasswordHash);
            writer.Write($"Stored at {DateTime.Now}");
            writer.Flush();
            writer.Close();

            SetCurrentUser(account);
        }

        public void ResetStoredUser()
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            isolatedStorage.DeleteFile(FileName);
            CurrentUser = null;
        }

        private void SetCurrentUser(UserAccount account)
        {
            var employee = _dbContext.QuerySet<UserAccount>().FirstOrDefault(x => x.Login == account.Login && x.PasswordHash == account.PasswordHash)?.Employee;
            if (employee == null)
            {
                return;
            }
            CurrentUser = _mapper.Map<EmployeeInfoModel>(employee);

            Func<RoleEnum, bool> adminOrManager = (role) => role == RoleEnum.Admin || role == RoleEnum.Manager;

            Permissions.AddNewProjects = CurrentUser.Roles.Any(adminOrManager);
            Permissions.EditProjects = CurrentUser.Roles.Any(adminOrManager);
            Permissions.EditEmployee = CurrentUser.Roles.Any(adminOrManager);
            Permissions.EditEmployeeAdminRole = CurrentUser.Roles.Any(x => x == RoleEnum.Admin);
            Permissions.EditEmployeeManagerRole = CurrentUser.Roles.Any(adminOrManager);
        }
    }
}
