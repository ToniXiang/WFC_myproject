using System.Collections.Generic;
namespace Project1
{
    public class World
    {
        private int sizeX;
        private int sizeY;
        private List<List<Tile>> tileRows;

        public int SizeX => sizeX;
        public int SizeY => sizeY;

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
            int hCount = System.Math.Max(1, sizeY / 12);
            int vCount = System.Math.Max(1, sizeX / 16);

            for (int i = 0; i < hCount; i++)
            {
                int row = TileDef.getRandom(sizeY);
                for (int x = 0; x < sizeX; x++)
                {
                    if (TileDef.getRandom(100) < 85)
                    {
                        Tile t = tileRows[row][x];
                        t.SetPossibilities(new List<int>() { TileDef.TILE_ROAD_H });
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
                        t.SetPossibilities(new List<int>() { TileDef.TILE_ROAD_V });
                    }
                }
            }
        }

        public int GetEntropy(int y, int x) => tileRows[y][x].entropy;
        public List<int> GetPossibilities(int y, int x) => tileRows[y][x].possibilities;

        public Tile GetTileLowestEntropy()
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
                        int cur_len = System.Math.Abs(y - sizeY / 2) + System.Math.Abs(x - sizeX / 2);
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

        public bool WaveFunctionCollapse()
        {
            Tile tileToCollapse = GetTileLowestEntropy();
            if (tileToCollapse == null) return false;
            tileToCollapse.Collapse();
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
                        bool reduced = neighbour.Constrain(tilePossibilities, direction);
                        if (reduced)
                        {
                            stack.Push(neighbour);
                        }
                    }
                }
            }
            return true;
        }

        // Diagnostic: number of tiles still with entropy > 0
        public int GetUnresolvedCount()
        {
            int count = 0;
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    if (tileRows[y][x].entropy > 0) count++;
                }
            }
            return count;
        }
    }
}
