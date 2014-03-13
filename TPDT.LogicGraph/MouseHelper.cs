using SharpDX;
using SharpDX.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph
{
    public sealed class MouseHelper : GameComponent
    {
        public MouseManager MouseManager { get; set; }
        public MouseState CurrentState { get; set; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(Game.GraphicsDevice.Viewport.Width * CurrentState.X, Game.GraphicsDevice.Viewport.Height * CurrentState.Y);
            }
        }

        public static MouseHelper CurrentHelper { get; set; }

        public MouseHelper(LogicGraph game)
            : base(game)
        {
            MouseManager = new MouseManager(game);
            CurrentHelper = this;
            this.UpdateOrder = -1;
        }

        public override void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            this.CurrentState = MouseManager.GetState();
            base.Update(gameTime);
        }
    }
}
