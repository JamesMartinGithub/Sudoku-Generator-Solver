namespace Sudoku
{
    public class SudokuSolver
    {
        // Returns all solutions to given sukodu board
        public List<int[,]> GetSolutions(int[,] board) {
            List<int[,]> solutions = new List<int[,]>();
            return Step(solutions, board);
        }

        // Select most common uncompleted number, and try all possibilities in the first big square not containing it
        private List<int[,]> Step(List<int[,]> solutions, int[,] board) {
            // Check if board is filled
            BoardStates state = Utility.getBoardState(board);
            if (state == BoardStates.FILLEDINVALID) {
                return solutions;
            }
            if (state == BoardStates.FILLEDVALID) {
                solutions.Add(board);
                return solutions;
            }
            // Choose number and get possible locations
            Possiblities possibilities = Utility.GetPossibilities(board);
            // Try all possibilities
            foreach ((int, int) coord in possibilities.coords) {
                int[,] boardCopy = (int[,])board.Clone();
                boardCopy[coord.Item1, coord.Item2] = possibilities.num;
                solutions = Step(solutions, boardCopy);
            }
            return solutions;
        }
    }
}