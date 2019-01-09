using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms;
using Sandwichapp.Models;
using System.Linq;
using Sandwichapp.Models.ConnectionsCFG;
using Sandwichapp;
using System.Collections.ObjectModel;

namespace Sandwichapp
{
    class NewsCollection
    {

		private bool isBusy;    // идет ли загрузка с сервера
		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				isBusy = value;
			}
		}
		public bool IsLoaded
		{
			get { return !isBusy; }
		}


		public ObservableCollection<New> News { get; set; }
		NewsService Service = new NewsService();

		public NewsCollection()
		{
			isBusy = false;
			News = new ObservableCollection<New>();
			connecttoserver();
		}


		public async Task GetNews()
		{
			IEnumerable<New> news = await Service.Get();

			// очищаем список
			//Friends.Clear();
			while (News.Any())
				News.RemoveAt(News.Count - 1);

			// добавляем загруженные данные
			foreach (New n in news)
				News.Add(n);

			News = new ObservableCollection<New>(News.OrderByDescending(x => x.Id));
		}

		protected async void connecttoserver()

		{
			await GetNews();
			isBusy = false;
			//add method to run trough all types and create diferent albles
		}
	}
}
