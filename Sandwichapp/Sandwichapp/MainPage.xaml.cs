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



namespace Sandwichapp
{
	public partial class MainPage : ContentPage
	{
		public ObservableCollection<Offer> Offers { get; set; }
		OffersService Service = new OffersService();
		StackLayout holder = new StackLayout();
		StackLayout mainoffersstack = new StackLayout();
		offerscollection ofcl = new offerscollection();
		public MainPage()
		{



			Offers = new ObservableCollection<Offer>();
			connecttoserver();
			InitializeComponent();
			appheader head = new appheader();
			appfooter foot = new appfooter();
			top.Children.Add(head);
			buttom.Children.Add(foot);
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				ScrollView mainscrol = new ScrollView();// скрол вюхи
				
				Image mainimage = new Image();//image of main offer 
				mainimage.Source = new UriImageSource
				{
					CachingEnabled = false,
					Uri = new System.Uri("https://sandwichbistro.ru/img/Offers/10percentOffer.jpg")
				};


				offersstack offers = new offersstack();
				NewsStacklayout news = new NewsStacklayout();
				Offerframe offer1 = new Offerframe("Akcia 1"," akcia dla vladelcev prilazhuhi",mainimage);
				//main mainoffer = new main();
				mainoffersstack.Children.Add(offer1);
				holder.Children.Add(mainoffersstack);
				getmainoffers();
				holder.Children.Add(offers);
				holder.Children.Add(news);
				mainscrol.Content = holder;
				center.Children.Add(mainscrol);
				
			}
			else
			{
				Interneterror nonet = new Interneterror();
				center.Children.Add(nonet);
			}

		}
		private async void nointernet()
		{
			await DisplayAlert("Где интернет?", "Связь с итернетом потеряна", "ОK");
		}
		private void getmainoffers()
		{
			foreach(Offer offer in Offers)
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
				

				
			}
		}
		public async Task GetOffers()
		{
			IEnumerable<Offer> offers = await Service.Get();

			// очищаем список
			//Friends.Clear();
			while (Offers.Any())
				Offers.RemoveAt(Offers.Count - 1);

			// добавляем загруженные данные
			foreach (Offer f in offers)
				Offers.Add(f);
		}

		protected async void connecttoserver()

		{
			await GetOffers();
			//add method to run trough all types and create diferent albles
		}





		protected override void OnAppearing()
		{
		}





	}
}
