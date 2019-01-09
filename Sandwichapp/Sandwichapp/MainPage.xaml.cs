using System;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sandwichapp.Models.ConnectionsCFG;
using Sandwichapp.Models;

//todo try to rise the preformance be changing the frame to stacks

namespace Sandwichapp
{
	public partial class MainPage : ContentPage
	{


		//to do  create a class wich connects to server seperatly from main page

		offersstack offers = new offersstack();
		offerscollection of1 = new offerscollection();
		//button wich expands with news
		ActivityIndicator act1 = new ActivityIndicator();
		StackLayout indicatorholder = new StackLayout();


		StackLayout holder = new StackLayout();
		StackLayout mainoffersstack = new StackLayout();

		bool busy;

		public MainPage()
		{
			busy = true;
			InitializeComponent();
			appheader head = new appheader();
			appfooter foot = new appfooter();
			top.Children.Add(head);
			buttom.Children.Add(foot);
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				
				//button wich expands with offers
				
				ScrollView mainscrol = new ScrollView();// скрол вюхи
				NewsView news = new NewsView();
				holder.Children.Add(indicatorholder);
				holder.Children.Add(mainoffersstack);
				holder.Children.Add(offers);
				holder.Children.Add(news);
				act1.IsRunning = true;
				indicatorholder.HorizontalOptions = LayoutOptions.CenterAndExpand;
				indicatorholder.VerticalOptions = LayoutOptions.Center;
				indicatorholder.Orientation = StackOrientation.Horizontal;
				Label loadinglabel = new Label();
				loadinglabel.VerticalOptions = LayoutOptions.CenterAndExpand;
				loadinglabel.Text = "Загрузка данных.....";
				indicatorholder.Children.Add(act1);
				indicatorholder.Children.Add(loadinglabel);
				mainscrol.Content = holder;

				if (of1.IsLoaded)
				{
					IsBusy = false;
					fillmainofferframe();
					fillsecondaryoffersframe();
					center.Children.Add(mainscrol);
				}
				
				
			}
			else
			{
				Interneterror nonet = new Interneterror();
				center.Children.Add(nonet);
			}

		}


		protected async void fillmainofferframe()

		{
			await of1.GetOffers();
			foreach (Offer offer in of1.Offers.Where(x => x.IsMain == true && x.IsActive == true))
			{
				if (offer.ImagePath != null)
				{
					Image offerimage = new Image();
					offerimage.Source = new UriImageSource
					{
						CachingEnabled = false,
						Uri = new System.Uri(offer.ImagePath)
					};
					Offerframe offer1 = new Offerframe(offer.Name, offer.Discription, offerimage);
					mainoffersstack.Children.Add(offer1);
				}
				indicatorholder.IsVisible = IsBusy;


			}

		}

		protected async void fillsecondaryoffersframe()
		{
			await of1.GetOffers();
			foreach (Offer offer in of1.Offers.Where(x => x.IsMain == false && x.IsActive == true))
			{
				if (offer.ImagePath != null)
				{
					Image offerimage = new Image();
					offerimage.Source = new UriImageSource
					{
						CachingEnabled = false,
						Uri = new System.Uri(offer.ImagePath)
					};
					Offerframe offer1 = new Offerframe(offer.Name, offer.Discription, offerimage);

					//offers.fillexpander(offer1);
					offers.expanded.Children.Add(offer1);
					//mainoffersstack.Children.Add(offer1);
				}
				indicatorholder.IsVisible = IsBusy;


			}
			busy = false;
		}


		protected override void OnAppearing()
		{
			
		}





	}
}
