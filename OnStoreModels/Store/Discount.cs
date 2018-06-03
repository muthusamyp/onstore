using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnStoreModels
{
    [Serializable]
    public class Discount
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Percentage { get; set; }

        public Discount() {}
        public Discount(int id, string name, string desc, int percentage)
        {
            this.ID = id;
            this.Name = name;
            this.Desc = desc;
            this.Percentage = percentage;
        }
    }

}
