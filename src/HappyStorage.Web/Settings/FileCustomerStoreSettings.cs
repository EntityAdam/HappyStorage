﻿using HappyStorage.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyStorage.Web.Settings
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
