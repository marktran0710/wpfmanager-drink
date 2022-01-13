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
    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private ObservableCollection<Role> _Role;
        public ObservableCollection<Role> Role { get => _Role; set { _Role = value; OnPropertyChanged(); } }

        private User _SelectedItem;
        public User SelectedItem
        {
            get => _SelectedItem; set
            {
                _SelectedItem = value; OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id_user=SelectedItem.Id_user;
                    Name_user = SelectedItem.Name_user;
                    Phone_number = SelectedItem.Phone_number;
                    Email = SelectedItem.Email;
                    Address = SelectedItem.Address;
                    Password = SelectedItem.Password;
                    Created_at = SelectedItem.Created_at;
                    Update_at = SelectedItem.Update_at;
                    Role_id = SelectedItem.Role_id;

                }
            }
        }
        private int? _Role_id;
        public int? Role_id { get => _Role_id; set { _Role_id = value; OnPropertyChanged(); } }
        private string _Name_user;
        public string Name_user { get => _Name_user; set { _Name_user = value; OnPropertyChanged(); } }

        private int? _Id_user;
        public int? Id_user { get => _Id_user; set { _Id_user = value; OnPropertyChanged(); } }

        private string _Phone_number;
        public string Phone_number { get => _Phone_number; set { _Phone_number = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private DateTime? _Created_at;
        public DateTime? Created_at { get => _Created_at; set { _Created_at = value; OnPropertyChanged(); } }
        private DateTime? _Update_at;
        public DateTime? Update_at { get => _Update_at; set { _Update_at = value; OnPropertyChanged(); } }



        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }



        public UserViewModel()
        {
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            Role = new ObservableCollection<Role>(DataProvider.Ins.DB.Roles);
            AddCommand = new RelayCommand<object>((p) =>
            {
                
                return true;
            }, (p) =>
            {
                var User = new User() { Name_user = Name_user, Address=Address, Email= Email, Password = Password ,Phone_number = Phone_number, Role_id= Role_id, Update_at= Update_at, Created_at= (DateTime)Created_at };
                DataProvider.Ins.DB.Users.Add(User);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(User);
            });
            /////////////////////////////////////////////////////////////////////
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Name_user) || SelectedItem == null)
                    return false;
                var displayList = DataProvider.Ins.DB.Users.Where(x => x.Name_user == Name_user);
                var displayList1 = DataProvider.Ins.DB.Users.Where(x => x.Address == Address);
                var displayList2 = DataProvider.Ins.DB.Users.Where(x => x.Phone_number == Phone_number);
                var displayList3 = DataProvider.Ins.DB.Users.Where(x => x.Update_at == Update_at);
                var displayList4 = DataProvider.Ins.DB.Users.Where(x => x.Email == Email);
                if ((displayList != null || displayList.Count() == 0) || (displayList1 != null || displayList1.Count() == 0) || (displayList2 != null || displayList2.Count() == 0) || (displayList3 != null || displayList3.Count() == 0) || (displayList4 != null || displayList4.Count() == 0))
                    return true;
                return false;
            }, (p) =>
            {
                var User = DataProvider.Ins.DB.Users.Where(x => x.Id_user == SelectedItem.Id_user).SingleOrDefault();
                User.Name_user = Name_user;
                User.Phone_number = Phone_number;
                User.Update_at = Update_at;
                User.Email = Email;
                User.Address = Address;
                User.Created_at = (DateTime)Created_at;
                
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Name_user = Name_user;

            });


            /*ĐỂ XÓA DỮ LIỆU:
             * 1. Tìm thuộc tính khóa (hoặc giá trị không trùng nhau) nằm trong bảng thực hiện
             * 2. Thực hiện xóa hàng có chứa thuộc tính đã tìm ở (1)
             * 3. Lưu lại những thay đổi vừa mới thực hiện
             * 4. Xóa hoặc hoán vị để làm mất vị trí của hàng đã xóa.
             * 
             */
            DeleteCommand = new RelayCommand<object>((p) =>
            {
               
                if (SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var User = DataProvider.Ins.DB.Users.Where(x => x.Id_user == SelectedItem.Id_user);
              
                User _user= DataProvider.Ins.DB.Users.Find(Id_user);
                DataProvider.Ins.DB.Users.Remove(_user);
                    DataProvider.Ins.DB.SaveChanges();
                                  
            });

            ChangePasswordCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Password) || SelectedItem == null)
                    return false;
                return true;
            }, (p) =>
            {
                var User = DataProvider.Ins.DB.Users.Where(x => x.Id_user == SelectedItem.Id_user).SingleOrDefault();
                User.Password = Password;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Password = Password;

            });
        }


    }
  
}
