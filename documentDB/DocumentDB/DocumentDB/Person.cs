using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocumentDB;

namespace DocumentDB
{
    class Person
    {
        [JsonProperty(PropertyName="id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual Email Email { get; set; } 
 
        public virtual PhoneNr PhoneNr { get; set; }

 
        public PrimaryAddress PrimaryAddress { get; set; }

        public AltAddress AltAddress { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

