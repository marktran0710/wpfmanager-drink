using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography;

using manager_drink.Model;

namespace manager_drink.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }


        public ICommand UserCommand { get; set; }

        public LoginViewModel()
        {
            
                        IsLogin = false;
                        Password = "";
                        UserName = "";
                        LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
                        CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

                        PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; }); 
                    }
                    void Login(Window p)
                    {
                        if (p == null)
                            return;

                        IsLogin = true;

                         //string passEncode = MD5Hash(Base64Encode(Password));
                         var accCount = DataProvider.Ins.DB.Users.Where(x => x.Name_user== UserName && x.Password==Password).Count();
                           if (accCount > 0)
                           {
                               IsLogin = true; 
                               p.Close(); 
                           }
                           else {
                                IsLogin = false;
                               MessageBox.Show("Maybe password or usename is wrong!!!");
                           }
                       }

                    /*   public static string Base64Encode(string plainText)
                       {
                           var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                           return System.Convert.ToBase64String(plainTextBytes);
                       }

                       public static string MD5Hash(string input)
                       {
                           StringBuilder hash = new StringBuilder();
                           MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
                           byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
                           for (int i = 0; i < bytes.Length; i++)
                           {
                               hash.Append(bytes[i].ToString("x2"));
                           }
                           return hash.ToString();

                       }
                    */
          //  UserCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserWindow wd = new UserWindow(); wd.ShowDialog(); });

        }
    } 

