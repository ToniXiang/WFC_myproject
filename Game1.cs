using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static Random rnd = new Random();
        private SpriteFont font;
        private Texture2D texture;
        private RenderTarget2D renderTarget;
        private static bool previousTKeyState;
        //  大小
        private static int sizeX = 50;
        private static int sizeY = 30;
        private static int TILESIZE = 16;
        //  方向
        public static int NORTH = 0;
        public static int EAST = 1;
        public static int SOUTH = 2;
        public static int WEST = 3;
        //  瓦片與邊緣的圖片位置
        private Dictionary<int, Rectangle> tileSprites = new Dictionary<int, Rectangle>()
        {
            {TILE_WATER,new Rectangle(0,0,16,16) },
            {TILE_GRASS,new Rectangle(16,0,16,16) },
            {TILE_FOREST,new Rectangle(32,0,16,16) },
            {TILE_ROAD,new Rectangle(48,0,16,16) },
            {TILE_CITYROAD,new Rectangle(64,0,16,16) }
        };
        //  瓦片類型
        private static int TILE_WATER = 0;
        private static int TILE_GRASS = 1;
        private static int TILE_FOREST = 2;
        private static int TILE_ROAD = 3;
        private static int TILE_CITYROAD = 4;
        private static int TILE_COAST_N = 5;
        private static int TILE_COAST_E = 6;
        private static int TILE_COAST_W = 7;
        private static int TILE_COAST_S = 8;
        private static int TILE_COAST_NE = 9;
        private static int TILE_COAST_ES = 10;
        private static int TILE_COAST_SW = 11;
        private static int TILE_COAST_WN = 12;
        private static int TILE_COAST_NE2 = 13;
        private static int TILE_COAST_ES2 = 14;
        private static int TILE_COAST_SW2 = 15;
        private static int TILE_COAST_WN2 = 16;
        private static int TILE_FOREST_N = 17;
        private static int TILE_FOREST_E = 18;
        private static int TILE_FOREST_W = 19;
        private static int TILE_FOREST_S = 20;
        private static int TILE_FOREST_NE = 21;
        private static int TILE_FOREST_ES = 22;
        private static int TILE_FOREST_SW = 23;
        private static int TILE_FOREST_WN = 24;
        private static int TILE_FOREST_NE2 = 25;
        private static int TILE_FOREST_ES2 = 26;
        private static int TILE_FOREST_SW2 = 27;
        private static int TILE_FOREST_WN2 = 28;
        //  邊緣類型
        private static int WATER = 0;
        private static int GRASS = 1;
        private static int COAST_N = 2;
        private static int COAST_E = 3;
        private static int COAST_S = 4;
        private static int COAST_W = 5;
        private static int FOREST = 6;
        private static int FOREST_N = 7;
        private static int FOREST_E = 8;
        private static int FOREST_S = 9;
        private static int FOREST_W = 10;
        private static int ROAD = 11;
        private static int CITYROAD = 12;
        public static Dictionary<int, List<int>> tileRules = new Dictionary<int, List<int>>()
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
        //  所有瓦片的權重
        public static Dictionary<int, int> tileWeights = new Dictionary<int, int>()
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
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        private static World world;
        private static bool restart;
        private static string msg;
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
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_WATER], Color.White);
                            }
                            else if (tile_entopy >= 10)
                            {
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_ROAD], Color.White);
                            }
                            else if (tile_entopy < 10)
                            {
                                _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_CITYROAD], Color.White);
                            }
                        }
                        else
                        {
                            if (tile_type == 2 || tile_type >= 17) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_FOREST], Color.White);
                            else if (tile_type == 1 || tile_type >= 5) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_GRASS], Color.White);
                            else if (tile_type == 0) _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_WATER], Color.White);
                            else _spriteBatch.Draw(texture, new Vector2(x * 16, y * 16), tileSprites[TILE_ROAD], Color.White);
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
                    if (y > 0) tile.addNeighbour(Game1.NORTH, tileRows[y - 1][x]);
                    if (x < sizeX - 1) tile.addNeighbour(Game1.EAST, tileRows[y][x + 1]);
                    if (y < sizeY - 1) tile.addNeighbour(Game1.SOUTH, tileRows[y + 1][x]);
                    if (x > 0) tile.addNeighbour(Game1.WEST, tileRows[y][x - 1]);
                }
            }
        }
        public int getEntropy(int y, int x) => tileRows[y][x].entropy;
        public List<int> getPossibilities(int y, int x) => tileRows[y][x].possibilities;
        public int getLowestEntropy()
        {
            int lowestEntropy = Game1.tileRules.Keys.Count;
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
            int lowestEntropy = Game1.tileRules.Keys.Count;
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
            Tile tileToCollapse = tilesLowestEntropy[Game1.rnd.Next(tilesLowestEntropy.Count)];
            tileToCollapse.collapse();
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(tileToCollapse);
            while (stack.Count > 0)
            {
                Tile tile = stack.Pop();
                List<int> tilePossibilities = tile.getPossibilities();
                List<int> directions = tile.getDirections();
                foreach (int direction in directions)
                {
                    Tile neighbour = tile.getNeighbour(direction);
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
            possibilities = Game1.tileRules.Keys.ToList();
            entropy = possibilities.Count;
            neighbours = new Dictionary<int, Tile>();
        }
        public void addNeighbour(int dire,Tile tile)
        {
            neighbours[dire] = tile;
        }
        public Tile getNeighbour(int dire) => neighbours[dire];
        public List<int> getDirections() => new List<int>(neighbours.Keys);
        public List<int> getPossibilities() => possibilities;
        public void collapse()
        {
            var weights = possibilities.Select(possibility => Game1.tileWeights[possibility]).ToList();
            int totalWeight = weights.Sum();
            int randomValue = Game1.rnd.Next(totalWeight);
            int cumulativeWeight = 0;
            int selectedPossibility = possibilities.First();
            foreach (var possibility in possibilities)
            {
                cumulativeWeight += Game1.tileWeights[possibility];
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
                    connectors.Add(Game1.tileRules[neighbourPossibility][direction]);
                }
                int opposite = (direction + 2) % 4;
                foreach (int possibility in possibilities.ToList())
                {
                    if (!connectors.Contains(Game1.tileRules[possibility][opposite]))
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
 * 數學：機率與統計、線代、圖論、離散
 * Wave Function Collapse 基於規則(約束式生成演算法) 來決定 tile 放置（不像噪音是平滑變化）保證符合規則
 * 
 * 哥本哈根詮釋
 * 從一種疊加狀態塌縮(collapse)到確定的本徵態
 * 
 * gumin's implementation 隨機地圖生成、圖案生成、建築佈局
 * 
 * 1.分析像素圖片其中的圖案
 * 2.初始化輸出陣列(所有單元格還未觀測)
 * 3.重複以下
 *  a.找到 entropy 最少的單元格
 *  b.collapse 它為其中一種可能的狀態(機率去選)
 *  c.更新其周圍單元格的可能狀態(確保有效)
 *  d.如果某個單元違反規則，則丟棄重新開始
 * 4.完成並輸出結果
 *  a.成功：輸出
 *  b.違反：重試
 */
