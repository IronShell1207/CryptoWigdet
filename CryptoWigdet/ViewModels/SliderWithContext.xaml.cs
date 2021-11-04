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
    /// Логика взаимодействия для SliderWithContext.xaml
    /// </summary>
    public partial class SliderWithContext : UserControl
    {
        public double MinValue
        {
            get
            {
                return Slider.Minimum;
            }
            set
            {
                Slider.Minimum = value;
            }
        }
        public double MaxValue
        {
            get
            {
                return Slider.Maximum;
            }
            set
            {
                Slider.Maximum = value;
            }
        }
        public double Value
        {
            get
            {
                return Slider.Value;
            }
            set
            {
                Slider.Value = value;
            }
        }
        public string MinText
        {
            get
            {
                return LeftText.Text;
            }
            set
            {
                LeftText.Text = value;
            }
        }
        public string MaxText
        {
            get
            {
                return RightText.Text;
            }
            set
            {
                RightText.Text = value;
            }
        }
        public string TitleText
        {
            get
            {
                return TitleTextBox.Text;
            }
            set
            {
                TitleTextBox.Text = value;
            }
        }
        public double StepVal
        {
            get
            {
                return Slider.TickFrequency;
            }
            set
            {
                Slider.TickFrequency = value;
            }
        }

        public event System.Windows.RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add
            {
                Slider.ValueChanged += value;
            }
            remove
            {
                Slider.ValueChanged -= value;
            }
        }

        public SliderWithContext()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
