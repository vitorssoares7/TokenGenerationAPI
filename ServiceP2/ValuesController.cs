using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceP2.Models;
using ServiceP2.Services;
using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceP2{
    [Route("p2")]
    [ApiController]
    public class ValuesController : ControllerBase{
        public IValidationService _validationService;
        public IFindPrimes _findPrimesService;
        public HttpClient client = new HttpClient();
        public ValuesController(IValidationService validationService, IFindPrimes findPrimesService)
        {
            _validationService = validationService;
            _findPrimesService = findPrimesService;
        }

        [HttpPost]
        public async Task<string> P2ValidationAsync([FromBody] P2BodyModel model){
           try{
            bool validated = _validationService.ValidateInformations(model);

            if (validated){
                try{
                    string primeHash = _findPrimesService.GenerateHash(model.n, model.code);
                    return "Hash Gerada: " + primeHash;
                }
                catch(Exception ex){
                    return ex.Message;
                }
            }
            else
                return "Parâmetros inválidos";
            
           }
           catch(Exception ex){
            return ex.Message;     
           }
           
        }
    }
}

