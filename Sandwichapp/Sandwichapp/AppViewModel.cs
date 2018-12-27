// помошник для считывания данных и их редактирования с сервера
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Sandwichapp.Models.ConnectionsCFG;
using Sandwichapp.Models;

namespace Sandwichapp
{
	public class AppViewModel : INotifyPropertyChanged
	{
		bool initialized = false;   // была ли начальная инициализация
		Meal selectedMeal;  // выбранноу Блюдо
		private bool isBusy;    // идет ли загрузка с сервера

		public ObservableCollection<Meal> Meals { get; set; }
		MealsService mealsService = new MealsService();
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand CreateMealCommand { protected set; get; }
		public ICommand DeleteMealCommand { protected set; get; }
		public ICommand SaveMealCommand { protected set; get; }
		public ICommand BackCommand { protected set; get; }

		public INavigation Navigation { get; set; }

		public bool IsBusy
		{
			get { return isBusy; }
			set
			{
				isBusy = value;
				OnPropertyChanged("IsBusy");
				OnPropertyChanged("IsLoaded");
			}
		}
		public bool IsLoaded
		{
			get { return !isBusy; }
		}

		public AppViewModel()
		{
			Meals = new ObservableCollection<Meal>();
			IsBusy = false;
			//CreateMealCommand = new Command(CreateMeal);
			//DeleteMealCommand = new Command(DeleteMeal);
			//SaveMealCommand = new Command(SaveMeal);
			BackCommand = new Command(Back);
		}

		public Meal SelectedMeal
		{
			get { return selectedMeal; }
			set
			{
				if (selectedMeal != value)
				{
					Meal tempMeal = new Meal()
					{
						Id = value.Id,
						Name = value.Name,
						Discription = value.Discription,
						ImagePath = value.ImagePath,
						Price = value.Price,
						MealTypeId = value.MealTypeId
					};
					selectedMeal = null;
					OnPropertyChanged("SelectedMeal");
					Navigation.PushAsync(new MealsPage(tempMeal, this));
				}
			}
		}
		protected void OnPropertyChanged(string propName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		/*private async void CreateMeal()
		{
			await Navigation.PushAsync(new MealsPage(new Meal(), this));
		}*/
		private void Back()
		{
			Navigation.PopAsync();
		}

		public async Task GetMeals()
		{
			if (initialized == true) return;
			IsBusy = true;
			IEnumerable<Meal> meals = await mealsService.Get();

			// очищаем список
			//Friends.Clear();
			while (Meals.Any())
				Meals.RemoveAt(Meals.Count - 1);

			// добавляем загруженные данные
			foreach (Meal f in meals)
				Meals.Add(f);
			IsBusy = false;
			initialized = true;
		}
		/*private async void SaveMeal(object mealObject)
		{
			Meal meal = mealObject as Meal;
			if (meal != null)
			{
				IsBusy = true;
				// редактирование
				if (meal.Id > 0)
				{
					Meal updatedMeal = await mealsService.Update(meal);
					// заменяем объект в списке на новый
					if (updatedMeal != null)
					{
						int pos = Meals.IndexOf(updatedMeal);
						Meals.RemoveAt(pos);
						Meals.Insert(pos, updatedMeal);
					}
				}
				// добавление
				else
				{
					Meal addedMeal = await mealsService.Add(meal);
					if (addedMeal != null)
						Meals.Add(addedMeal);
				}
				IsBusy = false;
			}
			Back();
		}
		private async void DeleteMeal(object mealObject)
		{
			Meal meal = mealObject as Meal;
			if (meal != null)
			{
				IsBusy = true;
				Meal deletedMeal = await mealsService.Delete(meal.Id);
				if (deletedMeal != null)
				{
					Meals.Remove(deletedMeal);
				}
				IsBusy = false;
			}
			Back();
		}*/

	}
}
