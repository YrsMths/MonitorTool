using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServer
{
    public abstract class ProdComponent
    {
        public string Name { get; set; }
        public ProdComponent(string name)
        {
            this.Name = name;
        }

        public abstract void Add(ProdComponent prod);
        public abstract void Remove(ProdComponent prod);
        public abstract void Display(int dept);
        
    }
}
