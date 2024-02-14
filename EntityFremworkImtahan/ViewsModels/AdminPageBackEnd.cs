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
    public class AdminPageBackEnd
    {
        public Admin admin { get; set; }
        public ICommand AdminLoginCommand { get; set; }
        public ICommand AdminBackCommand { get; set; }
        public ICommand AdminPageHomeCommand { get; set; }
        public BaseRepostori<Admin> AdminContext { get; set; }
        public AdminPageBackEnd()
        {
            AdminContext=new BaseRepostori<Admin>();
            admin = AdminContext.GetAdmin();
            AdminLoginCommand = new RelayCommand(ExLoginCommand, CanExLoginCommand);
            AdminBackCommand = new RelayCommand(ExAdminBackCommand, CanAdminBackCommand);
            AdminPageHomeCommand = new RelayCommand(ExAdminPageHomeCommand, CanAdminPageHomeCommand);
        }

        private bool CanAdminPageHomeCommand(object? obj)
        {
            return true;
        }

        private void ExAdminPageHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu=new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanAdminBackCommand(object? obj)
        {
            return true;
        }

        private void ExAdminBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        private bool CanExLoginCommand(object? obj)
        {
            return true;
        }

        private void ExLoginCommand(object? obj)
        {

            var AdminName = ((TextBox)((Page)obj).FindName("AdminNameTb")).Text.ToString();
            var Password = ((TextBox)((Page)obj).FindName("AdminPasswordTb")).Text.ToString();
            if (admin.AdminName == AdminName && admin.Password == Password)
            {
                var page = obj as Page;
                var newPage = new AdminShowPage();
                newPage.DataContext = new AdminShowPageBackEnd();
                page.NavigationService.Navigate(newPage);
            }
        }

        
    }
}

