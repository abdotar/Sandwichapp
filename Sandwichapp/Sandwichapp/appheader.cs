using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;
using Xamarin.Forms.Xaml;

namespace Sandwichapp
{
    public class appheader : StackLayout
    {
		public appheader()
		{
			Grid headergrid = new Grid();
			headergrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50,GridUnitType.Absolute) });

			//добовляем столбцы
			int i = 6;
			while(i>0)
			{
				headergrid.ColumnDefinitions.Add(new ColumnDefinition());
				i--;
			}

			Image image = new Image();
			image.Aspect = Aspect.Fill;
			image.Source = ImageSource.FromResource("Sandwichapp.images.logo2.png");


			headergrid.Children.Add(image, 0, 0);
			Grid.SetColumnSpan(image, 3);

			this.Children.Add(headergrid);
		}
    }
}
