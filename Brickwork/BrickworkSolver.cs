namespace Brickwork
{
    public class BrickworkSolver
    {
        private readonly int[,] inputLayer;
        private readonly int[,] outputLayer;

        public BrickworkSolver(int[,] inputLayer)
        {
            this.inputLayer = inputLayer;

            var n = this.inputLayer.GetLength(0);
            var m = this.inputLayer.GetLength(1);

            this.outputLayer = new int[n, m];
        }

        public int[,] Solve()
        {
            TryAddBrick(1);

            return outputLayer;
        }

        private void TryAddBrick(int number)
        {
            var n = this.outputLayer.GetLength(0);
            var m = this.outputLayer.GetLength(1);

            int brickCount = n * m / 2;

            if (number > brickCount)
            {

                return;
                // check if we have a solution;
            }
            else
            {
                // extend the partial solution in every possible ways
                for (var i = 0; i < n; i++)
                {
                    for (var j = 0; j < m; j++)
                    {
                        // if can add horizontal brick
                        var canAddHorizontalBrick = j < m - 1 &&
                            this.outputLayer[i, j] == 0 &&
                            this.outputLayer[i, j + 1] == 0 &&
                            this.inputLayer[i, j] != this.inputLayer[i, j + 1];
                        var canAddVerticalBrick = i < n - 1 &&
                            this.outputLayer[i, j] == 0 &&
                            this.outputLayer[i + 1, j] == 0 &&
                            this.inputLayer[i, j] != this.inputLayer[i + 1, j];

                        // register solution
                        if (canAddHorizontalBrick)
                        {
                            this.outputLayer[i, j] = number;
                            this.outputLayer[i, j + 1] = number;
                        }
                        else if (canAddVerticalBrick)
                        {
                            this.outputLayer[i, j] = number;
                            this.outputLayer[i + 1, j] = number;
                        }

                        if (canAddHorizontalBrick || canAddVerticalBrick)
                        {
                            TryAddBrick(number + 1);
                            return;
                        }

                        // remove the registration
                        if (canAddHorizontalBrick)
                        {
                            this.outputLayer[i, j] = 0;
                            this.outputLayer[i, j + 1] = 0;
                        }
                        else if (canAddVerticalBrick)
                        {
                            this.outputLayer[i, j] = 0;
                            this.outputLayer[i + 1, j] = 0;
                        }
                    }
                }
            }
        }
    }
}
