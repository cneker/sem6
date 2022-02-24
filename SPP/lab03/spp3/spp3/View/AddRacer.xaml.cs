using System.Linq;
using System.Windows;
using spp3.Model;

namespace spp3.View
{
    /// <summary>
    /// Логика взаимодействия для AddRacer.xaml
    /// </summary>
    public partial class AddRacer : Window
    {
        public AddRacer()
        {
            InitializeComponent();
            using RaceContext context = new RaceContext();

            Race.ItemsSource = context.Races
                .Select(r => r.Name)
                .ToList();
            Car.ItemsSource = context.Cars
                .Select(c => c.Mark)
                .ToList();
            Team.ItemsSource = context.Teams
                .Select(t => t.Name)
                .ToList();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
