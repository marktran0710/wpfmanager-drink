using manager_drink.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace manager_drink.ViewModel
{
    public class RoleViewModel : BaseViewModel
    {
        private ObservableCollection<Role> _List;
        public ObservableCollection<Role> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private Role _SelectedItem;
        public Role SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id_role = SelectedItem.Id_role;
                    Name_role = SelectedItem.Name_role;
                }
            }
        }
        private string _Name_role;
        public string Name_role { get => _Name_role; set { _Name_role = value; OnPropertyChanged(); } }
        private int _Id_role;
        public int Id_role { get => _Id_role; set { _Id_role = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public RoleViewModel()
        {
            List = new ObservableCollection<Role>(DataProvider.Ins.DB.Roles);
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name_role))
                    return false;
                var displayList = DataProvider.Ins.DB.Roles.Where(x => x.Name_role == Name_role);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var role = new Role() { Name_role = Name_role };
                DataProvider.Ins.DB.Roles.Add(role);
                DataProvider.Ins.DB.SaveChanges(); 
                List.Add(role);
            });
            /////////////////////////////////////////////////////////////////////
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name_role) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Roles.Where(x => x.Name_role == Name_role);
                if (displayList == null || displayList.Count() != 0)
                    return false;
                return true;
            }, (p) =>
            {
                var role = DataProvider.Ins.DB.Roles.Where(x => x.Id_role == SelectedItem.Id_role).SingleOrDefault();
                role.Name_role = Name_role;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Name_role = Name_role;
                
            });
        }


    }
}
