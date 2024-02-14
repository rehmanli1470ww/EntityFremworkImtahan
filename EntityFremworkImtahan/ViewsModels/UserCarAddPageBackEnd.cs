using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EntityFremworkImtahan.Commands;

using EntityFremworkImtahan.Views.Pages;
using Ef_Model.Entities.Concret;
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;

namespace EntityFremworkImtahan.ViewsModels
{
    public class UserCarAddPageBackEnd
    {
        public ICommand CarAddCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand UserCarAddHomeCommand { get; set; }
        public ICommand UserShowBackCommand { get; set; }
        public ObservableCollection<User> Users { get; set; }
        public User User { get; set; }

        public IBaseRepostori<User> UserContext { get; set; }
        public IBaseRepostori<Car> CarContext { get; set; }
        public UserCarAddPageBackEnd(ObservableCollection<User> users,User user)
        {
            UserContext = new BaseRepostori<User>();
            CarContext = new BaseRepostori<Car>();
            Users = users;
            User = user;
            CarAddCommand = new RelayCommand(ExCarAddCommand, CanExCarAddCommand);
            CancelCommand = new RelayCommand(ExCancelCommand, CanCancelCommand);
            UserCarAddHomeCommand = new RelayCommand(ExUserCarAddHomeCommand, CanUserCarAddHomeCommand);
            UserShowBackCommand = new RelayCommand(ExUserShowBackCommand, CanUserShowBackCommand);
        }

        private bool CanUserShowBackCommand(object? obj)
        {
            return true;
        }

        private void ExUserShowBackCommand(object? obj)
        {
            var page = obj as Page;
            ShowPage menu = new ShowPage();
            menu.DataContext = new ShowPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanUserCarAddHomeCommand(object? obj)
        {
            return true;
        }

        private void ExUserCarAddHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu = new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanCancelCommand(object? obj)
        {
            return true;
        }

        private void ExCancelCommand(object? obj)
        {
            var page = obj as Page;
            ShowPage menu = new ShowPage();
            menu.DataContext = new ShowPageBackEnd(User);
            page.NavigationService.Navigate(menu);


            
        }

        private bool CanExCarAddCommand(object? obj)
        {
            return true;
        }

        private void ExCarAddCommand(object? obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                if (!File.Exists($"..\\..\\..\\Images\\{Path.GetFileName(fileDialog.FileName)}"))
                    File.Copy(fileDialog.FileName, $"..\\..\\..\\Images\\{Path.GetFileName(fileDialog.FileName)}");
                var path = $@"\Images\{System.IO.Path.GetFileName(fileDialog.FileName)}";

                var NewCar = new Car();
                Ef_Model.Entities.Concret.Image image = new Ef_Model.Entities.Concret.Image();
               
                NewCar.Marka = ((TextBox)((Page)obj).FindName("MarkaTb")).Text.ToString();
                NewCar.Year = ((TextBox)((Page)obj).FindName("YearTb")).Text.ToString();
                NewCar.Model = ((TextBox)((Page)obj).FindName("ModelTb")).Text.ToString();
                NewCar.Money = ((TextBox)((Page)obj).FindName("MoneyTb")).Text.ToString();
                NewCar.UserId = User.Id;
                NewCar.Image= new()
                {
                    FullPath = path,
                    CreateTime = DateTime.Now,
                    DeleteTime = DateTime.Now,
                    Car= NewCar
                };
                
                CarContext.Add(NewCar);
                CarContext.Save();


            }


            
        }

       

}
}
