using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Weather_Forecast {
    class WeatherInfo {
        private const string API_KEY = "cf5fdf2bed1dab2669170240d5347228";
        private string BASE_URL = "http://api.openweathermap.org/data/2.5/weather?q=";
        private string uri = "";
        private WebRequest wr;

        public WeatherInfo(string city, string countryCode) {
            uri = BASE_URL + city + "," + countryCode + "&units=metric&appid=" + API_KEY;
            wr = WebRequest.Create(new Uri(uri));
        }
        public dynamic getCurrentWeatherData() {
            try {
                Stream objStream;
                objStream = wr.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                string data = objReader.ReadToEnd();
                if(data != null) {
                    dynamic weatherData = JsonConvert.DeserializeObject<dynamic>(data);
                    return weatherData;
                }
                return null;
                
            } catch(Exception e) {
                MessageBox.Show(e.ToString()); 
                return null;
            }
        }
    }
}
