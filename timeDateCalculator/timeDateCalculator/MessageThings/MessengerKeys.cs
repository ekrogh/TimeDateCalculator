namespace TimeDateCalculatorP.MessageThings
{
	public class MessengerKeys
	{
		// Add product to basket  
		private const string PrivLandscapeOrientationRequest = "LandscapeOrientationRequest";
		private const string PrivPortraitOrientationRequest = "PortraitOrientationRequest";
		private const string PrivAllButUpsideDowntOrientationRequest = "AllButUpsideDowntOrientationRequest";

		public static string LandscapeOrientationRequest => PrivLandscapeOrientationRequest;
		public static string PortraitOrientationRequest => PrivPortraitOrientationRequest;
		public static string AllButUpsideDowntOrientationRequest => PrivAllButUpsideDowntOrientationRequest;
	}
}
