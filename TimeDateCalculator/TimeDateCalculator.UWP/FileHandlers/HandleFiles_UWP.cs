using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Xamarin.Forms;
using TimeDateCalculator.FileHandlers;
using TimeDateCalculator.MessageThings;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.UWP.FileHandlers.HandleFiles_UWP))]
namespace TimeDateCalculator.UWP.FileHandlers
{
	class HandleFiles_UWP : IHandleFiles
	{

		public async Task SelectFilesToReadFrom(string[] filetypes)
		{
			try
			{
				FileOpenPicker openPicker = new FileOpenPicker();
				//openPicker.ViewMode = PickerViewMode.Thumbnail;
				foreach (string filetype in filetypes)
				{
					openPicker.FileTypeFilter.Add("." + filetype);
				}
				//openPicker.FileTypeFilter.Add(".pdf");

				IReadOnlyList<StorageFile> files = await openPicker.PickMultipleFilesAsync();

				SelectFileResultMessageArgs args = new SelectFileResultMessageArgs();

				if (files.Count > 0)
				{
					args.DidPick = true;

					args.TheSelectedFileInfo = new List<SelectedFileInfo>();

					SelectedFileInfo urlHere = new SelectedFileInfo();

					// Application now has read/write access to the picked file(s)
					var tokenNo = 1;
					string tokenNam = "PickedFolderToken" + tokenNo.ToString();
					foreach (StorageFile file in files)
					{
						Windows.Storage.AccessCache.StorageApplicationPermissions.
					 FutureAccessList.AddOrReplace(tokenNam, file);

						// Open the document for read
						StorageFile XmlStorageFile = await StorageFile.GetFileFromPathAsync(file.Path);
						IRandomAccessStream readStream = await XmlStorageFile.OpenAsync(FileAccessMode.Read);

						urlHere.TheStream = readStream.AsStreamForRead();

						args.TheSelectedFileInfo.Add(urlHere);

						tokenNo++;
						tokenNam = "PickedFolderToken" + tokenNo.ToString();
					}
				}
				else
				{
					args.DidPick = false;
				}

				// Fire the message
				MessagingCenter.Send((TimeDateCalculator.App)Application.Current,
									 message: MessengerKeys.FileToReadFromSelected,
									 args);

			}
			catch (Exception ex)
			{
				var excptn = ex.ToString();
			}
		}

		public async Task SelectFilesToSaveTo(string[] filetypes, string mesgKey)
		{
			SelectFileResultMessageArgs args = new SelectFileResultMessageArgs
			{
				DidPick = false // In case of exception
			};

			try
			{
				var savePicker = new FileSavePicker
				{
					SuggestedFileName = "NewFile.txt"
				};
				foreach (string filetype in filetypes)
				{
					savePicker.FileTypeChoices.Add("", new List<string>() { "." + filetype });
				}

				var file = await savePicker.PickSaveFileAsync();


				if (file != null)
				{
					args.DidPick = true;

					args.TheSelectedFileInfo = new List<SelectedFileInfo>();

					SelectedFileInfo urlHere = new SelectedFileInfo();

					// Application now has read/write access to the picked file(s)
					var tokenNo = 1;
					string tokenNam = "PickedFolderToken" + tokenNo.ToString();
					Windows.Storage.AccessCache.StorageApplicationPermissions.
						FutureAccessList.AddOrReplace(tokenNam, file);

					// Open the document for read
					StorageFile XmlStorageFile = await StorageFile.GetFileFromPathAsync(file.Path);
					IRandomAccessStream saveStream = await XmlStorageFile.OpenAsync(FileAccessMode.ReadWrite);

					urlHere.TheStream = saveStream.AsStreamForWrite();

					args.TheSelectedFileInfo.Add(urlHere);
				}

			}
			catch (Exception ex)
			{
				var xcptn = ex.ToString();
			}

			// Fire the message
			MessagingCenter.Send((TimeDateCalculator.App)Application.Current,
								 mesgKey,
								 args);
		}

		public async Task<bool> PathExists(string PathName)
		{
			//return Directory.Exists(PathName);
			try
			{
				StorageFolder StorageFolderForPathName = await StorageFolder.GetFolderFromPathAsync(PathName);
				return StorageFolderForPathName.IsOfType(StorageItemTypes.Folder);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("The process failed: {0}", e.ToString());
				return false;
			}
			finally { }
		}

		public async Task<bool> FileExists(string FilePathAndName)
		{
			try
			{
				StorageFile StorageFileForFilePathAndName = await StorageFile.GetFileFromPathAsync(FilePathAndName);
				return StorageFileForFilePathAndName.IsOfType(StorageItemTypes.File);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("The process failed: {0}", e.ToString());
				return false;
			}
			finally { }
		}

		public async Task<string> ReadFromTextFile(string FilePathAndName)
		{
			try
			{
				StorageFile StorageFileForFilePathAndName = await StorageFile.GetFileFromPathAsync(FilePathAndName);
				if (!StorageFileForFilePathAndName.IsOfType(StorageItemTypes.File))
				{
					return "Error";
				}
				return await FileIO.ReadTextAsync(StorageFileForFilePathAndName);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("The process failed: {0}", e.ToString());
				return "Error";
			}
			finally
			{
			}
		}

		public async Task<string> ReadFromTextFile(Stream TheByteFileStream)
		{
			try
			{
				TheByteFileStream.Position = 0;
				using (StreamReader strRdr = new StreamReader(TheByteFileStream))
				{
					return await strRdr.ReadToEndAsync();
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("The process failed: {0}", e.ToString());
				return "Error";
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
				TheByteFileStream.Position = 0;
				await TheByteFileStream.ReadAsync(bfr, 0, (int)TheByteFileStream.Length);
				return bfr;
			}
			catch (Exception ex)
			{
				var excptn = ex.ToString();
			}

			return null;
		}

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

		public async Task<bool> SaveBytesToFile(string ByteFilePath, byte[] ByteBuffer)
		{
			try
			{
				StorageFolder StorageFolderForPathName = await StorageFolder.GetFolderFromPathAsync(ByteFilePath);
				StorageFile ByteStorageFile = await StorageFolderForPathName.CreateFileAsync(ByteFilePath, CreationCollisionOption.ReplaceExisting);
				if (!ByteStorageFile.IsOfType(StorageItemTypes.File))
				{
					return false;
				}
				await FileIO.WriteBytesAsync(ByteStorageFile, ByteBuffer);
				return true;
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("WriteToXmlFile failed: {0}", e.ToString());
				return false;
			}
			finally
			{
			}
		}

		public string GetCurrentDirectory()
		{
			return Directory.GetCurrentDirectory();
		}

	}
}
