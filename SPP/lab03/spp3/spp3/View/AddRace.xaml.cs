using System.Windows;

namespace spp3.View
{
    /// <summary>
    /// Логика взаимодействия для AddRace.xaml
    /// </summary>
    public partial class AddRace : Window
    {
        public AddRace()
        {
            InitializeComponent();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
