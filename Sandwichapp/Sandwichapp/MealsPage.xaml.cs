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
	public partial class MealsPage : ContentPage
	{
		public Meal Model { get; private set; }
		public AppViewModel ViewModel { get; private set; }
		public MealsPage(Meal model, AppViewModel viewModel)
		{
			InitializeComponent();
			Model = model;
			ViewModel = viewModel;
			this.BindingContext = this;
		}
	}
}