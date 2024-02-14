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
using DocumentFormat.OpenXml.Spreadsheet;
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;
using Ef_Model.Entities.Concret;
using EntityFremworkImtahan.Commands;
using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using Microsoft.IdentityModel.Tokens;
using Page = System.Windows.Controls.Page;

namespace EntityFremworkImtahan.ViewsModels
{
    
    public class AdminShowUsersBackEnd
    {
        public ObservableCollection<User> users { get; set; }
        public ICommand UserShowUsersHomeCommand { get; set; }
        public ICommand UserShowUsersBackCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public IBaseRepostori<User> UserContext { get; set; }
        public IBaseRepostori<Car> CarContext { get; set; }
        public AdminShowUsersBackEnd()
        {
            CarContext= new BaseRepostori<Car>();
            UserContext =new BaseRepostori<User>();
            users = new (UserContext.GetAll().ToList());
            UserShowUsersHomeCommand = new RelayCommand(ExUserShowUsersHomeCommand, CanUserShowUsersHomeCommand);
            UserShowUsersBackCommand = new RelayCommand(ExUserShowUsersBackCommand, CanUserShowUsersBackCommand);
            DeleteCommand = new RelayCommand(ExDeleteCommand, CanDeleteCommand);
        }

        private void ExDeleteCommand(object? obj)
        {
            var uu = users[(int)obj];
            var cars = CarContext.GetQuery().Where(e => e.UserId == uu.Id);
            foreach (var item in cars)
            {
                CarContext.Remove(item);
            }
            CarContext.Save();
            users.RemoveAt((int)obj);
            UserContext.Remove(uu);
            UserContext.Save();
        }

        private bool CanDeleteCommand(object? obj)
        {
            return true;
        }

        private bool CanUserShowUsersBackCommand(object? obj)
        {
            return true;
        }

        private void ExUserShowUsersBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        private bool CanUserShowUsersHomeCommand(object? obj)
        {
            return true;
        }

        private void ExUserShowUsersHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu = new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }
    }
}
