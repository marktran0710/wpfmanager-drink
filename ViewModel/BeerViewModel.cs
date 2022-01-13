using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace manager_drink.ViewModel
{


     class BeerViewModel:BaseViewModel
    {
        public class MovieData
        {
            //private string _Title;
            //public string Title
            //{
            //    get { return this._Title; }
            //    set { this._Title = value; }
            //}

            //private BitmapImage _ImageData;
            //public BitmapImage ImageData
            //{
            //    get { return this._ImageData; }
            //    set { this._ImageData = value; }
            //}
            public string Title;
            public BitmapImage ImageData;

        }
       

        
        //private BitmapImage _ImageData;
        //public BitmapImage ImageData { get => _ImageData; set { _ImageData = value; OnPropertyChanged(); } }
        //private string _Title;
        //public string Title { get => _Title; set { _Title = value; OnPropertyChanged(); } }
        private BitmapImage LoadImage(string filename)
        {
            return new BitmapImage(new Uri("pack://application:,,,/" + filename));
        }
        private List<MovieData> _ListBeer;

        public List<MovieData> ListBeer { get => _ListBeer; set { _ListBeer = value; OnPropertyChanged(); } }
        public BeerViewModel()
        {
            
            ListBeer.Add(new MovieData { Title = "Movie 1", ImageData = LoadImage("Images/productbia333.jpg") });
            ListBeer.Add(new MovieData { Title = "Movie 1", ImageData = LoadImage("Images/productbia333.jpg") });
            ListBeer.Add(new MovieData { Title = "Movie 1", ImageData = LoadImage("Images/productbia333.jpg") });

           



        }
    }
}
