using Adder.Visitors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Components
{
    public abstract class Component
    {
        public string Name { get; set; }
        public string OutputName { get; set; } = null;
        public int Visited { get; set; } = 0;
        public bool Resolved { get; set; } = false;
        public bool Printed { get; set; } = false;
        public TimeSpan TimeSpan { get; set; }
        public string ClassType { get { return this.GetType().Name;} }


        public virtual void Run(IVisitor visitor)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            this.Accept(visitor);

            timer.Stop();
           
            this.TimeSpan = timer.Elapsed;
        }


        public virtual void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void PrintTime()
        {
            Console.WriteLine("{0} ran for: {1} (ns)",  this.Name, GetTime());
        }

        public long GetTime()
        {
            // 1 tick = 100 ns
            return (long) TimeSpan.Ticks * 100;
        }

    }
}
