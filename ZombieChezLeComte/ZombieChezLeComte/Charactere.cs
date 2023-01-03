using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieChezLeComte
{
    internal class Charactere
    {
        private Texture2D _texture;
        private Vector2 _position;
        private int _vitesse;

        public Texture2D Texture
        {
            get
            {
                return this._texture;
            }

            set
            {
                this._texture = value;
            }
        }
        public Vector2 Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }
        public int Vitesse
        {
            get
            {
                return this._vitesse;
            }

            set
            {
                this._vitesse = value;
            }
        }


        public void Initialize(Texture2D _texture2D, Vector2 _position, int _vitesse)
        {
            this.Position = _position;
            this.Texture = _texture2D;
            this.Vitesse = _vitesse;
        }

        public void LoadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(this.Texture, this.Position, Color.White);
            _spriteBatch.End();
        }
        public void Movement(Vector2 _vector2, float _delattime)
        {
            this.Position += _vector2 * this.Vitesse * _delattime;
        }
    }
}
