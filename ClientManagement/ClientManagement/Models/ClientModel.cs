using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientManagement.Controllers;

namespace ClientManagement.Models
{
    public class ClientModel
    {
        public List<CLIENT> clients { get; set; }
        public List<TASK> tasks { get; set; }
    }
}