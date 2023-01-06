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
        private Vector2 virtualPos;
        private int lastContinueX;
        private int lastContinueY;

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
        public Vector2 VirtualPos
        {
            get
            {
                return this.virtualPos;
            }

            set
            {
                this.virtualPos = value;
            }
        }
        public int LastContinueX
        {
            get
            {
                return this.lastContinueX;
            }

            set
            {
                this.lastContinueX = value;
            }
        }
        public int LastContinueY
        {
            get
            {
                return this.lastContinueY;
            }

            set
            {
                this.lastContinueY = value;
            }
        }

        public void Initialiaze(Vector2 _position, int _speed)
        {
            this.ZombieChar = new Charactere();
            this.ZombieChar.Initialize(_position, 1);
            this.Speed = _speed;
            this.VirtualPos = new Vector2(this.ZombieChar.Position.X, this.ZombieChar.Position.Y);
            this.LastContinueX = 0;
            this.LastContinueY = 0;
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
            ushort tileX = (ushort)(this.GetMapPos(_commonNight.Camera).X / _commonNight.TiledMap.TileWidth);
            ushort tileY = (ushort)(this.GetMapPos(_commonNight.Camera).Y / _commonNight.TiledMap.TileHeight);
            Console.WriteLine(this.GetIntDir(dir));
            
            if (!this.IsCollision((ushort)(tileX + this.GetIntDir(dir).X), (ushort)(tileY + this.GetIntDir(dir).Y), _commonNight.MapLayer)) 
            {
                this.VirtualPos += dir * this.Speed * _commonNight.DeltaTime;
                this.ZombieChar.Movement(dir * this.Speed, _commonNight.DeltaTime, false);
                this.LastContinueX = 0;
                this.LastContinueY = 0;
            } else if(!this.IsCollision((ushort)(tileX + this.GetIntDir(dir).X), (ushort)(tileY), _commonNight.MapLayer))
            {
                this.LastContinueY = 0;
                dir.Y = 0;
                if (this.LastContinueX == 0)
                {
                    this.LastContinueX = (int)this.GetIntDir(dir).X;
                }
                dir.X = LastContinueX;
                this.VirtualPos += dir * this.Speed * _commonNight.DeltaTime;
                this.ZombieChar.Movement(dir * this.Speed, _commonNight.DeltaTime, false);
            } else if (!this.IsCollision((ushort)(tileX), (ushort)(tileY + this.GetIntDir(dir).Y), _commonNight.MapLayer))
            {
                this.LastContinueX = 0;
                dir.X = 0;
                if(this.LastContinueY == 0)
                {
                    this.LastContinueY = (int)this.GetIntDir(dir).Y;
                }
                dir.Y = LastContinueY;
                this.VirtualPos += dir * this.Speed * _commonNight.DeltaTime;
                this.ZombieChar.Movement(dir * this.Speed, _commonNight.DeltaTime, false);
            }
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
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        public Vector2 GetMapPos(OrthographicCamera cam)
        {
            //Console.WriteLine(cam.ScreenToWorld(this.VirtualPos));
            Vector2 res = this.VirtualPos;
            res.X = res.X + 4260 + 360;
            res.Y = res.Y + 6392 + 360;
            return res;
        }

        public Vector2 GetIntDir(Vector2 vec)
        {
            Vector2 res = Vector2.Zero;
            if (vec.X < -0.1)
                res.X = -1;
            else if (vec.X > 0.1)
                res.X = 1;
            else
                res.X = 0;
            if (vec.Y < -0.1)
                res.Y = -1;
            else if (vec.Y > 0.1)
                res.Y = 1;
            else
                res.Y = 0;
            return res;
        }
    }
}
