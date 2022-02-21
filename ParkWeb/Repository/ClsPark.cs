using ParkWeb.Models;
using ParkWeb.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParkWeb.Repository
{
    public class ClsPark : ClsRepo<Park>,IPark
    {
        IHttpClientFactory _clientfactory;
        public ClsPark(IHttpClientFactory client) : base(client)
        {
            _clientfactory = client;

        }
    }
}
