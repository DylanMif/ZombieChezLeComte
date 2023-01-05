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

namespace ZombieChezLeComte
{
    public class DoorGhost
    {
        private Charactere ghost;
        private int speed;
        private Vector2 direction;

        public static Vector2[] position = new Vector2[]
        {
            new Vector2(100, 100)
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
        }

        public void LoadContent(SpriteSheet _spritesheet)
        {
            this.Ghost.LoadContent(_spritesheet);
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.Ghost.Update(_gameTime);
            this.Ghost.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime, false);
            if(this.Ghost.SpriteRect.Intersects(_commonNight.Player.SpriteRect))
            {
                _game.LoadMainMenu();
            }
        }

        public void Draw(SpriteBatch _sb)
        {
            this.Ghost.Draw(_sb);
        }
    }
}
