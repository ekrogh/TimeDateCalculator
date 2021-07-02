using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Widget;
using pdfCalcProj.FileHandlers;
using pdfCalcProj.MessageThings;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(pdfCalcProj.Droid.FileHandlers.HandleFiles_Android))]
namespace pdfCalcProj.Droid.FileHandlers
{
	class HandleFiles_Android : IHandleFiles
	{
		Context LoclContext => CrossCurrentActivity.Current.Activity;

		public async Task SelectFilesToReadFrom(string[] filetypes)
		{
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
				if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
					{
						Toast.MakeText(LoclContext, "Need Storage permission to access to your files.", ToastLength.Long).Show();
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
					status = results[Plugin.Permissions.Abstractions.Permission.Storage];
				}

				if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
				{
					var fileIntent = new Intent(
						Intent.ActionPick);
					fileIntent.SetType("application/pdf");
					fileIntent.PutExtra(Intent.ExtraAllowMultiple, true);
					fileIntent.SetAction(Intent.ActionGetContent);
					((Activity)LoclContext).StartActivityForResult(
						Intent.CreateChooser(fileIntent, "Select file"), MainActivity.MYOPENFILECODE);

				}
				else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
				{
					Toast.MakeText(LoclContext, "Permission Denied. Can not continue, try again.", ToastLength.Long).Show();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				Toast.MakeText(LoclContext, "Error. Can not continue, try again.", ToastLength.Long).Show();
			}
		}

		public async Task SelectFilesToSaveTo(string[] filetypes, string mesgKey)
		{
			SelectFilesResultMessageArgs args = new SelectFilesResultMessageArgs();
			args.TheSelectedFilesInfo = new List<SelectedFileInfo>();

			args.DidPick = false; // In case try catches an exception

			try
			{
				var r = await UserDialogs.Instance.PromptAsync(new PromptConfig
				{
					Title = "Enter file name",
					Message = "(Just file name. No Path!). \".txt\" will be added)",
					Placeholder = "filename",
					OnTextChanged = AreArgsValid(),
				});


				if (r.Ok)
				{
					SelectedFileInfo urlHere = new SelectedFileInfo();

					// Open the document
					urlHere.TheStream =
						new FileStream(Path.Combine(FileSystem.CacheDirectory, r.Text + ".txt"),
									   FileMode.OpenOrCreate,
									   FileAccess.Write);

					args.DidPick = true;

					args.TheSelectedFilesInfo = new List<SelectedFileInfo>();

					args.TheSelectedFilesInfo.Add(urlHere);
				}
				else
				{
					args.DidPick = false;
				}

				// Fire the message
				MessagingCenter.Send(
					(App)Xamarin.Forms.Application.Current,
					MessengerKeys.FilesToSaveToSelected,
					args);

			}
			catch (Exception ex)
			{
				var tsstr = ex.ToString();
			}
		}

		private static Action<PromptTextChangedArgs> AreArgsValid()
		{
			return args =>
			{
				args.IsValid =
					!(args.Value.Contains("/", StringComparison.CurrentCultureIgnoreCase)
					|| args.Value.Contains("\\", StringComparison.CurrentCultureIgnoreCase))
					&& (args.Value.IndexOfAny(Path.GetInvalidFileNameChars()) == ((int)(-1)))
					&& (args.Value.Length != 0);
			};
		}


		public async Task<bool> PathExists(string PathName)
		{
			return await PathExists(PathName);
		}

		public async Task<bool> FileExists(string FilePathAndName)
		{
			return await FileExists(FilePathAndName);
		}

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
			catch (Exception ex)
			{
				var excptn = ex.ToString();
			}

			return null;
		}


		public async Task<string> ReadFromTextFile(string FilePathAndName)
		{
			return File.ReadAllText(FilePathAndName);
		}

		public async Task<bool> SaveToTextFile(System.IO.Stream TheTextFileStream, string TheText)
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

				await Share.RequestAsync(new ShareFileRequest
				{
					Title = "Save file",
					File = new ShareFile((TheTextFileStream as FileStream).Name)
				});

				return true;
			}
			catch (Exception ex)
			{
				var tsstr = ex.ToString();
			}

			return false;
		}

		public async Task<byte[]> ReadAllBytesFromFile(Stream TheByteFileStream)
		{
			string excptn = "";

			try
			{
				int lgth = 100000;
				byte[] inbfr = new byte[lgth];

				var r = await TheByteFileStream.ReadAsync(inbfr, 0, lgth);

				return inbfr;
			}
			catch (ArgumentNullException ex)
			{
				//buffer is null.
				excptn = ex.ToString();
			}
			catch (ArgumentOutOfRangeException ex)
			{
				// offset or count is negative.
				excptn = ex.ToString();
			}
			catch (ArgumentException ex)
			{
				//The sum of offset and count is larger than the buffer length.
				excptn = ex.ToString();
			}
			catch (NotSupportedException ex)
			{
				//The stream does not support reading.
				excptn = ex.ToString();
			}
			catch (ObjectDisposedException ex)
			{
				//The stream has been disposed.
				excptn = ex.ToString();
			}
			catch (InvalidOperationException ex)
			{
				//The stream is currently in use by a previous read operation.
				excptn = ex.ToString();
			}
			catch (Exception ex)
			{
				// Anything else
				excptn = ex.ToString();
			}

			return null;
		}

		public async Task<bool> SaveBytesToFile(string thePathAndFile, byte[] ByteBuffer)
		{
			File.WriteAllBytes(thePathAndFile, ByteBuffer);
			return true;
		}

		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}

	}
}