using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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
using Weather.Wpf.Properties;
using WeatherLib;

namespace Weather.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IWeatherService weatherService;

        public MainWindow()
        {
            InitializeComponent();
            this.weatherService = new OpenWeatherMapService(ConfigurationManager.AppSettings["ApiKey"]);
        }


        private async void getCurrentWeather(object sender, RoutedEventArgs e)
        {
            try
            {
                var city = this.cityInput.Text;
                if (string.IsNullOrWhiteSpace(city))
                    return;

                var result = await this.weatherService.GetCurrentWeather(city);
                this.resultsTxt.Text = $"Weather in {city}: {result.Minimum}ºC to {result.Maximum}ºC.{Environment.NewLine}" + this.resultsTxt.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened :(" + Environment.NewLine + ex.Message);
            }
        }
    }
}
