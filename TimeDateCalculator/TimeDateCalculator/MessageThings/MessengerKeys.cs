namespace TimeDateCalculator.MessageThings
{
	public class MessengerKeys
	{
		// Add product to basket  
		private const string PrivLandscapeOrientationRequest = "LandscapeOrientationRequest";
		private const string PrivPortraitOrientationRequest = "PortraitOrientationRequest";
		private const string PrivAllButUpsideDowntOrientationRequest = "AllButUpsideDowntOrientationRequest";

		private const string filesToReadFromSelected = "FilesToReadFromSelected";
		private const string filesToSaveToSelected = "FilesToSaveToSelected";
		private const string filesToSaveRawTextToSelected = "FilesToSaveRawTextToSelected";


		public static string LandscapeOrientationRequest => PrivLandscapeOrientationRequest;
		public static string PortraitOrientationRequest => PrivPortraitOrientationRequest;
		public static string AllButUpsideDowntOrientationRequest => PrivAllButUpsideDowntOrientationRequest;

		public static string FilesToReadFromSelected => filesToReadFromSelected;
		public static string FilesToSaveToSelected => filesToSaveToSelected;
		public static string FilesToSaveRawTextToSelected => filesToSaveRawTextToSelected;
	}
}
