using System;
using System.Reflection;
using TimeDateCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.GTK.Version_GTK))]
namespace TimeDateCalculator.GTK
{
	public class Version_GTK : IAppVersion
	{
		private readonly string[] separators = { ",", " " };

		public string GetAppTitle()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string fullName = assembly.FullName;
			string[] fullNameParts = fullName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			string[] nameParts = fullNameParts[0].Split('.');
			return nameParts[0];
		}

		public string GetVersion()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string fullName = assembly.FullName;
			string[] fullNameParts = fullName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			string[] verseparators = { ".", "=" };
			string[] verNbrs = fullNameParts[1].Split(verseparators, StringSplitOptions.RemoveEmptyEntries);
			return verNbrs[1] + '.' + verNbrs[2];
		}

		public string GetBuild()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string fullName = assembly.FullName;
			string[] fullNameParts = fullName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			string[] verseparators = { ".", "=" };
			string[] verNbrs = fullNameParts[1].Split(verseparators, StringSplitOptions.RemoveEmptyEntries);
			return '.' + verNbrs[3];
		}

		public string GetRevision()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string fullName = assembly.FullName;
			string[] fullNameParts = fullName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
			string[] verseparators = { ".", "=" };
			string[] verNbrs = fullNameParts[1].Split(verseparators, StringSplitOptions.RemoveEmptyEntries);
			return '.' + verNbrs[4];
		}
	}
}
