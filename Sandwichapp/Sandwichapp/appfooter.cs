using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using Xamarin.Forms.Xaml;

using Plugin.Connectivity;

namespace Sandwichapp
{
    public class appfooter : StackLayout
    {

		public appfooter()
		{
			Grid btgrd = new Grid();
			btgrd.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Absolute) });

			//добовляем столбцы
			int i = 3;
			while (i > 0)
			{
				btgrd.ColumnDefinitions.Add(new ColumnDefinition());
				i--;
			}

			btgrd.RowSpacing = 0;
			btgrd.ColumnSpacing = 0;

			//menubuttons main = new menubuttons(); main.Text = "main"; main.Clicked += ToMainPage;

			Image img = new Image();
			img.Source = ImageSource.FromResource("Sandwichapp.images.iconAkcii.png");
			//main.Image= ImageSource.FromResource("Sandwichapp.images.logo2.png");

			Image menu = new Image();
			menu.Source = ImageSource.FromResource("Sandwichapp.images.menuIcon.png");

			Image about = new Image();
			about.Source = ImageSource.FromResource("Sandwichapp.images.about.png");
			/*//Creating TapGestureRecognizers    
			var tapmain = new TapGestureRecognizer();
			//Binding events    
			tapmain.Tapped += ToMainPage;
			//Associating tap events to the image buttons    
			img.GestureRecognizers.Add(tapmain);*/

			pressableimage(img,ToMainPage);
			pressableimage(menu, ToOurmenuPage);
			pressableimage(about, ToAboutPage);


			btgrd.BackgroundColor = Color.Tomato;
			btgrd.Padding = 5;
			btgrd.Children.Add(img,0,0);
			btgrd.Children.Add(menu,1,0);
			btgrd.Children.Add(about,2,0);

			this.Children.Add(btgrd);
		}

		private void pressableimage(Image im , EventHandler ev )
		{
			//Creating TapGestureRecognizers    
			var tapmain = new TapGestureRecognizer();
			//Binding events    
			tapmain.Tapped += ev;
			//Associating tap events to the image buttons    
			im.GestureRecognizers.Add(tapmain);

		}

		private async void ToMainPage(object sender, EventArgs e)
		{
			

			await Navigation.PushAsync(new MainPage(), true);
				var existingPages = Navigation.NavigationStack.ToList();
				foreach (var page in existingPages)
				{
					Navigation.RemovePage(page);
				}
			

		}


		private async void ToOurmenuPage(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new OurMenu(), true);
			var existingPages = Navigation.NavigationStack.ToList();
			foreach (var page in existingPages)
			{
				Navigation.RemovePage(page);
			}
		}
		private async void ToOffersPage(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new offers(), true);
			var existingPages = Navigation.NavigationStack.ToList();
			foreach (var page in existingPages)
			{
				Navigation.RemovePage(page);
			}
		}
		private async void ToAboutPage(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new offers(), true);
			var existingPages = Navigation.NavigationStack.ToList();
			foreach (var page in existingPages)
			{
				Navigation.RemovePage(page);
			}
		}

	}
}
