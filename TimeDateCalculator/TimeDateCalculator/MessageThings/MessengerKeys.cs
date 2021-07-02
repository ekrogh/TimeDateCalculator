namespace TimeDateCalculator.MessageThings
{
	public class MessengerKeys
	{
		// Orientation msgs.
		private const string PrivLandscapeOrientationRequest = "LandscapeOrientationRequest";
		private const string PrivPortraitOrientationRequest = "PortraitOrientationRequest";
		private const string PrivAllButUpsideDowntOrientationRequest = "AllButUpsideDowntOrientationRequest";

		public static string LandscapeOrientationRequest => PrivLandscapeOrientationRequest;
		public static string PortraitOrientationRequest => PrivPortraitOrientationRequest;
		public static string AllButUpsideDowntOrientationRequest => PrivAllButUpsideDowntOrientationRequest;

		// File handl. msgs.
		private const string filesToReadFromSelected = "FilesToReadFromSelected";
		private const string filesToSaveToSelected = "FilesToSaveToSelected";
		private const string filesToSaveRawTextToSelected = "FilesToSaveRawTextToSelected";

		public static string FilesToReadFromSelected => filesToReadFromSelected;
		public static string FilesToSaveToSelected => filesToSaveToSelected;
		public static string FilesToSaveRawTextToSelected => filesToSaveRawTextToSelected;

		// .ics Description entered
		private const string icsDescriptionEntered = "icsDescriptionEntered";
		public static string IcsDescriptionEntered => icsDescriptionEntered;

	}
}
