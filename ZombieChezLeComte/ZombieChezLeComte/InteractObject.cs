using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombieChezLeComte
{
    public class InteractObject
    {
        private Rectangle interactRect;
        private string interactName;

        public Rectangle InteractRect
        {
            get
            {
                return this.interactRect;
            }

            set
            {
                this.interactRect = value;
            }
        }

        public string InteractName
        {
            get
            {
                return this.interactName;
            }

            set
            {
                this.interactName = value;
            }
        }

        public void Initialize(Vector2 topLeftPos, int width, int height, string interactName)
        {
            this.InteractRect = new Rectangle((int)topLeftPos.X, (int)topLeftPos.Y, width, height);
            this.InteractName = interactName;
        }

        public bool InteractWith(Vector2 pos)
        {
            return this.InteractRect.Contains(pos);
        }
    }
}
