using Gtk;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculator.FileHandlers;
using TimeDateCalculator.MessageThings;
using Xamarin.Forms;
using static System.IO.File;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.gtk.FileHandlers.HandleFiles_Gtk))]
namespace TimeDateCalculator.gtk.FileHandlers
{
	class HandleFiles_Gtk : IHandleFiles
	{
		public Window Toplevel { get; private set; }

#pragma warning disable 1998
		public async Task SelectFilesToReadFrom(string[] filetypes)
		{
			SelectFileResultMessageArgs args = new SelectFileResultMessageArgs();

			args.DidPick = false;

			// creates a custom choose dialog
			Gtk.FileChooserDialog fc =
				 new Gtk.FileChooserDialog("Select .ics file", this.Toplevel as Gtk.Window, FileChooserAction.Open,
					 "Cancel", ResponseType.Cancel,
					 "Select", ResponseType.Accept);

			//Gtk.FileFilter filter = new Gtk.FileFilter();
			//filter.Name = "*." + filetypes[0];
			//filter.AddPattern("*." + filetypes[0]);
			//fc.AddFilter(filter);

			fc.LocalOnly = false;

			if (fc.Run() == (int)ResponseType.Accept)
			{
				string path = fc.Filename;

				args.DidPick = true;

				args.TheSelectedFileInfo = new SelectedFileInfo();

				SelectedFileInfo urlHere = new SelectedFileInfo();

				// Open the document
				urlHere.TheStream = new FileStream(fc.Filename, FileMode.Open, FileAccess.Read);

				args.TheSelectedFileInfo = urlHere;
			}

			fc.Destroy();

			// Fire the message
			MessagingCenter.Send(
				(App)Xamarin.Forms.Application.Current,
				MessengerKeys.FileToReadFromSelected,
				args);

		}
#pragma warning restore 1998

#pragma warning disable 1998
		public async Task SelectFilesToSaveTo(string SuggestedNameOfFileToSaveTo, string[] filetypes, string mesgKey)
		{
			SelectFileResultMessageArgs args = new SelectFileResultMessageArgs();

			args.DidPick = false;

			// creates a custom choose dialog
			Gtk.FileChooserDialog fc =
				 new Gtk.FileChooserDialog("Select .ics file to save to", this.Toplevel as Gtk.Window, FileChooserAction.Save,
					 "Cancel", ResponseType.Cancel,
					 "Select", ResponseType.Accept);

			//Gtk.FileFilter filter = new Gtk.FileFilter();
			//filter.Name = "*." + filetypes[0];
			////filter.Name = "*." + filetypes[0];
			//filter.AddPattern("." + filetypes[0]);
			//fc.AddFilter(filter);

			fc.LocalOnly = false;

			if (fc.Run() == (int)ResponseType.Accept)
			{
				string path = fc.Filename;

				args.DidPick = true;

				args.TheSelectedFileInfo = new SelectedFileInfo();

				SelectedFileInfo urlHere = new SelectedFileInfo();

				// Open the document
				urlHere.TheStream = new FileStream(fc.Filename, FileMode.OpenOrCreate, FileAccess.Write);

				args.TheSelectedFileInfo = urlHere;
			}

			fc.Destroy();

			// Fire the message
			MessagingCenter.Send(
				(App)Xamarin.Forms.Application.Current,
								 mesgKey,
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
				outbfr = Encoding.UTF8.GetBytes(TheText);

				await TheTextFileStream.WriteAsync(outbfr, 0, outbfr.Length);
				await TheTextFileStream.FlushAsync(); //Write all
				TheTextFileStream.Close();

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
