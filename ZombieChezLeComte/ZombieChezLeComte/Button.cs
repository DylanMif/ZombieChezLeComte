using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombieChezLeComte
{
    public class Button
    {
        private string text;
        private string writeText;
        private Color bgColor;
        private Vector2 position;
        private uint width;
        private uint height;
        private SpriteFont font;
        private uint paddindTop;
        private uint paddingLeft;
        private string name;

        private string hoverText;
        private Color currentColor;
        private Color clickBgColor;

        private Texture2D rectTex;

        private Color textColor;
        private Color hoverTextColor;
        private Color clickTextColor;
        private Color currentTextColor;

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        public Color BgColor
        {
            get
            {
                return this.bgColor;
            }

            set
            {
                this.bgColor = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public uint Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }

        public uint Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
            }
        }

        public string HoverText
        {
            get
            {
                return this.hoverText;
            }

            set
            {
                this.hoverText = value;
            }
        }

        public Color CurrentColor
        {
            get
            {
                return this.currentColor;
            }

            set
            {
                this.currentColor = value;
            }
        }

        public Color ClickBgColor
        {
            get
            {
                return this.clickBgColor;
            }

            set
            {
                this.clickBgColor = value;
            }
        }

        public Rectangle ButtonRect
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)this.Width, (int)this.Height);
            }
        }

        public string WriteText
        {
            get
            {
                return this.writeText;
            }

            set
            {
                this.writeText = value;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return this.font;
            }

            set
            {
                this.font = value;
            }
        }

        public uint PaddindTop
        {
            get
            {
                return this.paddindTop;
            }

            set
            {
                this.paddindTop = value;
            }
        }

        public uint PaddingLeft
        {
            get
            {
                return this.paddingLeft;
            }

            set
            {
                this.paddingLeft = value;
            }
        }

        public Texture2D RectTex
        {
            get
            {
                return this.rectTex;
            }

            set
            {
                this.rectTex = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }

            set
            {
                this.textColor = value;
            }
        }

        public Color HoverTextColor
        {
            get
            {
                return this.hoverTextColor;
            }

            set
            {
                this.hoverTextColor = value;
            }
        }

        public Color ClickTextColor
        {
            get
            {
                return this.clickTextColor;
            }

            set
            {
                this.clickTextColor = value;
            }
        }

        public Color CurrentTextColor
        {
            get
            {
                return this.currentTextColor;
            }

            set
            {
                this.currentTextColor = value;
            }
        }

        public void Initialize(string _text, Color _bgColor, Vector2 _position, uint _width, uint _height, Color _clickBgColor, 
            uint _paddingTop, uint _paddingLeft, Color _textColor, Color _hoverTextColor, Color _clickTextColor, string _hoverText)
        {
            this.Text = _text;
            this.BgColor = _bgColor;
            this.Position = _position;
            this.Width = _width;
            this.Height = _height;
            this.ClickBgColor = _clickBgColor;
            this.PaddindTop = _paddingTop;
            this.PaddingLeft = _paddingLeft;
            this.TextColor = _textColor;
            this.HoverTextColor = _hoverTextColor;
            this.ClickTextColor = _clickTextColor;
            this.HoverText = _hoverText;
            this.WriteText = this.Text;
        }

        public void LoadContent(SpriteFont _font, Texture2D _rectText)
        {
            this.Font = _font;
            this.RectTex = _rectText;
        }

        public void Update(MouseState _ms, Game1 game)
        {
            if (_ms.LeftButton == ButtonState.Pressed && this.ButtonRect.Intersects(new Rectangle(_ms.Position, new Point(1, 1))))
            {
                this.CurrentColor = this.ClickBgColor;
                this.CurrentTextColor = this.ClickTextColor;
                if (this.Text == "Nouvelle Partie")
                {
                    game.LoadIntro();
                } else if(this.Text == "Continuer")
                {
                    int nightNumber = DataSaver.LoadNight();
                    switch(nightNumber)
                    {
                        case 0:
                            game.LoadIntro();
                            break;
                        case 1:
                            game.LoadNight1();
                            break;
                        case 2:
                            game.LoadNight2();
                            break;
                        case 3:
                            game.LoadNight3();
                            break;
                        case 4:
                            game.LoadNight4();
                            break;
                        case 5:
                            game.LoadNight5();
                            break;
                    }
                } else if(this.Text == "Quitter")
                {
                    game.Exit();
                } else if(this.Text == "Commandes")
                {
                    game.LoadCommand();
                }
                return;
            }
            else
            {
                this.CurrentColor = this.BgColor;
                this.CurrentTextColor = this.TextColor;
            }
            if (this.ButtonRect.Intersects(new Rectangle(_ms.Position, new Point(1, 1))))
            {
                this.WriteText = this.HoverText;
                this.CurrentTextColor = this.HoverTextColor;
                
            } else
            {
                this.WriteText = this.Text;
                this.CurrentTextColor = this.TextColor;
            }

           
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(this.RectTex, this.ButtonRect, this.BgColor);
            _sb.DrawString(this.Font, this.WriteText, this.Position + new Vector2(this.PaddingLeft, this.PaddindTop),
                this.CurrentTextColor);
        }
    }
}
