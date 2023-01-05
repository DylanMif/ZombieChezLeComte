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

namespace ZombieChezLeComte
{
    public class Night2 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Night2(Game1 game) : base(game) { }
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


        public override void Initialize()
        {
            _nuit2.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize("erer", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));
            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i] = new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Comment le lit peut etre defait alors que personne n'est venu depuis hier?!?");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Bizzare... Le lit est defait");
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
            armures[0].Initialize(new Vector2(4798, 6380), 102, 8, "armure1", "J'ai jamais vu autant de poussiere");
            armures[1].Initialize(new Vector2(4225, 6540), 60, 8, "armure2", "Elles doivent valoir une fortune");
            feu.Initialize(new Vector2(4990, 6382), 40, 3, "feu", "Ca fait du bien de se rechauffer !");
            cuisine.Initialize(new Vector2(3979, 6573), 33, 27, "cuisine", "Ca sera plus simple de cuisiner si j'ai la viande");
            recupViande.Initialize(new Vector2(3918, 5718), 67, 702, "lit4", "Beurk...C'est quoi cette viande ! Peut etre du boeuf orientale...");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit2.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            textInfo.Text = "Brrrr... Il fait un froid de canard dans cette maison";
            textInfo.ActiveText(Constantes.TEMPS_TEXTE);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit2.Update(gameTime);
            textInfo.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
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
                        if (armures[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(armures[i], textInfo, "Toute belle toute propre");
                            nombreArm += 1;
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
                    if (litDormir.InteractWith(-_nuit2.Camera.Position))
                    {
                        if (litDormir.HasAlreadyInteractable == false)
                            litDormir.HasAlreadyInteractable = true;
                        else
                        {
                            textInfo.Text = litDormir.InteractText;
                            textInfo.ActiveText(2);
                            Game.LoadNight3();
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit2.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
