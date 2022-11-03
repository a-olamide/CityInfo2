using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo2.API.Services
{
    public class LocalMailService : IMailService
    {
       
        private IConfiguration _config;

        public LocalMailService(IConfiguration configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void Send(string subject, string message)
        {
            Debug.WriteLine($"mail from {_config["mailSettings:mailFromAddress"]} to {_config["mailSettings:mailToAddress"]} from Local mail service");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");

         
        }
    }
}
