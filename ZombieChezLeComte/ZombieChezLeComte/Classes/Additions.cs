using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZombieChezLeComte
{
    public class Additions
    {
        /// <summary>
        /// Retourne dans quelle direction dois se déplacer le joueur en fonction des touches enfoncees sur l'axe X
        /// </summary>
        /// <param name="_ks">le keyboardState</param>
        /// <returns>0 si le joueur ne doit pas bouger, -1 s'il doit aller à gauche ou 1 s'il doit aller à droite</returns>
        public static int GetAxisX(KeyboardState _ks)
        {
            int x = 0;
            if(_ks.IsKeyDown(Constantes.leftKeys))
            {
                x -= 1;
            }
            if(_ks.IsKeyDown(Constantes.rightKeys))
            {
                x += 1;
            }
            return x;
        }

        /// <summary>
        /// Retourne dans quelle direction dois se déplacer le joueur en fonction des touches enfoncees sur l'axe Y
        /// </summary>
        /// <param name="_ks">le keyboardState</param>
        /// <returns>0 si le joueur ne doit pas bouger, -1 s'il doit aller vers le haut ou 1 s'il doit aller vers le bas</returns>
        public static int GetAxisY(KeyboardState _ks)
        {
            int y = 0;
            if (_ks.IsKeyDown(Constantes.upKeys))
            {
                y -= 1;
            }
            if (_ks.IsKeyDown(Constantes.downKeys))
            {
                y += 1;
            }
            return y;
        }

        /// <summary>
        /// Retourne le vecteur2 donnant la direction de déplacement de joueur en fonction des touches enfoncees
        /// </summary>
        /// <param name="_ks">Le keyboardState</param>
        /// <returns>Un Vector2 non-normalisé</returns>
        public static Vector2 GetAxis(KeyboardState _ks)
        {
            return new Vector2(Additions.GetAxisX(_ks), Additions.GetAxisY(_ks));
        }

        /// <summary>
        /// Nous avons remarqué un vecteur 2 valant 0 en X et 0 en Y ne peut pas être normalisé
        /// Nous avons donc fait une méthode qui retourne un veteur normalisé quoi qu'il arrive
        /// </summary>
        /// <param name="vec">Vecteur à normalisé</param>
        /// <returns>Vecteur normalisé</returns>
        public static Vector2 Normalize(Vector2 vec)
        {
            if(vec == Vector2.Zero)
            {
                return Vector2.Zero;
            }
            return Vector2.Normalize(vec);
        }
        
        /// <summary>
        /// Regroupe le code en commun pour gérer l'interaction des objets
        /// </summary>
        /// <param name="objetInteraction">l'objet interactif à modifier</param>
        /// <param name="textInfo">Le textInfo qui affichera le texte</param>
        /// <param name="newString">La nouvelle chaine de caractère qui sera donnée à l'object interactif</param>
        public static void InteractionObjet(InteractObject objetInteraction,TextInfo textInfo ,String newString)
        {
            objetInteraction.HasAlreadyInteractable = true;
            textInfo.Text = objetInteraction.InteractText;
            textInfo.ActiveText(Constantes.TEMPS_TEXTE);
            objetInteraction.InteractText = newString;
        }

    }
}
