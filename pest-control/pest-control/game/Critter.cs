﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace pest_control
{
    public class Critter : LivingThing
    {
        private Decider decider;
        private static Random rand = new Random();
        private int numberOfChromosomes;
        private int critterSize;
        private int awarenessMod;
        private List<Chromosome> chromosomes = new List<Chromosome>();
        private Attributes needs;

        public Critter(BoundingBox boundingBox)
        {
            decider = new Decider();
            this.BoundingBox = boundingBox;
            this.speed = 1;
            this.Sprite = "critter-left";
            this.critterSize = 1;
            this.health = 10 * critterSize;
            this.awarenessMod = 1;
            this.needs = new Attributes(critterSize);

            this.assignChromosomes(critterSize);
        }

        public Critter(BoundingBox boundingBox, int critterSize)
        {
            decider = new Decider();
            this.BoundingBox = boundingBox;
            this.speed = 1;
            this.Sprite = "critter-left";
            this.critterSize = critterSize;
            this.health = 10 * critterSize;
            this.awarenessMod = 1;
            this.needs = new Attributes(critterSize);

            this.assignChromosomes(critterSize);
        }

        private void assignChromosomes(int num)//generates chromosomes according to value of numberOfChromasomes, then modifies the critter's attributes accordingly
        {
            for (int i = 0; i < critterSize; i++)
            {
                chromosomes.Add(new Chromosome((ChromosomeType)rand.Next(9) + 1, true));
            }

            foreach (Chromosome c in chromosomes)
            {
                if (c.Type == ChromosomeType.SpeedMod)//only the speed modifier chromosome is implemented here, as the other attributes do not yet exist to be modified
                {
                    if (c.Increase)
                        this.speed *= 2;
                    else
                        this.speed = (int)(speed * .5);
                }
                else if (c.Type == ChromosomeType.AwarenessMod)
                {
                    if (c.Increase)
                        this.awarenessMod *= 2;
                    else
                        this.awarenessMod = (int)(awarenessMod * .5);
                }
            }
        }

        public void takeDamage(DamageType damageType, double baseDamage)
        {
            double damageDealt = baseDamage;

            foreach (Chromosome c in chromosomes)
            {
                if (c.Type.ToString() == damageType.ToString())
                {
                    if (c.Increase)
                        damageDealt *= 0.5;
                    else
                        damageDealt *= 2;
                }
            }

            this.health -= (int)damageDealt;
        }


        public override void act()
        {
            base.act();
        }
        public void act(List<Thing> things)
        {
            List<Thing> nearbyThings = new List<Thing>();
            foreach (Thing t in things)
                if (this.BoundingBox.DistanceTo(t.BoundingBox) > critterSize * awarenessMod)
                    nearbyThings.Add(t);
            this.direction = decider.decide(needs, nearbyThings);
            base.act();
        }
    }
}

