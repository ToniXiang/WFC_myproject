using System.Collections.Generic;
using System.Linq;
namespace Project1
{
    public class Tile
    {
        public List<int> possibilities { get; private set; }
        public int entropy { get; private set; }
        public Dictionary<int, Tile> neighbours { get; private set; }

        public Tile()
        {
            possibilities = TileDef.tileRules.Keys.ToList();
            entropy = possibilities.Count;
            neighbours = new Dictionary<int, Tile>();
        }

        public void SetPossibilities(List<int> newPossibilities)
        {
            possibilities = newPossibilities.ToList();
            // If empty, entropy = -1 to indicate contradiction
            // If single possibility, entropy = 0 (collapsed)
            // Otherwise, entropy = count of possibilities
            if (possibilities.Count == 0)
            {
                entropy = -1;  // Contradiction state
            }
            else if (possibilities.Count == 1)
            {
                entropy = 0;   // Collapsed state
            }
            else
            {
                entropy = possibilities.Count;
            }
        }

        public void Collapse()
        {
            // weighted random pick using tileWeights and adjacency bonus
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
                    int adj = System.Math.Max(1, baseWeight * multiplier);
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
                    adjustedWeights.Add(System.Math.Max(1, baseWeight));
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
            SetPossibilities(new List<int>() { selectedPossibility });
        }

        public bool Constrain(List<int> neighbourPossibilities, int direction)
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
                // Update entropy after constraint
                if (possibilities.Count == 0)
                {
                    entropy = -1;  // Contradiction
                }
                else if (possibilities.Count == 1)
                {
                    entropy = 0;   // Collapsed
                }
                else
                {
                    entropy = possibilities.Count;
                }
            }
            return reduced;
        }
    }
}
