using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph.Base
{
    public abstract class GameEntity : IDisposable
    {
        public int Id { get; protected set; }

        public string Description { get; protected set; }

        public string Name { get; set; }

        public abstract void Dispose();
    }
}
