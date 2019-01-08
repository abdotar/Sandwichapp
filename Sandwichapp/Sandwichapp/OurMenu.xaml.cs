using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using Sandwichapp.Models;
using Sandwichapp.Models.ConnectionsCFG;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Sandwichapp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OurMenu : ContentPage
	{
		bool initialized = false;
		bool loaded = false;
		AppViewModel viewModel;
		MealTypesStack tobind;
		MealsService mealsService = new MealsService();
		public OurMenu ()
		{
			InitializeComponent ();
			appheader head = new appheader();
			appfooter foot = new appfooter();
			top.Children.Add(head);
			buttom.Children.Add(foot);
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				//menugrid();
				menuholder.IsVisible = true;
				StackLayout mealstack = new StackLayout();
				mealstack.VerticalOptions = LayoutOptions.StartAndExpand;
				mealstack.HorizontalOptions = LayoutOptions.Start;

				mealstack.VerticalOptions = LayoutOptions.StartAndExpand;
				mealstack.HorizontalOptions = LayoutOptions.Start;

				MealTypesStack prosted = new MealTypesStack(1);
				MealTypesStack burgers = new MealTypesStack(3);
				MealTypesStack sandwiches = new MealTypesStack(2);
				MealTypesStack complex = new MealTypesStack(4);
				MealTypesStack shawerma = new MealTypesStack(5);


				// clear the stack to avoid repeating when opens several times
				mealstack.Children.Add(prosted);
				mealstack.Children.Add(burgers);
				mealstack.Children.Add(sandwiches);
				mealstack.Children.Add(complex);
				mealstack.Children.Add(shawerma);



				/*foreach (Meal meal in viewModel.Meals.Where(x => x.MealTypeId == 3))
				{
					string name = meal.Name;
					string imagepath = meal.ImagePath;
					string discription = meal.Discription;
					int price = meal.Price;
					//Grid curentmealgrid = new Grid();
					//curentmealgrid = mealgrid(name, imagepath, discription, price);
					//mealstack.Children.Add(curentmealgrid);
					StackLayout s1 = new StackLayout();
					s1 = menustack(name, imagepath, discription, price);
					mealstack.Children.Add(s1);


				}*/
				menuholder.Children.Add(mealstack);


			}
			else
			{
				menuholder.IsVisible = false;
				Interneterror nonet = new Interneterror();
				center.Children.Add(nonet);
			}
			
			/*
			 * нужные шаги для выполнения
			 * связь с сервером
			 * получить количество элементов меню
			 * создать таблицу для меню по количеству элементов
			 * пополнить созданную таблицу
			 */
			

		}

		private Grid mealgrid(string name, string imagepath, string discription, int price)
		{
			Grid mealgrid = new Grid();
			{
				mealgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
				mealgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
				mealgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
				mealgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Absolute) });
				mealgrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80, GridUnitType.Absolute) });
			};
			Image mealimage = new Image();
			Label mealname = new Label();
			mealname.Text = name;
			mealname.FontSize = 20;
			mealname.FontAttributes = FontAttributes.Bold;
			Label mealdisc = new Label();
			mealdisc.Text = discription;
			mealdisc.FontSize = 16;
			mealdisc.FontAttributes = FontAttributes.Italic;
			Label mealprice = new Label();
			mealprice.Text = Convert.ToString(price);
			if (imagepath == null)
			{
				
				mealgrid.Children.Add(mealname, 0, 0);
				Grid.SetColumnSpan(mealname, 2);
				StackLayout s2 = new StackLayout();
				s2.Children.Add(mealdisc);
				ScrollView s1 = new ScrollView();
				s1.Content = s2;
				mealgrid.Children.Add(s1, 0, 1);
				Grid.SetColumnSpan(s1, 3);
				mealgrid.Children.Add(mealprice, 2, 0);
			}
			else
			{
				mealimage.Source = new UriImageSource
				{
					CachingEnabled = false,
					Uri = new System.Uri(imagepath)
				};
				mealimage.Aspect = Aspect.Fill;
				mealgrid.Children.Add(mealimage, 0, 0); // судя поместить картинку с блюдом
				Grid.SetRowSpan(mealimage, 2);
				mealgrid.Children.Add(mealname, 1, 0);
				Grid.SetColumnSpan(mealname, 2);
				StackLayout s2 = new StackLayout();
				s2.Children.Add(mealdisc);
				ScrollView s1 = new ScrollView();
				s1.Content = s2;
				mealgrid.Children.Add(s1, 1, 1);
				Grid.SetColumnSpan(mealname, 2);
				mealgrid.Children.Add(mealprice, 2, 0);

			}
			return mealgrid;

		}

	/*	private StackLayout menustack(string name, string imagepath, string discription, int price)
		{

			Grid discriptiongrid = new Grid();
			{

				discriptiongrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) });
				discriptiongrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				discriptiongrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
				discriptiongrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			};
			// 
			Grid imagegrid = new Grid();
			{
				imagegrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				imagegrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(370, GridUnitType.Absolute) });
			};
			StackLayout menustack = new StackLayout();
			StackLayout namestack = new StackLayout();
			namestack.VerticalOptions = LayoutOptions.FillAndExpand;
			Image mealimage = new Image();
			Label mealname = new Label();
			Label mealdisc = new Label();
			Label mealprice = new Label();
			mealname.HorizontalTextAlignment = TextAlignment.Center;
			mealname.Text = name;
			mealdisc.Text = discription;
			mealprice.Text ="Цена: " + Convert.ToString(price);
			mealprice.HorizontalOptions = LayoutOptions.FillAndExpand;
			mealprice.VerticalTextAlignment = TextAlignment.End;

			if (imagepath == null)
			{

				namestack.Children.Add(mealname);
				menustack.Children.Add(namestack);
				discriptiongrid.Children.Add(mealdisc, 0,0);
				Grid.SetRowSpan(mealdisc, 1);
				Grid.SetColumnSpan(mealdisc, 2);
				discriptiongrid.Children.Add(mealprice, 1, 1);
				menustack.Children.Add(discriptiongrid);

			}
			else
			{
				

				mealimage.Source = new UriImageSource
				{
					CachingEnabled = false,
					Uri = new System.Uri(imagepath)
				};

				// стеклайоут с индикатором загрузки фото
				imageloader loadimage = new imageloader(mealimage);

				//imagebutton.Image = imagepath;
				mealimage.Aspect = Aspect.Fill;
				namestack.Children.Add(mealname);
				menustack.Children.Add(namestack);

				imagegrid.Children.Add(mealimage, 0, 0);
				imagegrid.Children.Add(loadimage, 0, 0);
				menustack.Children.Add(imagegrid);

				discriptiongrid.Children.Add(mealdisc, 0, 0);
				Grid.SetRowSpan(mealdisc, 1);
				Grid.SetColumnSpan(mealdisc, 2);
				discriptiongrid.Children.Add(mealprice, 1, 1);
				menustack.Children.Add(discriptiongrid);



				menustack.Children.Add(imagegrid);

			}


			return menustack;
		}
		*/

		protected override async void OnAppearing()
		{
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				

				base.OnAppearing();
				//StackLayout mealstack = new StackLayout();
				
				

			}

		}
				

			
	}
}