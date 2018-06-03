using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels.Common
{
    public class Configuration
    {
        public string PayUKey { get; set; }
        public string PayUSalt { get; set; }
        public string PayUSuccessUrl { get; set; }
        public string PayUFailureUrl { get; set; }
    }
}
