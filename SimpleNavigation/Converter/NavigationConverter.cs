using SimpleNavigation.Model;
using SimpleNavigation.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimpleNavigation.Converter
{
    public class NavigationConverter : MarkupExtension, IValueConverter
    {
        private static NavigationConverter _converter = null;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter is null)
            {
                _converter = new NavigationConverter();
            }
            return _converter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            NavigationModel navigateTo = (NavigationModel)value;
            NavigationService navigation = NavigationService.GetInstance; 
                if (navigateTo is null) 
                return null;
            return navigation.NavigateToModel(navigateTo);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}
