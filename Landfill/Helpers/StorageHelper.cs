using System.IO;
using System.IO.IsolatedStorage;
using System.Security.Principal;
using System.Threading;

namespace Landfill.Helpers
{
    public static class StorageHelper
    {
        private const string FileName = "cookie.txt";
        public static string GetStoredUser()
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            var reader = new StreamReader(new IsolatedStorageFileStream(FileName, FileMode.OpenOrCreate, isolatedStorage));

            string userName = null;
            if (reader != null)
            {
                while (!reader.EndOfStream)
                {
                    userName = reader.ReadLine().Split('-')[0];
                }
            }
            reader.Close();

            if (userName != null)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
            }

            return userName;
        }

        public static void SetUser(string userName)
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            var writer = new StreamWriter(new IsolatedStorageFileStream(FileName, FileMode.OpenOrCreate, isolatedStorage));
            
            writer.WriteLine($"{userName}-Stored at {System.DateTime.Now}");
            writer.Flush();
            writer.Close();

            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
        }

        public static void ResetStoredUser()
        {
            var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            isolatedStorage.DeleteFile(FileName);
            Thread.CurrentPrincipal = null;
        }
    }
}
