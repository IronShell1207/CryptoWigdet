using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoWigdet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ThreadSuspendder = true;
        public MainWindow()
        {
            InitializeComponent();
            GenerateWidgetStartup();
            MainBackground.Opacity = ExchangesHandlers.SettingsH.WOpacity;
            FormScaler.ScaleX = ExchangesHandlers.SettingsH.WSize;
            FormScaler.ScaleY = ExchangesHandlers.SettingsH.WSize;


        }
        private void GenerateWidgetStartup()
        {
            if (ExchangesHandlers.SettingsH.FavPairs.Count > 0)
            {
                CurrenciesWigdet.Children.Clear();
                MainWin.Height = 50;
                foreach (string pair in ExchangesHandlers.SettingsH.FavPairs)
                {
                    AddCur(pair);
                }
                CurrUpdater.Start();
            }
        }
        private CryptoWiElem elementFinder(string PairName)
        {
            if (CurrenciesWigdet.Children.Count > 0)
            {
                foreach (UIElement el in CurrenciesWigdet.Children)
                {
                    var element = (CryptoWiElem)el;
                    if (element.PairName == PairName)
                        return element;
                }
            }
            return null;
        }
        private Thread _curruptr ;
        private Thread CurrUpdater
        {
            get
            {
                if (_curruptr != null) return _curruptr;
                _curruptr = new Thread(() =>
                {
                    try
                    {
                        while (ThreadSuspendder)
                        {

                            if (ExchangesHandlers.SettingsH.FavPairs.Count > 0)
                            {
                                var curs = BinanceHandler.CurrenciesInfo;
                                if (curs != null)
                                {
                                    foreach (string Pair in ExchangesHandlers.SettingsH.FavPairs)
                                    {
                                        CryptoWiElem elem = null;
                                        Dispatcher.Invoke(new Action(() => { elem = elementFinder(Pair); }));
                                        if (elem != null)
                                        {
                                            var objcCurNow = curs.Where(x => x.symbol == Pair).ToList()[0];
                                            Dispatcher.Invoke(new Action(() =>
                                            {
                                                elem.Price = double.Parse(objcCurNow.lastPrice);
                                                elem.PriceChanges24h = double.Parse(objcCurNow.priceChange);
                                                elem.Procent = double.Parse(objcCurNow.priceChangePercent);
                                            }));
                                        }
                                        else
                                        {
                                            ExchangesHandlers.SettingsH.FavPairs.Remove(Pair);
                                        }
                                    }
                                }
                            }
                            Thread.Sleep(2700);
                        }
                    }
                    catch (TaskCanceledException)
                    {

                    }

                });
                return _curruptr;
            }
            set
            {
                _curruptr = value;
            }
            
        }

        private void AddCur(string pairname)
        {
            var cryptoW = new CryptoWiElem();
            cryptoW.PairName = pairname;
            
            CurrenciesWigdet.Children.Add(cryptoW);
            MainWin.Height += 100;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
                DragMove();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow(CurrenciesWigdet.Children, FormScaler, MainBackground);
            ThreadSuspendder = false;
            CurrUpdater = null;
            sw.ShowDialog();
            ThreadSuspendder = true;
            GenerateWidgetStartup();
        }
        double lastpos = 0;
        private void ButtonRestoreWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Left = lastpos;
            Ushko.Visibility = Visibility.Collapsed;
            this.Width = 140;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lastpos = this.Left;
            this.Left = -190;
            Ushko.Visibility = Visibility.Visible;
            this.Width = 240;

            
        }
    }
}
