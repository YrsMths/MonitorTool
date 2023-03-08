using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServer
{
    public class Prod<T> : ProdComponent
    {
        public T data { get; set; }
        public List<ProdComponent> children = new List<ProdComponent>();
        public Prod(string name) : base(name)
        {
            this.Name = name;
        }
        
        public override void Add(ProdComponent prod)
        {
            children.Add(prod);
        }

        public override void Remove(ProdComponent prod)
        {
            children.Remove(prod);
        }
        
        public override void Display(int dept)
        {
            Console.WriteLine(new string('-', dept) + Name);
            foreach (var item in children)
            {
                item.Display(dept + 1);
            }
        }

        public ProdComponent FindChild(string name)
        {
            return children.Find(x => x.Name == name);
        }
    }
}
