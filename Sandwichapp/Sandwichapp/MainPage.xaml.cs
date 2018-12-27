using Xamarin.Forms;
using Plugin.Connectivity;



namespace Sandwichapp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			appheader head = new appheader();
			appfooter foot = new appfooter();
			top.Children.Add(head);
			buttom.Children.Add(foot);
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected == true)
			{
				main mainoffer = new main();
				center.Children.Add(mainoffer);
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

		protected override async void OnAppearing()
		{
			center.Children.Clear();
			main mainoffer = new main();
			center.Children.Add(mainoffer);
		}





	}
}
