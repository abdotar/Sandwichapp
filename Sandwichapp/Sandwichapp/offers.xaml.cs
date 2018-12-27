using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sandwichapp.interfaces;
using Xamarin.Forms;
using Plugin.Connectivity;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace Sandwichapp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class offers : ContentPage
	{
		public offers ()
		{
			InitializeComponent ();
			//

			appheader head = new appheader();
			appfooter foot = new appfooter();

			top.Children.Add(head);
			buttom.Children.Add(foot);
			
			

		}

		protected override async void OnAppearing()
		{
			center.Children.Clear();
			ask();
			Button openinyandex = new Button();
			openinyandex.Text = "Открыть в яндекс картах";
			openinyandex.Clicked += new EventHandler(openyandexapp);
			center.Children.Add(openinyandex);

		}

		public async void openyandexapp(object sender, EventArgs e)
		{
			//DependencyService.Get<IOpenApp>().OpenExternalApp();
			Device.BeginInvokeOnMainThread(() =>
			{
				double lat = 55.653002;
				double lon = 37.502493;
			Device.OpenUri(new Uri("yandexmaps://maps.yandex.ru/?pt=37.502493,55.653002&z=12&l=map"));
			});


		}



		public async void ask()
		{

				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
					{
						await DisplayAlert("Предупреждкние", "Требуеться доступ к данным о местоположении", "OK");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
					//Best practice to always check that the key exists
					if (results.ContainsKey(Permission.Location))
						status = results[Permission.Location];
				}

				if (status == PermissionStatus.Granted)
				{
					displaymap();
				}
				else if (status != PermissionStatus.Unknown)
				{
					await DisplayAlert("Трубуеться доступ", "При отказе данная страница не отобразиться, разрешите доступ к местоположению.", "OK");
				}

		}
	

		public void displaymap()
		{
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				//center.Children.Add(); adding stacklyout with offers
				var map = new Map(
		  MapSpan.FromCenterAndRadius(
				  new Position(55.653002, 37.502493), Distance.FromMiles(0.3)))
				{
					IsShowingUser = true,
					HeightRequest = 100,
					WidthRequest = 960,
					VerticalOptions = LayoutOptions.FillAndExpand
				};
				var stack = new StackLayout { Spacing = 0 };
				stack.HeightRequest = 320;
				stack.Children.Add(map);
				var position = new Position(55.653002, 37.502493); // Latitude, Longitude
				var pin = new Pin
				{
					Type = PinType.Place,
					Position = position,
					Label = "Бистро Sandwich",
					Address = "Миклухо - Маклая 11 б."
					

				};
				map.Pins.Add(pin);
				center.Children.Add(stack);

			}
			else
			{
				Interneterror nonet = new Interneterror();
				center.Children.Add(nonet);
			}
		}
	}
}