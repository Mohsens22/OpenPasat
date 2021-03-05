using System;
using System.Collections.Generic;
using System.Text;

namespace UnoTest.Services
{
    public interface INotify
    {
        void InApp(string message, string info = "", NotifyType type = NotifyType.Info);
        void Toast(string message, string info = "", NotifyType type = NotifyType.Info);
        void Log(string message, string info = "", NotifyType type = NotifyType.Info);
        void Debug(string message, string info = "", NotifyType type = NotifyType.Info);
        void Console(string message, string info = "", NotifyType type = NotifyType.Info);
    }

    public enum NotifyType
    {
        Info,
        Warning,
        Error
    }
}
