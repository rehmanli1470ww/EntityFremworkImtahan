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
using EntityFremworkImtahan.Service;
using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EntityFremworkImtahan.ViewsModels
{
    public class AdminShowPageBackEnd : NotificationService, ICommand
    {
        private ObservableCollection<Car> cars1;

        public event EventHandler? CanExecuteChanged;

        public Admin admin { get; }
        public ObservableCollection<Car> cars { get => cars1; set { cars1 = value; OnPropertyChanged(); } }


        public ICommand AdminShowBackCommand { get; set; }
        public ICommand AdminSearchCommand { get; set; }
        public ICommand AdminShowHomeCommand { get; set; }
        public ICommand UsersCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public IBaseRepostori<Car> CarContext { get; set; }
        public BaseRepostori<Admin> AdminContext { get; set; }

        public AdminShowPageBackEnd()
        {
            CarContext=new BaseRepostori<Car>();
            AdminContext = new BaseRepostori<Admin>();
            cars = new(CarContext.GetQuery().Include(x => x.Image).ToList());

            admin = AdminContext.GetAdmin();
            GetUser.car = cars.ToList();
            AdminShowBackCommand = new RelayCommand(ExAdminShowBackCommand, CanAdminShowBackCommand);
            AdminSearchCommand = new RelayCommand(ExAdminSearchCommand, CanAdminSearchCommand);
            AdminShowHomeCommand = new RelayCommand(ExAdminShowHomeCommand, CanAdminShowHomeCommand);
            UsersCommand = new RelayCommand(ExUsersCommand, CanUsersCommand);
            DeleteCommand = new RelayCommand(ExDeleteCommand, CanDeleteCommand);
        }

        private bool CanDeleteCommand(object? obj)
        {
            return true;
        }

        private void ExDeleteCommand(object? obj)
        {
            var Id = cars[(int)obj].Id;
            var car=CarContext.GetQuery().FirstOrDefault(x => x.Id == Id);
            CarContext.Remove(car);
            CarContext.Save();
            cars = new(CarContext.GetQuery().Include(x => x.Image).ToList());
        }

        private bool CanUsersCommand(object? obj)
        {
            return true;
        }

        private void ExUsersCommand(object? obj)
        {
            var page=obj as Page;
            AdminShowUsers admin=new AdminShowUsers();
            admin.DataContext = new AdminShowUsersBackEnd();
            page.NavigationService.Navigate(admin);

        }

        private bool CanAdminShowHomeCommand(object? obj)
        {
            return true;
        }

        private void ExAdminShowHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu = new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanAdminSearchCommand(object? obj)
        {
            return true;
        }

        private void ExAdminSearchCommand(object? obj)
        {
            var txt = obj as string;
            if (string.IsNullOrEmpty(txt))
            {
                cars.Clear();
                foreach (var item in GetUser.car)
                {
                    cars.Add(item);
                }
                return;
            }
            cars.Clear();


            foreach (var item in GetUser.car)
            {
                if ((item.Model.ToLower().Contains(txt.ToLower())))
                {
                    cars.Add(item);

                }
            }
        }

        private bool CanAdminShowBackCommand(object? obj)
        {
            return true;
        }

        private void ExAdminShowBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
