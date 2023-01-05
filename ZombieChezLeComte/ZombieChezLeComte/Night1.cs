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
    internal class Night1 : GameScreen
    {
        
        private new Game1 Game => (Game1)base.Game;
        public Night1(Game1 game) : base(game) { }
        private CommonNight _nuit1 = new CommonNight();

        private InteractObject[] litInteraction = new InteractObject[4];
        private int nombreLitFait = 0;
        private InteractObject[] armoireInteraction = new InteractObject[3];
        private int nombreArmoireFait = 0;
        private InteractObject solNettoyage = new InteractObject();
        private InteractObject porteCave = new InteractObject();
        private InteractObject tableCuisine = new InteractObject();
        private TextInfo textInfo =  new TextInfo();

        public override void Initialize()
        {
            _nuit1.Initialize(Game.Window, Game.GraphicsDevice);
            textInfo.Initialize(" ",Color.White,new Vector2(0,0));
            for(int i = 0; i < armoireInteraction.Length; i++)
            {
                armoireInteraction[i] = new InteractObject();
            }
            armoireInteraction[0].Initialize(new Vector2(4482, 6381), 50, 50, "armoire1", "Plein de poussiere !");
            armoireInteraction[1].Initialize(new Vector2(4281, 6384), 50, 50, "armoire2", "C'est une porcherie !");
            armoireInteraction[2].Initialize(new Vector2(4184, 6384), 50, 50, "armoire3", "cette armoire est vraiment ranger n'importe comment");

            solNettoyage.Initialize(new Vector2(4765,6637), 103, 26, "sol", "Le sol avait vraiment besoin d'une coup de nettoyage");
            porteCave.Initialize(new Vector2(4917, 6637), 103, 26, "porte", "La porte est ferme a cle. Bizzare...");
            tableCuisine.Initialize(new Vector2(4306,6576), 117,40,"table","Beurk!");

            for (int i = 0; i < litInteraction.Length; i++)
            {
                litInteraction[i]= new InteractObject();
            }
            litInteraction[0].Initialize(new Vector2(4675, 5902), 37, 73, "lit1", "Une bonne chose de faites");
            litInteraction[1].Initialize(new Vector2(4098, 5900), 37, 73, "lit2", "c'est degeulasse");
            litInteraction[2].Initialize(new Vector2(4098, 6088), 37, 73, "lit3", "Sympa les draps");
            litInteraction[3].Initialize(new Vector2(4452, 6131), 37, 73, "lit4", "Je me prendrai du cafe une fois mes taches finis");
            base.Initialize();
        }
        public override void LoadContent()
        {
            _nuit1.LoadContent(Game.GraphicsDevice, Game.Content.Load<TiledMap>("map"), 
                Game.Content.Load<SpriteSheet>("joueur.sf", new JsonContentLoader()));
            textInfo.LoadContent(Game.Content.Load<SpriteFont>("MeanFont"));
            
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _nuit1.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                for(int i = 0; i < litInteraction.Length; i++)
                {
                    if (litInteraction[i].InteractWith(-_nuit1.Camera.Position))
                    {
                        if(litInteraction[i].HasAlreadyInteractable == false)
                        {
                            textInfo.Text = litInteraction[i].InteractText;
                            litInteraction[i].HasAlreadyInteractable=true;
                            nombreLitFait += 1;
                            litInteraction[i].InteractText = "Ce lit est fait";
                        }
                        textInfo.Text = litInteraction[i].InteractText;
                        Console.WriteLine("Interaction");
                    }
                    else
                    {
                        Console.WriteLine("Non");
                    } 
                }
                for (int i = 0; i < armoireInteraction.Length; i++)
                {
                    if (armoireInteraction[i].InteractWith(-_nuit1.Camera.Position))
                    {
                        if (armoireInteraction[i].HasAlreadyInteractable == false)
                        {
                            textInfo.Text = armoireInteraction[i].InteractText;
                            armoireInteraction[i].HasAlreadyInteractable = true;
                            nombreArmoireFait += 1;
                            armoireInteraction[i].InteractText = "Cette bibliotheque est rangee";
                        }
                        textInfo.Text = armoireInteraction[i].InteractText;
                        Console.WriteLine("Interaction");
                    }
                    else
                    {
                        Console.WriteLine("Non");
                    }
                }
                if (porteCave.InteractWith(-_nuit1.Camera.Position))
                {
                    porteCave.HasAlreadyInteractable = true;
                    textInfo.Text = porteCave.InteractText;
                    Console.WriteLine("Interaction");
                }
                else
                {
                    Console.WriteLine("non");
                }
                if (tableCuisine.InteractWith(-_nuit1.Camera.Position))
                {
                    textInfo.Text = tableCuisine.InteractText;
                    tableCuisine.HasAlreadyInteractable = true;
                    tableCuisine.InteractText = "La table est nettoyee";
                    Console.WriteLine("Interaction");
                }
                else
                {
                    Console.WriteLine("non");
                }
                if (solNettoyage.InteractWith(-_nuit1.Camera.Position))
                {
                    textInfo.Text = solNettoyage.InteractText;
                    solNettoyage.HasAlreadyInteractable = true;
                    solNettoyage.InteractText = "Le sol est prope";
                    Console.WriteLine("Interaction");
                }
                else
                {
                    Console.WriteLine("non");
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.White);
            _nuit1.Draw(Game.SpriteBatch);
            Game.SpriteBatch.Begin();
            textInfo.Draw(Game.SpriteBatch);
            Game.SpriteBatch.End();
        }
    }
}
