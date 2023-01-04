using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace ZombieChezLeComte
{
    public class CommonNight
    {
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;


        public TiledMap TiledMap
        {
            get
            {
                return this._tiledMap;
            }

            set
            {
                this._tiledMap = value;
            }
        }
        public TiledMapRenderer TiledMapRenderer
        {
            get
            {
                return this._tiledMapRenderer;
            }

            set
            {
                this._tiledMapRenderer = value;
            }
        }


        public void Initialize()
        {

        }
        public void LoadContent(GraphicsDevice _graphicsDevice, TiledMap _tilMap)
        {
            this.TiledMap = _tilMap;
            this.TiledMapRenderer = new TiledMapRenderer(_graphicsDevice, this.TiledMap);
        }
        public void Update(GameTime _gameTime)
        {
            this.TiledMapRenderer.Update(_gameTime);
        }
        public void Draw()
        {
            this.TiledMapRenderer?.Draw();
        }
    }
}
