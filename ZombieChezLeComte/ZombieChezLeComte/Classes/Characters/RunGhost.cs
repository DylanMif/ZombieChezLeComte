using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe permettant de gérer le fantôme coureure
    /// Ce fantôme apparait hors de l'écran et fonce assez rapidement vers le joueur.
    /// Cependant une fois lancé il ne change plus de direction
    /// </summary>
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
            // On utilise de l'aléatoire pour le faire réapparaître
            if(random.Next(0, Constantes.NIGHT3_RUNGHOST_CHANCE) == 1)
            {
                this.Spawn(_commonNight);
            }
            if(this.Enable)
            {
                this.Ghost.Movement(this.Direction * this.Speed, (float)_gameTime.ElapsedGameTime.TotalSeconds, false);
                if(_commonNight.Player.SpriteRect.Intersects(this.Ghost.SpriteRect) && !Constantes.GOD_MOD)
                {
                    // S'il touche le joueur on tue le joueur
                    _game.killBy = "runGhost";
                    _game.LoadJumpScare();
                }
            }
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime);
            _timer -= (float)_gameTime.GetElapsedSeconds();
            // On joue un son si le joueur est assez proche
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

        /// <summary>
        /// Méthode faisant apparaître le fantôme quelque part et lui donnant la direction actuel vers joeur
        /// </summary>
        /// <param name="_commonNight"></param>
        public void Spawn(CommonNight _commonNight)
        {
            this.Ghost.Position = this.ChooseRandom();
            this.Enable = true;
            this.Direction = Additions.Normalize(_commonNight.Player.Position - this.Ghost.Position);
        }

        /// <summary>
        /// Méthode retournant des coordonnées qui sont en dehors de l'écran
        /// </summary>
        /// <returns></returns>
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
