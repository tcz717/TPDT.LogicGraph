using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPDT.LogicGraph.UI
{
    class Button : TPDT.LogicGraph.GameElement, TPDT.LogicGraph.IColor
    {
        private string backName;
        public event EventHandler Click;

        public Button(LogicGraph game, string back, string text, Vector2 position, Size2 size, SpriteFont font, Color color)
            : base(game)
        {
            backName = back;
            Text = text;
            Position = position;
            Size = size;
            Font = font;
            Color = color;
            this.OnButtonDown += Button_OnButtonDown;
            this.OnButtonUp += Button_OnButtonUp;
            this.OnHover += Button_OnHover;
            this.OnLeave += Button_OnLeave;
            this.DrawOrder = 10;
        }

        void Button_OnLeave(object sender, EventArgs e)
        {
            HoverState = false;
        }

        void Button_OnHover(object sender, EventArgs e)
        {
            HoverState = true;
        }

        void Button_OnButtonUp(object sender, EventArgs e)
        {
            PressState = false;
            if (Click != null)
                this.Click(this, new EventArgs());
        }

        void Button_OnButtonDown(object sender, EventArgs e)
        {
            PressState = true;
        }

        public Texture2D Background { get; set; }

        public string Text { get; set; }

        public bool PressState { get; set; }
        public bool HoverState { get; set; }

        public SpriteFont Font { get; set; }

        public SharpDX.Toolkit.Graphics.Texture2D HoverBackground { get; set; }

        public Color Color { get; set; }
        
        public override void Initialize()
        {
            PressState = false;
            HoverState = false;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            if (Background == null)
                Background = Content.Load<Texture2D>(backName);
            base.LoadContent();
        }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            if (PressState || HoverState)
                Game.SpriteBatch.Draw(HoverBackground ?? Background, AbsoluteRectangle, Color.White);
            else
                Game.SpriteBatch.Draw(Background, AbsoluteRectangle, Color.White);
            Game.SpriteBatch.DrawString(Font ?? Game.BasicFont, Text, Position, Color);
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            //if(Background!=null)
            //    Content.
            base.UnloadContent();
        }

        public override void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
