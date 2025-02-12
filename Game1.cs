using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Project1
{
    public static class TileDef
    {
        private static Random rnd = new Random();
        public static int getRandom(int number) => rnd.Next(0, number);
        //  方向
        public static int NORTH = 0;
        public static int EAST = 1;
        public static int SOUTH = 2;
        public static int WEST = 3;
        //  瓦片類型
        public static int TILE_WATER = 0;
        public static int TILE_GRASS = 1;
        public static int TILE_FOREST = 2;
        public static int TILE_ROAD = 3;
        public static int TILE_CITYROAD = 4;
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
        //  邊緣類型
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
        public static int CITYROAD = 12;
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
                {TILE_CITYROAD,new Rectangle(64,0,16,16) }
            };
            tileRules = new Dictionary<int, List<int>>()
            {
                { TILE_WATER,new List<int>(){WATER,WATER,WATER, WATER } },
                { TILE_GRASS,new List<int>(){GRASS,GRASS,GRASS,GRASS}},
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
            };
            tileWeights = new Dictionary<int, int>()
            {
                {TILE_WATER,5 },
                {TILE_GRASS,16},
                {TILE_FOREST,5 },
                {TILE_COAST_N,5 },
                {TILE_COAST_E,5 },
                {TILE_COAST_S,5 },
                {TILE_COAST_W,5 },
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
            };
        }
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private Texture2D texture;
        private RenderTarget2D renderTarget;
        private static bool previousTKeyState;
        private static World world;
        private static bool restart;
        private static string msg;
        private static int sizeX = 50;
        private static int sizeY = 30;
        private static int TILESIZE = 16;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            Window.Title = "Wave Function Collapse";
            msg = "Press T restart";
            world = new World(sizeY, sizeX);
            restart = false;
            renderTarget = new RenderTarget2D(GraphicsDevice, sizeX * TILESIZE, sizeY * TILESIZE);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Grass");
            font = Content.Load<SpriteFont>("DefaultFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            bool currentTKeyState = keyboardState.IsKeyDown(Keys.T);
            if (currentTKeyState && !previousTKeyState)
            {
                restart = true;
            }
            previousTKeyState = currentTKeyState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            if (restart)
            {
                msg = "Press T restart";
                world = new World(sizeY, sizeX);
                restart = false;
            }
            if (world.waveFunctionCollapse())
            {
                GraphicsDevice.SetRenderTarget(renderTarget);
                _spriteBatch.Begin();
                GraphicsDevice.Clear(Color.CornflowerBlue);
                int lowestEntropy = world.getLowestEntropy();
                for (int y = 0; y < sizeY; y++)
                {
                    for (int x = 0; x < sizeX; x++)
                    {
                        int tile_entopy = world.getEntropy(y, x);
                        List<int> possibilities = world.getPossibilities(y, x);
                        if (possibilities.Count == 0)
                        {
                            msg = "RUNNING ERROR! Press T restart";
                            restart = true;
                            GraphicsDevice.SetRenderTarget(null);
                            _spriteBatch.End();
                            return;
                        }
                        int tile_type = world.getPossibilities(y, x)[0];
                        if (tile_entopy > 0)
                        {
                            if (tile_entopy == 27)
                            {
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_WATER], Color.White);
                            }
                            else if (tile_entopy >= 10)
                            {
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_ROAD], Color.White);
                            }
                            else if (tile_entopy < 10)
                            {
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_CITYROAD], Color.White);
                            }
                        }
                        else
                        {
                            if (tile_type == 2 || tile_type >= 17) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_FOREST], Color.White);
                            else if (tile_type == 1 || tile_type >= 5) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_GRASS], Color.White);
                            else if (tile_type == 0) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_WATER], Color.White);
                            else _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), TileDef.tileSprites[TileDef.TILE_ROAD], Color.White);
                        }
                    }
                }
                _spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);
            }
            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(font, $"{msg}", new Vector2(0, 0), Color.Black);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
    public class World
    {
        private int sizeX;
        private int sizeY;
        public List<List<Tile>> tileRows;
        public World(int sizeY, int sizeX)
        {
            this.sizeY = sizeY;
            this.sizeX = sizeX;
            tileRows = new List<List<Tile>>();
            for (int y = 0; y < sizeY; y++)
            {
                List<Tile> tiles = new List<Tile>();
                for (int x = 0; x < sizeX; x++)
                {
                    Tile tile = new Tile();
                    tiles.Add(tile);
                }
                tileRows.Add(tiles);
            }
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    Tile tile = tileRows[y][x];
                    if (y > 0) tile.neighbours[TileDef.NORTH] = tileRows[y - 1][x];
                    if (x < sizeX - 1) tile.neighbours[TileDef.EAST] = tileRows[y][x + 1];
                    if (y < sizeY - 1) tile.neighbours[TileDef.SOUTH] = tileRows[y + 1][x];
                    if (x > 0) tile.neighbours[TileDef.WEST] = tileRows[y][x - 1];
                }
            }
        }
        public int getEntropy(int y, int x) => tileRows[y][x].entropy;
        public List<int> getPossibilities(int y, int x) => tileRows[y][x].possibilities;
        public int getLowestEntropy()
        {
            int lowestEntropy = TileDef.tileRules.Keys.Count;
            for(int y = 0; y < sizeY; y++)
            {
                for(int x = 0; x < sizeX; x++)
                {
                    int tileEntropy = tileRows[y][x].entropy;
                    if (tileEntropy > 0 && tileEntropy < lowestEntropy)
                    {
                        lowestEntropy = tileEntropy;
                    }
                }
            }
            return lowestEntropy;
        }
        public List<Tile> getTilesLowestEntropy()
        {
            int lowestEntropy = TileDef.tileRules.Keys.Count;
            List<Tile> tiles = new List<Tile>();
            for(int y = 0; y < sizeY; y++)
            {
                for(int x = 0; x < sizeX; x++)
                {
                    int tileEntropy = tileRows[y][x].entropy;
                    if (tileEntropy > 0)
                    {
                        if (tileEntropy < lowestEntropy)
                        {
                            tiles.Clear();
                            lowestEntropy = tileEntropy;
                        }
                        if (tileEntropy == lowestEntropy)
                        {
                            tiles.Add(tileRows[y][x]);
                        }
                    }
                }
            }
            return tiles;
        }
        public bool waveFunctionCollapse()
        {
            List<Tile> tilesLowestEntropy = getTilesLowestEntropy();
            if (tilesLowestEntropy.Count == 0) return false;
            Tile tileToCollapse = tilesLowestEntropy[TileDef.getRandom(tilesLowestEntropy.Count)];
            tileToCollapse.collapse();
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(tileToCollapse);
            while (stack.Count > 0)
            {
                Tile tile = stack.Pop();
                List<int> tilePossibilities = tile.possibilities;
                List<int> directions = new List<int>(tile.neighbours.Keys);
                foreach (int direction in directions)
                {
                    Tile neighbour = tile.neighbours[direction];
                    if (neighbour.entropy != 0)
                    {
                        bool reduced = neighbour.constrain(tilePossibilities, direction);
                        if (reduced)
                        {
                            stack.Push(neighbour);
                        }
                    }
                }
            }
            return true;
        }
    }
    public class Tile
    {
        public List<int> possibilities;
        public int entropy;
        public Dictionary<int,Tile> neighbours;
        public Tile()
        {
            possibilities = TileDef.tileRules.Keys.ToList();
            entropy = possibilities.Count;
            neighbours = new Dictionary<int, Tile>();
        }
        public void collapse()
        {
            int randomValue = TileDef.getRandom(possibilities.Select(possibility => TileDef.tileWeights[possibility]).Sum());
            int cumulativeWeight = 0;
            int selectedPossibility = possibilities.First();
            foreach (var possibility in possibilities)
            {
                cumulativeWeight += TileDef.tileWeights[possibility];
                if (randomValue < cumulativeWeight)
                {
                    selectedPossibility = possibility;
                    break;
                }
            }
            possibilities = new List<int>() { selectedPossibility };
            entropy = 0;
        }
        /*
         * Ex.
         * weight = [2,5,6,7]
         * totalWeight = 20
         * rnd = 12(小於 totalWeight 且大於零的亂數)
         * weight的前綴和 = [2,7,13,20]
         * 因為 13 > 12 所以 selectedpossibility = 2 entropy = 0
         * 以此塌縮成一種狀態
         */
        public bool constrain(List<int> neighbourPossibilities, int direction)
        {
            bool reduced = false;
            if (entropy > 0)
            {
                List<int> connectors = new List<int>();
                foreach (int neighbourPossibility in neighbourPossibilities)
                {
                    connectors.Add(TileDef.tileRules[neighbourPossibility][direction]);
                }
                int opposite = (direction + 2) % 4;
                foreach (int possibility in possibilities.ToList())
                {
                    if (!connectors.Contains(TileDef.tileRules[possibility][opposite]))
                    {
                        possibilities.Remove(possibility);
                        reduced = true;
                    }
                }
                entropy = possibilities.Count;
            }
            return reduced;
        }
    }
}
/*
 * 基於規則(約束式生成演算法) 來決定 tile 放置（不像噪音是平滑變化）保證符合規則
 * 
 * 1.分析像素圖片其中的圖案
 * 2.初始化 Tile 陣列(所有單元格還未觀測)
 * 3.重複以下
 *  a.找到 entropy(熵) 最少的單元格
 *  b.塌縮此單元格成其中一種狀態
 *  c.更新其周圍單元格的可能狀態
 *  d.如果某個單元格違反規則，則丟棄重新開始
 * 4.完成並輸出結果
 *  a.成功：直接輸出畫面
 *  b.違反：暫停等待重試
 */
