
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sandwichapp

{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
			//MainPage = new NavigationPage(new MealsListPage());

		}
		public interface IOpenApp
		{
			void OpenExternalApp();
		}

		protected override void OnStart()
		{
			AppCenter.Start("e37f4e8c-133a-4dbf-8bcb-ed6ace654895", typeof(Push));
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
