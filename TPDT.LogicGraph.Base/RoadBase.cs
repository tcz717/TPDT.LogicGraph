/*
 * 由SharpDevelop创建。
 * 用户： Tangent.CZ
 * 日期: 06/19/2013
 * 时间: 17:02
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using TPDT.LogicGraph.Army;

namespace TPDT.LogicGraph.Base
{
	/// <summary>
	/// Description of RoadBase.
	/// </summary>
    public abstract class RoadBase : GameEntity
    {
        private static int index = 0;
        public new static string Name { get; protected set; }
        public RoadDisplayMode RoadDisplayMode { get;  set; }
        public new static string Description { get; protected set; }
        public int Middle { get;  set; }
        public NodeBase Node1 { get; protected set; }
        public NodeBase Node2 { get; protected set; }
		
		public abstract bool IsMoveable(ArmyBase army);
        public RoadBase(int id, NodeBase node1, NodeBase node2)
        {
            Node1 = node1;
            node1.Roads.Add(this);
            Node2 = node2;
            node2.Roads.Add(this);
            this.RoadDisplayMode = RoadDisplayMode.Direct;
            this.Id = id;
        }

        public static RoadBase CreateRode(int typeId,NodeBase node1,NodeBase node2)
        {
            RoadBase road;

            road = ResourceManager.CurrentResouceManager.LoadedRoads[
                ResourceManager.CurrentResouceManager.RoadDefinitions[typeId]].InvokeMember(
                null,
                System.Reflection.BindingFlags.CreateInstance,
                null,
                null,
                new object[] { index++, node1, node2 }
                ) as RoadBase;

            return road;
        }

        public override void Dispose()
        {
            if (Node1 != null)
                Node1.Roads.Remove(this);
            if (Node2 != null)
                Node2.Roads.Remove(this);
            Node1 = null;
            Node2 = null;
            GC.SuppressFinalize(this);
        }
    }
}
