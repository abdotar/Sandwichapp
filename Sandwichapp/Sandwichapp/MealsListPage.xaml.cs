using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sandwichapp.Models;

namespace Sandwichapp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MealsListPage : ContentPage
	{
		AppViewModel viewModel;
		public MealsListPage()
		{
			InitializeComponent();
			viewModel = new AppViewModel() { Navigation = this.Navigation };
			BindingContext = viewModel;
		}

		protected override async void OnAppearing()
		{
			await viewModel.GetMeals();
			base.OnAppearing();
		}
	}
}