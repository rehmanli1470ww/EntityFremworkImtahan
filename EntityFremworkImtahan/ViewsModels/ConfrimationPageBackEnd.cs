using System;
using System.Collections.Generic;
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

using EntityFremworkImtahan.Statics;
using EntityFremworkImtahan.Views.Pages;
using static System.Net.WebRequestMethods;

namespace EntityFremworkImtahan.ViewsModels
{



    

    public class ConfrimationPageBackEnd
    {
        public string veripiCode { get; set; }
        public ICommand RegistrationCommand { get; set; }
        public ICommand ConfrimationBackCommand { get; set; }
        public IBaseRepostori<User> UserContext { get; set; }
        public User User { get; }

        public ConfrimationPageBackEnd(User user)
        {
            UserContext = new BaseRepostori<User>();

            User = user;
            RegistrationCommand = new RelayCommand(ExConfrimandCommand, CanExConfrimandCommand);
            ConfrimationBackCommand = new RelayCommand(ExConfrimationBackCommand, CanConfrimationBackCommand);
            veripiCode = GetCode.GmailVerify(user.Email);
        }

        private bool CanConfrimationBackCommand(object? obj)
        {
            return true;
        }

        private void ExConfrimationBackCommand(object? obj)
        {
            ((Page)obj).NavigationService.GoBack();
        }

        private void ExConfrimandCommand(object? obj)
        {
           if (((TextBox)((Page)obj).FindName("UserNameTb")).Text == veripiCode) 
            {
                GetUser.Users.Add(User);
                UserContext.Add(User);
                UserContext.Save();
                var page = obj as Page;
                var newPage = new ShowPage();
                newPage.DataContext = new ShowPageBackEnd(User);
                page.NavigationService.Navigate(newPage);
            }
        }

        private bool CanExConfrimandCommand(object? obj)
        {
            return true;
        }
    }
}
