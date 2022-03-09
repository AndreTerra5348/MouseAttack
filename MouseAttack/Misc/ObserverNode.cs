using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MouseAttack.Misc
{
    public abstract class ObserverNode : Control
    {
        protected List<Binding> DataBindings { get; } = new List<Binding>();

        protected void Initialize()
        {
            foreach (Binding propertyBinding in DataBindings)
            {
                propertyBinding.SetProperty();
            }
        }
    }
}
