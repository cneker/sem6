using System.Windows;

namespace spp3.View
{
    /// <summary>
    /// Логика взаимодействия для AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Window
    {
        public AddTeam()
        {
            InitializeComponent();
        }

        private void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
