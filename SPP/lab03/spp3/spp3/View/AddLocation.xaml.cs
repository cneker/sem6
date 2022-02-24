using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using spp3.Model;

namespace spp3.View
{
    /// <summary>
    /// Логика взаимодействия для AddLocation.xaml
    /// </summary>
    public partial class AddLocation : Window
    {
        public AddLocation()
        {
            InitializeComponent();

            using RaceContext context = new RaceContext();
            Race.ItemsSource = context.Races
                .Include(r => r.Location)
                .Where(r => r.Location == null)
                .Select(r => r.Name)
                .ToList();
            var a = (Race.ItemsSource as List<string>)?.Count;
            if (a is 0)
            {
                Label.Visibility = Visibility.Visible;
                insertBtn.IsEnabled = false;
            }
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
