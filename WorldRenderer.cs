using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project1
{
    public class WorldRenderer
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Texture2D _mainTexture;
        private Texture2D _overlayTexture;
        private Texture2D _pixel;
        private int _tileSize;

        public WorldRenderer(SpriteBatch spriteBatch, SpriteFont font, Texture2D mainTexture, Texture2D overlayTexture, Texture2D pixel, int tileSize)
        {
            _spriteBatch = spriteBatch;
            _font = font;
            _mainTexture = mainTexture;
            _overlayTexture = overlayTexture;
            _pixel = pixel;
            _tileSize = tileSize;
        }

        public void RenderTo(GraphicsDevice graphicsDevice, World w, RenderTarget2D rt, int widthTiles, int heightTiles)
        {
            graphicsDevice.SetRenderTarget(rt);
            _spriteBatch.Begin();
            graphicsDevice.Clear(Color.DarkSlateBlue);

            for (int y = 0; y < heightTiles; y++)
            {
                for (int x = 0; x < widthTiles; x++)
                {
                    int tile_entropy = w.GetEntropy(y, x);
                    List<int> possibilities = w.GetPossibilities(y, x);
                    if (possibilities == null || possibilities.Count == 0)
                    {
                        _spriteBatch.Draw(_pixel, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), Color.Black);
                        continue;
                    }
                    int tile_type = possibilities[0];
                    if (tile_entropy <= 0)
                    {
                        if (tile_type == TileDef.TILE_HOUSE)
                        {
                            Draw(x, y, _mainTexture, TileDef.TILE_GRASS);
                            Draw(x, y, _overlayTexture, TileDef.TILE_HOUSE);
                        }
                        else if (tile_type == TileDef.TILE_ROAD || tile_type >= TileDef.TILE_ROAD_H) Draw(x, y, _mainTexture, TileDef.TILE_ROAD);
                        else if (tile_type == TileDef.TILE_FOREST || tile_type >= TileDef.TILE_FOREST_N) Draw(x, y, _mainTexture, TileDef.TILE_FOREST);
                        else if (tile_type == TileDef.TILE_GRASS || tile_type >= TileDef.TILE_COAST_N) Draw(x, y, _mainTexture, TileDef.TILE_GRASS);
                        else if (tile_type == TileDef.TILE_WATER) Draw(x, y, _mainTexture, TileDef.TILE_WATER);
                    }
                    else
                    {
                        _spriteBatch.Draw(_pixel, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), Color.Black);
                        string e = tile_entropy.ToString();
                        Vector2 size = _font.MeasureString(e);
                        Vector2 pos = new Vector2(x * _tileSize + (_tileSize - size.X) / 2f, y * _tileSize + (_tileSize - size.Y) / 2f);
                        _spriteBatch.DrawString(_font, e, pos, Color.White);
                    }
                }
            }

            _spriteBatch.End();
            graphicsDevice.SetRenderTarget(null);
        }

        private void Draw(int x, int y, Texture2D texture, int tileName)
        {
            _spriteBatch.Draw(texture, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize), TileDef.tileSprites[tileName], Color.White);
        }
    }
}
