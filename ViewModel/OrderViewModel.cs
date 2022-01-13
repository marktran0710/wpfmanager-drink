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
    class OrderViewModel : BaseViewModel
    {
        private ObservableCollection<Order> _List;
        public ObservableCollection<Order> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<Status> _Status;
        public ObservableCollection<Status> Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private Order _SelectedItem;
        public Order SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name_customer = SelectedItem.Name_customer;
                    Phone_number = SelectedItem.Phone_number;
                    User_id = SelectedItem.User_id;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    SelectedStatus = SelectedItem.Status;


                }
            }
        }


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

        private Status _SelectedStatus;

        public Status SelectedStatus
        {
            get => _SelectedStatus; set
            {
                _SelectedStatus = value; OnPropertyChanged();

            }
        }

        private string _IsVisible;
        public string IsVisible
        {
            get => _IsVisible; set
            {
                _IsVisible = value; OnPropertyChanged();

            }
        }


        
        public string ContentSeacrh { get; set; }

        private string _Name_customer;
        public string Name_customer { get => _Name_customer; set { _Name_customer = value; OnPropertyChanged(); } }

        private string _Phone_number;
        public string Phone_number { get => _Phone_number; set { _Phone_number = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private int? _User_id;
        public int? User_id { get => _User_id; set { _User_id = value; OnPropertyChanged(); } }



        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand FilterAscendCommand { get; set; }
        public ICommand FilterDescendCommand { get; set; }


        public OrderViewModel()
        {
            IsVisible = "Hidden";
            List = new ObservableCollection<Order>(DataProvider.Ins.DB.Orders);
            Status = new ObservableCollection<Status>(DataProvider.Ins.DB.Status);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List);
            CollectionViewSource.GetDefaultView(List).Refresh();

            //Load Data
            List.Clear();
            var Order_data= DataProvider.Ins.DB.Orders.Select(x => x);
            foreach (var item in Order_data)
            {
                List.Add(item);
            }

            AddCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {

                var Order = new Order() { Name_customer = Name_customer, Address = Address, Email = Email, Order_status = SelectedStatus.Id_status, Phone_number = Phone_number, User_id = 1,Order_date=DateTime.Now };
                DataProvider.Ins.DB.Orders.Add(Order);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Order);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Orders.Where(x => x.Id_order == SelectedItem.Id_order);
                if (displayList != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                var Order = DataProvider.Ins.DB.Orders.Where(x => x.Id_order == SelectedItem.Id_order).FirstOrDefault();
                int number;
                if (!int.TryParse(Phone_number, out number))
                {
                    IsVisible = "Visible";
                    Phone_number = "";
                    return;
                }
                IsVisible = "Hidden";
                Order.Phone_number = Phone_number;
                Order.User_id = User_id;
                Order.Status = SelectedStatus;
                Order.Name_customer = Name_customer;
                Order.User_id = User_id;
                Order.Email = Email;
                Order.Address = Address;
                DataProvider.Ins.DB.SaveChanges();

                DataProvider.Ins.DB.Entry(List[List.Count() - 1]).Reload();
                var Orders = DataProvider.Ins.DB.Orders.Select(x => x);

                List.Clear();
                foreach (var item in Orders)
                {
                    List.Add(item);
                }

            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Orders.Where(x => x.Id_order == SelectedItem.Id_order);

                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Order = DataProvider.Ins.DB.Orders.Where(x => x.Id_order == SelectedItem.Id_order).FirstOrDefault();
                DataProvider.Ins.DB.Orders.Remove(Order);
                DataProvider.Ins.DB.SaveChanges();

                List.Remove(Order);
                Name_customer = "";
                Address = "";
                Email = "";
                Phone_number = "";
                SelectedStatus = null;


            });

            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContentSearch == string.Empty)
                {
                    var Orders = DataProvider.Ins.DB.Orders.Select(x => x);
                    List.Clear();
                    foreach (var item in Orders)
                    {
                        List.Add(item);
                    }
                    return false;
                }  
                return true;
            }, (p) =>
            {
                var Order = DataProvider.Ins.DB.Orders.Where(x => x.Name_customer.Contains(ContentSeacrh));
                List.Clear();
                foreach (var item in Order)
                {
                    List.Add(item);
                }
                Name_customer = "";
                Address = "";
                Email = "";
                Phone_number = "";
                SelectedStatus = null;


            });

            FilterAscendCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(Order.Order_date), ListSortDirection.Descending));

                view.SortDescriptions.Add(new SortDescription(nameof(Order.Order_date), ListSortDirection.Ascending));

                Name_customer = "";
                Address = "";
                Email = "";
                Phone_number = "";
                SelectedStatus = null;


            });

            FilterDescendCommand = new RelayCommand<object>((p) =>
            {
               
                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(Order.Order_date), ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription(nameof(Order.Order_date), ListSortDirection.Descending));

                Name_customer = "";
                Address = "";
                Email = "";
                Phone_number = "";
                SelectedStatus = null;


            });

        } }
}
