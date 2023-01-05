using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace ZombieChezLeComte
{
    public class Zombie
    {
        private Charactere zombieChar;
        private int speed;

        public Charactere ZombieChar
        {
            get
            {
                return this.zombieChar;
            }

            set
            {
                this.zombieChar = value;
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

        public void Initialiaze(Vector2 _position, int _speed)
        {
            this.ZombieChar = new Charactere();
            this.ZombieChar.Initialize(_position, 1);
            this.Speed = _speed;
        }

        public void LoadContent(SpriteSheet _spriteSheet)
        {
            this.ZombieChar.LoadContent(_spriteSheet);
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight)
        {
            this.ZombieChar.Update(_gameTime);
            this.ZombieChar.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime, false);

            Vector2 dir = Additions.Normalize(_commonNight.Player.Position - this.ZombieChar.Position);
            ushort tileX = (ushort)(this.GetMapPos().X / _commonNight.TiledMap.TileWidth);
            ushort tileY = (ushort)(this.GetMapPos().Y / _commonNight.TiledMap.TileHeight);
            //Console.WriteLine(tileX + ", " + tileY);
            Console.WriteLine(_commonNight.Camera.ScreenToWorld(this.ZombieChar.Position));
        }

        public void Draw(SpriteBatch _sb)
        {
            this.ZombieChar.Draw(_sb);
        }

        public bool IsCollision(ushort x, ushort y, TiledMapTileLayer mapLayer)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            mapLayer.TryGetTile(x, y, out tile);
            //Console.WriteLine(tile);
            //Console.WriteLine(tile);
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        public Vector2 GetMapPos()
        {
            Vector2 res = this.ZombieChar.Position;
            res.X = res.X;
            res.Y = res.Y;
            return res;
        }
    }
}
