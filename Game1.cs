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
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        private Texture2D texture;
        private Texture2D texture2;
        private Texture2D pixel;
        private RenderTarget2D renderTarget;

        private World previewWorld;
        private RenderTarget2D previewRenderTarget;
        private int previewSizeX = 40;
        private int previewSizeY = 24;

        private float transitionTimer = 0f;
        private float transitionDuration = 0.6f;
        private bool transitioning = false;
        private bool fadeToPlay = false;
        private static bool previousTKeyState;
        private static bool previousStartKeyState;
        private enum GameState { Title, Playing }
        private static GameState gameState;
        private static World world;
        private static bool restart;
        public static int sizeX = 80;
        public static int sizeY = 48;
        private static int TILESIZE = 32;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            Window.Title = "演算法演示";

            gameState = GameState.Title;
            restart = false;

            _graphics.PreferredBackBufferWidth = sizeX * TILESIZE;
            _graphics.PreferredBackBufferHeight = sizeY * TILESIZE;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("Grass");
            texture2 = Content.Load<Texture2D>("PurpleChapels");
            font = Content.Load<SpriteFont>("DefaultFont");

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            previewSizeX = Math.Min(previewSizeX, sizeX);
            previewSizeY = Math.Min(previewSizeY, sizeY);
            previewWorld = new World(previewSizeY, previewSizeX, false);
            previewRenderTarget = new RenderTarget2D(GraphicsDevice, previewSizeX * TILESIZE, previewSizeY * TILESIZE);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState keyboardState = Keyboard.GetState();
            bool currentTKeyState = keyboardState.IsKeyDown(Keys.T);
            bool currentStartKeyState = keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter);


            if (currentStartKeyState && !previousStartKeyState && gameState == GameState.Title)
            {

                world = new World(sizeY, sizeX, true);
                renderTarget = new RenderTarget2D(GraphicsDevice, sizeX * TILESIZE, sizeY * TILESIZE);
                restart = false;

                transitioning = true;
                fadeToPlay = true;
                transitionTimer = 0f;
                previousTKeyState = false;
                previousStartKeyState = false;
            }
            previousStartKeyState = currentStartKeyState;


            if (gameState == GameState.Playing && currentTKeyState && !previousTKeyState)
            {
                restart = true;
            }
            previousTKeyState = currentTKeyState;

            if (gameState == GameState.Title && !transitioning)
            {
                previewWorld.waveFunctionCollapse();
            }


            if (transitioning)
            {
                transitionTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (fadeToPlay && transitionTimer >= transitionDuration)
                {

                    gameState = GameState.Playing;
                    fadeToPlay = false;
                    transitionTimer = 0f;
                }
                else if (!fadeToPlay && transitionTimer >= transitionDuration)
                {

                    transitioning = false;
                    transitionTimer = 0f;
                }
            }
            base.Update(gameTime);
        }
        private void draw(int x, int y, Texture2D texture, int tilename)
        {
            _spriteBatch.Draw(texture, new Rectangle(x * TILESIZE, y * TILESIZE, TILESIZE, TILESIZE), TileDef.tileSprites[tilename], Color.White);
        }

        private void RenderWorld(World w, RenderTarget2D rt)
        {
            GraphicsDevice.SetRenderTarget(rt);
            _spriteBatch.Begin();
            GraphicsDevice.Clear(Color.DarkSlateBlue);
            for (int y = 0; y < (w == world ? sizeY : previewSizeY); y++)
            {
                for (int x = 0; x < (w == world ? sizeX : previewSizeX); x++)
                {
                    int tile_entopy = w.getEntropy(y, x);
                    List<int> possibilities = w.getPossibilities(y, x);
                    if (possibilities.Count == 0)
                    {
                        _spriteBatch.Draw(pixel, new Rectangle(x * TILESIZE, y * TILESIZE, TILESIZE, TILESIZE), Color.Black);
                        continue;
                    }
                    int tile_type = possibilities[0];
                    if (tile_entopy <= 0)
                    {
                        if (tile_type == 4)
                        {
                            draw(x, y, texture, TileDef.GRASS);
                            draw(x, y, texture2, TileDef.TILE_HOUSE);
                        }
                        else if (tile_type == 3 || tile_type >= 29) draw(x, y, texture, TileDef.TILE_ROAD);
                        else if (tile_type == 2 || tile_type >= 17) draw(x, y, texture, TileDef.TILE_FOREST);
                        else if (tile_type == 1 || tile_type >= 5) draw(x, y, texture, TileDef.TILE_GRASS);
                        else if (tile_type == 0) draw(x, y, texture, TileDef.TILE_WATER);
                    }
                    else
                    {
                        _spriteBatch.Draw(pixel, new Rectangle(x * TILESIZE, y * TILESIZE, TILESIZE, TILESIZE), Color.Black);
                        string e = tile_entopy.ToString();
                        Vector2 size = font.MeasureString(e);
                        Vector2 pos = new Vector2(x * TILESIZE + (TILESIZE - size.X) / 2f, y * TILESIZE + (TILESIZE - size.Y) / 2f);
                        _spriteBatch.DrawString(font, e, pos, Color.White);
                    }
                }
            }
            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
        }

        protected override void Draw(GameTime gameTime)
        {
            // 進入畫面 / 標題
            if (gameState == GameState.Title)
            {
                // 將預覽世界渲染到預覽目標
                RenderWorld(previewWorld, previewRenderTarget);

                GraphicsDevice.Clear(Color.DeepSkyBlue);
                _spriteBatch.Begin();
                // 繪製預覽（縮放並置中，右側保留資訊面板空間）
                int screenW = _graphics.PreferredBackBufferWidth;
                int screenH = _graphics.PreferredBackBufferHeight;
                int panelWidth = Math.Min(360, screenW / 3);
                int contentW = screenW - panelWidth - 40;
                float scale = Math.Min((contentW * 0.9f) / (previewSizeX * TILESIZE), (screenH * 0.8f) / (previewSizeY * TILESIZE));
                Vector2 destSize = new Vector2(previewSizeX * TILESIZE * scale, previewSizeY * TILESIZE * scale);
                Vector2 destPos = new Vector2((contentW - destSize.X) / 2f + 20, (screenH - destSize.Y) / 2f - 20);
                _spriteBatch.Draw(previewRenderTarget, new Rectangle((int)destPos.X, (int)destPos.Y, (int)destSize.X, (int)destSize.Y), Color.White);
                // 標題（左上）
                _spriteBatch.DrawString(font, "algorithm preview", new Vector2(32, 24), Color.Black);

                // 繪製側欄（半透明）
                int panelX = screenW - panelWidth - 20;
                int panelY = 40;
                int panelH = Math.Min(screenH - 80, 420);
                _spriteBatch.Draw(pixel, new Rectangle(panelX, panelY, panelWidth, panelH), Color.Black * 0.7f);
                // 面板文字（白色）
                int textX = panelX + 16;
                int textY = panelY + 16;
                int lineHeight = (int)font.MeasureString("T").Y + 6;
                _spriteBatch.DrawString(font, "Controls:", new Vector2(textX, textY), Color.White);
                _spriteBatch.DrawString(font, "  Space / Enter - Start", new Vector2(textX, textY + lineHeight), Color.White);
                _spriteBatch.DrawString(font, "  T - Restart (in game)", new Vector2(textX, textY + lineHeight * 2), Color.White);
                _spriteBatch.DrawString(font, "  Esc - Quit", new Vector2(textX, textY + lineHeight * 3), Color.White);
                // 資訊
                _spriteBatch.DrawString(font, "", new Vector2(textX, textY + lineHeight * 4), Color.White);
                _spriteBatch.DrawString(font, $"Preview size: {previewSizeX} x {previewSizeY}", new Vector2(textX, textY + lineHeight * 5), Color.White);
                _spriteBatch.DrawString(font, $"Main grid: {sizeX} x {sizeY}", new Vector2(textX, textY + lineHeight * 6), Color.White);
                // 圖例
                _spriteBatch.DrawString(font, "", new Vector2(textX, textY + lineHeight * 7), Color.White);
                _spriteBatch.DrawString(font, "Legend:", new Vector2(textX, textY + lineHeight * 8), Color.White);
                // 小圖例：已解析格與熵格
                int legendY = textY + lineHeight * 9;
                // 已解析樣例（草地）
                _spriteBatch.Draw(texture, new Rectangle(textX, legendY, TILESIZE, TILESIZE), TileDef.tileSprites[TileDef.TILE_GRASS], Color.White);
                _spriteBatch.DrawString(font, " resolved", new Vector2(textX + TILESIZE + 8, legendY + 4), Color.White);
                // 熵格樣例（黑色方塊）
                _spriteBatch.Draw(pixel, new Rectangle(textX, legendY + lineHeight, TILESIZE, TILESIZE), Color.Black);
                _spriteBatch.DrawString(font, " entropy (remaining)", new Vector2(textX + TILESIZE + 8, legendY + lineHeight + 4), Color.White);
                _spriteBatch.End();
                // 若有轉場則畫遮罩
                if (transitioning)
                {
                    float alpha = fadeToPlay ? MathHelper.Clamp(transitionTimer / transitionDuration, 0f, 1f) : MathHelper.Clamp(1f - (transitionTimer / transitionDuration), 0f, 1f);
                    _spriteBatch.Begin();
                    _spriteBatch.Draw(pixel, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * alpha);
                    _spriteBatch.End();
                }
                base.Draw(gameTime);
                return;
            }

            if (restart)
            {
                world = new World(sizeY, sizeX);
                restart = false;
            }
            // 主世界：每幀進行一個塌縮步驟
            world.waveFunctionCollapse();
            // 將主世界渲染到目標
            RenderWorld(world, renderTarget);
            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
            // 若有轉場則畫遮罩
            if (transitioning)
            {
                float alpha = fadeToPlay ? MathHelper.Clamp(transitionTimer / transitionDuration, 0f, 1f) : MathHelper.Clamp(1f - (transitionTimer / transitionDuration), 0f, 1f);
                _spriteBatch.Begin();
                _spriteBatch.Draw(pixel, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Black * alpha);
                _spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
    public class World
    {
        private int sizeX;
        private int sizeY;
        private List<List<Tile>> tileRows;
    public World(int sizeY, int sizeX, bool placeRoadSeeds = true)
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
            if (placeRoadSeeds)
            {
                PlaceRoadSeeds();
            }
        }
        private void PlaceRoadSeeds()
        {
            int hCount = Math.Max(1, sizeY / 12);
            int vCount = Math.Max(1, sizeX / 16);

            for (int i = 0; i < hCount; i++)
            {
                int row = TileDef.getRandom(sizeY);
                for (int x = 0; x < sizeX; x++)
                {
                    if (TileDef.getRandom(100) < 85)
                    {
                        Tile t = tileRows[row][x];
                        t.possibilities = new List<int>() { TileDef.TILE_ROAD_H };
                        t.entropy = 0;
                    }
                }
            }

            for (int i = 0; i < vCount; i++)
            {
                int col = TileDef.getRandom(sizeX);
                for (int y = 0; y < sizeY; y++)
                {
                    if (TileDef.getRandom(100) < 85)
                    {
                        Tile t = tileRows[y][col];
                        t.possibilities = new List<int>() { TileDef.TILE_ROAD_V };
                        t.entropy = 0;
                    }
                }
            }
        }
        public int getEntropy(int y, int x) => tileRows[y][x].entropy;
        public List<int> getPossibilities(int y, int x) => tileRows[y][x].possibilities;
        public Tile getTilesLowestEntropy()
        {
            int lowestEntropy = TileDef.tileRules.Keys.Count;
            Tile tile = null;
            int len = (int)1E5;
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    int tileEntropy = tileRows[y][x].entropy;
                    if (tileEntropy > 0)
                    {
                        int cur_len = Math.Abs(y - sizeY / 2) + Math.Abs(x - sizeX / 2);
                        if (tileEntropy < lowestEntropy)
                        {
                            tile = tileRows[y][x];
                            len = cur_len;
                            lowestEntropy = tileEntropy;
                        }
                        else if (tileEntropy == lowestEntropy && cur_len < len)
                        {
                            tile = tileRows[y][x];
                            len = cur_len;
                        }
                    }
                }
            }
            return tile;
        }
        public bool waveFunctionCollapse()
        {
            Tile tileToCollapse = getTilesLowestEntropy();
            if (tileToCollapse == null) return false;
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
        public Dictionary<int, Tile> neighbours;
        public Tile()
        {
            possibilities = TileDef.tileRules.Keys.ToList();
            entropy = possibilities.Count;
            neighbours = new Dictionary<int, Tile>();
        }
        public void collapse()
        {
            List<int> orderedPoss = possibilities.ToList();
            List<int> adjustedWeights = new List<int>();
            foreach (var poss in orderedPoss)
            {
                int baseWeight = TileDef.tileWeights.ContainsKey(poss) ? TileDef.tileWeights[poss] : 1;
                int matchCount = 0;
                bool forbiddenByRoadAdjacency = false;
                foreach (var kv in neighbours)
                {
                    int dir = kv.Key;
                    Tile neigh = kv.Value;
                    if (neigh != null && neigh.entropy == 0 && neigh.possibilities.Count == 1)
                    {
                        int neighTile = neigh.possibilities[0];
                        int opposite = (dir + 2) % 4;
                        if (TileDef.tileRules.ContainsKey(poss) && TileDef.tileRules.ContainsKey(neighTile))
                        {
                            bool possIsRoad = poss >= TileDef.TILE_ROAD_H && poss <= TileDef.TILE_ROAD_T_W;
                            bool neighIsRoad = neighTile >= TileDef.TILE_ROAD_H && neighTile <= TileDef.TILE_ROAD_T_W;
                            if (possIsRoad && neighIsRoad)
                            {
                                if (!(TileDef.tileRules[poss][dir] == TileDef.ROAD && TileDef.tileRules[neighTile][opposite] == TileDef.ROAD))
                                {
                                    forbiddenByRoadAdjacency = true;
                                    break;
                                }
                                else
                                {
                                    matchCount++;
                                }
                            }
                            else
                            {
                                if (TileDef.tileRules[poss][dir] == TileDef.tileRules[neighTile][opposite])
                                {
                                    matchCount++;
                                }
                            }
                        }
                    }
                }
                    if (forbiddenByRoadAdjacency)
                    {
                        adjustedWeights.Add(0);
                    }
                else
                {
                    int multiplier = 1 + 3 * matchCount;
                    if (poss == TileDef.TILE_WATER)
                    {
                        multiplier = 1 + 6 * matchCount;
                    }
                    int adj = Math.Max(1, baseWeight * multiplier);
                    adjustedWeights.Add(adj);
                }
            }

            int total = adjustedWeights.Sum();
            if (total == 0)
            {
                adjustedWeights.Clear();
                foreach (var poss in orderedPoss)
                {
                    int baseWeight = TileDef.tileWeights.ContainsKey(poss) ? TileDef.tileWeights[poss] : 1;
                    adjustedWeights.Add(Math.Max(1, baseWeight));
                }
                total = adjustedWeights.Sum();
            }
            int randomValue = TileDef.getRandom(total);
            int cumulativeWeight = 0;
            int selectedPossibility = orderedPoss.First();
            for (int i = 0; i < orderedPoss.Count; i++)
            {
                cumulativeWeight += adjustedWeights[i];
                if (randomValue < cumulativeWeight)
                {
                    selectedPossibility = orderedPoss[i];
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