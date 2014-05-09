using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph
{
    public sealed class Node : GameElement
    {
        private Texture2D tex;
        public Node(LogicGraph game, NodeBase data)
            : base(game)
        {
            NodeData = data;
            this.DrawOrder = 1;
        }
        public NodeBase NodeData { get; set; }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            Game.SpriteBatch.Draw(tex, new SharpDX.Vector2(this.Position.X, this.Position.Y), Color.White);
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            tex = Content.Load<Texture2D>(string.Format("Node_{0}.png", NodeData.Definition.Id));
            this.Position = new Vector2(NodeData.Position.Item1 - tex.Width / 2, NodeData.Position.Item2 - tex.Height / 2);
            this.Size = new Size2(tex.Width, tex.Height);
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            NodeData.Dispose();
            NodeData = null;
            base.UnloadContent();
        }

        public override void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
