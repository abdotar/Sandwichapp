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
    public class Interneterror: StackLayout
    {
		public Interneterror()
		{
			Label nonetlabel = new Label();
			nonetlabel.Text = "Нет интернет соединения";
			nonetlabel.VerticalOptions = LayoutOptions.CenterAndExpand;
			nonetlabel.HorizontalOptions = LayoutOptions.CenterAndExpand;
			this.Children.Add(nonetlabel);
			this.VerticalOptions = LayoutOptions.FillAndExpand;
			this.HorizontalOptions = LayoutOptions.FillAndExpand;
		}
	}
}
