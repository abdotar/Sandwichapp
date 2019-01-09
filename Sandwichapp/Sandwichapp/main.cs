using Xamarin.Forms;


namespace Sandwichapp
{
    public class main :StackLayout
    {
		public main()
		{
			ScrollView mainscrol = new ScrollView();// скрол вюхи
			StackLayout stackinScrol = new StackLayout();//стек который будет хранить элементы сын mainscrol
			Grid mainoffergrid = new Grid();// this grid contains main offer image and child of stackinScrol whe put grid to adjust the height of main offer
			StackLayout newsStac = new StackLayout();//this stack contains the rest news on the main page and its child of stackinscroll
			stackinScrol.VerticalOptions = LayoutOptions.FillAndExpand;
			stackinScrol.HorizontalOptions = LayoutOptions.FillAndExpand;
			mainoffergrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(500, GridUnitType.Absolute) });
			mainoffergrid.ColumnDefinitions.Add(new ColumnDefinition());
			Image mainimage = new Image();//image of main offer on main page child of mainoffergrid
			mainimage.Source = new UriImageSource
			




			/*
			 * to do
			 * add script wich takes main offer image path from Db via web api
			 */

			{
				CachingEnabled = false,
				Uri = new System.Uri("https://sandwichbistro.ru/img/Offers/10percentOffer.jpg")
			};


			// стеклайоут с индикатором загрузки фото
			imageloader loadimage = new imageloader(mainimage);

			
			mainoffergrid.Children.Add(loadimage, 0, 0);
			if (!loadimage.IsVisible)
			{
				mainimage.Aspect = Aspect.Fill;//fill the grid with image
				mainoffergrid.Children.Add(mainimage, 0, 0);
			}


			//создаем  рамку для главной акции
			Frame mainofferFrame = new Frame();
			mainofferFrame.Padding = 0;
			mainofferFrame.Margin = 20;
			mainofferFrame.CornerRadius = 8;
			mainofferFrame.Content = mainoffergrid;
			stackinScrol.Children.Add(mainofferFrame);

			/*
			 * connect to database and fil newsStack with last 10 news
			 * stackinScrol.Children.Add(newsStack);
			 */


			//youtube video testing

			
			
			//NewsStack st1 = new NewsStack();

			//stackinScrol.Children.Add(st1);

			mainscrol.Content = stackinScrol;
			this.Children.Add(mainscrol);
		}
    }
}
