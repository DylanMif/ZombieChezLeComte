﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombieChezLeComte
{
    public class TextInfo
    {
        private string text;
        private string writeText;
        private Color textColor;
        private Vector2 textPosition;
        private SpriteFont font;
        private bool writingText;
        private float textDuration;
        private float currentTextLifeTime;

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

        public Vector2 TextPosition
        {
            get
            {
                return this.textPosition;
            }

            set
            {
                this.textPosition = value;
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

        public bool WritingText
        {
            get
            {
                return this.writingText;
            }

            set
            {
                this.writingText = value;
            }
        }

        public float TextDuration
        {
            get
            {
                return this.textDuration;
            }

            set
            {
                this.textDuration = value;
            }
        }

        public float CurrentTextLifeTime
        {
            get
            {
                return this.currentTextLifeTime;
            }

            set
            {
                this.currentTextLifeTime = value;
            }
        }

        public void Initialize(string _text, Color _textColor, Vector2 textPosition)
        {
            this.Text = _text;
            this.TextColor = _textColor;
            this.TextPosition = textPosition;
            this.writeText = "";
            this.writingText = false;
            this.CurrentTextLifeTime = 0;
        }

        public void LoadContent(SpriteFont _font)
        {
            this.Font = _font;
        }

        public void Update(GameTime gameTime)
        {
            if(this.WritingText)
            {
                if(this.WriteText.Length == this.Text.Length)
                {
                    this.CurrentTextLifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else
                {
                    this.WriteText += this.Text[this.WriteText.Length];
                }
            }
            if(this.CurrentTextLifeTime <= 0)
            {
                this.WritingText = false;
                this.WriteText = "";
            }
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.DrawString(this.Font, this.WriteText, this.TextPosition, this.TextColor);
        }

        public void ActiveText(float textDuration)
        {
            this.TextDuration = textDuration;
            this.CurrentTextLifeTime = textDuration;
            this.WriteText = "";
            this.WritingText = true;
        }
    }
}