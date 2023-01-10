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
using Microsoft.Xna.Framework.Audio;

namespace ZombieChezLeComte
{
    /// <summary>
    /// Screen gérant la nuit 2
    /// </summary>
    public class ScreenNight2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public ScreenNight2(Game1 game) : base(game) { }
        private CommonNight _nuit2 = new CommonNight();

        private InteractObject[] litInteraction = new InteractObject[3];
        private int nombreLitFait = 0;
        private InteractObject litDormir = new InteractObject();
        private InteractObject feu = new InteractObject();
        private InteractObject recupViande = new InteractObject();
        private InteractObject cuisine = new InteractObject();
        private InteractObject[] armures = new InteractObject[2];
        private int nombreArm = 0;
        private TextInfo textInfo = new TextInfo();
        private InteractObject[] kitchenPapers = new InteractObject[6];
        private bool estRecuperer = false;
        private bool sonJouer;

        private bool isInJumpScare;
        private Texture2D jumpScareImage;
        private SoundEffect jumpScareSound;
        private Vector2 jumpScareDrawPos;
        private SoundEffect armorSound;
        private SoundEffect cookingSound;
        private SoundEffect glassSound;
        private float currentTime;

        public override void Initialize()
        {
            _nuit2.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize("erer", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));

            // Initialisation des objets interactifs
            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i] = new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Comment le lit peut etre defait?? Personne n'est venu depuis hier?!?");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Bizarre... Le lit est defait");
            litInteraction[2].Initialize(new Vector2(4098, 6088), 47, 103, "lit3", "Ca doit etre les souris qui ont defait le lit");
            litDormir.Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "Une bonne sieste bien merite!");
            for (int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Alimenter le feu en bois");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Cuisiner de la viande");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "Nettoyer les armures");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Faire les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Aller dormir une fois les taches finis");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "Recuperer la viande dans le stockage");
            for (int i = 0; i < armures.Length; i++)
            {
                armures[i] = new InteractObject();
            }
            armures[0].Initialize(new Vector2(4798, 6380), 102, 18, "armure1", "J'ai jamais vu autant de poussiere");
            armures[1].Initialize(new Vector2(4225, 6540), 60, 18, "armure2", "Elles doivent valoir une fortune");
            feu.Initialize(new Vector2(4990, 6382), 40, 20, "feu", "Ca fait du bien de se rechauffer !");
            cuisine.Initialize(new Vector2(3979, 6563), 33, 37, "cuisine", "Ca sera plus simple de cuisiner si j'ai la viande");
            recupViande.Initialize(new Vector2(3918, 5718), 67, 702, "lit4", "Beurk...C'est quoi cette viande ! Peut etre du boeuf orientale...");
            sonJouer = false;

            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit2.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()), Game);
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            textInfo.Text = "Brrrr... Il fait un froid de canard dans cette maison";
            textInfo.ActiveText(Constantes.TEMPS_TEXTE);

            jumpScareImage = Game.Content.Load<Texture2D>("endGhostJumpScareImage");
            jumpScareSound = Game.Content.Load<SoundEffect>("endGhostJumpScareSound");

            currentTime = (float)jumpScareSound.Duration.TotalSeconds;

            jumpScareDrawPos = new Vector2(
                (Constantes.WINDOW_WIDTH - jumpScareImage.Width) / 2,
                (Constantes.WINDOW_HEIGHT - jumpScareImage.Height) / 2
                );

            armorSound = Game.Content.Load<SoundEffect>("Armure");
            cookingSound = Game.Content.Load<SoundEffect>("Cooking");
            glassSound = Game.Content.Load<SoundEffect>("GlassBreaking");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit2.Update(gameTime);
            textInfo.Update(gameTime);

            // Il y a un jumpscare surprise lors de la nuit
            // Donc s'il est en cours on réduit son temps de vie
            if(isInJumpScare)
            {
                currentTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(currentTime <= 0)
                {
                    isInJumpScare = false;
                }
            }

            
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                // On regarde l'interaction qui doit être lancé s'il y en a une
                for (int i = 0; i < kitchenPapers.Length; i++)
                {
                    if (kitchenPapers[i].InteractWith(-_nuit2.Camera.Position))
                    {
                        Additions.InteractionObjet(kitchenPapers[i], textInfo, kitchenPapers[i].InteractText);
                    }
                }
                if (feu.InteractWith(-_nuit2.Camera.Position))
                {
                    Additions.InteractionObjet(feu, textInfo, "Le feu est suffisament charge en bois");
                }
                if (recupViande.InteractWith(-_nuit2.Camera.Position))
                {
                    Additions.InteractionObjet(recupViande, textInfo, "J'ai assez de viande je pense");
                    cuisine.InteractText = "Cette viande est vraiment douteuse";
                    estRecuperer = true;
                }
                for (int i = 0; i < litInteraction.Length; i++)
                {
                    if (litInteraction[i].InteractWith(-_nuit2.Camera.Position))
                    {
                        if (litInteraction[i].HasAlreadyInteractable == false)
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
                if (cuisine.InteractWith(-_nuit2.Camera.Position))
                {
                    if (estRecuperer)
                    {
                        if (!textInfo.WritingText)
                        {
                            cookingSound.Play();
                        }
                        Additions.InteractionObjet(cuisine, textInfo, "Cette viande est vraiment douteuse");
                    }
                    else
                    {
                        Additions.InteractionObjet(cuisine, textInfo, cuisine.InteractText);
                    }
                }
                for (int i = 0; i < armures.Length; i++)
                {
                    if (armures[i].InteractWith(-_nuit2.Camera.Position))
                    {
                        if(!textInfo.WritingText)
                            armorSound.Play();
                        if (armures[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(armures[i], textInfo, "Toute belle toute propre");
                            nombreArm += 1;
                            if(i == 1)
                            {
                                isInJumpScare = true;
                                jumpScareSound.Play();
                            }
                        }
                        else
                        {
                            Additions.InteractionObjet(armures[i], textInfo, armures[i].InteractText);
                        }
                    }
                }
                if(nombreArm == armures.Length && nombreLitFait==litInteraction.Length && feu.HasAlreadyInteractable == true &&
                    cuisine.HasAlreadyInteractable == true && recupViande.HasAlreadyInteractable == true)
                {
                    if (sonJouer == false && !textInfo.WritingText)
                    {
                        textInfo.Text = "C'etait quoi ce bruit ?!?";
                        textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                        sonJouer = true;
                        glassSound.Play();
                    }
                    if (litDormir.InteractWith(-_nuit2.Camera.Position))
                    {
                        if (litDormir.HasAlreadyInteractable == false)
                            litDormir.HasAlreadyInteractable = true;
                        else
                        {
                            if (!textInfo.WritingText)
                            {
                                textInfo.Text = litDormir.InteractText;
                                textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                                Game.LoadBetween2And3();
                            }
                        }
                    }
                } else if (litDormir.InteractWith(-_nuit2.Camera.Position))
                {
                    textInfo.Text = "Vous dormirez apres avoir tout fait";
                    textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.Black);
            // On dessine soit le jumpscare soit le jeu normalement en fonction de ce qui se passe
            if(isInJumpScare)
            {
                Game.SpriteBatch.Begin();
                Game.SpriteBatch.Draw(jumpScareImage, jumpScareDrawPos, Color.White);
                Game.SpriteBatch.End();
            } else
            {
                _nuit2.Draw(Game.SpriteBatch);
                Game.SpriteBatch.Begin();
                _nuit2.DrawVision(Game.SpriteBatch);
                textInfo.Draw(Game.SpriteBatch);
                Game.SpriteBatch.End();
            }
        }
    }
}
