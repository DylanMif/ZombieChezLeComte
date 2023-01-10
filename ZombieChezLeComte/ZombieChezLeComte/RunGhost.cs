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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    public class RunGhost
    {
        private Charactere ghost;
        private int speed;
        private bool enable;
        private Vector2 direction;
        private SoundEffect ghostSounds;
        private Random aleatoire;
        private float maxTemps;
        private float _timer;

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

        public SoundEffect GhostSounds
        {
            get
            {
                return this.ghostSounds;
            }

            set
            {
                this.ghostSounds = value;
            }
        }

        public Random Aleatoire
        {
            get
            {
                return this.aleatoire;
            }

            set
            {
                this.aleatoire = value;
            }
        }

        public float MaxTemps
        {
            get
            {
                return this.maxTemps;
            }

            set
            {
                this.maxTemps = value;
            }
        }

        public float Timer
        {
            get
            {
                return this._timer;
            }

            set
            {
                this._timer = value;
            }
        }

        public void Initialize(Vector2 _position, int _speed)
        {
            this.Ghost = new Charactere();
            this.Ghost.Initialize(_position, 1);
            this.Speed = _speed;
            this.Enable = false;
            this.Aleatoire = new Random();
        }

        public void LoadContent(SpriteSheet _spritesheet, Game1 _game)
        {
            this.Ghost.LoadContent(_spritesheet);
            this.GhostSounds = _game.Content.Load<SoundEffect>("GhostRun");
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
                if(_commonNight.Player.SpriteRect.Intersects(this.Ghost.SpriteRect) && !Constantes.GOD_MOD)
                {
                    _game.killBy = "runGhost";
                    _game.LoadJumpScare();
                }
            }
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime);
            _timer -= (float)_gameTime.GetElapsedSeconds();
            if (this.Timer <= 0 && Vector2.Distance(_commonNight.Player.Position, this.Ghost.Position) <= 150)
            {
                GhostSounds.Play();
                MaxTemps = Aleatoire.Next(6, 8);
                Timer = MaxTemps;
            }

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
