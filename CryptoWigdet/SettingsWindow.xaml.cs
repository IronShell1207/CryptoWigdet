using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace CryptoWigdet
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private UIElementCollection uisS;
        private ScaleTransform Scaller;
        private Rectangle rectang;
        public SettingsWindow(UIElementCollection pairsVs, ScaleTransform scaler, Rectangle rect)
        {
            InitializeComponent();
            uisS = pairsVs;
            Scaller = scaler;
            rectang = rect;
        }

        private void Slide_Transperency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           ExchangesHandlers.SettingsH.WOpacity = rectang.Opacity = ((Slider)sender).Value / 100;
            JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Slide_Transperency.ValueChanged += Slide_Transperency_ValueChanged;
            Slider_WidgetSize.ValueChanged += Slider_Scaler_ValueChanged;
            CreateCurlist();
        }
        private void CreateCurlist()
        {
            if (ExchangesHandlers.SettingsH.FavPairs.Count > 0)
            {
                listView_currienses.Items.Clear();
                foreach (string pair in ExchangesHandlers.SettingsH.FavPairs)
                {
                    var element = new CryptoPairListElement { CurrName = pair };
                    element.ButtonRemove_click += ElementRemoveClick;
                    element.ButtonDown_click += ElementDownClick;
                    element.ButtonUP_click += ElementUPClick;
                    listView_currienses.Items.Add(element);
                }
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string curname = TB_CurName.Text.Trim().ToUpper();
            if (!String.IsNullOrWhiteSpace(curname) && BinanceHandler.IsCurrencyValid(curname))
            {
                if (!ExchangesHandlers.SettingsH.FavPairs.Contains(curname))
                {
                    ExchangesHandlers.SettingsH.FavPairs.Add(curname);
                    JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
                    CreateCurlist();
                    TB_CurName.Text = "";
                    CtrlAss.Snackbarer(SnackbarTwo, 1.5, "Pair has been added!", this).Start();

                }
            }

        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ElementRemoveClick(object sender, RoutedEventArgs e)
        {
            var Curname = ((CryptoPairListElement)(((StackPanel)(((Button)sender).Parent)).Parent)).CurrName;
            ExchangesHandlers.SettingsH.FavPairs.Remove(Curname);
            JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
            CreateCurlist();
            CtrlAss.Snackbarer(SnackbarTwo, 1.5, "Pair has been removed!", this).Start();
        }
        private void ElementDownClick(object sender, RoutedEventArgs e)
        {
            var Curname = ((CryptoPairListElement)(((StackPanel)(((Button)sender).Parent)).Parent)).CurrName;
         
           var currentId = ExchangesHandlers.SettingsH.FavPairs.FindIndex(x => x == Curname);
            if (currentId < ExchangesHandlers.SettingsH.FavPairs.Count-1)
            {
                ExchangesHandlers.SettingsH.FavPairs.Remove(Curname);
                ExchangesHandlers.SettingsH.FavPairs.Insert(currentId + 1, Curname);
                JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
                CreateCurlist();
            }

        }
        private void ElementUPClick(object sender, RoutedEventArgs e)
        {
            var Curname = ((CryptoPairListElement)(((StackPanel)(((Button)sender).Parent)).Parent)).CurrName;

            var currentId = ExchangesHandlers.SettingsH.FavPairs.FindIndex(x => x == Curname);
            if (currentId > 0)
            {
                ExchangesHandlers.SettingsH.FavPairs.Remove(Curname);
                ExchangesHandlers.SettingsH.FavPairs.Insert(currentId - 1, Curname);
                JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
                CreateCurlist();
            }

        }
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }


        private void Slider_Scaler_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ExchangesHandlers.SettingsH.WSize = Scaller.ScaleX = ((Slider)sender).Value  * 0.01;
            Scaller.ScaleY = ((Slider)sender).Value * 0.01;
            JsonHandler.SaveJson(ExchangesHandlers.SettingsH, ExtPathes.FavCursPath);
        }

        private void TB_CurName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ButtonAdd.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }

        }

        private void SettingsWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
                DragMove();
        }
    }
}
