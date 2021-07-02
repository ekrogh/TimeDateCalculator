using System;
using System.Collections.Generic;

namespace pdfCalcProj.MessageThings
{
	public struct SelectedFileInfo
	{
		public System.IO.Stream TheStream { get; set; }
	}
	public class SelectFilesResultMessageArgs : EventArgs
	{
		public bool DidPick { get; set; }
		public List<SelectedFileInfo> TheSelectedFilesInfo { get; set; }
	}

}
