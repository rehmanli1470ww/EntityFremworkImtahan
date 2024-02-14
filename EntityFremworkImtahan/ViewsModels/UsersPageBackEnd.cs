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
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;
using Ef_Model.Entities.Concret;
using EntityFremworkImtahan.Commands;

using EntityFremworkImtahan.Views.Pages;

namespace EntityFremworkImtahan.ViewsModels
{
    public class UsersPageBackEnd
    {
        public ObservableCollection<User> users { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public ICommand UserPageBackCommand { get; set; }
        public ICommand UserPageHomeCommand { get; set; }
        public IBaseRepostori<User> UserContext { get; set; }
        public UsersPageBackEnd()
        {
            UserContext=new BaseRepostori<User>();
            users = new(UserContext.GetAll().ToList());
            LoginCommand = new RelayCommand(ExLoginCommand, CanExLoginCommand);
            RegistrationCommand = new RelayCommand(ExRegistrationCommand, CanExRegistrationCommand);
            UserPageBackCommand = new RelayCommand(ExUserPageBackCommand, CanUserPageBackCommand);
            UserPageHomeCommand = new RelayCommand(ExUserPageHomeCommand, CanUserPageHomeCommand);
        }

        private bool CanUserPageHomeCommand(object? obj)
        {
            return true;
        }

        private void ExUserPageHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu=new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanUserPageBackCommand(object? obj)
        {
            return true;
        }

        private void ExUserPageBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        private void ExRegistrationCommand(object? obj)
        {
            var page = obj as Page;
            var newPage = new UsersRegistrationPage();
            newPage.DataContext = new UsersRegistrationPageBackEnd();
            page.NavigationService.Navigate(newPage);
        }

        private bool CanExRegistrationCommand(object? obj)
        {
            return true;
        }

        private bool CanExLoginCommand(object? obj)
        {
            return true;
        }

        private void ExLoginCommand(object? obj)
        {

            var UserName=((TextBox)((Page)obj).FindName("UserNameTb")).Text.ToString();
            var Password=((TextBox)((Page)obj).FindName("PasswordTb")).Text.ToString();
            foreach (var item in users)
            {
                if (item.UserName== UserName && item.Password== Password)
                {
                    var page = obj as Page;
                    var newPage = new ShowPage();
                    newPage.DataContext =new ShowPageBackEnd(item);
                    page.NavigationService.Navigate(newPage);
                }
            }
        }

       
    }
}
