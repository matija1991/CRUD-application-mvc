using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.ViewModel
{
    public class multipleData
    {
        public IEnumerable<Studenti> Student { get; set; }

        public IEnumerable<Kolegiji> Kolegij { get; set; }
    }
}