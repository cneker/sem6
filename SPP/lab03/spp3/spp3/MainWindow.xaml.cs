using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using spp3.Model;
using spp3.View;

namespace spp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillTable();
        }

        public void FillTable()
        {
            using RaceContext context = new RaceContext();
            DtaGrid.ItemsSource = context.Racers
                .Include(r => r.Car)
                .Include(r => r.Race)
                .Include(r => r.Team)
                .Include(r => r.Race.Location)
                .ToList();
        }

        private void AddCar(object sender, RoutedEventArgs e)
        {
            var addCar = new AddCar();
            if (addCar.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                context.Cars.Add(new Car() {Mark = addCar.Mark.Text});
                context.SaveChanges();
            }
        }

        private void AddLocation(object sender, RoutedEventArgs e)
        {
            var addLocation = new AddLocation();
            if (addLocation.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                var raceID = context.Races.FirstOrDefault(r => r.Name == addLocation.Race.Text)?.Id;
                if (raceID != null)
                    context.Locations.Add(new Location()
                        {Name = addLocation.Name.Text, Country = addLocation.Country.Text, RaceId = raceID.Value});
                context.SaveChanges();
            }
        }

        private void AddTeam(object sender, RoutedEventArgs e)
        {
            var addTeam = new AddTeam();
            if (addTeam.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                context.Teams.Add(new Team() {Name = addTeam.Team.Text});
                context.SaveChanges();
            }
        }
        private void AddRace(object sender, RoutedEventArgs e)
        {
            var addRace = new AddRace();
            if (addRace.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                context.Races.Add(new Race() {Name = addRace.Name.Text});
                context.SaveChanges();
            }
        }
        private void AddRacer(object sender, RoutedEventArgs e)
        {
            var addRacer = new AddRacer();
            if (addRacer.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                var raceId = context.Races.FirstOrDefault(r => r.Name == addRacer.Race.Text)?.Id;
                var carId = context.Cars.FirstOrDefault(r => r.Mark == addRacer.Car.Text)?.Id;
                var teamId = context.Teams.FirstOrDefault(r => r.Name == addRacer.Team.Text)?.Id;
                if (carId != null && teamId != null && raceId != null)
                    context.Racers.Add(new Racer()
                    {
                        Name = addRacer.FIO.Text, CarId = carId.Value, RaceId = raceId.Value,
                        TeamId = teamId.Value
                    });
                context.SaveChanges();
                FillTable();
            }
        }

        private void DtaGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = DtaGrid.SelectedItem as Racer;
            var updateRacer = new UpdateRacer(selectedItem);
            if (updateRacer.ShowDialog() == true)
            {
                using RaceContext context = new RaceContext();
                var item = context.Racers.First(r => r == selectedItem);
                var raceId = context.Races.FirstOrDefault(r => r.Name == updateRacer.Race.Text)?.Id;
                var carId = context.Cars.FirstOrDefault(r => r.Mark == updateRacer.Car.Text)?.Id;
                var teamId = context.Teams.FirstOrDefault(r => r.Name == updateRacer.Team.Text)?.Id;
                if (carId != null && teamId != null && raceId != null)
                {
                    item.Name = updateRacer.FIO.Text;
                    item.CarId = (int) carId;
                    item.RaceId = (int) raceId;
                    item.TeamId = (int) teamId;
                    context.Racers.Update(item);
                }
                context.SaveChanges();
            }
            FillTable();
        }
    }
}
