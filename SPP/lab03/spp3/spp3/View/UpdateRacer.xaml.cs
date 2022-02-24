using System.Linq;
using System.Windows;
using spp3.Model;

namespace spp3.View
{
    /// <summary>
    /// Логика взаимодействия для UpdateRacer.xaml
    /// </summary>
    public partial class UpdateRacer : Window
    {
        private static Racer _racer;
        public UpdateRacer(Racer racer)
        {
            InitializeComponent();
            using RaceContext context = new RaceContext();
            _racer = racer;
            Race.ItemsSource = context.Races
                .Select(r => r.Name)
                .ToList();
            Car.ItemsSource = context.Cars
                .Select(c => c.Mark)
                .ToList();
            Team.ItemsSource = context.Teams
                .Select(t => t.Name)
                .ToList();
            Race.SelectedItem = racer.Race.Name;
            Car.SelectedItem = racer.Car.Mark;
            Team.SelectedItem = racer.Team.Name;
            FIO.Text = racer.Name;
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using RaceContext context = new RaceContext();
            context.Racers.Remove(context.Racers.First(r => r == _racer));
            context.SaveChanges();
            this.Close();
        }
    }
}
