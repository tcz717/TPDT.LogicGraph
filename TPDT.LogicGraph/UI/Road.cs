using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPDT.LogicGraph.Base;

namespace TPDT.LogicGraph
{
    public sealed class Road : GameElement, IColor
    {
        public RoadBase RodeData { get; set; }
        public Color Color { get; set; }

        private Texture2D line;
        public Road(LogicGraph game, RoadBase data)
            : base(game)
        {
            RodeData = data;
            this.Position = new Vector2(data.Node1.Position.Item1
                + data.Node2.Position.Item1, data.Node1.Position.Item2 + data.Node2.Position.Item2) / 2;
            this.Size = new Size2();
            Color = Color.Green;
            this.DrawOrder = 0;
        }
        protected override void LoadContent()
        {
            line = Content.Load<Texture2D>("Line.png");
            base.LoadContent();
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(SharpDX.Toolkit.GameTime gameTime)
        {
            Vector2 p1=new Vector2(RodeData.Node1.Position.Item1,RodeData.Node1.Position.Item2),
                p2=new Vector2(RodeData.Node2.Position.Item1,RodeData.Node2.Position.Item2);
            DrawLine(p1, p2, this.RodeData.Middle, line, Game, Color, RodeData.RoadDisplayMode);
            base.Draw(gameTime);
        }

        public static void DrawLine(Vector2 p1, Vector2 p2, int mid, Texture2D line,
            LogicGraph game, Color color, RoadDisplayMode mode)
        {
            Vector2 m1, m2;
            switch (mode)
            {
                case RoadDisplayMode.Direct:
                default:
                    game.SpriteBatch.Draw(line, p1, new Rectangle(0, 2, (int)((p2 - p1).Length()), 4)
                        , color, getAngle(p1, p2), Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
                case RoadDisplayMode.DownTuring:
                    m1 = new Vector2(p1.Y > p2.Y ? p2.X : p1.X, Math.Max(p1.Y, p2.Y));
                    game.SpriteBatch.Draw(line, p1, new Rectangle(0, 2, (int)((m1 - p1).Length()), 4)
                        , color, getAngle(p1, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, p2, new Rectangle(0, 2, (int)((m1 - p2).Length()), 4)
                        , color, getAngle(p2, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
                case RoadDisplayMode.UpTurning:
                    m1 = new Vector2(p1.Y < p2.Y ? p2.X : p1.X, Math.Min(p1.Y, p2.Y));
                    game.SpriteBatch.Draw(line, p1, new Rectangle(0, 2, (int)((m1 - p1).Length()), 4)
                        , color, getAngle(p1, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, p2, new Rectangle(0, 2, (int)((m1 - p2).Length()), 4)
                        , color, getAngle(p2, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
                case RoadDisplayMode.HorizontalSurround:
                    m1 = new Vector2(p1.X, mid);
                    m2 = new Vector2(p2.X, mid);
                    game.SpriteBatch.Draw(line, p1, new Rectangle(0, 2, (int)((m1 - p1).Length()), 4)
                        , color, getAngle(p1, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, m1, new Rectangle(0, 2, (int)((m1 - m2).Length()), 4)
                        , color, getAngle(m1, m2), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, p2, new Rectangle(0, 2, (int)((m2 - p2).Length()), 4)
                        , color, getAngle(p2, m2), Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
                case RoadDisplayMode.VerticalSurround:
                    m1 = new Vector2(mid, p1.Y);
                    m2 = new Vector2(mid, p2.Y);
                    game.SpriteBatch.Draw(line, p1, new Rectangle(0, 2, (int)((m1 - p1).Length()), 4)
                        , color, getAngle(p1, m1), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, m1, new Rectangle(0, 2, (int)((m1 - m2).Length()), 4)
                        , color, getAngle(m1, m2), Vector2.Zero, 1, SpriteEffects.None, 0);
                    game.SpriteBatch.Draw(line, p2, new Rectangle(0, 2, (int)((m2 - p2).Length()), 4)
                        , color, getAngle(p2, m2), Vector2.Zero, 1, SpriteEffects.None, 0);
                    break;
            }
        }
         public static void DrawLine(Vector2 p1, Vector2 p2,Texture2D line, 
            LogicGraph game, Color color, RoadDisplayMode mode )
        {
            DrawLine(p1, p2, 0, line, game, color, mode);
        }

        protected override void UnloadContent()
        {
            RodeData.Dispose();
            RodeData = null;
            base.UnloadContent();
        }

        private static float getAngle(Vector2 p1, Vector2 p2)
        {
            Vector2 tmp = p2 - p1;
            tmp.Normalize();
            double result = Vector2.Dot(tmp, Vector2.UnitX);
            result = Math.Acos(result);
            if (p1.Y > p2.Y)
                result = -result;
            return (float)result;
        }
    }
}
