﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Serialization;
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant la nuit 1
    /// </summary>
    public class ScreenNight1 : GameScreen
    {
        
        private new Game1 Game => (Game1)base.Game;
        public ScreenNight1(Game1 game) : base(game) { }
        private CommonNight _nuit1 = new CommonNight();

        private InteractObject[] litInteraction = new InteractObject[3];
        private int nombreLitFait = 0;
        private InteractObject[] armoireInteraction = new InteractObject[3];
        private int nombreArmoireFait = 0;
        private InteractObject solNettoyage = new InteractObject();
        private InteractObject porteCave = new InteractObject();
        private InteractObject tableCuisine = new InteractObject();
        private InteractObject litDormir = new InteractObject();
        private InteractObject[] kitchenPapers = new InteractObject[6];
        private TextInfo textInfo =  new TextInfo();
        private SoundEffect balaisSound;
        private SoundEffect doorSound;

        public override void Initialize()
        {
            _nuit1.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize("erer",Color.White,new Vector2(10,Constantes.WINDOW_HEIGHT-150));

            // Initialisation de tout les objets interactifs
            for(int i = 0; i < armoireInteraction.Length; i++)
            {
                armoireInteraction[i] = new InteractObject();
            }
            armoireInteraction[0].Initialize(new Vector2(4482, 6381), 50, 50, "armoire1", "Plein de poussiere !");
            armoireInteraction[1].Initialize(new Vector2(4281, 6384), 50, 50, "armoire2", "C'est une porcherie !");
            armoireInteraction[2].Initialize(new Vector2(4184, 6384), 50, 50, "armoire3", "Range comme du n'importe quoi");
            for (int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Ranger les bibliotheques");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Ranger la table de la cuisine");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "Balayer le couloir devant la cave");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Faire les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Aller dormir une fois les taches finis");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "Rien a faire sur ce papier");

            solNettoyage.Initialize(new Vector2(4765,6637), 103, 26, "sol", "Ca merite un coup de balais");
            porteCave.Initialize(new Vector2(4917, 6627), 103, 36, "porte", "Ferme a cle! Bizarre...");
            tableCuisine.Initialize(new Vector2(4306,6576), 117,40,"table","Beurk!");

            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i]= new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Une bonne chose de faite");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Si seulement j'avais un cafe");
            litInteraction[2].Initialize(new Vector2(4098, 6088), 47, 103, "lit3", "Sympa les draps");
            litDormir.Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "Allons dormir!");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit1.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"), 
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()), Game);
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            balaisSound = Game.Content.Load<SoundEffect>("Balais");
            doorSound = Game.Content.Load<SoundEffect>("DoorClosed");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit1.Update(gameTime);
            textInfo.Update(gameTime);

            // Si le joueur appuie sur <espace> on check tous les objets interactifs pour détecter s'il y a ou non une interaction
            // à faire
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                for(int i = 0; i < litInteraction.Length; i++)
                {
                    if (litInteraction[i].InteractWith(-_nuit1.Camera.Position))
                    {
                        if(litInteraction[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(litInteraction[i], textInfo, "Ce lit est fait");
                            nombreLitFait += 1;
                        } 
                        else
                        {
                            Additions.InteractionObjet(litInteraction[i], textInfo, litInteraction[i].InteractText);
                        }
                    }
                }
                for (int i = 0; i < armoireInteraction.Length; i++)
                {
                    if (armoireInteraction[i].InteractWith(-_nuit1.Camera.Position))
                    {
                        if (armoireInteraction[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(armoireInteraction[i], textInfo, "Cette bibliotheque est rangee");
                            nombreArmoireFait += 1;
                        }
                        else
                        {
                            Additions.InteractionObjet(armoireInteraction[i], textInfo, armoireInteraction[i].InteractText);
                        }
                    }
                }
                for (int i = 0; i < kitchenPapers.Length; i++)
                {
                    if (kitchenPapers[i].InteractWith(-_nuit1.Camera.Position))
                    {
                        Additions.InteractionObjet(kitchenPapers[i], textInfo, kitchenPapers[i].InteractText);
                    }
                }
                if (porteCave.InteractWith(-_nuit1.Camera.Position))
                {
                    if(!textInfo.WritingText)
                    {
                        doorSound.Play();
                    }
                    Additions.InteractionObjet(porteCave, textInfo, porteCave.InteractText);     
                }
                if (tableCuisine.InteractWith(-_nuit1.Camera.Position))
                {
                    Additions.InteractionObjet(tableCuisine, textInfo, "La table est nettoyee");
                }
                if (solNettoyage.InteractWith(-_nuit1.Camera.Position))
                {
                    if (!textInfo.WritingText)
                        balaisSound.Play();
                    Additions.InteractionObjet(solNettoyage,textInfo, "Le sol est propre");
                }

                // Si le joueur a fait toutes ces tâches on regardera s'il intéragit avec son lit pour dormir
                if (nombreArmoireFait == armoireInteraction.Length && nombreLitFait == litInteraction.Length
                            && solNettoyage.HasAlreadyInteractable == true && tableCuisine.HasAlreadyInteractable == true)
                {
                    if (litDormir.InteractWith(-_nuit1.Camera.Position))
                    {
                        if(litDormir.HasAlreadyInteractable == false)
                            litDormir.HasAlreadyInteractable = true;
                        else
                        {
                            textInfo.Text = litDormir.InteractText;
                            textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                            Game.LoadBetween1And2();
                        }
                    }
                } else if(litDormir.InteractWith(-_nuit1.Camera.Position))
                {
                    textInfo.Text = "Vous dormirez apres avoir tout fait";
                    textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                }


            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit1.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            _nuit1.DrawVision(Game.SpriteBatch);
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
