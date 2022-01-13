
using manager_drink.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;

namespace manager_drink.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        private ObservableCollection<Order> _List;
        public ObservableCollection<Order> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Beverage> _Beverages;
        public ObservableCollection<Beverage> Beverages { get => _Beverages; set { _Beverages = value; OnPropertyChanged(); } }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand OrderCommand { get; set; }
        public ICommand OrderDetailCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand BeverageCommand { get; set; }
        public ICommand TypeOfBeverageCommand { get; set; }
        public ICommand RoleCommand { get; set; }
        public ICommand HomeCommand { get; set; }


        public SeriesCollection Data { get; set; }
        
        public string[] Labels { get; set; }
        public Func<double,string> Fomartter { get; set; }

        private int _TotalInput;
        public int TotalInput { get => _TotalInput; set { _TotalInput = value; OnPropertyChanged(); } }
        private int _TotalOutput;
        public int TotalOutput { get => _TotalOutput; set { _TotalOutput = value; OnPropertyChanged(); } }
        private int _InventoryUI;
        public int InventoryUI { get => _InventoryUI; set { _InventoryUI = value; OnPropertyChanged(); } }
        public MainViewModel()
        {
            var Total_Input=DataProvider.Ins.DB.Beverages.Select(x => x.Total_input).Sum().ToString();
            if (Total_Input != null)
            {
                TotalInput = Int32.Parse(Total_Input);
            }

            var Inventory = DataProvider.Ins.DB.Beverages.Select(x => x.Inventory).Sum().ToString();
            if (Inventory != null)
            {
                InventoryUI = Int32.Parse(Inventory);
            }

            TotalOutput = TotalInput-InventoryUI;
            
            ChartValues<double> ChartValues = new ChartValues<double>(); 
            
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Fomartter =value=>value.ToString("N");
            Data = new SeriesCollection()
            {
                //new ColumnSeries
                //{

                //    Values=new ChartValues<double>{10,50,39,50}
                //},
                new ColumnSeries
                {
                    Title="2021",
                    Values=ChartValues
                },

            };

            DateTime Jan = new DateTime(2022,01,01,0,0,0);
            DateTime Feb = new DateTime(2022, 02, 01, 0, 0, 0);
            DateTime Mar = new DateTime(2022, 03, 01, 0, 0, 0);
            DateTime Apr = new DateTime(2022, 04, 01, 0, 0, 0);
            DateTime May = new DateTime(2022, 05, 01, 0, 0, 0);
            DateTime June = new DateTime(2022, 06, 01, 0, 0, 0);
            DateTime July = new DateTime(2022, 07, 01, 0, 0, 0);
            DateTime Agu = new DateTime(2022, 08, 01, 0, 0, 0);
            DateTime Sep = new DateTime(2022, 09, 01, 0, 0, 0);
            DateTime Oct = new DateTime(2022, 10, 01, 0, 0, 0);
            DateTime Nov = new DateTime(2022, 11, 01, 0, 0, 01);
            DateTime Dev = new DateTime(2022, 12, 01, 0, 0, 0);


            DateTime JanF = new DateTime(2022, 01, 31, 23, 59, 59);
            DateTime FebF = new DateTime(2022, 02, 28, 23, 59, 59);
            DateTime MarF = new DateTime(2022, 03, 31, 23, 59, 59);
            DateTime AprF = new DateTime(2022, 04, 30, 23, 59, 59);
            DateTime MayF = new DateTime(2022, 05, 31, 23, 59, 59);
            DateTime JuneF = new DateTime(2022, 06, 30, 23, 59, 59);
            DateTime JulyF = new DateTime(2022, 07, 31, 23, 59, 59);
            DateTime AguF = new DateTime(2022, 08,31, 23, 59, 59);
            DateTime SepF = new DateTime(2022, 09, 30, 23, 59, 59);
            DateTime OctF = new DateTime(2022, 10, 31, 23, 59, 59);
            DateTime NovF = new DateTime(2022, 11, 30, 23, 59, 59);
            DateTime DevF = new DateTime(2021, 12, 31, 23, 59, 59);


            var displayList1 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Jan && x.Order_date <= JanF).Count().ToString();
            var displayList2 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Feb && x.Order_date <= FebF).Count().ToString();
            var displayList3 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Mar && x.Order_date <= MarF).Count().ToString();
            var displayList4 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Agu && x.Order_date <= MayF).Count().ToString();
            var displayList5 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= May && x.Order_date <= JanF).Count().ToString();
            var displayList6 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= June && x.Order_date <= JuneF).Count().ToString();
            var displayList7 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= July && x.Order_date <= JulyF).Count().ToString();
            var displayList8 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Agu && x.Order_date <= AguF).Count().ToString();
            var displayList9 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Sep && x.Order_date <= SepF).Count().ToString();
            var displayList10 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Oct && x.Order_date <= OctF).Count().ToString();
            var displayList11 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Nov && x.Order_date <= NovF).Count().ToString();
            //var displayList12 = DataProvider.Ins.DB.Orders.Select(x=>x).Count().ToString();

            var displayList12 = DataProvider.Ins.DB.Orders.Where(x => x.Order_date >= Dev && x.Order_date<=DevF).Count().ToString();



            ChartValues.Add(Convert.ToDouble(displayList1));
            ChartValues.Add(Convert.ToDouble(displayList2));
            ChartValues.Add(Convert.ToDouble(displayList3));
            ChartValues.Add(Convert.ToDouble(displayList4));
            ChartValues.Add(Convert.ToDouble(displayList5));
            ChartValues.Add(Convert.ToDouble(displayList6));
            ChartValues.Add(Convert.ToDouble(displayList7));
            ChartValues.Add(Convert.ToDouble(displayList8));
            ChartValues.Add(Convert.ToDouble(displayList9));
            ChartValues.Add(Convert.ToDouble(displayList10));
            ChartValues.Add(Convert.ToDouble(displayList11));
            ChartValues.Add(Convert.ToDouble(displayList12));


            //Beverage = new ObservableCollection<Beverage>(DataProvider.Ins.DB.Beverages);
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null) return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                p.Show();

                if (loginWindow.DataContext == null)
                    return;

                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }


            });

            HomeCommand = new RelayCommand<object>((p) => { return true; }, (p) => { HomeWindow wd = new HomeWindow(); wd.ShowDialog(); });
            OrderCommand = new RelayCommand<object>((p) => { return true; }, (p) => {OrderWindow wd = new OrderWindow(); wd.ShowDialog(); });
            OrderDetailCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OrderDetailWindow wd = new OrderDetailWindow(); wd.ShowDialog(); });
            UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });
            BeverageCommand = new RelayCommand<object>((p) => { return true; }, (p) => { BeverageWindow wd = new BeverageWindow(); wd.ShowDialog(); });
            TypeOfBeverageCommand = new RelayCommand<object>((p) => { return true; }, (p) => {TypeOfBeverageWindow wd = new TypeOfBeverageWindow(); wd.ShowDialog(); });
            RoleCommand = new RelayCommand<object>((p) => { return true; }, (p) => { RoleWindow wd = new RoleWindow(); wd.ShowDialog(); });

           
        }
    }
}
