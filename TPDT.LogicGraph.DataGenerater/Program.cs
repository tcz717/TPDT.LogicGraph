using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TPDT.LogicGraph.Base;
using TPDT.LogicGraph.Default;

namespace TPDT.LogicGraph.DataGenerater
{
    class Program
    {
        static void Main(string[] args)
        {
            int i=0;

            if(!File.Exists("TPDT.LogicGraph.Default.dll"))
            {
                Console.WriteLine("no dll");
                Console.ReadKey();
            }

            Assembly ass = Assembly.LoadFrom("TPDT.LogicGraph.Default.dll");

            ResourceManager rm = new ResourceManager();
            rm.LoadedAssemblys.Add(ass);
            rm.NodeDefinitions.Add(i, new NodeDefinition()
            {
                DefaultDescription = "this is a node",
                DefaultName = "Node",
                Id = i++,
                EntityType = typeof(DefaultNode).FullName,
            });

            i = 0;
            rm.ArmyDefinitions.Add(i, new ArmyDefinition()
            {
                DefaultAttack = 1,
                DefaultAttackRange = 1,
                DefaultMove = 1,
                DefaultDescription = "this is a soilder",
                DefaultName = "Soilder",
                Id = i++,
                EntityType = typeof(Soilder).FullName,
            });

            i = 0;
            rm.CardDefinitions.Add(i, new CardDefinition()
            {
                DefaultDescription = "this is a card",
                DefaultName = "Soilder",
                Id = i++,
                EntityType = typeof(Spirit).FullName,
            });
            rm.CardDefinitions[0].Tags.Add("AttackIncrement", "1");
            rm.CardDefinitions[0].Tags.Add("Span", "1");

            rm.RoadDefinitions.Add(0, typeof(TwoWayRoad).FullName);

            rm.Save("default.def");

            ResourceManager r = new ResourceManager();
            r.Load("default.def");

            NodeBase nd = NodeBase.CreateNode(r.NodeDefinitions[0], new Tuple<float, float>(0, 0));

            Console.ReadKey();
        }
    }
}
