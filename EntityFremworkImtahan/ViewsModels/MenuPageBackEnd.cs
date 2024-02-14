using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using EntityFremworkImtahan.Commands;
using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using Ef_Model.Entities.Concret;
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;
using Microsoft.IdentityModel.Tokens;

namespace EntityFremworkImtahan.ViewsModels
{

    public class MenuPageBackEnd: ICommand
    {
        public ICommand UserCommand { get; set; }
        public ICommand AdminCommand { get; set; }
        public ICommand GuestCommand { get; set; }
        public IBaseRepostori<User> UserContext { get; set; }
        public IBaseRepostori<Car> CarContext { get; set; }
        public MenuPageBackEnd()
        {
            CarContext=new BaseRepostori<Car>();
            UserContext =new BaseRepostori<User>();
            GetUser.Users = UserContext.GetAll().ToList();
            GetUser.car = CarContext.GetAll().ToList();
            
            UserCommand = new RelayCommand(Execute, CanExecute);
            GuestCommand = new RelayCommand(GuestExecute, GuestCanExecute);
            AdminCommand = new RelayCommand(ExAdminCommand, CanExAdminCommand);
        }

        private bool GuestCanExecute(object? obj)
        {
            return true;
        }

        private void GuestExecute(object? obj)
        {
            var page = obj as MenuPage;

            GuestPage userp = new GuestPage();
            page.DataContext = userp;
            page.NavigationService.Navigate(userp);
        }

        private bool CanExAdminCommand(object? obj)
        {
            return true;
        }

        private void ExAdminCommand(object? obj)
        {
            var page = obj as MenuPage;

            AdminPage userp = new AdminPage();
            page.DataContext = userp;
            page.NavigationService.Navigate(userp);
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var page = parameter as MenuPage;

            UsersPage userp = new UsersPage();
            page.DataContext = userp;
            page.NavigationService.Navigate(userp);
           
        }
    }
}
