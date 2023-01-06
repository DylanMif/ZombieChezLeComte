﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ZombieChezLeComte
{
    public class Constantes
    {
        public const string GAME_TITLE = "Zombie Chez le Comte";
        public const int WINDOW_WIDTH = 1080;
        public const int WINDOW_HEIGHT = 720;
        public const int VITESSE_JOUEUR = 300;
        public const int VITESSE_ZOMBIE = 120;
        public const float TEMPS_TEXTE = 2;
        public static readonly Vector2 POSITION_JOUEUR = new Vector2(-4080,-6392);
        public const int CHARACTER_SPRITE_SIZE = 32;

        public const Keys upKeys = Keys.Z;
        public const Keys downKeys = Keys.S;
        public const Keys leftKeys = Keys.Q;
        public const Keys rightKeys = Keys.D;

        public const int NIGHT3_RUNGHOST_CHANCE = 360;
        public const int RUNGHOST_SPEED = 500;
        public const int END_NIGHT3_NB_GHOST = 5;
        public const int END_NIGHT3_GHOST_SPEED = 70;

        public const int ZOMBIE_SPEED = 50;

        public const int HUNT_GHOST_SPEED = 100;

        public const int DOOR_GHOST_MIN_STAY_TIME = 15;
        public const int DOOR_GHOST_MAX_STAY_TIME = 30;
    }
}
