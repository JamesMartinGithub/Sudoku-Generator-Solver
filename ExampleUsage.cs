// Example usage of generator and solver classes
namespace Sudoku
{
    class Example
    {
        static void Main(string[] args) {
            DateTime time1 = DateTime.Now;
            Console.WriteLine("Generating sudoku board:");
            int[,] board = Generate();
            Console.WriteLine("\n\nSolving example sudoku board:");
            Solve(board);
            DateTime time2 = DateTime.Now;
            Console.WriteLine("Elapsed time: " + (time2 - time1));
        }

        static int[,] Generate() {
            SudokuGenerator generator = new SudokuGenerator();
            int[,] board = generator.Generate();
            Console.WriteLine(TwoDArrayToString(board));
            int zeroCount = 0;
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    if (board[y, x] == 0) {
                        zeroCount++;
                    }
                }
            }
            Console.WriteLine(zeroCount + " gaps");
            return board;
        }

        static void Solve(int[,] board) {
            SudokuSolver solver = new SudokuSolver();
            List<int[,]> solutions = solver.GetSolutions(board);
            if (solutions.Count == 0) {
                Console.WriteLine("Unsolveable");
            } else {
                Console.WriteLine("Solved: " + solutions.Count + " solution(s)");
                foreach (int[,] solution in solutions) {
                    Console.WriteLine(TwoDArrayToString(solution));
                }
            }
        }

        // Returns a string representation of a 2D array for displaying
        static string TwoDArrayToString(int[,] arr) {
            string s = "";
            for (int y = 0; y < arr.GetLength(0); y++) {
                for (int x = 0; x < arr.GetLength(1); x++) {
                    s += arr[y, x] + "  ";
                }
                s += "\n";
            }
            return s;
        }
    }
}