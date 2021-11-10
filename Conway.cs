using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    class Conway
    {
        private List<List<bool>> newGrid_ = new List<List<bool>>();
        private List<List<bool>> oldGrid_ = new List<List<bool>>();
        private List<(int, int)> coords_ = new List<(int, int)>();
        private int gridWidth_;
        private int gridHeight_;

        public static (int, int) TupleAddition((int, int) a, (int, int) b)
        {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        public Conway(int gridWidth, int gridHeight)
        {
            this.gridWidth_ = gridWidth;
            this.gridHeight_ = gridHeight;

            // zero-initialize the graphics grid
            bool[] sizeRef = new bool[gridHeight];
            for (int j = 0; j < gridWidth; j++)
            {
                List<bool> row = new List<bool>(sizeRef);
                newGrid_.Add(row);

                List<bool> row2 = new List<bool>(sizeRef);
                oldGrid_.Add(row2);
            }

            // define search coordinates
            coords_.Add((0, 1));
            coords_.Add((0, -1));
            coords_.Add((1, 0));
            coords_.Add((-1, 0));
            coords_.Add((1, 1));
            coords_.Add((-1, -1));
            coords_.Add((1, -1));
            coords_.Add((-1, 1));
        }

        public void randomInitialization(int sparseness)
        {
            // fill grid with random values
            Random rnd = new Random();
            for (int i = 0; i < gridWidth_; i++)
            {
                for (int j = 0; j < gridHeight_; j++)
                {
                    this.newGrid_[i][j] = this.oldGrid_[i][j] = Convert.ToBoolean(rnd.Next(sparseness));
                }
            }
        }
        public List<List<bool>> fullGrid()
        {
            return newGrid_;
        }

        bool readGrid((int, int) cc)
        {
            return oldGrid_[cc.Item1][cc.Item2];
        }

        /* write a pixel in the grid */
        void writeGrid((int, int) cc, bool val)
        {
            newGrid_[cc.Item1][cc.Item2] = val;
        }

        private (int, int) verifyCoordBounds((int, int) cc)
        {
            if (cc.Item1 < 0)
            {
                cc.Item1 += gridWidth_ - 1;
            }
            else if (cc.Item1 >= gridWidth_)
            {
                cc.Item1 %= gridWidth_;
            }
            if (cc.Item2 < 0)
            {
                cc.Item2 += gridHeight_ - 1;
            }
            else if (cc.Item2 >= gridHeight_)
            {
                cc.Item2 %= gridHeight_;
            }
            return cc;
        }

        public void Update()
        {

            // swap old and new grids
            var tempGrid = oldGrid_;
            oldGrid_ = newGrid_;
            newGrid_ = tempGrid;

            // update grid
            for (int i = 0; i < gridWidth_; i++)
            {
                for (int j = 0; j < gridHeight_; j++)
                {
                    (int, int) idx = (i, j);
                    short numLiveNeighbors = 0;
                    bool isAlive = readGrid(idx);

                    // calculate number of neighbors
                    foreach (var cc in coords_)
                    {
                        (int, int) nIdx = TupleAddition(idx, cc);
                        nIdx = verifyCoordBounds(nIdx);    // verify neighbor coordinates
                                                    // use mirroring of out of bounds
                        if (readGrid(nIdx) == true)
                        {
                            numLiveNeighbors++;
                        }
                    }

                    // Algorithm:
                    // Any live cell with two or three live neighbours survives.
                    if (isAlive && (numLiveNeighbors == 2 || numLiveNeighbors == 3))
                    {
                        writeGrid(idx, true);
                    }

                    // Any dead cell with three live neighbours becomes a live cell.
                    else if (!isAlive && numLiveNeighbors == 3)
                    {
                        writeGrid(idx, true);
                    }

                    // All other live cells die in the next generation.
                    // Similarly, all other dead cells stay dead.
                    else
                    {
                        writeGrid(idx, false);
                    }
                }
            }
}
    }
}
