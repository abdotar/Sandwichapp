using System;
using System.Collections.Generic;
using System.Text;
using Sandwichapp.interfaces;
using UIKit;
using Foundation;
using System.Threading.Tasks;


[assembly: Xamarin.Forms.Dependency(typeof(IOpenApp))]

namespace Sandwichapp.iOS
{
	public class OpenAppiOS : IOpenApp
	{
		public OpenAppiOS() { }

		public void OpenExternalApp()
		{
			/*NSUrl request = new NSUrl("yourapp://");

			try
			{
				bool isOpened = UIApplication.SharedApplication.OpenUrl(request);

				if (isOpened == false)
					UIApplication.SharedApplication.OpenUrl(new NSUrl("yourappurl"));
			}
			catch (Exception ex)
			{
				var alertView = new UIAlertView("Error", ex.Message, null, "OK", null);

				alertView.Show();
			}*/
		}
		public Task<bool> LaunchApp(string uri)
		{
			var canOpen = UIApplication.SharedApplication.CanOpenUrl(new NSUrl(uri));

			if (!canOpen)
			{
				// display error no yandex
				return Task.FromResult(false);
			}
				

			return Task.FromResult(UIApplication.SharedApplication.OpenUrl(new NSUrl(uri)));
		}
	}
}