﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace pest_control
{
    public class Character : LivingThing
    {
        public int CharacterIndex
        {
            get { return characterIndex; }
            set { characterIndex = value; }
        }

        private List<Weapon> weapons;
        private Weapon activeWeapon;
        private int characterIndex;

        public Character(int characterIndex, BoundingBox boundingBox)
        {
            this.CharacterIndex = characterIndex;
            this.BoundingBox = boundingBox;
            this.Sprite = "player-front-step-left";
        }

        public void setDirection(Direction d) { this.direction = d; }

        public void setSpeed(int s) { this.speed = s; }
    }
}
