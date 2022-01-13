using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace manager_drink.ViewModel
{
    public class HomeViewModel:BaseViewModel
    {
        public ICommand BeerCommand { get; set; }

        public ICommand WaterCommand { get; set; }
        public ICommand WhiskyCommand { get; set; }

        public ICommand BeverageUICommand { get; set; }
    
        public HomeViewModel()
        {
            BeerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { BeerWindow wd = new BeerWindow(); wd.ShowDialog(); });
            WaterCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WaterWindow wd = new WaterWindow(); wd.ShowDialog(); });
            WhiskyCommand = new RelayCommand<object>((p) => { return true; }, (p) => { WhiskyWindow wd = new WhiskyWindow(); wd.ShowDialog(); });
            BeverageUICommand = new RelayCommand<object>((p) => { return true; }, (p) => { BeverageUIWindow wd = new BeverageUIWindow(); wd.ShowDialog(); });


        }
    }
}
