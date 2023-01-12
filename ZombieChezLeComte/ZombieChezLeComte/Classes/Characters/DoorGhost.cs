using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe permettant de gérer le fantôme porte
    /// Ce fantôme ne bouge pas mais il bouche un chemin, ce qui nous empèche de passer car si on le touche on meurt.
    /// il change de position après un certain temps
    /// </summary>
    public class DoorGhost
    {
        private Charactere ghost;
        private int speed;
        private Vector2 direction;
        private float currentStayTime;
        private int currentPos;
        private SoundEffect ghostSounds;
        private Random aleatoire;
        private float maxTemps;
        private float _timer;

        /// <summary>
        /// Les différentes positions que peut prendre le fantôme
        /// </summary>
        public static Vector2[] positions = new Vector2[]
        {
            new Vector2(200, 45),
            new Vector2(40, -210),
            new Vector2(770, -240),
            new Vector2(-50, 250),
        };

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
        public int CurrentPos
        {
            get
            {
                return this.currentPos;
            }

            set
            {
                this.currentPos = value;
            }
        }
        public float CurrentStayTime
        {
            get
            {
                return this.currentStayTime;
            }

            set
            {
                this.currentStayTime = value;
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
            Random random = new Random();
            this.Ghost = new Charactere();
            this.Ghost.Initialize(_position, 1);
            this.Speed = _speed;
            this.currentPos = random.Next(0, DoorGhost.positions.Length);
            this.Ghost.Position += DoorGhost.positions[this.CurrentPos];
            this.CurrentStayTime = random.Next(Constantes.DOOR_GHOST_MIN_STAY_TIME, Constantes.DOOR_GHOST_MAX_STAY_TIME);
            this.Aleatoire = new Random();
        }

        public void LoadContent(SpriteSheet _spritesheet, Game1 _game)
        {
            this.Aleatoire = new Random();
            this.Timer = 0;
            this.MaxTemps = Aleatoire.Next(5, 6);
            this.Ghost.LoadContent(_spritesheet);
            this.GhostSounds=  _game.Content.Load<SoundEffect>("GhostDoor");
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime);
            // Si le joueur le touche on tue le joueur
            if(this.Ghost.SpriteRect.Intersects(_commonNight.Player.SpriteRect) && !Constantes.GOD_MOD)
            {
                _game.killBy = "doorGhost";
                _game.LoadJumpScare();
            }
            _timer -= (float)_gameTime.GetElapsedSeconds();
            // On joue le son si le joueur est assez proche
            if (this.Timer <= 0 && Vector2.Distance(_commonNight.Player.Position, this.Ghost.Position) <= 150)
            {
                GhostSounds.Play();
                MaxTemps = Aleatoire.Next(5, 6);
                Timer = MaxTemps;
            }
            // Il ne reste pas indéfiniment sur sa position donc après un certain temps on la change
            this.CurrentStayTime -= (float)_gameTime.ElapsedGameTime.TotalSeconds;
            if(this.CurrentStayTime < 0)
            {
                this.ChangePos();
            }
        }

        public void Draw(SpriteBatch _sb)
        {
            this.Ghost.Draw(_sb);
        }

        /// <summary>
        /// Méthode permettant de déplacer le fantôme à une autre position aléatoire parmis ces posiitons possibles
        /// </summary>
        public void ChangePos()
        {
            Random random = new Random();
            this.Ghost.Position -= DoorGhost.positions[this.CurrentPos];
            this.currentPos = random.Next(0, DoorGhost.positions.Length);
            this.Ghost.Position += DoorGhost.positions[this.CurrentPos];
            this.CurrentStayTime = random.Next(Constantes.DOOR_GHOST_MIN_STAY_TIME, Constantes.DOOR_GHOST_MAX_STAY_TIME);
        }
        
    }
}
