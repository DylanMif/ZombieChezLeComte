using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;

namespace ZombieChezLeComte
{
    public class Charactere
    {
        private Vector2 _position;
        private int _vitesse;
        private AnimatedSprite _perso;
        private SpriteSheet _spriteSheet;
        private String _currentAnimation;

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
        public AnimatedSprite Perso
        {
            get
            {
                return this._perso;
            }

            set
            {
                this._perso = value;
            }
        }
        public SpriteSheet SpriteSheet
        {
            get
            {
                return this._spriteSheet;
            }

            set
            {
                this._spriteSheet = value;
            }
        }
        public string CurrentAnimation
        {
            get
            {
                return this._currentAnimation;
            }

            set
            {
                this._currentAnimation = value;
            }
        }

        public Rectangle SpriteRect
        {
            get
            {
                return new Rectangle(
                    (int)this.Position.X,
                    (int)this.Position.Y,
                    Constantes.CHARACTER_SPRITE_SIZE,
                    Constantes.CHARACTER_SPRITE_SIZE
                    );
            }
        }

        public void Initialize( Vector2 _position, int _vitesse)
        {
            this.Position = _position;
            this.Vitesse = _vitesse;
            this.CurrentAnimation="idle";
        }

        public void LoadContent(SpriteSheet _spriteSheet)
        {
            this.SpriteSheet = _spriteSheet;
            this.Perso = new AnimatedSprite(_spriteSheet);
        }

        public void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.Perso.Play(this.CurrentAnimation);
            this.Perso.Update(deltaSeconds);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(this.Perso, this.Position);
            _spriteBatch.End();
        }
        public void Movement(Vector2 _vector2, float _delattime, bool isPlayer)
        {
            if (_vector2.X == 0 && _vector2.Y == 0)
            {
                if (this.CurrentAnimation== "estWalk" || this.CurrentAnimation == "idleEst")
                {
                    this.CurrentAnimation = "idleEst";
                }
                else if (this.CurrentAnimation == "northWalk" || this.CurrentAnimation == "idleNorth")
                {
                    this.CurrentAnimation = "idleNorth";
                }
                else if (this.CurrentAnimation == "westWalk" || this.CurrentAnimation == "idleWest")
                {
                    this.CurrentAnimation = "idleWest";
                }
                else if (this.CurrentAnimation == "southWalk" || this.CurrentAnimation == "idle")
                {
                    this.CurrentAnimation = "idle";
                }
            }
            if(_vector2.X > 0)
            {
                this.CurrentAnimation = "estWalk";
            }
            if (_vector2.X <0)
            {
                this.CurrentAnimation = "westWalk";
            }
            if (_vector2.Y > 0)
            {
                this.CurrentAnimation = "southWalk";
            }
            if (_vector2.Y < 0)
            {
                this.CurrentAnimation = "northWalk";
            }
            if(!isPlayer)
                this.Position += _vector2 * this.Vitesse * _delattime;
        }
        public void MovementWithoutAnim(Vector2 _vector2, float _delattime, bool isPlayer)
        {
            if (!isPlayer)
                this.Position += _vector2 * this.Vitesse * _delattime;
        }
    }
}
