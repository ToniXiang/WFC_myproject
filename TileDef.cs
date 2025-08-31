using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
namespace Project1
{
    public static class TileDef
    {
        private static Random rnd = new Random();
        public static int getRandom(int number) => rnd.Next(0, number);
        // directions
        public static int NORTH = 0;
        public static int EAST = 1;
        public static int SOUTH = 2;
        public static int WEST = 3;
        // tile ids
        public static int TILE_WATER = 0;
        public static int TILE_GRASS = 1;
        public static int TILE_FOREST = 2;
        public static int TILE_ROAD = 3;
        public static int TILE_HOUSE = 4;
        public static int TILE_COAST_N = 5;
        public static int TILE_COAST_E = 6;
        public static int TILE_COAST_W = 7;
        public static int TILE_COAST_S = 8;
        public static int TILE_COAST_NE = 9;
        public static int TILE_COAST_ES = 10;
        public static int TILE_COAST_SW = 11;
        public static int TILE_COAST_WN = 12;
        public static int TILE_COAST_NE2 = 13;
        public static int TILE_COAST_ES2 = 14;
        public static int TILE_COAST_SW2 = 15;
        public static int TILE_COAST_WN2 = 16;
        public static int TILE_FOREST_N = 17;
        public static int TILE_FOREST_E = 18;
        public static int TILE_FOREST_W = 19;
        public static int TILE_FOREST_S = 20;
        public static int TILE_FOREST_NE = 21;
        public static int TILE_FOREST_ES = 22;
        public static int TILE_FOREST_SW = 23;
        public static int TILE_FOREST_WN = 24;
        public static int TILE_FOREST_NE2 = 25;
        public static int TILE_FOREST_ES2 = 26;
        public static int TILE_FOREST_SW2 = 27;
        public static int TILE_FOREST_WN2 = 28;
        public static int TILE_ROAD_H = 29;
        public static int TILE_ROAD_V = 30;
        public static int TILE_ROAD_NE = 31;
        public static int TILE_ROAD_ES = 32;
        public static int TILE_ROAD_SW = 33;
        public static int TILE_ROAD_WN = 34;
        public static int TILE_ROAD_T_N = 35;
        public static int TILE_ROAD_T_E = 36;
        public static int TILE_ROAD_T_S = 37;
        public static int TILE_ROAD_T_W = 38;
        // edge types
        public static int WATER = 0;
        public static int GRASS = 1;
        public static int COAST_N = 2;
        public static int COAST_E = 3;
        public static int COAST_S = 4;
        public static int COAST_W = 5;
        public static int FOREST = 6;
        public static int FOREST_N = 7;
        public static int FOREST_E = 8;
        public static int FOREST_S = 9;
        public static int FOREST_W = 10;
        public static int ROAD = 11;
        public static int HOUSE = 12;

        public static Dictionary<int, Rectangle> tileSprites;
        public static Dictionary<int, List<int>> tileRules;
        public static Dictionary<int, int> tileWeights;

        static TileDef()
        {
            tileSprites = new Dictionary<int, Rectangle>()
            {
                {TILE_WATER,new Rectangle(0,0,16,16) },
                {TILE_GRASS,new Rectangle(16,0,16,16) },
                {TILE_FOREST,new Rectangle(32,0,16,16) },
                {TILE_ROAD,new Rectangle(48,0,16,16) },
                {TILE_HOUSE,new Rectangle(0,16,16,16) },
            };
            tileRules = new Dictionary<int, List<int>>()
            {
                { TILE_WATER,new List<int>(){ WATER,WATER,WATER, WATER } },
                { TILE_GRASS,new List<int>(){ GRASS,GRASS,GRASS,GRASS} },
                { TILE_FOREST,new List<int>() { FOREST, FOREST, FOREST, FOREST } },
                { TILE_COAST_N,new List<int>() { GRASS, COAST_E, WATER, COAST_W }},
                { TILE_COAST_E,new List<int>() { COAST_E, GRASS, COAST_E, WATER } },
                { TILE_COAST_S,new List<int>() { WATER, COAST_S, GRASS, COAST_S } },
                { TILE_COAST_W,new List<int>() { COAST_W, WATER, COAST_W, GRASS } },
                { TILE_COAST_NE,new List<int>() { GRASS, GRASS, COAST_E, COAST_N } },
                { TILE_COAST_ES,new List<int>() { COAST_E, GRASS, GRASS, COAST_S } },
                { TILE_COAST_SW,new List<int>() { COAST_W, COAST_S, GRASS, GRASS } },
                { TILE_COAST_WN,new List<int>() { GRASS, COAST_W, COAST_N, GRASS } },
                { TILE_COAST_NE2,new List<int>() { COAST_E, COAST_N, WATER, WATER } },
                { TILE_COAST_ES2,new List<int>() { WATER, COAST_S, COAST_E, WATER } },
                { TILE_COAST_SW2,new List<int>() { WATER, WATER, COAST_W, COAST_S } },
                { TILE_COAST_WN2,new List<int>() { COAST_W, WATER, WATER, COAST_N } },
                { TILE_FOREST_N,new List<int>() { FOREST, FOREST_N, GRASS, FOREST_N } },
                { TILE_FOREST_E,new List<int>() { FOREST_E, FOREST, FOREST_E, GRASS } },
                { TILE_FOREST_S,new List<int>() { GRASS, FOREST_S, FOREST, FOREST_S } },
                { TILE_FOREST_W,new List<int>() { FOREST_W, GRASS, FOREST_W, FOREST } },
                { TILE_FOREST_NE,new List<int>() { FOREST_E, FOREST_N, GRASS, GRASS } },
                { TILE_FOREST_ES,new List<int>() { GRASS, FOREST_S, FOREST_E, GRASS } },
                { TILE_FOREST_SW,new List<int>() { GRASS, GRASS, FOREST_W, FOREST_S } },
                { TILE_FOREST_WN,new List<int>() { FOREST_W, GRASS, GRASS, FOREST_N } },
                { TILE_FOREST_NE2,new List<int>() { FOREST, FOREST, FOREST_E, FOREST_N } },
                { TILE_FOREST_ES2,new List<int>() { FOREST_E, FOREST, FOREST, FOREST_S } },
                { TILE_FOREST_SW2,new List<int>() { FOREST_W, FOREST_S, FOREST, FOREST } },
                { TILE_FOREST_WN2,new List<int>() { FOREST, FOREST_N, FOREST_W, FOREST } },
                { TILE_ROAD_H,   new List<int>() { GRASS, ROAD, GRASS, ROAD } },
                { TILE_ROAD_V,   new List<int>() { ROAD, GRASS, ROAD, GRASS } },
                { TILE_ROAD_NE,  new List<int>() { GRASS, ROAD, ROAD, GRASS } },
                { TILE_ROAD_ES,  new List<int>() { GRASS, GRASS, ROAD, ROAD } },
                { TILE_ROAD_SW,  new List<int>() { ROAD, GRASS, GRASS, ROAD } },
                { TILE_ROAD_WN,  new List<int>() { ROAD, ROAD, GRASS, GRASS } },
                { TILE_ROAD_T_N, new List<int>() { ROAD, ROAD, ROAD, GRASS } },
                { TILE_ROAD_T_E, new List<int>() { GRASS, ROAD, ROAD, ROAD } },
                { TILE_ROAD_T_S, new List<int>() { ROAD, GRASS, ROAD, ROAD } },
                { TILE_ROAD_T_W, new List<int>() { ROAD, ROAD, GRASS, ROAD } },
                { TILE_HOUSE,new List<int>(){FOREST,FOREST,FOREST,FOREST} },
            };
            tileWeights = new Dictionary<int, int>()
            {
                {TILE_WATER,30 },
                {TILE_GRASS,16},
                {TILE_FOREST,5 },
                {TILE_COAST_N,12 },
                {TILE_COAST_E,12 },
                {TILE_COAST_S,12 },
                {TILE_COAST_W,12 },
                {TILE_COAST_NE,3 },
                {TILE_COAST_ES,3 },
                {TILE_COAST_SW,3 },
                {TILE_COAST_WN,3 },
                {TILE_COAST_NE2,2 },
                {TILE_COAST_ES2,2 },
                {TILE_COAST_SW2,2 },
                {TILE_COAST_WN2,2 },
                {TILE_FOREST_N,5 },
                {TILE_FOREST_E,5 },
                {TILE_FOREST_S,5 },
                {TILE_FOREST_W,5 },
                {TILE_FOREST_NE,5 },
                {TILE_FOREST_ES,5 },
                {TILE_FOREST_SW,5 },
                {TILE_FOREST_WN,5 },
                {TILE_FOREST_NE2,3 },
                {TILE_FOREST_ES2,3 },
                {TILE_FOREST_SW2,3 },
                {TILE_FOREST_WN2,3 },
                { TILE_ROAD_H, 40 },
                { TILE_ROAD_V, 40 },
                { TILE_ROAD_NE, 4 },
                { TILE_ROAD_ES, 4 },
                { TILE_ROAD_SW, 4 },
                { TILE_ROAD_WN, 4 },
                { TILE_ROAD_T_N, 2 },
                { TILE_ROAD_T_E, 2 },
                { TILE_ROAD_T_S, 2 },
                { TILE_ROAD_T_W, 2 },
                { TILE_HOUSE, 5 },
            };
        }
    }
}
