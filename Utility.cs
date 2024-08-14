namespace Sudoku
{
    public static class Utility
    {
        // Returns the state of the given board; unfilled, filled but invalid, filled and valid
        public static BoardStates getBoardState(int[,] board) {
            bool filled = true;
            bool valid = true;
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    if (board[y, x] == 0) {
                        filled = false;
                    } else {
                        if (Utility.numberInCross(board, board[y, x], y, x)) {
                            valid = false;
                        }
                    }
                }
            }
            if (!filled) {
                if (valid) {
                    return BoardStates.NOTFILLEDVALID;
                } else {
                    return BoardStates.NOTFILLEDINVALID;
                }
            } else {
                if (valid) {
                    return BoardStates.FILLEDVALID;
                } else {
                    return BoardStates.FILLEDINVALID;
                }
            }
        }

        public static Possiblities GetPossibilities(int[,] board, bool lowest = false) {
            // Returns all possible positions for a given number in a given big square
            Possiblities getPossibilitiesForValue(int[,] board, int num, Square square) {
                List<(int, int)> coords = new List<(int, int)>();
                for (int y = 0; y < 3; y++) {
                    for (int x = 0; x < 3; x++) {
                        if ((square.values[y, x] == 0) && !(numberInCross(board, num, (square.topLeft.Item1 + y), (square.topLeft.Item2 + x)))) {
                            coords.Add((square.topLeft.Item1 + y, square.topLeft.Item2 + x));
                        }
                    }
                }
                return new Possiblities(coords, num);
            }
            // Get first big square not containing chosen value
            int selectedNum;
            if (lowest) {
                selectedNum = getLowestIncompleteNum(board);
            } else {
                selectedNum = Utility.getMostFrequentNum(board);
            }
            Square square = Utility.getFirstBigSquareExcludingNumber(board, selectedNum);
            // Get possible coordinates for chosen value
            return getPossibilitiesForValue(board, selectedNum, square);
        }

        // Returns true if the given number appears on the same row or column of the given position
        private static bool numberInCross(int[,] board, int num, int y, int x) {
            for (int xs = 0; xs < 9; xs++) {
                if (board[y, xs] == num && xs != x) {
                    return true;
                }
            }
            for (int ys = 0; ys < 9; ys++) {
                if (board[ys, x] == num && ys != y) {
                    return true;
                }
            }
            return false;
        }

        // Returns count of each non-zero number
        private static int[] GetCounts(int[,] board) {
            int[] counts = new int[9];
            for (int y = 0; y < 9; y++) {
                for (int x = 0; x < 9; x++) {
                    if (board[y, x] != 0) {
                        counts[board[y, x] - 1] += 1;
                    }
                }
            }
            return counts;
        }

        // Returns lowest, non-zero, non-complete number
        private static int getLowestIncompleteNum(int[,] board) {
            // Count value appearances
            int[] counts = GetCounts(board);
            // Get numbers with count<9
            List<int> possibleNums = new List<int>();
            for (int i = 0; i < 9; i++) {
                if (counts[i] < 9) {
                    possibleNums.Add(i + 1);
                }
            }
            // Choose lowest possible number
            return possibleNums[0];
        }

        // Returns most frequent, non-zero, non-complete number in board
        private static int getMostFrequentNum(int[,] board) {
            // Count value appearances
            int[] counts = GetCounts(board);
            // Find most frequent number, with appearances < 9
            (int, int) highestCountVal = (-1, 0);
            for (int i = 0; i < 9; i++) {
                if (counts[i] > highestCountVal.Item1 && counts[i] < 9) {
                    highestCountVal = (counts[i], i + 1);
                }
            }
            if (highestCountVal.Item2 == 0) {
                // All numbers fill board
                Console.WriteLine("Error: no most frequent number");
                return -1;
            } else {
                // Non-complete, Non-zero number found
                return highestCountVal.Item2;
            }
        }

        // Returns first the big square that doesn't contain the given number
        private static Square getFirstBigSquareExcludingNumber(int[,] board, int num) {
            for (int squareY = 0; squareY < 9; squareY += 3) {
                for (int squareX = 0; squareX < 9; squareX += 3) {
                    if (!numberInBigSquare(num, getSquare(board, squareY, squareX))) {
                        return getSquare(board, squareY, squareX);
                    }
                }
            }
            Console.WriteLine("No big square found excluding number: " + num.ToString());
            return null;
        }

        // Returns true if big square contains the number
        private static bool numberInBigSquare(int num, Square square) {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    if (square.values[y, x] == num) {
                        return true;
                    }
                }
            }
            return false;
        }

        // Returns big square that contains value at board[y,x]
        private static Square getSquare(int[,] board, int y, int x) {
            // Big square y,x indexes (0-2)
            int y3 = (y / 3) * 3;
            int x3 = (x / 3) * 3;
            int[,] bigSquare = new int[3, 3];
            for (int yi = 0; yi < 3; yi++) {
                for (int xi = 0; xi < 3; xi++) {
                    bigSquare[yi, xi] = board[y3 + yi, x3 + xi];
                }
            }
            return new Square(bigSquare, (y3, x3));
        }
    }
}