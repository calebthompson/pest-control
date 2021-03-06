﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pest_control
{
    public class Terrain : Thing
    {
        
        public TerrainType terrainType;
        public override string Sprite
        {
            get { return "terrain-" + this.terrainType.ToString().ToLower(); }
            set
            {
                switch (value.Substring(7))
                {
                    case "tree":
                        terrainType = TerrainType.Tree;
                        break;
                    case "shrub":
                        terrainType = TerrainType.Shrub;
                        break;
                    case "water":
                        terrainType = TerrainType.Water;
                        break;
                    case "grass":
                        terrainType = TerrainType.Grass;
                        break;
                }
            }
        }

        public Terrain(TerrainType type, int x, int y)
        {
            this.terrainType = type;
            this.BoundingBox = new BoundingBox(x, y, 120, 120);
        }
    }
}
