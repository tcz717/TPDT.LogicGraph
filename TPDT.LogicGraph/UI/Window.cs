using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TPDT.LogicGraph
{
    public class Window : GameElement, IColor
    {
        private Texture2D back;

        public SharpDX.Color Color { get; set; }

        public Window(LogicGraph game)
            : base(game)
        {
            this.Position = Vector2.Zero;
            this.Size = new Size2(200, 100);
            this.Color = Color.Wheat;
            this.Name = "UnTittle";
        }
        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            Game.SpriteBatch.Draw(back, this.AbsoluteRectangle, Color);
            Game.SpriteBatch.DrawString(Game.BasicFont, Name, this.AbsoluteRectangle.TopLeft, Color.Black);
            base.Draw(gameTime);
        }
        protected override void LoadContent()
        {
            back = this.Content.Load<Texture2D>("Window.png");
            base.LoadContent();
        }
    }
}
