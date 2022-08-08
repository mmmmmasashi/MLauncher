﻿using LauncherModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MLauncherApp.Service
{
    internal class MessageService : IMessageService
    {
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowPathListWindow(List<FilePath> filePaths)
        {
        }
    }
}
