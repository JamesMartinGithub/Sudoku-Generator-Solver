namespace Sudoku
{
    public class Possiblities
    {
        public List<(int, int)> coords;
        public int num;
        public Possiblities(List<(int, int)> coords, int num) {
            this.coords = coords;
            this.num = num;
        }
    }
}