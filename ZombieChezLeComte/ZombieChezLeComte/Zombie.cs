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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe gérant le zombie.
    /// </summary>
    public class Zombie
    {
        private Charactere zombieChar;
        private int speed;
        private Vector2 virtualPos;
        private int lastContinueX;
        private int lastContinueY;
        private bool peutTuer ;
        private bool peutBouger;
        private SoundEffect[] zombieSounds;
        private Random aleatoire;
        private float maxTemps;
        private float _timer;

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
        /// <summary>
        /// Les positions de zombie présente dans la prorpiété ZombieChar ne sont pas correct car elles changent
        /// pour que le zombie reste fixé sur la map.
        /// La prorpiété VirtualPos contient donc la position du zombie sur la map
        /// </summary>
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
        public SoundEffect[] ZombieSounds
        {
            get
            {
                return this.zombieSounds;
            }

            set
            {
                this.zombieSounds = value;
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

        public void Initialiaze(Vector2 _position, int _speed)
        {
            this.ZombieChar = new Charactere();
            this.ZombieChar.Initialize(_position, 1);
            this.Speed = _speed;
            this.VirtualPos = new Vector2(this.ZombieChar.Position.X, this.ZombieChar.Position.Y);
            this.LastContinueX = 0;
            this.LastContinueY = 0;
            this.ZombieSounds = new SoundEffect[3];
            this.Aleatoire =  new Random();
            this.Timer = 0;
            this.MaxTemps = Aleatoire.Next(5, 6);
        }

        public void LoadContent(SpriteSheet _spriteSheet, Game1 _game)
        {
            this.ZombieChar.LoadContent(_spriteSheet);
            this.ZombieSounds[0] = _game.Content.Load<SoundEffect>("Zombie1");
            this.ZombieSounds[1] = _game.Content.Load<SoundEffect>("Zombie2");
            this.ZombieSounds[2] = _game.Content.Load<SoundEffect>("Zombie3");
        }

        public void Update(GameTime _gameTime, CommonNight _commonNight, Game1 _game)
        {
            this.ZombieChar.Update(_gameTime);
            this.ZombieChar.MovementWithoutAnim(_commonNight.CameraMove, _commonNight.DeltaTime);
            _timer -= (float)_gameTime.GetElapsedSeconds();
            if(this.Timer <= 0 && Vector2.Distance(_commonNight.Player.Position, this.ZombieChar.Position) <= 150)
            {
                ZombieSounds[(int)Aleatoire.Next(0, 3)].Play();
                MaxTemps = Aleatoire.Next(5,6);
                Timer = MaxTemps;
            }
            if (PeutBouger)
            {

                // On initialise sa direction
                Vector2 dir = Vector2.Zero;

                // On récupère la tuile sous ses pieds
                ushort tileX = (ushort)(this.GetMapPos(_commonNight.Camera).X / _commonNight.TiledMap.TileWidth);
                ushort tileY = (ushort)(this.GetMapPos(_commonNight.Camera).Y / _commonNight.TiledMap.TileHeight);

                // on récupère la prochiane case pour pousuivre le joueur déterminer à l'aide de l'algorithme A*
                NodeCase nextCase = this.GetNextCase(new NodeCase(tileX, tileY), _commonNight.GetPlayerCase(), _commonNight.MapLayer);
                // Si elle n'est pas null (cela arrive si le zombie est sur la même case que le joueur, on met à jour 
                // sa direction de déplacement
                if(!(nextCase is null))
                {
                    dir = new Vector2(nextCase.X - tileX, nextCase.Y - tileY);
                }
;

                this.ZombieChar.Movement(dir * this.Speed, _commonNight.DeltaTime, false);
                this.VirtualPos += dir * this.Speed * _commonNight.DeltaTime;

                // Si le joueur touche le zombie, on fait des choses différentes selon les conditions
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

        /// <summary>
        /// Détermine la prochaine case que le zombie doit prendre pour poursuivre le joueur.
        /// La case est déterminé avec l'algorithme A*.
        /// </summary>
        /// <param name="zombieCase"></param>
        /// <param name="playerCase"></param>
        /// <param name="layer">layer collision de la map</param>
        /// <returns>Un NodeCase qui est la prochaine case à emprunter ou la valeur null si le joueur et le zombie
        /// sont sur la même case</returns>
        public NodeCase GetNextCase(NodeCase zombieCase, NodeCase playerCase, TiledMapTileLayer layer)
        {
            // Si le zombie est sur la même case que le joueur on retourne tout de suite null
            if(zombieCase == playerCase)
            {
                return null;
            }

            // Initialisation de quelques variables
            zombieCase.SetHeuristique(playerCase);
            PriorityQueue priorityQueue = new PriorityQueue();
            Dictionary<NodeCase, NodeCase> predecesseur = new Dictionary<NodeCase, NodeCase>();
            Dictionary<NodeCase, int> accesCost = new Dictionary<NodeCase, int>();

            priorityQueue.Add(zombieCase, 0);
            predecesseur.Add(zombieCase, null);
            accesCost.Add(zombieCase, 0);

            // Tant que la file n'est pas vide
            while(!priorityQueue.IsEmpty())
            {
                NodeCase c = priorityQueue.Pop();
                // Si la case c est celle du joueur on passe à l'étape de BackTracing
                if(c == playerCase)
                {
                    return this.BackTracing(predecesseur, zombieCase, playerCase);
                } else
                {
                    // Pour chaque voisin
                    foreach(NodeCase v in c.GetNeighbors(layer))
                    {
                        // Si on a pas déjà de cout d'accês, on l'ajoute
                        if(!accesCost.ContainsKey(v))
                        {
                            predecesseur.Add(v, c);
                            accesCost.Add(v, accesCost[c] + 1);
                            v.SetHeuristique(playerCase);
                            priorityQueue.Add(v, accesCost[v] + v.Heuristique);
                        } // sinon, si le cout est plus bas que celui qu'on a déjà on le met à jour 
                        else if(accesCost[v] > accesCost[c] + 1)
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

        /// <summary>
        /// Après avoir fait un A*, cette métode donne la case juste après la case de départ vers laquelle il faudrait se 
        /// diriger
        /// </summary>
        /// <param name="predecesseur">Un dictionnaire des prédecesseurs</param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns>La case qui suit le départ</returns>
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

        /// <summary>
        /// Retourne un Vecteur2 contenant la position réelle sur la map du zombie
        /// </summary>
        /// <param name="cam"></param>
        /// <returns></returns>
        public Vector2 GetMapPos(OrthographicCamera cam)
        {
            Vector2 res = this.VirtualPos;
            res.X = res.X + 4260 + 360;
            res.Y = res.Y + 6392 + 360;
            return res;
        }
    }
}
