using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace SimpleNavigation.Model
{
    public class NavigationModel
    {
        public NavigationModel(string title, string description, Brush color)
        {
            Title = title;
            Description = description;
            Color = color;
        }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public Brush Color { get; set; }

        public override bool Equals(object obj)
        {
            return obj is NavigationModel model &&
                   Title == model.Title &&
                   Description == model.Description &&
                   Color == model.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Description, Color);
        }
    }
}
