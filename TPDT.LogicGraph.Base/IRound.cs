using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public interface IRound
    {
        event EventHandler OnStarted;

        event EventHandler OnToggled;

        event EventHandler OnEnded;
    
        int Round
        {
            get;
            set;
        }
    
        void Start();

        void Toggle();
    }
}
