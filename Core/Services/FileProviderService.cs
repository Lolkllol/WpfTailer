using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace Core.Services
{
    public class FileProviderService : IFIleProvider
    {
        public string SelectFile()
        {
            var fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != true)
            {
                return null;
            }

            return fileDialog.FileName;
        }
    }
}
