﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pest_control
{
    public class Decider
    {
        private static Random rand = new Random();
        private enum Concern { food, water, sex, fear, agression };
        public Direction decide(Attributes attributes, List<Thing> nearbyThings)
        {
            switch (MostImportantConcern(attributes, nearbyThings))
            {
                case Concern.food:
                    foreach (Thing thing in nearbyThings)
                        if (thing is Terrain)
                            //if ((thing as Terrain).Type == water)
                                break;
                    break;
                case Concern.water:
                    break;
                case Concern.sex:
                    break;
                case Concern.fear:
                    break;
                case Concern.agression:
                    break;
            }
            
            return (Direction)rand.Next(8) + 1;
        }

        private Concern MostImportantConcern(Attributes attributes, List<Thing> nearbyThings)
        {
            return Concern.sex;
        }

        private double LargestOf(params double[] values)
        {
            double currentValue = 0;
            foreach (double value in values)
            {
                if (value > currentValue)
                    currentValue = value;
            }
            return currentValue;
        }


        private double Normalize(int value, int maxValue)
        {
            return Clamp(value/maxValue);
        }

        private double Clamp(double value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
            }
        }
}
