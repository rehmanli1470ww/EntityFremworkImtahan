
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;
using EntityFremworkImtahan.Commands;

using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using Ef_Model.Entities.Concret;
using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;
using Microsoft.EntityFrameworkCore;
using EntityFremworkImtahan.Service;

namespace EntityFremworkImtahan.ViewsModels
{
    public class ShowPageBackEnd : NotificationService, ICommand
    {
        private ObservableCollection<Car> cars1;

        public User user{ get; set; }
        public bool Check = true;
        public ObservableCollection<Car> cars { get => cars1; set {  cars1 = value; OnPropertyChanged(); } }
        public ObservableCollection<User> users { get; set; }
        public ICommand MyCars { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UserShowBackCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UserShowHomeCommand { get; set; }

        public IBaseRepostori<User> UserContext { get; set; }
        public IBaseRepostori<Car> CarContext { get; set; }


        public ShowPageBackEnd(User user)
        {
            UserContext = new BaseRepostori<User>();
            CarContext = new BaseRepostori<Car>();
            cars =new(CarContext.GetQuery().Include(x => x.Image).ToList());
            users = new(UserContext.GetAll().ToList());
            this .user= user;
            GetUser.car=cars.ToList();
            MyCars = new RelayCommand(Execute, CanExecute);
            AddCommand = new RelayCommand(ExAdCommand, CanExAdCommand);
            UserShowBackCommand = new RelayCommand(ExUserShowBackCommand, CanUserShowBackCommand);
            SearchCommand = new RelayCommand(ExSearchCommand, CanSearchCommand);
            DeleteCommand = new RelayCommand(ExDeleteCommand, CanDeleteCommand);
            UserShowHomeCommand = new RelayCommand(ExUserShowHomeCommand, CanUserShowHomeCommand);
            this.user = user;
        }

        private bool CanDeleteCommand(object? obj)
        {
            return true;

        }

        private void ExDeleteCommand(object? obj)
        {
            var Id = cars[(int)obj].Id;
            var car = CarContext.GetQuery().FirstOrDefault(x => x.Id == Id);
            CarContext.Remove(car);
            CarContext.Save();
            cars = new(CarContext.GetQuery().Include(x => x.Image).ToList());
        }

        private bool CanUserShowHomeCommand(object? obj)
        {
            return true;
        }

        private void ExUserShowHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu= new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanSearchCommand(object? obj)
        {
            return true;
        }

        private void ExSearchCommand(object? obj)
        {
            
            var txt=obj as string;
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
       
        private bool CanUserShowBackCommand(object? obj)
        {
            return true;
        }

        private void ExUserShowBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        public ShowPageBackEnd()
        {

        }

        private bool CanExAdCommand(object? obj)
        {
            return true;
        }

        private void ExAdCommand(object? obj)
        {
            var page = obj as Page;
            var newPage = new UserCarAddPage();
            newPage.DataContext = new UserCarAddPageBackEnd(users,user);
            page.NavigationService.Navigate(newPage);

        }

        public event EventHandler? CanExecuteChanged;

       

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            
            if (Check)
            {
                Check = false;
                ((ListView)(((Grid)parameter).FindName("viewMarket"))).Visibility = Visibility.Hidden;
                ((ListView)(((Grid)parameter).FindName("viewMarketuser"))).Visibility = Visibility.Visible;
                cars = new(cars.Where(e => e.UserId == user.Id).ToList());
                ((Button)(((Grid)parameter).FindName("MyCarsBtn"))).Content = "All Cars";
            }
            else
            {
                cars = new(CarContext.GetQuery().Include(x => x.Image).ToList());
                Check = true;
                ((ListView)(((Grid)parameter).FindName("viewMarketuser"))).Visibility = Visibility.Hidden;
                ((ListView)(((Grid)parameter).FindName("viewMarket"))).Visibility = Visibility.Visible;
                
                ((Button)(((Grid)parameter).FindName("MyCarsBtn"))).Content = "My Cars";
            }

            

        }
    }
}
