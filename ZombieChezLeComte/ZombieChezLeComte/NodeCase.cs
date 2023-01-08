using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieChezLeComte
{
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

        public void SetHeuristique(NodeCase finish)
        {
            this.Heuristique = Math.Abs(finish.X - this.X) + Math.Abs(finish.Y - this.Y);
        }

        public bool IsAccessible(TiledMapTileLayer layer)
        {
            TiledMapTile? tile;
            if (layer.TryGetTile((ushort)this.X, (ushort)this.Y, out tile) == false)
                return true;
            if (!tile.Value.IsBlank)
                return false;
            return true;
        }

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
