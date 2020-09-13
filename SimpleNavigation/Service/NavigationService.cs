using SimpleNavigation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleNavigation.Service
{
    /// <summary>
    /// For Simplicity of the exaple, the service will be a singleton
    /// </summary>
    public class NavigationService
    {

        public List<NavigationModel> NavigationOptions { get=>NavigationNameToUserControl.Keys.ToList(); }

        public UserControl NavigateToModel(NavigationModel _navigationModel)
        {
            if (_navigationModel is null) 
                //Or throw exception
                return null;
            if (NavigationNameToUserControl.ContainsKey(_navigationModel))
            {
                return NavigationNameToUserControl[_navigationModel].Invoke();
            }
            //Ideally you should throw here Custom Exception
            return null;
        }

        private readonly Dictionary<NavigationModel, Func<UserControl>> NavigationNameToUserControl = new Dictionary<NavigationModel, Func<UserControl>>
        {
            { new NavigationModel("Navigate To A","This will navigate to the A View",Brushes.Aqua), ()=>{ return new View.ViewA(); } },
            { new NavigationModel("Navigate To B","This will navigate to the B View",Brushes.GreenYellow), ()=>{ return new View.ViewB(); } }
        };

        #region SingletonThreadSafe
        private static readonly object Instancelock = new object();
        
        private static NavigationService instance = null;

        public static NavigationService GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (Instancelock)
                    {
                        if (instance == null)
                        {
                            instance = new NavigationService();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
    }
}
