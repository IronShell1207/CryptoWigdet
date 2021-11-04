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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoWigdet
{
    /// <summary>
    /// Логика взаимодействия для CryptoPairListElement.xaml
    /// </summary>
    public partial class CryptoPairListElement : ListViewItem
    {
        public event RoutedEventHandler ButtonUP_click
        {
            add
            {
                ButtonUp.Click += value;
            }
            remove
            {
                ButtonUp.Click += value;
            }
        }
        public event RoutedEventHandler ButtonDown_click
        {
            add
            {
                ButtonDown.Click += value;
            }
            remove
            {
                ButtonDown.Click += value;
            }
        }
        public event RoutedEventHandler ButtonRemove_click
        {
            add
            {
                ButtonRemove.Click += value;
            }
            remove
            {
                ButtonRemove.Click += value;
            }
        }
        public string CurrName
        {
            get
            {
                return lblCurname.Text;
            }
            set
            {
                lblCurname.Text = value;
            }
        }
        public CryptoPairListElement()
        {
            InitializeComponent();
        }
    }
}
