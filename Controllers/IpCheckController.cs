using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIConfig.Class;
using APIConfig.Interface;
using APIConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APIConfig.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpCheckController : ControllerBase
    {
        private readonly ProjectManager2Context _context;
        private readonly HttpClient _httpClient;
        private readonly ILogger<IpCheckController> _logger;
        public IpCheckController(ILogger<IpCheckController> logger,HttpClient httpClient,ProjectManager2Context context)
        {
            _logger = logger;
            _httpClient = httpClient;
            _context = context;
        }

        [HttpPost("Country")]
        public async Task<IActionResult> GetIpFromClient([FromBody] IpRequest request)
        {   
            var ipAddress = request.IpAddress;
            string apiKey = "23c2ac8967e5fd"; // Replace with your IPinfo API key
            string url = $"https://ipinfo.io/{ipAddress}?token={apiKey}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                List<IpInfo> infoList = new List<IpInfo>{
                    JsonConvert.DeserializeObject<IpInfo>(responseContent)
                };

                var countryInfo = ParseCountryFromResponse(responseContent);

                if(countryInfo == "TH"){
                    
                    var rsult = await _context.UserManagers.Where(a => a.Password == ipAddress.ToString()).SingleOrDefaultAsync();
                    if(rsult == null){
                        return NotFound("IP Invalid");
                    }
                    
                    
                    return Ok(responseContent);
                }
                else{
                     return BadRequest(responseContent);
                }
                //return Ok(new { Country = countryInfo });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("Error fetching data from IPinfo.io: " + ex.Message);
                return StatusCode(500, "Failed to retrieve country information.");
            }
        }

        private string ParseCountryFromResponse(string responseContent)
        {
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return jsonResponse.country;
        }
    }

      public class IpRequest
        {
            public string IpAddress { get; set; }
        }
    public class IpInfo
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Org { get; set; }
        public string Postal { get; set; }
        public string Timezone { get; set; }
    }
}