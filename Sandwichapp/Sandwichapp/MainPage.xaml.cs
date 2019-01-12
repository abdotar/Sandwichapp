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
		StackLayout holder = new StackLayout();

		bool busy;

		public MainPage()
		{
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
				offersview offers = new offersview();
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



		protected override void OnAppearing()
		{
			
		}





	}
}
