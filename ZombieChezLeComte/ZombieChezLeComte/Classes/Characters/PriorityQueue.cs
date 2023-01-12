using System;
using System.Collections.Generic;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe gérant des files à priorité. La file ne peut contenir que des objets de type NodeCase
    /// </summary>
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

        /// <summary>
        /// Permet de savoir si la file est vide ou non
        /// </summary>
        /// <returns>Un boolean indiquant si la file est vide ou non</returns>
        public bool IsEmpty()
        {
            return this.Queue.Count == 0;
        }

        /// <summary>
        /// Ajoute une valeur à la file avec un priorité
        /// Plus la priorité est un petit nombre plus l'element à une haute priorité
        /// </summary>
        /// <param name="_case"></param>
        /// <param name="priority"></param>
        public void Add(NodeCase _case, int priority)
        {
            this.Queue.Add(_case, priority);
        }

        /// <summary>
        /// Retire et renvoie la valeur à la plus haute priorité selon le principe FIFO
        /// </summary>
        /// <returns>Une instance de la classe NodeCase</returns>
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

        /// <summary>
        /// Des files sont égales si elles contiennent toutes les deux les mêmes élements
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
