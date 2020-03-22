using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Interfaces
{
    public interface ILogger
    {
        void Info(string message);
        void Error(Exception exception);
        void Error(Exception exception, string message);
        void Warn(string message);
    }
}
