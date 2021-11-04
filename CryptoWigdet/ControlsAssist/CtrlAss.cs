using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace CryptoWigdet
{
    class CtrlAss
    {
        public static Thread Snackbarer(Snackbar sb, double timing, string message, Window formw)
        {
            var th = new Thread(() =>
            {
                formw.Dispatcher.Invoke(new Action(() => { sb.IsActive = true; sb.Message = new SnackbarMessage() { Content = message }; }));
                Thread.Sleep((int)(timing * 1000));
                formw.Dispatcher.Invoke(new Action(() => sb.IsActive = false));
            });
            return th;
        }
    }
}
