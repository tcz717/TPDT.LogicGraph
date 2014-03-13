using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public interface ISelectable
    {
        event EventHandler OnNodeSelected;

        event EventHandler OnNodeNotSelected;

        NodeBase SelectNode
        {
            get;
            set;
        }
    
        void Select(NodeBase node);

        bool SelectTest(TPDT.LogicGraph.Base.NodeBase node);
    }
}
