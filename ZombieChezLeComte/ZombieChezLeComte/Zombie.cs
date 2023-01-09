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
        private bool peutTuer ;
        private bool peutBouger;

        public bool PeutTuer
        {
            get
            {
                return this.peutTuer;
            }

            set
            {
                this.peutTuer = value;
            }
        }
        public bool PeutBouger
        {
            get
            {
                return this.peutBouger;
            }

            set
            {
                this.peutBouger = value;
            }
        }
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

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.ZombieChar.Update(_gameTime);
            this.ZombieChar.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime, false);

            if (PeutBouger)
            {


                Vector2 dir = Vector2.Zero;
                ushort tileX = (ushort)(this.GetMapPos(_commonNight.Camera).X / _commonNight.TiledMap.TileWidth);
                ushort tileY = (ushort)(this.GetMapPos(_commonNight.Camera).Y / _commonNight.TiledMap.TileHeight);


                NodeCase nextCase = this.GetNextCase(new NodeCase(tileX, tileY), _commonNight.GetPlayerCase(), _commonNight.MapLayer);


                if(!(nextCase is null))
                {
                    dir = new Vector2(nextCase.X - tileX, nextCase.Y - tileY);
                }
;

                this.ZombieChar.Movement(dir * this.Speed, _commonNight.DeltaTime, false);
                this.VirtualPos += dir * this.Speed * _commonNight.DeltaTime;

                if (this.ZombieChar.SpriteRect.Intersects(_commonNight.Player.SpriteRect))
                {
                    if (this.PeutTuer == true && !Constantes.GOD_MOD)
                    {
                        _game.killBy = "zombie";
                        _game.LoadJumpScare();
                    }
                    else if(this.PeutTuer == false)
                    {
                        _game.LoadBetween4And5();
                    }
                }
            }
        }

        public NodeCase GetNextCase(NodeCase zombieCase, NodeCase playerCase, TiledMapTileLayer layer)
        {
            if(zombieCase == playerCase)
            {
                return null;
            }

            zombieCase.SetHeuristique(playerCase);
            PriorityQueue priorityQueue = new PriorityQueue();
            Dictionary<NodeCase, NodeCase> predecesseur = new Dictionary<NodeCase, NodeCase>();
            Dictionary<NodeCase, int> accesCost = new Dictionary<NodeCase, int>();

            priorityQueue.Add(zombieCase, 0);
            predecesseur.Add(zombieCase, null);
            accesCost.Add(zombieCase, 0);


            while(!priorityQueue.IsEmpty())
            {
                NodeCase c = priorityQueue.Pop();
                if(c == playerCase)
                {
                    return this.BackTracing(predecesseur, zombieCase, playerCase);
                } else
                {
                    foreach(NodeCase v in c.GetNeighbors(layer))
                    {
                        if(!accesCost.ContainsKey(v))
                        {
                            predecesseur.Add(v, c);
                            accesCost.Add(v, accesCost[c] + 1);
                            v.SetHeuristique(playerCase);
                            priorityQueue.Add(v, accesCost[v] + v.Heuristique);
                        } else if(accesCost[v] > accesCost[c] + 1)
                        {
                            predecesseur[v] = c;
                            accesCost[v] = accesCost[c] + 1;
                            v.SetHeuristique(playerCase);
                            priorityQueue.Queue[v] = accesCost[v] + v.Heuristique;
                        }
                    }
                }
            }
            return null;
        }

        public NodeCase BackTracing(Dictionary<NodeCase, NodeCase> predecesseur, NodeCase start, NodeCase finish)
        {
            NodeCase tempCase = finish;
            while (predecesseur[tempCase] != start)
            {
                tempCase = predecesseur[tempCase];
            }
            return tempCase;
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
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }

        public Vector2 GetMapPos(OrthographicCamera cam)
        {
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
