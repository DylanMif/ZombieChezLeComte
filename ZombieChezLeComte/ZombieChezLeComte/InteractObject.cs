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
        private bool hasAlreadyInteractable;
        private string interactText;

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

        public bool HasAlreadyInteractable
        {
            get
            {
                return this.hasAlreadyInteractable;
            }

            set
            {
                this.hasAlreadyInteractable = value;
            }
        }

        public string InteractText
        {
            get
            {
                return this.interactText;
            }

            set
            {
                this.interactText = value;
            }
        }

        public void Initialize(Vector2 topLeftPos, int width, int height, string interactName, string _interactText)
        {
            this.InteractRect = new Rectangle((int)topLeftPos.X, (int)topLeftPos.Y, width, height);
            this.InteractName = interactName;
            this.hasAlreadyInteractable = false;
            this.InteractText = _interactText;
        }

        public bool InteractWith(Vector2 pos)
        {
            return this.InteractRect.Contains(pos);
        }


        public void Destroy()
        {
            //On le sort de l'écran
            this.InteractRect = new Rectangle(-150, -150, 1, 1);
        }
    }
}
