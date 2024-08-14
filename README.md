# Sudoku-Generator-Solver
A set of classes that can generate random valid sudoku boards, and find all solutions to boards

### Usage:
`SudokuGenerator.Generate()` returns a 9x9 2D int array containing a random, 1-solution sudoku puzzle.

`SudokuSolver.GetSolutions(int[,])` takes a 9x9 2D int array representing the sudoku board, and returns a `List<int[,]>` of complete solutions.

`ExampleUsage.cs` contains example use of both methods.

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
