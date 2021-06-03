using System;
using System.Collections.Generic;
using System.Text;

namespace TimeDateCalculatorP.Interfaces
{
    public interface IAppVersion
    {
        string GetAppTitle();
        string GetVersion();
        string GetBuild();
		string GetRevision();
	}
}
