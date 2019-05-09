using HappyStorage.FileStorage;
using System;
using System.IO;

namespace HappyStorage.BlazorWeb.Settings
{
    public class FileCustomerStoreSettings : IFileCustomerStoreSettings
    {
        public string GetRootPath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "HappyStorage");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }
    }
}
