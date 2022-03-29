using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Client
{
    public partial class MainWindow : Window
    {
        private static int _port = 35768;
        private static string _ip = "127.0.0.1";
        private static Socket? _socket;
        private static Button[] _buttons = new Button[20];
        private static Thread? _thread;
        private static string? _status;

        public MainWindow()
        {
            InitializeComponent();

            CreateButtons();
        }

        private void CreateButtons()
        {
            int x = 40;
            for (int i = 0; i < 20; i++)
            {
                Button bnt = new Button()
                {
                    Margin = new Thickness(x * i, 90, 700 - x * i, 180),
                    Content = "#",
                    Name = "Button" + i,
                    Width = 40,
                    Height = 60
                };
                bnt.Click += Click;

                grid.Children.Add(bnt);
                _buttons[i] = bnt;
            }

            foreach (var button in _buttons)
            {
                button.IsEnabled = false;
            }
        }
        private void Connect(object sender, RoutedEventArgs e)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(ipPoint);

            GetResponseFromServer(out StringBuilder builder);
            Debug.WriteLine($"Get data: {builder}");
            var numbers = SplitNumbers(builder);

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Content = numbers[i];
            }
            var button = sender as Button;
            button.IsEnabled = false;

            Thread.Sleep(1000); //было 3

            _thread = new Thread(() =>
            {
                while (true)
                {
                    if (_socket.Available <= 0) continue;
                    GetResponseFromServer(out builder);
                    Debug.WriteLine($"Get data: {builder}");
                    switch (builder.ToString())
                    {
                        case "go":
                            _status = "go";
                            foreach (var button in _buttons)
                            {
                                button.Dispatcher.Invoke(() => button.IsEnabled = true);
                            }
                            break;
                        case "wait":
                            _status = "wait";
                            break;
                    }

                    Status.Dispatcher.Invoke(() => Status.Content = _status);
                    break;
                }
                ListenServer();
            });
            _thread.Start();
        }

        //этот поток фоном проверяет переменную _state: если go, то скипает,
        //если wait, то ожидает сообщения от сервера.
        //нажатие на цифру как раз и меняет состояние на wait, а получение сообщения на go
        private void ListenServer()
        {
            while (true)
            {
                if (!_status.Equals("wait")) continue;

                if (_socket.Available > 0)
                {
                    GetResponseFromServer(out StringBuilder response);
                    Debug.WriteLine($"Get data: {response}");
                    var numbers = SplitNumbers(response);
                    if (IsFirstButtonNull(response))
                    {
                        Status.Dispatcher.Invoke(() => Status.Content = "You win");
                        try
                        {
                            _thread.Interrupt();
                        }
                        finally
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                        }
                        break;
                    }
                    for (int i = 0; i < _buttons.Length; i++)
                    {
                        _buttons[i].Dispatcher.Invoke(() =>
                        {
                            _buttons[i].IsEnabled = true;
                            _buttons[i].Content = numbers[i];
                        });
                    }
                    CheckContentForNull();

                    _status = "go";

                    Status.Dispatcher.Invoke(() => Status.Content = _status);
                }
            }
        }

        private void GetResponseFromServer(out StringBuilder builder)
        {
            builder = new StringBuilder();
            var data = new byte[256];

            var bytes = _socket.Receive(data, data.Length, 0);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) 
                return;
            var buttonContent = int.Parse(button.Content.ToString() ?? string.Empty);
            switch (buttonContent)
            {
                case 0:
                    button.Content = "null";
                    break;
                default:
                    button.Content = (buttonContent - 1).ToString();
                    break;
            }
            CheckContentForNull();
            foreach (var button1 in _buttons)
            {
                button1.IsEnabled = false;
            }
            SendToAll(out StringBuilder builder);
            if (IsFirstButtonNull(builder))
            {
                Status.Content = "You lose";
                try
                {
                    _thread.Interrupt();
                }
                finally
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }
                return;
            }
            _status = "wait";
            Status.Content = _status;
        }

        private void SendToAll(out StringBuilder builder)
        {
            builder = new StringBuilder();

            foreach (var button in _buttons)
            {
                builder.Append(button.Content + ";");
            }
            Debug.WriteLine($"Send data: {builder}");

            byte[] data = Encoding.Unicode.GetBytes(builder.ToString());
            _socket.Send(data);
        }

        private string[] SplitNumbers(StringBuilder response) => response.ToString().Split(';');

        private void CheckContentForNull()
        {
            foreach (var button in _buttons)
            {
                var buttonContent = button.Dispatcher.Invoke(() => button.Content);
                if (buttonContent.Equals("null"))
                {
                    var buttonName = button.Dispatcher.Invoke(() => button.Name);
                    MakeButtonsInvisible(buttonName);
                    return;
                }
            }
        }

        private void MakeButtonsInvisible(string buttonName)
        {
            bool make = false;

            foreach (var t in _buttons)
            {
                var name = t.Dispatcher.Invoke(() => t.Name);
                if (name == buttonName)
                    make = true;
                if (make)
                    t.Dispatcher.Invoke(() => t.Visibility = Visibility.Hidden);
            }
        }

        private bool IsFirstButtonNull(StringBuilder builder) => SplitNumbers(builder).First() == "null";
    }
}
