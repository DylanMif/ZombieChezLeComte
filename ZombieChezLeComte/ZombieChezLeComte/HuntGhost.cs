using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    /// <summary>
    /// Classe permettant de gérer le fantôme chasseur
    /// </summary>
    public class HuntGhost
    {
        private Charactere ghost;
        private int speed;
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
            this.Aleatoire= new Random();
        }

        public void LoadContent(SpriteSheet _spritesheet, Game1 _game)
        {
            this.Ghost.LoadContent(_spritesheet);
            this.Ghost.Perso.Alpha = 0.2f;
            this.GhostSounds = _game.Content.Load<SoundEffect>("GhostHunt");
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime);
            this.Ghost.Movement(Additions.Normalize(_commonNight.Player.Position - this.Ghost.Position) * this.Speed,
                _commonNight.DeltaTime, false);
            // Si le joueur le touche on tue le joueur
            if (_commonNight.Player.SpriteRect.Intersects(this.Ghost.SpriteRect) && !Constantes.GOD_MOD)
            {
                _game.killBy = "huntGhost";
                _game.LoadJumpScare();
            }
            // On joue le son si le joueur est proche
            if (this.Timer <= 0 && Vector2.Distance(_commonNight.Player.Position, this.Ghost.Position) <= 150)
            {
                GhostSounds.Play();
                MaxTemps = Aleatoire.Next(5, 6);
                Timer = MaxTemps;
            }
            _timer -= (float)_gameTime.GetElapsedSeconds();
        }

        public void Draw(SpriteBatch _sb, CommonNight _commonNight)
        {
            // Comme il deveint invisible s'il se rapproche trop du joueur on le dessine juste plus
            // Si ça distance au joueur est inférieur à 10 on le redessine, car à cette distance il tuera
            // le joueur, on le fait donc réapparaître pour que le joueur voit un minimum pourquoi il meurt
            if(Vector2.Distance(this.Ghost.Position, _commonNight.Player.Position) > 150 ||
                Vector2.Distance(this.Ghost.Position, _commonNight.Player.Position) < 10)
                this.Ghost.Draw(_sb);
        }
    }
}
