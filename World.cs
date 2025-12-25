using System.Collections.Generic;
using System.Linq;
namespace Project1
{
    public class World
    {
        private int sizeX;
        private int sizeY;
        private List<List<Tile>> tileRows;
        private bool hasContradiction;

        public int SizeX => sizeX;
        public int SizeY => sizeY;
        public bool HasContradiction => hasContradiction;

        public World(int sizeY, int sizeX, bool forceInit = false)
        {
            this.sizeY = sizeY;
            this.sizeX = sizeX;
            this.hasContradiction = false;
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
        public int GetEntropy(int y, int x) => tileRows[y][x].entropy;
        public List<int> GetPossibilities(int y, int x) => tileRows[y][x].possibilities;

        public Tile GetTileLowestEntropy()
        {
            // Check for contradictions first
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    if (tileRows[y][x].entropy == -1)
                    {
                        hasContradiction = true;
                        return null;
                    }
                }
            }

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
            if (hasContradiction)
            {
                return false;
            }

            Tile tileToCollapse = GetTileLowestEntropy();
            if (tileToCollapse == null)
            {
                // Either complete or contradiction
                return false;
            }

            tileToCollapse.Collapse();
            Stack<Tile> stack = new Stack<Tile>();
            stack.Push(tileToCollapse);
            while (stack.Count > 0)
            {
                Tile tile = stack.Pop();
                List<int> tilePossibilities = tile.possibilities;
                
                // Check if current tile has contradiction
                if (tile.entropy == -1)
                {
                    hasContradiction = true;
                    return false;
                }

                List<int> directions = tile.neighbours.Keys.ToList();
                foreach (int direction in directions)
                {
                    Tile neighbour = tile.neighbours[direction];
                    if (neighbour.entropy > 0)  // Only constrain uncollapsed tiles
                    {
                        bool reduced = neighbour.Constrain(tilePossibilities, direction);
                        
                        // Check if constraint caused contradiction
                        if (neighbour.entropy == -1)
                        {
                            hasContradiction = true;
                            return false;
                        }
                        
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
