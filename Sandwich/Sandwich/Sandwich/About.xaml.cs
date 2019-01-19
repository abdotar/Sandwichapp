using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sandwich.prefabs;
using Xamarin.Forms.Maps;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Sandwich
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class About : ContentPage
	{
		public About ()
		{
			InitializeComponent ();

			Header head = new Header();
			Footer foot = new Footer();
			ask();
			top.Children.Add(head);
			buttom.Children.Add(foot);
		}

		protected override void OnAppearing()
		{
			center.Children.Clear();
			ask();
			Button openinyandex = new Button();
			openinyandex.Text = "Маршрут в яндекс навигаторе";
			openinyandex.Clicked += new EventHandler(openyandexapp);
			center.Children.Add(openinyandex);

		}

		public void openyandexapp(object sender, EventArgs e)
		{
			DependencyService.Get<IOpenApp>().LaunchApp("yandexmaps://maps.yandex.ru/?pt=37.502493,55.653002&z=12&l=map");
			
			/*OpenAppService.Launch()
			Device.BeginInvokeOnMainThread(() =>
			{
				double lat = 55.653002;
				double lon = 37.502493;
				Device.OpenUri(new Uri("yandexmaps://maps.yandex.ru/?pt=37.502493,55.653002&z=12&l=map"));
			});*/


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
			var current = Connectivity.NetworkAccess;
			if (current == NetworkAccess.Internet)
			{
				//center.Children.Add(); adding stacklyout with offers
				var map = new Xamarin.Forms.Maps.Map(
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