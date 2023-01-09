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
using Microsoft.Xna.Framework.Audio;


namespace ZombieChezLeComte
{
    internal class Nuit4 : GameScreen
    {
        private new Game1 Game => (Game1)base.Game;
        public Nuit4(Game1 game) : base(game) { }
        private CommonNight _nuit4 = new CommonNight();

        private TextInfo textInfo = new TextInfo();

        private Zombie zombie = new Zombie();

        private bool estRecuperer = false;
        private bool uneTache = false;

        private int nombreLitFait = 0;
        private int nombreLivre = 0;
        private int nombreMorceauxArme = 0;

        private InteractObject[] kitchenPapers = new InteractObject[6];
        private InteractObject[] litInteractions = new InteractObject[4];
        private InteractObject[] laverLivres = new InteractObject[3];
        private InteractObject couteuxFour = new InteractObject();
        private InteractObject recupViande = new InteractObject();
        private InteractObject debutArme = new InteractObject();

        private SoundEffect knifeSound;
        private SoundEffect washSound;

        public override void Initialize()
        {
            _nuit4.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize("erer", Color.White, new Vector2(10, Constantes.WINDOW_HEIGHT - 150));
            for (int i = 0; i < litInteractions.Length; i++)
            {
                litInteractions[i] = new InteractObject();
            }
            litInteractions[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Ca n'a aucun sens ce que je fais...");
            litInteractions[1].Initialize(new Vector2(4098, 5900), 63, 83, "lit2", "Pourquoi je dois mettre de la viande sur des lits?!?");
            litInteractions[2].Initialize(new Vector2(4098, 6088), 47, 103, "lit3", "C'est moi qui devient fou ou cette maison tourne par rond?");
            litInteractions[3].Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "Je devrais demisionner... Ca devient trop dangereux");
            couteuxFour.Initialize(new Vector2(3979, 6573), 33, 27, "couteux","Des couteux au four?? Drole de nourriture...");
            for (int i = 0; i < kitchenPapers.Length; i++)
            {
                kitchenPapers[i] = new InteractObject();
            }
            kitchenPapers[0].Initialize(new Vector2(3843, 6591), 23, 16, "papier1", "Mettre des couteaux de cuisine au four");
            kitchenPapers[1].Initialize(new Vector2(3925, 6572), 31, 38, "papier2", "Laver les livres de la biblitotheque");
            kitchenPapers[2].Initialize(new Vector2(3955, 6632), 42, 35, "papier3", "");
            kitchenPapers[3].Initialize(new Vector2(4021, 6572), 30, 27, "papier4", "Mettre de la viande sur les lits");
            kitchenPapers[4].Initialize(new Vector2(4086, 6571), 36, 27, "papier5", "Tout intrus doit etre eradique");
            kitchenPapers[5].Initialize(new Vector2(4120, 6605), 36, 27, "papier6", "Recuperer la viande dans le stockage");
            for (int i = 0; i < laverLivres.Length; i++)
            {
                laverLivres[i] = new InteractObject();
            }
            laverLivres[0].Initialize(new Vector2(4482, 6381), 50, 50, "armoire1", "Les livres sont foutu maintenant");
            laverLivres[1].Initialize(new Vector2(4281, 6384), 50, 50, "armoire2", "Pourquoi laver les livres?");
            laverLivres[2].Initialize(new Vector2(4184, 6384), 50, 50, "armoire3", "Decidemment, ca devient de plus en plus bizzare...");
            recupViande.Initialize(new Vector2(3918, 5718), 67, 702, "viande", "Toujours la meme viande ragoutante...");
            debutArme.Initialize(new Vector2(0, 0), 32, 26, "arme", "J'ai une epee. Vite l'eau benite de la buandrie !");
            zombie.Initialiaze(new Vector2(-350, 0), Constantes.ZOMBIE_SPEED);
            zombie.PeutTuer = false;
            zombie.PeutBouger = false;
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit4.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"),
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()), Game);
            zombie.LoadContent(Game.Content.Load<SpriteSheet>("zombie.sf", new JsonContentLoader()));
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            textInfo.Text = "Qu'est ce qui s'est passe? Je me suis evanouis je crois...";
            textInfo.ActiveText(Constantes.TEMPS_TEXTE);
            knifeSound = Game.Content.Load<SoundEffect>("Knife");
            washSound = Game.Content.Load<SoundEffect>("Washing");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit4.Update(gameTime);
            textInfo.Update(gameTime);
            zombie.Update(gameTime, _nuit4, Game);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                for (int i = 0; i < kitchenPapers.Length; i++)
                {
                    if (kitchenPapers[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        Additions.InteractionObjet(kitchenPapers[i], textInfo, kitchenPapers[i].InteractText);
                    }
                }
                for (int i = 0; i < litInteractions.Length; i++)
                {
                    if (litInteractions[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        if (litInteractions[i].HasAlreadyInteractable == false)
                        {
                            if (estRecuperer == true && litInteractions[i].HasAlreadyInteractable == false)
                            {
                                Additions.InteractionObjet(litInteractions[i], textInfo, "J'ai fais ce lit");
                                nombreLitFait += 1;
                                uneTache = true;
                                estRecuperer = false;
                            }
                            else if(estRecuperer == false && litInteractions[i].HasAlreadyInteractable == false)
                            {
                                textInfo.Text = "Il me faut de la viande avant";
                                textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                            }
                        }
                        else
                        {
                            Additions.InteractionObjet(litInteractions[i], textInfo, litInteractions[i].InteractText);
                        }
                    }
                }
                if (recupViande.InteractWith(-_nuit4.Camera.Position))
                {
                    if (nombreLitFait == litInteractions.Length)
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
                    if (!textInfo.WritingText)
                    {
                        knifeSound.Play();
                    }
                    Additions.InteractionObjet(couteuxFour, textInfo, "Les couteaux sont deja au fourneaux...");
                    uneTache = true;
                }
                for (int i = 0; i < laverLivres.Length; i++)
                {
                    if (laverLivres[i].InteractWith(-_nuit4.Camera.Position))
                    {
                        if (!textInfo.WritingText)
                        {
                            washSound.Play();
                        }
                        if (laverLivres[i].HasAlreadyInteractable == false)
                        {
                            Additions.InteractionObjet(laverLivres[i], textInfo, "Les livres sont laver ici");
                            nombreLivre += 1;
                            uneTache = true;
                        }
                        else
                        {
                            Additions.InteractionObjet(laverLivres[i], textInfo, laverLivres[i].InteractText);
                        }
                    }
                }
                if (debutArme.InteractWith(-_nuit4.Camera.Position))
                {
                    if(nombreMorceauxArme == 0)
                    {
                        Additions.InteractionObjet(debutArme, textInfo, "Mon epee est benite. Il faut que je purifie cette arme dans le feu !");
                        debutArme.InteractRect = new Rectangle(4778, 6193, 20, 23);
                    }
                    if (nombreMorceauxArme == 1)
                    {
                        Additions.InteractionObjet(debutArme, textInfo, "Deus Vult ! Je vais t'envoyer en enfer vile creature !");
                        debutArme.InteractRect = new Rectangle(4963, 6381, 50, 40);
                    }
                    if (nombreMorceauxArme == 2)
                    {
                        textInfo.Text = "Deus Vult ! Je vais t'envoyer en enfer vile creature !";
                        textInfo.ActiveText(Constantes.TEMPS_TEXTE);
                        debutArme.InteractRect = new Rectangle(4963, 6381, 50, 40);
                    }
                    nombreMorceauxArme += 1;
                }
                if (uneTache && zombie.PeutBouger==false)
                {
                    zombie.PeutTuer = true;
                    zombie.PeutBouger = true;
                }
                if(nombreMorceauxArme == 3)
                {
                    zombie.PeutTuer = false;
                }
            }
            if ((nombreLitFait == litInteractions.Length && nombreLivre == laverLivres.Length && couteuxFour.HasAlreadyInteractable == true && nombreMorceauxArme == 0) || Keyboard.GetState().IsKeyDown(Keys.E))
            {
                debutArme.InteractRect = new Rectangle(5072, 6125, debutArme.InteractRect.Width, debutArme.InteractRect.Height);
                nombreMorceauxArme = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit4.Draw(Game.SpriteBatch);
            zombie.Draw(Game.SpriteBatch);
            _nuit4.Player.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            _nuit4.DrawVision(Game.SpriteBatch);
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }


        
    }
}
