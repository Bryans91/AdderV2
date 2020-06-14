using System;
using Adder.Components;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adder.Builders
{
    interface IBuilder
    {
        void setName(String name);
        void AddDefaultInput(string name, bool input);
        Node Result();
    }
}
