using System;
using System.Collections.Generic;
using System.Text;

namespace TimeDateCalculator.Interfaces
{
    public interface IAppVersion
    {
        string GetVersion();
        int GetBuild();
    }
}
