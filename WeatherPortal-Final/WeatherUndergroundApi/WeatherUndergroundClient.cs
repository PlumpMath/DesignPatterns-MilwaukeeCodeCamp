﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherUndergroundApi
{
    public class WeatherUndergroundClient
    {


        public CurrentConditionsResponse GetCurrentConditions(String zipCode)
        {
            String apiKey = "??????";
            String uri = $"http://api.wunderground.com/api/{apiKey}/conditions/q/{zipCode}.json";

            HttpClient httpClient = new HttpClient();
            String responseJson = httpClient.GetStringAsync(uri).Result;

            CurrentConditionsResponse response = JsonConvert.DeserializeObject<CurrentConditionsResponse>(responseJson);
            return response;
        }



    }
}
