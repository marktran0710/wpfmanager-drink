using manager_drink.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace manager_drink.ViewModel
{
    class TypeOfBeverageViewModel : BaseViewModel
    {
        private ObservableCollection<TypeOfBeverage> _List;
        public ObservableCollection<TypeOfBeverage> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private TypeOfBeverage _SelectedItem;
        public TypeOfBeverage SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    
                    Name_TypeOfBeverage = SelectedItem.Name_TypeOfBeverage;

                }
            }
        }
        private string _Name_TypeOfBeverage;
        public string Name_TypeOfBeverage { get => _Name_TypeOfBeverage; set { _Name_TypeOfBeverage = value; OnPropertyChanged(); } }

        private string _SelectedContentSearch;
        public string SelectedContentSearch
        {
            get => _SelectedContentSearch; set
            {
                _SelectedContentSearch = value; OnPropertyChanged();
                if (_SelectedContentSearch != null)
                {
                    ContentSearch = _SelectedContentSearch;
                }

            }
        }

        private string _ContentSeacrh { get; set; }
        public string ContentSearch { get => _ContentSeacrh; set { _ContentSeacrh = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ICommand FilterAscendCommand { get; set; }
        public ICommand FilterDescendCommand { get; set; }

        public void ClearFill()
        {
            Name_TypeOfBeverage = null;

        }

        public TypeOfBeverageViewModel()
        {
            List = new ObservableCollection<TypeOfBeverage>(DataProvider.Ins.DB.TypeOfBeverages);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List);
            CollectionViewSource.GetDefaultView(List).Refresh();
            AddCommand = new RelayCommand<object>((p) =>
            {
               
                return true;
            }, (p) =>
            {
                var TypeOfBeverage = new TypeOfBeverage() { Name_TypeOfBeverage = Name_TypeOfBeverage,Created_at=DateTime.Now };
                DataProvider.Ins.DB.TypeOfBeverages.Add(TypeOfBeverage);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(TypeOfBeverage);
            });
           
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList1 = DataProvider.Ins.DB.TypeOfBeverages.Where(x => x.Name_TypeOfBeverage == Name_TypeOfBeverage);
                

                if (  (displayList1 != null || displayList1.Count() == 0) )
                    return true;
                return false;
            }, (p) =>
            {
                var TypeOfBeverage = DataProvider.Ins.DB.TypeOfBeverages.Where(x => x.Id_TypeOfBeverage == SelectedItem.Id_TypeOfBeverage).SingleOrDefault();
                TypeOfBeverage.Name_TypeOfBeverage= Name_TypeOfBeverage;
                
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Name_TypeOfBeverage = Name_TypeOfBeverage;

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.TypeOfBeverages.Where(x => x.Id_TypeOfBeverage == SelectedItem.Id_TypeOfBeverage);
                   
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var TypeOfBeverage = DataProvider.Ins.DB.TypeOfBeverages.Where(x => x.Id_TypeOfBeverage == SelectedItem.Id_TypeOfBeverage).FirstOrDefault();
                DataProvider.Ins.DB.TypeOfBeverages.Remove(TypeOfBeverage);
                DataProvider.Ins.DB.SaveChanges();

                List.Remove(TypeOfBeverage);

            });
            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContentSearch == string.Empty)
                {
                    var typeOfBeverages = DataProvider.Ins.DB.TypeOfBeverages.Select(x => x);
                    List.Clear();
                    foreach (var item in typeOfBeverages)
                    {
                        List.Add(item);
                    }
                    return false;
                }
                return true;
                
            }, (p) =>
            {


                var typeOfBeverages = DataProvider.Ins.DB.TypeOfBeverages.Where(x => x.Name_TypeOfBeverage.Contains(ContentSearch));
                List.Clear();
                foreach (var item in typeOfBeverages)
                {
                    List.Add(item);
                }
                ClearFill();


            });

            FilterAscendCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(TypeOfBeverage.Name_TypeOfBeverage), ListSortDirection.Descending));

                view.SortDescriptions.Add(new SortDescription(nameof(TypeOfBeverage.Name_TypeOfBeverage), ListSortDirection.Ascending));

                ClearFill();


            });

            FilterDescendCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(TypeOfBeverage.Name_TypeOfBeverage), ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription(nameof(TypeOfBeverage.Name_TypeOfBeverage), ListSortDirection.Descending));

                ClearFill();

            });
        }


    }
   
}
