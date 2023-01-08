using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieChezLeComte
{
    public class PriorityQueue
    {
        private Dictionary<NodeCase, int> queue;

        public PriorityQueue()
        {
            this.Queue = new Dictionary<NodeCase, int>();
        }

        public Dictionary<NodeCase, int> Queue
        {
            get
            {
                return queue;
            }

            set
            {
                queue = value;
            }
        }

        public bool IsEmpty()
        {
            return this.Queue.Count == 0;
        }

        public void Add(NodeCase _case, int priority)
        {
            this.Queue.Add(_case, priority);
        }

        public NodeCase Pop()
        {
            NodeCase res = null;
            if(!this.IsEmpty())
            {
                foreach(NodeCase q in this.Queue.Keys)
                {
                    if(res is null)
                    {
                        res = q;
                    } else if (this.Queue[q] < this.Queue[res])
                    {
                        res = q;
                    }
                }
            }
            this.Queue.Remove(res);
            return res;
        }

        public override string ToString()
        {
            string text = "";
            foreach(NodeCase q in this.Queue.Keys)
            {
                text += $"{q} : {this.Queue[q]} ,";
            }
            return text;
        }

        public override bool Equals(object obj)
        {
            return obj is PriorityQueue queue &&
                   EqualityComparer<Dictionary<NodeCase, int>>.Default.Equals(this.Queue, queue.Queue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Queue);
        }

        public static bool operator ==(PriorityQueue left, PriorityQueue right)
        {
            return EqualityComparer<PriorityQueue>.Default.Equals(left, right);
        }

        public static bool operator !=(PriorityQueue left, PriorityQueue right)
        {
            return !(left == right);
        }
    }
}
