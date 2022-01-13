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
    class OrderDetailViewModel : BaseViewModel
    {
        private ObservableCollection<Order_Detail> _List;
        public ObservableCollection<Order_Detail> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<Order> _Order;
        public ObservableCollection<Order> Order { get => _Order; set { _Order = value; OnPropertyChanged(); } }

        private ObservableCollection<Beverage> _Beverage;
        public ObservableCollection<Beverage> Beverage { get => _Beverage; set { _Beverage = value; OnPropertyChanged(); } }

        private Order_Detail _SelectedItem;
        public Order_Detail SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    
                    Num = SelectedItem.Num;
                    Note = SelectedItem.Note;
                    SelectedOrder = SelectedItem.Order;
                    SelectedBeverage = SelectedItem.Beverage;
                    
                }
            }
        }
       

        private int? _Num;
        public int? Num { get => _Num; set { _Num = value; OnPropertyChanged(); } }

        private string _Note;
        public string Note { get => _Note; set { _Note = value; OnPropertyChanged(); } }

        private string _Price;
        public string Price { get => _Price; set { _Price = value; OnPropertyChanged(); } }

        private Beverage _SelectedBeverage;
        public Beverage SelectedBeverage
        {
            get => _SelectedBeverage;
            set
            {
                _SelectedBeverage = value;
                OnPropertyChanged();
            }
        }

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get => _SelectedOrder;
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged();
            }
        }

        private int ContentSeacrh { get; set; }
        

        private string _SelectedContentSearch;
        public string SelectedContentSearch
        {
            get => _SelectedContentSearch; set
            {
                _SelectedContentSearch = value; OnPropertyChanged();
                if (_SelectedContentSearch != null)
                {
                    int number;
                    if (!int.TryParse(_SelectedContentSearch, out number))
                    {
                        var Order_Details = DataProvider.Ins.DB.Order_Detail.Select(x => x);

                        List.Clear();
                        foreach (var item in Order_Details)
                        {
                            List.Add(item);
                        }
                        return;
                    }
                    ContentSeacrh = Int32.Parse(_SelectedContentSearch);
                }
            }
        }




        public void ClearFill()
        {
            SelectedBeverage = null;
            Num = null;
            Note = null;
            SelectedBeverage = null;
        }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ICommand FilterAscendCommand { get; set; }

        public ICommand FilterDescendCommand { get; set; }


        public OrderDetailViewModel()
        {
            List = new ObservableCollection<Order_Detail>(DataProvider.Ins.DB.Order_Detail);
            Order = new ObservableCollection<Order>(DataProvider.Ins.DB.Orders);
            Beverage = new ObservableCollection<Beverage>(DataProvider.Ins.DB.Beverages);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(List);
            CollectionViewSource.GetDefaultView(List).Refresh();

            AddCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, (p) =>
            {

                var Order_Detail = new Order_Detail() { Num = Num, Note = Note, Order_id = SelectedOrder.Id_order, Beverage_id = SelectedBeverage.Id_beverage };
                DataProvider.Ins.DB.Order_Detail.Add(Order_Detail);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Order_Detail);

                DataProvider.Ins.DB.Entry(List[List.Count() - 1]).Reload();
                var Order_Details = DataProvider.Ins.DB.Order_Detail.Select(x => x);

                List.Clear();
                foreach (var item in Order_Details)
                {
                    List.Add(item);
                }

                //Update for Order UI totalmoney

                for (int i = 0; i < Order.Count(); i++)
                {
                    if (Order[i].Id_order == Order_Detail.Order_id)
                    {
                        DataProvider.Ins.DB.Entry(Order[i]).Reload();

                    }
                }
                ClearFill();
            });
            
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Order_Detail.Where(x => x.Id_orderdetail ==SelectedItem.Id_orderdetail);
                
                if ( (displayList != null || displayList.Count() == 0) )
                    return true;
                return false;
            }, (p) =>
            {
                var OrderDetail = DataProvider.Ins.DB.Order_Detail.Where(x => x.Id_orderdetail == SelectedItem.Id_orderdetail).SingleOrDefault();
                OrderDetail.Note=Note;
                OrderDetail.Num=Num;
                OrderDetail.Beverage = SelectedBeverage;

                DataProvider.Ins.DB.SaveChanges();

                //Find record changed
                for(int i = 0; i < List.Count(); i++)
                {
                    if (List[i].Id_orderdetail == SelectedItem.Id_orderdetail)
                    {
                        DataProvider.Ins.DB.Entry(List[i]).Reload();

                    }
                }

                //Load to UI
                var Order_Details = DataProvider.Ins.DB.Order_Detail.Select(x => x);

                List.Clear();
                foreach (var item in Order_Details)
                {
                    List.Add(item);
                }

                //Update for Order UI totalmoney

                for (int i = 0; i < Order.Count(); i++)
                {
                    if (Order[i].Id_order == OrderDetail.Order_id)
                    {
                        DataProvider.Ins.DB.Entry(Order[i]).Reload();

                    }
                }
                ClearFill();
                
            });


            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Order_Detail.Where(x => x.Id_orderdetail == SelectedItem.Id_orderdetail);

                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Order_Detail = DataProvider.Ins.DB.Order_Detail.Where(x => x.Id_orderdetail == SelectedItem.Id_orderdetail).FirstOrDefault();
                DataProvider.Ins.DB.Order_Detail.Remove(Order_Detail);
                DataProvider.Ins.DB.SaveChanges();

                List.Remove(Order_Detail);

            });
            SearchCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedContentSearch == string.Empty)
                    return false;
                return true;
            }, (p) =>
            {

                
                var OrderDetail = DataProvider.Ins.DB.Order_Detail.Where(x => x.Order_id==ContentSeacrh);
                List.Clear();
                foreach (var item in OrderDetail)
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
                view.SortDescriptions.Remove(new SortDescription(nameof(Order_Detail.Total_money), ListSortDirection.Descending));

                view.SortDescriptions.Add(new SortDescription(nameof(Order_Detail.Total_money), ListSortDirection.Ascending));

                ClearFill();


            });

            FilterDescendCommand = new RelayCommand<object>((p) =>
            {

                return true;
            }, (p) =>
            {
                view.SortDescriptions.Remove(new SortDescription(nameof(Order_Detail.Total_money), ListSortDirection.Ascending));
                view.SortDescriptions.Add(new SortDescription(nameof(Order_Detail.Total_money), ListSortDirection.Descending));

                ClearFill();

            });
        }
    }
}
