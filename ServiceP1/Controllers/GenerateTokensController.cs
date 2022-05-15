﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceP1.Controllers
{
    [Route("p1")]
    [ApiController]
    public class GenerateTokensController : ControllerBase
    {
        public HttpClient client = new HttpClient();
        public List<string> tokensList = new List<string>();
        public List<string> ProcessesTimeList = new List<string>();
        public Random random = new Random();
        public P1BodyModel model = new P1BodyModel();
        public int count = 0;
        public Stopwatch stopwatchMacro;
        public Stopwatch stopwatchMicro;

        [HttpGet]
        public async Task<List<string>> P1CreateHashes()
        {
            stopwatchMacro = new Stopwatch();
            stopwatchMicro = new Stopwatch();

            stopwatchMacro.Start();
            while (stopwatchMacro.ElapsedMilliseconds < 4850)
            {
                stopwatchMicro.Start();

                model.n = random.Next(5000, 15000);
                model.code = random.Next(10000000) + 10000000;

                var jsonModel = JsonConvert.SerializeObject(model);
#if DEBUG
                var request = new HttpRequestMessage(HttpMethod.Post, "https://feitokengeneratorp2.azurewebsites.net/p2");
#else
                var request = new HttpRequestMessage(HttpMethod.Post, "https://feitokengeneratorp2.azurewebsites.net/p2");
#endif
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(jsonModel, Encoding.UTF8);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                tokensList.Add(content + " (tempo de geração - " + stopwatchMicro.ElapsedMilliseconds.ToString() + " ms).\n");

                stopwatchMicro.Stop();
                stopwatchMicro.Reset();
            }
            stopwatchMacro.Stop();
            tokensList.Add((tokensList.Count() - 1).ToString() + " tokens em: " + stopwatchMacro.ElapsedMilliseconds.ToString());
            
            return tokensList;
        }
    }
}
