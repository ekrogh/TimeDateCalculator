using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppKit;
using static System.IO.File;
using Xamarin.Forms;
using TimeDateCalculator.FileHandlers;
using TimeDateCalculator.MessageThings;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.macOS.FileHandlers.HandleFiles_macOS))]
namespace TimeDateCalculator.macOS.FileHandlers
{
	class HandleFiles_macOS : IHandleFiles
	{

#pragma warning disable 1998
		public async Task SelectFilesToReadFrom(string[] filetypes)
		{
			var dlg = NSOpenPanel.OpenPanel;
			dlg.CanChooseFiles = true;
			dlg.AllowsMultipleSelection = true;

			SelectFilesResultMessageArgs args = new SelectFilesResultMessageArgs();

			if (dlg.RunModal(filetypes) == 1)
			{
				if (dlg.Urls[0] != null)
				{
					args.DidPick = true;

					args.TheSelectedFilesInfo = new List<SelectedFileInfo>();

					SelectedFileInfo urlHere = new SelectedFileInfo();

					foreach (var url in dlg.Urls)
					{
						// Open the document
						urlHere.TheStream = new FileStream(url.Path, FileMode.Open, FileAccess.Read);

						args.TheSelectedFilesInfo.Add(urlHere);
					}
				}
				else
				{
					args.DidPick = false;
				}
			}
			else
			{
				args.DidPick = false;
			}

			// Fire the message
			MessagingCenter.Send(
				(App)Application.Current,
				MessengerKeys.FilesToReadFromSelected,
				args);

		}
#pragma warning restore 1998

#pragma warning disable 1998
		public async Task SelectFilesToSaveTo(string[] filetypes, string mesgKey)
		{
			var dlg = NSSavePanel.SavePanel;
			dlg.AllowedFileTypes = filetypes;
			dlg.AllowsOtherFileTypes = true;
			dlg.CanCreateDirectories = true;

			SelectFilesResultMessageArgs args = new SelectFilesResultMessageArgs();

			if (dlg.RunModal() == 1)
			{
				if (dlg.Url != null)
				{
					args.DidPick = true;

					args.TheSelectedFilesInfo = new List<SelectedFileInfo>();

					SelectedFileInfo urlHere = new SelectedFileInfo
					{

						// Open the document
						TheStream = new FileStream(
						dlg.Url.Path,
						FileMode.OpenOrCreate,
						FileAccess.Write)
					};

					args.TheSelectedFilesInfo.Add(urlHere);
				}
				else
				{
					args.DidPick = false;
				}
			}
			else
			{
				args.DidPick = false;
			}

			// Fire the message
			MessagingCenter.Send((App)Application.Current,
								 MessengerKeys.FilesToSaveToSelected,
								 args);

		}
#pragma warning restore 1998

#pragma warning disable 1998
		public async Task<bool> PathExists(string PathName)
		{
			return Directory.Exists(PathName);
		}
#pragma warning restore 1998

#pragma warning disable 1998
		public async Task<bool> FileExists(string FilePathAndName)
		{
			return Exists(FilePathAndName);
		}
#pragma warning restore 1998

		public async Task<string> ReadFromTextFile(System.IO.Stream TheTextFileStream)
		{
			try
			{
				byte[] bfr = new byte[TheTextFileStream.Length];
				await TheTextFileStream.ReadAsync(bfr, 0, (int)TheTextFileStream.Length);
				if (BitConverter.IsLittleEndian)
				{
					return Encoding.Unicode.GetString(bfr);
				}
				else
				{
					return Encoding.BigEndianUnicode.GetString(bfr);
				}
			}
			catch (Exception)
			{
			}

			return null;
		}

#pragma warning disable 1998
		public async Task<string> ReadFromTextFile(string FilePathAndName)
		{
			return ReadAllText(FilePathAndName);
		}
#pragma warning restore 1998

		public async Task<bool> SaveToTextFile(Stream TheTextFileStream, string TheText)
		{
			try
			{
				byte[] outbfr;
				if (BitConverter.IsLittleEndian)
				{
					outbfr = Encoding.Unicode.GetBytes(TheText);
				}
				else
				{
					outbfr = Encoding.BigEndianUnicode.GetBytes(TheText);
				}
				await TheTextFileStream.WriteAsync(outbfr, 0, outbfr.Length);
				await TheTextFileStream.FlushAsync(); //Write all

				return true;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("SaveToTextFile failed: {0}", e.ToString());
				return false;
			}
			finally
			{
			}
		}

		public async Task<byte[]> ReadAllBytesFromFile(Stream TheByteFileStream)
		{
			try
			{
				byte[] bfr = new byte[TheByteFileStream.Length];
				await TheByteFileStream.ReadAsync(bfr, 0, (int)TheByteFileStream.Length);
				return bfr;
			}
			catch (Exception)
			{
			}

			return null;
		}

#pragma warning disable 1998
		public async Task<bool> SaveBytesToFile(string ByteFilePath, Byte[] ByteBuffer)
		{
			//string thePathAndFile = ByteFilePath + Path.DirectorySeparatorChar + ByteFileName;
			WriteAllBytes(ByteFilePath, ByteBuffer);
			return true;
		}
#pragma warning restore 1998

		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}


	}
}
