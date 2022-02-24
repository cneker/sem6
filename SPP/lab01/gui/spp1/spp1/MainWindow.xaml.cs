using System;
using System.Threading;
using System.Windows;

namespace spp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static ManualResetEvent _suspend = new ManualResetEvent(true);
        private static Thread _thread;
        private bool _isPaused = false;
        private bool _isWorking = false;

        private static double Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            return n <= 2 ? n : n * Factorial(n - 1);
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isWorking)
                    return;
                var n = int.Parse(TextBoxN.Text);
                _thread = new Thread(obj =>
                {
                    try
                    {
                        if (obj is int n)
                        {
                            double sum = 0.0;
                            for (int i = 0; i < n; i++)
                            {
                                _suspend.WaitOne(Timeout.Infinite);

                                sum += 1 / Factorial(i);

                                TextVlock.Dispatcher.Invoke(() => TextVlock.Text = $"{sum}");

                                Thread.Sleep(1500);
                            }

                            _isWorking = false;
                        }
                    }
                    catch (ThreadInterruptedException)
                    {
                    }
                });
                _isWorking = true;
                _thread.Start(n);
            }
            catch (FormatException)
            {
                TextVlock.Text = "Invalid N";
            }
        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            if (!_isPaused)
                _suspend.Reset();
            else
                _suspend.Set();
            _isPaused = !_isPaused;
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            if (_isPaused)
                _suspend.Set();
            try
            {
                _thread.Interrupt();
            }
            catch (ThreadInterruptedException)
            { }

            _isPaused = false;
            _isWorking = false;
            TextVlock.Text = "0.0";
        }
    }
}
