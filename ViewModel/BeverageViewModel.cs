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
    public class BeverageViewModel : BaseViewModel
    {
        private ObservableCollection<Beverage> _List;
        public ObservableCollection<Beverage> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<TypeOfBeverage> _TypeOfBeverage;
        public ObservableCollection<TypeOfBeverage> TypeOfBeverage { get => _TypeOfBeverage; set { _TypeOfBeverage = value; OnPropertyChanged(); } }

        private Beverage _SelectedItem;
        public Beverage SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                   
                    Name_beverage = SelectedItem.Name_beverage;
                    Description = SelectedItem.Description;
                    
                    Price = SelectedItem.Price;
                    Manufacturing_date = SelectedItem.Manufacturing_date;
                    Expiry_date = SelectedItem.Expiry_date;
                    Total_input = SelectedItem.Total_input;
                    Inventory = SelectedItem.Inventory;
                    SelectedTypeOfBeverage = SelectedItem.TypeOfBeverage;

                }
            }
        }

        private TypeOfBeverage _SelectedTypeOfBeverage;

        public TypeOfBeverage SelectedTypeOfBeverage
        {
            get => _SelectedTypeOfBeverage; set
            {
                _SelectedTypeOfBeverage = value; OnPropertyChanged();

            }
        }

        private string _Name_beverage;
        public string Name_beverage { get => _Name_beverage; set { _Name_beverage = value; OnPropertyChanged(); } }
        private string _Description;
        public string Description { get => _Description; set { _Description = value; OnPropertyChanged(); } }

        private double? _Price;
        public double? Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private DateTime _Manufacturing_date;
        public DateTime Manufacturing_date { get => _Manufacturing_date; set { _Manufacturing_date = value; OnPropertyChanged(); } }

        private DateTime? _Expiry_date;
        public DateTime? Expiry_date { get => _Expiry_date; set { _Expiry_date = value; OnPropertyChanged(); } }
        
        private int? _Total_input;
        public int? Total_input { get => _Total_input; set { _Total_input = value; OnPropertyChanged(); } }

        private int? _Inventory;
        public int? Inventory { get => _Inventory; set { _Inventory = value; OnPropertyChanged(); } }
        private string _SelectedContentSearch;
        public string SelectedContentSearch
        {
            get => _SelectedContentSearch; set
            {
                _SelectedContentSearch = value; OnPropertyChanged();
                if (_SelectedContentSearch != null)
                {
                    ContentSeacrh = _SelectedContentSearch;
                }
            }
        }

        private string ContentSeacrh { get; set; }
        

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand FilterAscendCommand { get; set; }

        public ICommand FilterDescendCommand { get; set; }
        public ICommand ChartCommand { get; set; }



        public void ClearFill()
        {
            Name_beverage = null;
            SelectedTypeOfBeverage = null;
            Manufacturing_date = DateTime.Now;
            Expiry_date=DateTime.Now;
            Price = null;
            Total_input = null;
            Inventory = null;
            Description = null;
        }

        public BeverageViewModel()
        {
            List = new ObservableCollection<Beverage>(DataProvider.Ins.DB.Beverages);
            TypeOfBeverage = new ObservableCollection<TypeOfBeverage>(DataProvider.Ins.DB.TypeOfBeverages);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List);
            view.SortDescriptions.Remove(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Ascending));
            view.SortDescriptions.Remove(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Descending));
            ClearFill();

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                var Beverage = new Beverage() { Name_beverage = Name_beverage, Description = Description, Price = Price, Typeofbeverage_id = SelectedTypeOfBeverage.Id_TypeOfBeverage,  Total_input = Total_input, Expiry_date = Expiry_date, Manufacturing_date = Manufacturing_date,Date_in=DateTime.Now };
                DataProvider.Ins.DB.Beverages.Add(Beverage);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Beverage);

                DataProvider.Ins.DB.Entry(List[List.Count() - 1]).Reload();
                var Beverages = DataProvider.Ins.DB.Beverages.Select(x => x);

                List.Clear();
                foreach (var item in Beverages)
                {
                    List.Add(item);
                }
                ClearFill();
            });
            
            EditCommand = new RelayCommand<object>((p) =>
            {
                if ( SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Beverages.Where(x => x.Id_beverage == SelectedItem.Id_beverage);
                if ((displayList != null || displayList.Count() == 0))
                    return true;
                return false;
            }, (p) =>
            {
                var Beverage = DataProvider.Ins.DB.Beverages.Where(x => x.Id_beverage == SelectedItem.Id_beverage).SingleOrDefault();
                Beverage.Name_beverage= Name_beverage;
                Beverage.Description = Description;
                Beverage.Inventory = Inventory;
                Beverage.Price = Price;
                Beverage.TypeOfBeverage=SelectedTypeOfBeverage;
                Beverage.Manufacturing_date=Manufacturing_date;
                Beverage.Expiry_date = Expiry_date;
                Beverage.Total_input = Total_input;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Name_beverage= Name_beverage;
                DataProvider.Ins.DB.Entry(List[List.Count() - 1]).Reload();

            });
            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Beverages.Where(x => x.Id_beverage == SelectedItem.Id_beverage);

                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Beverage = DataProvider.Ins.DB.Beverages.Where(x => x.Id_beverage == SelectedItem.Id_beverage).FirstOrDefault();
                DataProvider.Ins.DB.Beverages.Remove(Beverage);
                DataProvider.Ins.DB.SaveChanges();

                List.Remove(Beverage);
                ClearFill();



            });

            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContentSearch == string.Empty)
                {
                    var Beverages = DataProvider.Ins.DB.Beverages.Select(x=>x);
                    List.Clear();
                    foreach (var item in Beverages)
                    {
                        List.Add(item);
                    }
                    return false;
                }
                return true;
            }, (p) =>
            {
                var Beverages = DataProvider.Ins.DB.Beverages.Where(x => x.Name_beverage.Contains(ContentSeacrh));
                List.Clear();
                foreach (var item in Beverages)
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
                view.SortDescriptions.Remove(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Descending));

                view.SortDescriptions.Add(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Ascending));

                ClearFill();


            });

            FilterDescendCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription(nameof(Beverage.Date_in), ListSortDirection.Descending));

                ClearFill();


            });
            ChartCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Chart wd = new Chart(); wd.ShowDialog(); });

        }


    }
}
