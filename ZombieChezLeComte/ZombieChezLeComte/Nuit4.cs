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
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace ZombieChezLeComte
{
    internal class Nuit4 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Nuit4(Game1 game) : base(game) { }
        private CommonNight _nuit4 = new CommonNight();


        private TextInfo textInfo = new TextInfo();
        private InteractObject[] kitchenPapers = new InteractObject[6];

        private bool estRecuperer = false;
        private bool possedeArme = false;
        private bool uneTache = false;
        private InteractObject[] litInteraction = new InteractObject[4];
        private int nombreLitFait = 0;
        private InteractObject couteuxFour = new InteractObject();
        private InteractObject[] laverLivre = new InteractObject[3];
        private int nombreLivre = 0;
        private InteractObject recupViande = new InteractObject();


        public override void Initialize()
        {
            _nuit4.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize("erer", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));
            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i] = new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Ca n'a aucun sens ce que je fais...");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Pourquoi je dois mettre de la viande sur des lits?!?");
            litInteraction[2].Initialize(new Vector2(4098, 6088), 47, 103, "lit3", "C'est moi qui devient fou ou cette maison tourne par rond?");
            litInteraction[3].Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "Je devrais demisionner... Ca devient trop dangereux");
            couteuxFour.Initialize(new Vector2(3979, 6573), 33, 27, "couteux","Des couteux au four?? Drole de nourriture...");
            for (int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Mettre des couteaux de cuisine au four");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Laver les livres de la biblitotheque");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Mettre de la viande sur les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "Recuperer la viande dans le stockage");
            for (int i = 0; i < laverLivre.Length; i++)
            {
                laverLivre[i] = new InteractObject();
            }
            laverLivre[0].Initialize(new Vector2(4482, 6381), 50, 50, "armoire1", "Les livres sont foutu maintenant");
            laverLivre[1].Initialize(new Vector2(4281, 6384), 50, 50, "armoire2", "Pourquoi laver les livres?");
            laverLivre[2].Initialize(new Vector2(4184, 6384), 50, 50, "armoire3", "Decidemment, ca devient de plus en plus bizzare...");
            recupViande.Initialize(new Vector2(3918, 5718), 67, 702, "lit4", "Toujours la meme viande ragoutante...");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit4.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            textInfo.Text = "Qu'est ce qui s'est passe? Je me suis evanouis je crois...";
            textInfo.ActiveText(Constantes.TEMPS_TEXTE);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit4.Update(gameTime);
            textInfo.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                for (int i = 0; i < kitchenPapers.Length; i++)
                {
                    if (kitchenPapers[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        Additions.InteractionObjet(kitchenPapers[i], textInfo, kitchenPapers[i].InteractText);
                    }
                }
                for (int i = 0; i < litInteraction.Length; i++)
                {
                    if (litInteraction[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        if (litInteraction[i].HasAlreadyInteractable == false)
                        {
                            if (estRecuperer == true && litInteraction[i].HasAlreadyInteractable == false)
                            {
                                Additions.InteractionObjet(litInteraction[i], textInfo, "J'ai fais ce lit");
                                nombreLitFait += 1;
                                uneTache = true;
                                estRecuperer = false;
                            }
                            else if(estRecuperer == false && litInteraction[i].HasAlreadyInteractable == false)
                            {
                                textInfo.Text = "Il me faut de la viande avant";
                                textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                            }
                        }
                        else
                        {
                            Additions.InteractionObjet(litInteraction[i], textInfo, litInteraction[i].InteractText);
                        }
                    }
                }
                if (recupViande.InteractWith(-_nuit4.Camera.Position))
                {
                    if (nombreLitFait == litInteraction.Length)
                    {
                        recupViande.InteractText = "J'ai deja fais tout les lits. Plus besoin de viande";
                        Additions.InteractionObjet(recupViande, textInfo, "J'ai deja fais tout les lits. Plus besoin de viande");
                    }
                    else
                    {
                        Additions.InteractionObjet(recupViande, textInfo, "Je vais devoir revenir chercher de la viande pour chaque lit...");
                        estRecuperer = true;
                    }
                }
                if (couteuxFour.InteractWith(-_nuit4.Camera.Position))
                {
                    Additions.InteractionObjet(couteuxFour, textInfo, "Les couteaux sont deja au fourneaux...");
                    uneTache = true;
                }
                for (int i = 0; i < laverLivre.Length; i++)
                {
                    if (laverLivre[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        if (laverLivre[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(laverLivre[i], textInfo, "Les livres sont laver ici");
                            nombreLivre += 1;
                            uneTache = true;
                        }
                        else
                        {
                            Additions.InteractionObjet(laverLivre[i], textInfo, laverLivre[i].InteractText);
                        }
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit4.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }


        
    }
}
