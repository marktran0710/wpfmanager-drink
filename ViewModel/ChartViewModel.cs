using manager_drink.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace manager_drink.ViewModel
{
     class ChartViewModel:BaseViewModel
    {
        private ObservableCollection<Beverage> _Beverage;
        public ObservableCollection<Beverage> Beverage { get => _Beverage; set { _Beverage = value; OnPropertyChanged(); } }
        public class DataPoint
        {
            public string Argument { get; set; }
            public double Value { get; set; }
            
            public static ObservableCollection<DataPoint> GetDataPoints()
            {
                
                var numofBeverages = DataProvider.Ins.DB.Beverages.Select(x => x.Inventory).ToList();
                var num = DataProvider.Ins.DB.Beverages.Select(x => x).ToList();
                var nameofBeverage = DataProvider.Ins.DB.Beverages.Select(x => x.Name_beverage).ToList();
                ObservableCollection<DataPoint> List = new ObservableCollection<DataPoint>();
                if (numofBeverages == null)
                {
                    return List;
                }

                for (int i= 1; i<= num.Count(); i++)
                {
                    List.Add(new DataPoint{ Argument = nameofBeverage[i-1], Value = (double)numofBeverages[i-1] });
                    Console.WriteLine(i);
                }
                
                return List;
            }
        }
        public ObservableCollection<DataPoint> Data { get; private set; }
        public ChartViewModel()
        {
            
            this.Data = DataPoint.GetDataPoints();
        }
    }
}
