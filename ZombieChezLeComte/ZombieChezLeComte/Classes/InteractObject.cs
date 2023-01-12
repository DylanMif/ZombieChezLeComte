using Microsoft.Xna.Framework;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Classe permettant de gérer les objets interactifs et tâche sur la map
    /// </summary>
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

        /// <summary>
        /// Si on veut les combiner avec un textInfo on peut lui donner un texte qu'il pourra donner au textInfo au moment voulu
        /// </summary>
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

        /// <summary>
        /// Méthode permettant de savoir si des coordonnées sont dans le rectangle de l'InteractObject
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool InteractWith(Vector2 pos)
        {
            return this.InteractRect.Contains(pos);
        }

    }
}
