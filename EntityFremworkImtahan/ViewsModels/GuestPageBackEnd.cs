using Ef_DbAccess.Repostorie.Abstarct;
using Ef_DbAccess.Repostorie.Concret;
using Ef_Model.Entities.Concret;
using EntityFremworkImtahan.Commands;
using EntityFremworkImtahan.Service;
using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace EntityFremworkImtahan.ViewsModels
{
    public class GuestPageBackEnd : NotificationService, ICommand
    {
        private ObservableCollection<Car> cars1;

        public event EventHandler? CanExecuteChanged;

        public ObservableCollection<Car> cars { get => cars1; set { cars1 = value; OnPropertyChanged(); } }
        public ICommand GuestShowBackCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand GuestShowHomeCommand { get; set; }
        public IBaseRepostori<Car> CarContext { get; set; }
        public GuestPageBackEnd()
        {
            CarContext = new BaseRepostori<Car>();
            cars = new(CarContext.GetQuery().Include(x => x.Image).ToList());
            GuestShowBackCommand = new RelayCommand(ExGuestShowBackCommand, CanGuestShowBackCommand);
            GetUser.car = cars.ToList();
            SearchCommand = new RelayCommand(ExSearchCommand, CanSearchCommand);
            GuestShowHomeCommand = new RelayCommand(ExGuestShowHomeCommand, CanGuestShowHomeCommand);
        }

        private bool CanGuestShowHomeCommand(object? obj)
        {
            return true;
        }

        private void ExGuestShowHomeCommand(object? obj)
        {
            var page = obj as Page;
            MenuPage menu = new MenuPage();
            menu.DataContext = new MenuPageBackEnd();
            page.NavigationService.Navigate(menu);
        }

        private bool CanSearchCommand(object? obj)
        {
            return true;
        }

        private void ExSearchCommand(object? obj)
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

        private bool CanGuestShowBackCommand(object? obj)
        {
            return true;
        }

        private void ExGuestShowBackCommand(object? obj)
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
