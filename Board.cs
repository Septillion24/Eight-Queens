// TODO: Make immutable

class Board
{
    

    Random random = new Random();

    // Columns, rows
    bool[][] board = new bool[8][];

    public override string ToString()
    {
        string stringValue = "";
        for (int y = 0; y < board.Length; y++)
        {
            stringValue += "\n|";
            for (int x = 0; x < board[0].Length; x++)
            {
                if (board[x][y])
                {
                    stringValue += "x";
                }
                else
                {
                    stringValue += " ";
                }
                stringValue += "|";
            }
        }
        stringValue += "\n";
        return stringValue;
    }

    public Board()
    {
        initialize();
    }
    public Board(Board other)
    {
        for (int x = 0; x < 8; x++)
        {
            this.board[x] = new bool[8];
            for (int y = 0; y < 8; y++)
            {
                this.board[x][y] = other.board[x][y];
            }
        }
    }

    private void initialize()
    {
        for (int x = 0; x < 8; x++)
        {
            board[x] = new bool[8];
            for (int y = 0; y < 8; y++)
            {
                board[x][y] = false;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            int randomInt = random.Next(8);
            board[i][randomInt] = true;
        }
    }


    // public void restartEmpty()
    // {
    //     // restarts++;
    //     for (int x = 0; x < 8; x++)
    //     {
    //         board[x] = new bool[8];
    //         for (int y = 0; y < 8; y++)
    //         {
    //             board[x][y] = false;
    //         }
    //     }
    // }

    public static void displayMask(List<Vector2Int> positions, int size)
    {
        // foreach (Vector2Int position in positions)
        // {
        //     Console.Write("(" + position.x + ", " + position.y + "),");
        // }
        for (int row = 0; row < size; row++)
        {
            Console.Write("\n|");
            for (int column = 0; column < size; column++)
            {
                if (positions.Contains(new Vector2Int(column, row)))
                {
                    Console.Write("x");
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write("|");
            }
        }
        Console.WriteLine("");
    }

    public int getRowFromColumn(int column)
    {
        for (int row = 0; row < 8; row++)
        {
            if (board[column][row] == true)
            {
                return row;
            }
        }
        return -1;
    }

    public bool getValueAtTile(int x, int y)
    {
        return board[x][y];
    }

    public List<Vector2Int> getAllPossibleMoves()
    {
        List<Vector2Int> allMoves = new();

        foreach(Vector2Int queen in getAllQueenCoordinates())
        {
            for (int row = 0; row < board.Length; row++)
            {
                allMoves.Add(new Vector2Int(queen.x, row));
            }
        }

        return allMoves;
    }

    public bool getValueAtTile(Vector2Int vector)
    {
        return board[vector.x][vector.y];
    }

    public List<Vector2Int> getAllQueenCoordinates()
    {
        List<Vector2Int> listOfQueens = new();
        for (int column = 0; column < board.Length; column++)
        {
            listOfQueens.Add(new Vector2Int(column, getRowFromColumn(column)));
        }
        return listOfQueens;
    }

    public Board getBoardAfterMovePiece(int column, int row)
    {
        Board returnBoard = new(this);
        returnBoard.board[column] = new bool[8];
        returnBoard.board[column][row] = true;
        return returnBoard;
    }
    public Board getBoardAfterMovePiece(Vector2Int input)
    {
        return getBoardAfterMovePiece(input.x, input.y);
    }

    public int getConflicts()
    {
        int count = 0;

        count += getRowConflicts();
        count += getDiagonalConflicts();

        return count;
    }

    private int getDiagonalConflicts()
    {
        int count = 0;

        foreach (Vector2Int queenLocation in getAllQueenCoordinates())
        {
            // Console.WriteLine(queenLocation.x);
            List<bool> diagonalList = getDiagonalList(queenLocation.x, queenLocation.y);
            int conflicts = diagonalList.Count((bool x) => x);
            count += conflicts;
        }
        // Console.WriteLine("Diagonal conflicts: " + count);
        return count;

        List<bool> getDiagonalList(int xInput, int yInput)
        {
            List<Vector2Int> diagonals = new List<Vector2Int>();
            int boardSize = board.Length;

            // Descending
            int sum = xInput + yInput;
            for (int column = 0; column < boardSize; column++)
            {
                for (int row = 0; row < boardSize; row++)
                {
                    if (column + row == sum && (column != xInput || row != yInput))
                    {
                        diagonals.Add(new Vector2Int(column, row));
                    }
                }
            }

            // Ascending
            int diff = xInput - yInput;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (i - j == diff && (i != xInput || j != yInput))
                    {
                        diagonals.Add(new Vector2Int(i, j));
                    }
                }
            }
            // displayMask(diagonals);
            List<bool> diagonalsValues = diagonals.Select(getValueAtTile).ToList();
            return diagonalsValues;
        }
    }

    public bool isWithinBounds(Vector2Int vector)
    {
        return vector.x < board.Length && vector.y < board.Length && vector.x >= 0 && vector.y >= 0;
    }

    private int getRowConflicts()
    {
        // This method is scalable.

        int count = 0;
        List<int> usedRows = new List<int>();

        for (int column = 0; column < board.Length; column++)
        {
            int occupiedRow = getRowFromColumn(column);
            if (usedRows.Contains(occupiedRow))
            {
                count++;
            }
            else
            {
                usedRows.Add(occupiedRow);
            }
        }
        return count;
    }

}