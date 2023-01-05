using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace ZombieChezLeComte
{
    public class RunGhost
    {
        private Charactere ghost;
        private int speed;
        private bool enable;
        private Vector2 direction;

        public Charactere Ghost
        {
            get
            {
                return this.ghost;
            }

            set
            {
                this.ghost = value;
            }
        }

        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

        public bool Enable
        {
            get
            {
                return this.enable;
            }

            set
            {
                this.enable = value;
            }
        }

        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }

        public void Initialize(Vector2 _position, int _speed)
        {
            this.Ghost = new Charactere();
            this.Ghost.Initialize(_position, 1);
            this.Speed = _speed;
            this.Enable = false;
        }

        public void LoadContent(SpriteSheet _spritesheet)
        {
            this.Ghost.LoadContent(_spritesheet);
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            Random random = new Random();
            if(random.Next(0, Constantes.NIGHT3_RUNGHOST_CHANCE) == 1)
            {
                this.Spawn(_commonNight);
            }
            if(this.Enable)
            {
                this.Ghost.Movement(this.Direction * this.Speed, (float)_gameTime.ElapsedGameTime.TotalSeconds, false);
                if(_commonNight.Player.SpriteRect.Intersects(this.Ghost.SpriteRect))
                {
                    _game.LoadMainMenu();
                }
            }
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime, false);

        }

        public void Draw(SpriteBatch _sb)
        {
            if (this.Enable)
                this.Ghost.Draw(_sb);
        }

        public void Spawn(CommonNight _commonNight)
        {
            this.Ghost.Position = this.ChooseRandom();
            this.Enable = true;
            this.Direction = Additions.Normalize(_commonNight.Player.Position - this.Ghost.Position);
        }

        public Vector2 ChooseRandom()
        {
            Random random = new Random();
            int r = random.Next(1, 5);
            if (r == 1)
            {
                // il sort du haut
                int x = random.Next(0, Constantes.WINDOW_WIDTH);
                return new Vector2(x, -Constantes.CHARACTER_SPRITE_SIZE);
            } else if(r == 2)
            {
                // il sort de la droite
                int y = random.Next(0, Constantes.WINDOW_HEIGHT);
                return new Vector2(Constantes.WINDOW_HEIGHT, y);
            } else if(r == 3)
            {
                // il sort du bas
                int x = random.Next(0, Constantes.WINDOW_WIDTH);
                return new Vector2(x, Constantes.WINDOW_HEIGHT);
            } else
            {
                // il sort de la gauche
                int y = random.Next(0, Constantes.WINDOW_HEIGHT);
                return new Vector2(-Constantes.CHARACTER_SPRITE_SIZE, y);
            }
        }
    }
}
