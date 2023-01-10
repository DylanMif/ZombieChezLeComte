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
    /// <summary>
    /// Classe gérant les textInfo, les textes qui s'affiche petit à petit à l'écran (un caractère par frame).
    /// Après avoir été complétement écrit il reste à l'écran pendant un certain temps avant de disparaître
    /// </summary>
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
        private bool isFinished;
        private bool textFullyWritten;

        /// <summary>
        /// Si le textInfo est en train d'écrire du texte, son texte ne pourra pas être changer
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }
            
            set
            {
                if(!this.WritingText)
                    this.text = value;
            }
        }
        /// <summary>
        /// Indique le texte affiché actuellement à l'écran alors que la propriété Text contient le texte qui sera écri
        /// </summary>
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

        /// <summary>
        /// Le boolean indique si le TextInfo est entrain d'écrire du texte ou non
        /// </summary>
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

        // boolean indiquant si le TextInfo à completement fini son travail
        public bool IsFinished
        {
            get
            {
                return this.isFinished;
            }

            set
            {
                this.isFinished = value;
            }
        }

        // boolean indiquant si le texte est completement écrit
        public bool TextFullyWritten
        {
            get
            {
                return this.textFullyWritten;
            }

            set
            {
                this.textFullyWritten = value;
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
            this.IsFinished = false;
            this.TextFullyWritten = false;
        }

        public void LoadContent(SpriteFont _font)
        {
            this.Font = _font;
        }

        public void Update(GameTime gameTime)
        {
            // Si le textInfo est entrain d'écrire
            if(this.WritingText)
            {
                // Si il a tout écrit
                if(this.WriteText.Length == this.Text.Length)
                {
                    // On commande a chronométré ça duré de vie
                    this.TextFullyWritten = true;
                    this.CurrentTextLifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                } else
                {
                    // On ajoute un caractère supplémentaire
                    this.WriteText += this.Text[this.WriteText.Length];
                }
            }
            if(this.CurrentTextLifeTime < 0)
            {
                this.WritingText = false;
                this.WriteText = "";
                this.IsFinished = true;
                this.TextFullyWritten = false;
            }
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.DrawString(this.Font, this.WriteText, this.TextPosition, this.TextColor);
        }

        public void ActiveText(float textDuration)
        {
            // Si le textInfo n'est pas déjà occupé on le lance pour une certaine durée
            if(!writingText)
            {
                this.TextDuration = textDuration;
                this.CurrentTextLifeTime = textDuration;
                this.WriteText = "";
                this.WritingText = true;
            }
        }
    }
}
