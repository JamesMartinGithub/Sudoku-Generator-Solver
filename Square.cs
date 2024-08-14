namespace Sudoku
{
    public class Square
    {
        public int[,] values;
        public (int, int) topLeft;
        public Square(int[,] values, (int, int) topLeft) {
            this.values = values;
            this.topLeft = topLeft;
        }
    }
}