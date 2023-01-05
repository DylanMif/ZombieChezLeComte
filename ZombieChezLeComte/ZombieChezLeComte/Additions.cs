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
    public class Additions
    {
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

        public static Vector2 GetAxis(KeyboardState _ks)
        {
            return new Vector2(Additions.GetAxisX(_ks), Additions.GetAxisY(_ks));
        }

        public static Vector2 Normalize(Vector2 vec)
        {
            if(vec == Vector2.Zero)
            {
                return Vector2.Zero;
            }
            return Vector2.Normalize(vec);
        }

        public static bool OutOfScreen(Vector2 vec)
        {
            if(vec.X < 0)
            {
                return true;
            }
            if(vec.X > Constantes.WINDOW_WIDTH)
            {
                return true;
            }
            if(vec.Y < 0)
            {
                return true;
            }
            if(vec.Y > Constantes.WINDOW_HEIGHT)
            {
                return true;
            }
            return false;
        }
    }
}
