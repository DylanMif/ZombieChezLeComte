using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe pour gérer les cases du jeu. Utile dans l'algorithme A* pour le zombie
    /// </summary>
    public class NodeCase
    {
        private int x;
        private int y;
        private int heuristique;

        public NodeCase(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public int Heuristique
        {
            get
            {
                return heuristique;
            }

            set
            {
                heuristique = value;
            }
        }

        /// <summary>
        /// Deux cases sont égales si leurs coordonnées X et Y sont identiques
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is NodeCase @case &&
                   this.X == @case.X &&
                   this.Y == @case.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public override string ToString()
        {
            return $"{this.X}, {this.Y}";
        }

        public static bool operator ==(NodeCase left, NodeCase right)
        {
            return EqualityComparer<NodeCase>.Default.Equals(left, right);
        }

        public static bool operator !=(NodeCase left, NodeCase right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Calcule et sotck l'heuristique dans la prorpiété de l'objet
        /// </summary>
        /// <param name="finish"></param>
        public void SetHeuristique(NodeCase finish)
        {
            this.Heuristique = Math.Abs(finish.X - this.X) + Math.Abs(finish.Y - this.Y);
        }

        /// <summary>
        /// Regarde si la case est accesible sur le mapLayer
        /// </summary>
        /// <param name="layer">mapLayer des collisions</param>
        /// <returns>un bollean indiquant si cette case est accesible</returns>
        public bool IsAccessible(TiledMapTileLayer layer)
        {
            TiledMapTile? tile;
            if (layer.TryGetTile((ushort)this.X, (ushort)this.Y, out tile) == false)
                return true;
            if (!tile.Value.IsBlank)
                return false;
            return true;
        }

        /// <summary>
        /// Méthode permettant d'obtenir les voisins accesibles de cette case
        /// </summary>
        /// <param name="layer">mapLayer des collisions</param>
        /// <returns>Une liste de NodeCase contenant les voisins de cette case</returns>
        public List<NodeCase> GetNeighbors(TiledMapTileLayer layer)
        {
            List<NodeCase> neighbors = new List<NodeCase>();
            if(new NodeCase(this.X - 1, this.Y).IsAccessible(layer))
            {
                neighbors.Add(new NodeCase(this.X - 1, this.Y));
            }
            if (new NodeCase(this.X + 1, this.Y).IsAccessible(layer))
            {
                neighbors.Add(new NodeCase(this.X + 1, this.Y));
            }
            if (new NodeCase(this.X, this.Y - 1).IsAccessible(layer))
            {
                neighbors.Add(new NodeCase(this.X, this.Y - 1));
            }
            if (new NodeCase(this.X, this.Y + 1).IsAccessible(layer))
            {
                neighbors.Add(new NodeCase(this.X, this.Y + 1));
            }
            return neighbors;
        }
    }
}
