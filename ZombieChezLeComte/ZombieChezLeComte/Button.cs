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
                return text;
            }

            set
            {
                text = value;
            }
        }

        public Color BgColor
        {
            get
            {
                return bgColor;
            }

            set
            {
                bgColor = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public uint Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public uint Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public string HoverText
        {
            get
            {
                return hoverText;
            }

            set
            {
                hoverText = value;
            }
        }

        public Color CurrentColor
        {
            get
            {
                return currentColor;
            }

            set
            {
                currentColor = value;
            }
        }

        public Color ClickBgColor
        {
            get
            {
                return clickBgColor;
            }

            set
            {
                clickBgColor = value;
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
                return writeText;
            }

            set
            {
                writeText = value;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return font;
            }

            set
            {
                font = value;
            }
        }

        public uint PaddindTop
        {
            get
            {
                return paddindTop;
            }

            set
            {
                paddindTop = value;
            }
        }

        public uint PaddingLeft
        {
            get
            {
                return paddingLeft;
            }

            set
            {
                paddingLeft = value;
            }
        }

        public Texture2D RectTex
        {
            get
            {
                return rectTex;
            }

            set
            {
                rectTex = value;
            }
        }

        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
            }
        }

        public Color HoverTextColor
        {
            get
            {
                return hoverTextColor;
            }

            set
            {
                hoverTextColor = value;
            }
        }

        public Color ClickTextColor
        {
            get
            {
                return clickTextColor;
            }

            set
            {
                clickTextColor = value;
            }
        }

        public Color CurrentTextColor
        {
            get
            {
                return currentTextColor;
            }

            set
            {
                currentTextColor = value;
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

        public void Update(MouseState _ms)
        {
            if (_ms.LeftButton == ButtonState.Pressed && this.ButtonRect.Intersects(new Rectangle(_ms.Position, new Point(1, 1))))
            {
                this.CurrentColor = this.ClickBgColor;
                this.CurrentTextColor = this.ClickTextColor;
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
            _sb.Begin();
            _sb.Draw(this.RectTex, this.ButtonRect, this.BgColor);
            _sb.DrawString(this.Font, this.WriteText, this.Position + new Vector2(this.PaddingLeft, this.PaddindTop),
                this.CurrentTextColor);
            _sb.End();
        }
    }
}
