using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Qupla.IndicatorServer.TrayClient
{
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            _notifyIcon = new NotifyIcon {Icon = new Icon("TrayIcon.ico"), Visible = true};
            _notifyIcon.DoubleClick += (s, e) => Maximize();
            _notifyIcon.Click += (s, e) => Maximize();

            Minimize();
        }

        private void Maximize()
        {
            Show();
            WindowState = WindowState.Normal;
        }

        private void Minimize()
        {
            WindowState = WindowState.Minimized;
            Hide();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }
    }
}