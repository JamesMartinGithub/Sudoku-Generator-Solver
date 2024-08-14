# Sudoku-Generator-Solver
A set of classes that can generate random valid sudoku boards, and find all solutions to boards

### Usage:
`SudokuGenerator.Generate()` returns a 9x9 2D int array containing a random, 1-solution sudoku puzzle.

`SudokuSolver.GetSolutions(int[,])` takes a 9x9 2D int array representing the sudoku board, and returns a `List<int[,]>` of complete solutions.

`ExampleUsage.cs` contains example use of both methods.

#### `ExampleUsage.cs` Example Output:
```
Generating sudoku board:
0  0  2  0  5  1  4  0  7
0  8  0  4  0  0  0  0  2
3  4  5  7  0  0  9  1  6
6  0  4  2  8  5  7  3  1
0  0  0  1  4  7  6  0  5
0  5  1  0  0  6  2  4  8
0  0  9  6  1  0  8  2  4
4  1  6  0  0  2  5  0  3
2  0  0  0  7  4  1  6  9

29 gaps


Solving example sudoku board:
Solved: 1 solution(s)
9  6  2  3  5  1  4  8  7
1  8  7  4  6  9  3  5  2
3  4  5  7  2  8  9  1  6
6  9  4  2  8  5  7  3  1
8  2  3  1  4  7  6  9  5
7  5  1  9  3  6  2  4  8
5  7  9  6  1  3  8  2  4
4  1  6  8  9  2  5  7  3
2  3  8  5  7  4  1  6  9
```

### Implementation:
#### Solver:
- Starting with the most-common, incomplete number and the first available big square, all possibilities are tested
- The program recurses, backtracking when no possibilities are found, to fill the board
- Complete solutions are added to the final list, which is returned upon completion
#### Generator:
- A full valid board of random numbers is generated using a similar backtracking algorithm
- Numbers are removed from the board until it has more than 1 valid solution
- The final board is returned having exactly 1 solution

### Credit:
The solver was initially written in python jointly with @Ben-elliot27. I then re-wrote it in c# for speed, modified it to find all solutions, and created the generator class.
