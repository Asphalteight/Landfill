using Landfill.MVVM.Models;

namespace Landfill.Services
{
    public interface ICredentialsService
    {
        public CredentialsModel Get();
        public void Set(CredentialsModel user);
    }

    public class CredentialsService : ICredentialsService
    {
        private CredentialsModel _credentials = new();
        public CredentialsModel Get() => _credentials;
        public void Set(CredentialsModel model) => _credentials = model;
    }
}
