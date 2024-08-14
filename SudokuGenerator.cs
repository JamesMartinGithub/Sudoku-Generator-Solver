namespace Sudoku
{
    public class SudokuGenerator
    {
        // Generates valid, 1-solution sukodu board
        public int[,] Generate() {
            // Generate full board
            int[,] fullBoard = GenerateFullBoard();
            // Remove numbers until hardest, valid board found
            List<(int, int)> coords = new List<(int, int)> ();
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    coords.Add((y, x));
                }
            }
            return StepReduce(fullBoard, coords);
        }

        // Try most common, non-complete number in random spot
        private int[,] StepReduce(int[,] board, List<(int, int)> coords) {
            // Choose random coordinate from list
            Random rand = new Random();
            int chosenIndex = rand.Next(coords.Count);
            (int, int) chosenCoord = coords[chosenIndex];
            coords.RemoveAt(chosenIndex);
            // Try removing coordinate
            int[,] boardCopy = (int[,])board.Clone();
            boardCopy[chosenCoord.Item1, chosenCoord.Item2] = 0;
            // Check if board is now invalid
            SudokuSolver solver = new SudokuSolver();
            if (solver.GetSolutions(boardCopy).Count != 1) {
                // Return previous board as new board is invalid
                return board;
            } else {
                // Recurse as board still valid
                return StepReduce(boardCopy, coords);
            }
        }

        // Generates full, valid sudoku board
        private int[,] GenerateFullBoard() {
            (BoardStates state, int[,] board) = StepFill(new int[9,9]);
            return board;
        }

        // Try most common, non-complete number in random spot
        private (BoardStates, int[,]) StepFill(int[,] board) {
            // Return if board is full and valid
            BoardStates state = Utility.getBoardState(board);
            if (state == BoardStates.FILLEDVALID || state == BoardStates.FILLEDINVALID) {
                return (state, board);
            }
            // Choose number and get possible locations
            Possiblities possibilities = Utility.GetPossibilities(board, lowest: true);
            // Try all possibilities in random order
            int count = possibilities.coords.Count;
            for (int i = 0; i < count; i++) {
                // Select random possible coordinate
                Random rand = new Random();
                int chosenIndex = rand.Next(possibilities.coords.Count);
                (int, int) coord = possibilities.coords[chosenIndex];
                possibilities.coords.RemoveAt(chosenIndex);
                // Try with selected value at coordinate
                int[,] boardCopy = (int[,])board.Clone();
                boardCopy[coord.Item1, coord.Item2] = possibilities.num;
                (BoardStates tryState, int[,] tryBoard) = StepFill(boardCopy);
                if (tryState == BoardStates.FILLEDVALID) {
                    return (tryState, tryBoard);
                }
            }
            return (state, board);
        }
    }
}