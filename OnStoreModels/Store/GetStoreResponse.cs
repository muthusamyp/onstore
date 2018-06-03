using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels
{
    [Serializable]
    public class GetStoreResponse
    {
       public bool Success { get; set; }
       public List<StoreItem> StoreItems { get; set; }
    }
}
