using System;

namespace TimeDateCalculator.MessageThings
{
	public class IcsDescriptionMessageArgs : EventArgs
	{
		public string TheDescription;
		public string EventName_Summary;
		public string Location;
	}
}
