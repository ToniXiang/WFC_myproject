using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace Project1
{
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
        private enum GameState { Title, Playing }
        private GameState gameState;
        private World world;
        private bool restart;
        public static int sizeX = 80;
        public static int sizeY = 48;
        private static int TILESIZE = 32;

        private WorldRenderer _renderer;
        private InputManager _input = new InputManager();

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

            _renderer = new WorldRenderer(_spriteBatch, font, texture, texture2, pixel, TILESIZE);
        }

        protected override void Update(GameTime gameTime)
        {
            // basic exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // input handling
            _input.Update();
            bool startPressed = _input.IsKeyPressedOnce(Keys.Space) || _input.IsKeyPressedOnce(Keys.Enter);
            bool tPressed = _input.IsKeyPressedOnce(Keys.T);

            if (startPressed && gameState == GameState.Title)
            {
                world = new World(sizeY, sizeX, true);
                renderTarget = new RenderTarget2D(GraphicsDevice, sizeX * TILESIZE, sizeY * TILESIZE);
                restart = false;

                transitioning = true;
                fadeToPlay = true;
                transitionTimer = 0f;
            }

            if (gameState == GameState.Playing && tPressed)
            {
                restart = true;
            }

            if (gameState == GameState.Title && !transitioning)
            {
                previewWorld.WaveFunctionCollapse();
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

        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.Title)
            {
                _renderer.RenderTo(GraphicsDevice, previewWorld, previewRenderTarget, previewSizeX, previewSizeY);

                GraphicsDevice.Clear(Color.DeepSkyBlue);
                _spriteBatch.Begin();

                int screenW = _graphics.PreferredBackBufferWidth;
                int screenH = _graphics.PreferredBackBufferHeight;
                int panelWidth = Math.Min(360, screenW / 3);
                int contentW = screenW - panelWidth - 40;
                float scale = Math.Min((contentW * 0.9f) / (previewSizeX * TILESIZE), (screenH * 0.8f) / (previewSizeY * TILESIZE));
                Vector2 destSize = new Vector2(previewSizeX * TILESIZE * scale, previewSizeY * TILESIZE * scale);
                Vector2 destPos = new Vector2((contentW - destSize.X) / 2f + 20, (screenH - destSize.Y) / 2f - 20);
                _spriteBatch.Draw(previewRenderTarget, new Rectangle((int)destPos.X, (int)destPos.Y, (int)destSize.X, (int)destSize.Y), Color.White);
                _spriteBatch.DrawString(font, "algorithm preview", new Vector2(32, 24), Color.Black);

                int panelX = screenW - panelWidth - 20;
                int panelY = 40;
                int panelH = Math.Min(screenH - 80, 420);
                _spriteBatch.Draw(pixel, new Rectangle(panelX, panelY, panelWidth, panelH), Color.Black * 0.7f);
                int textX = panelX + 16;
                int textY = panelY + 16;
                int lineHeight = (int)font.MeasureString("T").Y + 6;
                _spriteBatch.DrawString(font, "Controls:", new Vector2(textX, textY), Color.White);
                _spriteBatch.DrawString(font, "  Space / Enter - Start", new Vector2(textX, textY + lineHeight), Color.White);
                _spriteBatch.DrawString(font, "  T - Restart (in game)", new Vector2(textX, textY + lineHeight * 2), Color.White);
                _spriteBatch.DrawString(font, "  Esc - Quit", new Vector2(textX, textY + lineHeight * 3), Color.White);
                _spriteBatch.DrawString(font, $"Preview size: {previewSizeX} x {previewSizeY}", new Vector2(textX, textY + lineHeight * 5), Color.White);
                _spriteBatch.DrawString(font, $"Main grid: {sizeX} x {sizeY}", new Vector2(textX, textY + lineHeight * 6), Color.White);
                
                int previewUnresolved = previewWorld.GetUnresolvedCount();
                _spriteBatch.DrawString(font, $"Preview unresolved: {previewUnresolved}", new Vector2(textX, textY + lineHeight * 4), Color.White);

                int legendY = textY + lineHeight * 9;
                _spriteBatch.Draw(texture, new Rectangle(textX, legendY, TILESIZE, TILESIZE), TileDef.tileSprites[TileDef.TILE_GRASS], Color.White);
                _spriteBatch.DrawString(font, " resolved", new Vector2(textX + TILESIZE + 8, legendY + 4), Color.White);
                _spriteBatch.Draw(pixel, new Rectangle(textX, legendY + lineHeight, TILESIZE, TILESIZE), Color.Black);
                _spriteBatch.DrawString(font, " entropy (remaining)", new Vector2(textX + TILESIZE + 8, legendY + lineHeight + 4), Color.White);
                _spriteBatch.End();

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

            
            bool progressed = world.WaveFunctionCollapse();
            int unresolved = world.GetUnresolvedCount();
            _spriteBatch.Begin();
            _spriteBatch.DrawString(font, $"Unresolved: {unresolved} | ProgressedThisFrame: {progressed}", new Microsoft.Xna.Framework.Vector2(8, 8), Color.White);
            _spriteBatch.End();

            
            _renderer.RenderTo(GraphicsDevice, world, renderTarget, sizeX, sizeY);
            _spriteBatch.Begin();
            _spriteBatch.Draw(renderTarget, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

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