using SimpleNavigation.Model;
using SimpleNavigation.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleNavigation.ViewModel
{
    public class MainViewModel
    {
        public NavigationModel CurrentView { get; set; }
        public List<NavigationModel> NavigationOptions { get => NavigationService.GetInstance.NavigationOptions; }
    
    }
}
