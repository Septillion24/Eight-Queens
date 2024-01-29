using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

class Board
{
    public struct Vector2Int
    {
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; set; }
        public int y { get; set; }
    }

    // columns, rows
    Random random = new Random();

    // Columns, rows
    bool[][] board = new bool[8][];
    int restarts = 0;

    public void restart()
    {
        restarts++;
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

    public void displayBoard()
    {
        for (int y = 0; y < board.Length; y++)
        {
            Console.Write("\n|");
            for (int x = 0; x < board[0].Length; x++)
            {
                if (board[x][y])
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

    public void displayMask(List<Vector2Int> positions)
    {
        // foreach (Vector2Int position in positions)
        // {
        //     Console.Write("(" + position.x + ", " + position.y + "),");
        // }
        for (int row = 0; row < board[0].Length; row++)
        {
            Console.Write("\n|");
            for (int column = 0; column < board.Length; column++)
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

    public void movePiece(int column, int row)
    {
        board[column] = new bool[8];
        board[column][row] = true;
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
            int conflicts = diagonalList.Count((bool x) => x) - 2; // Center position is counted twice
            count += conflicts;
        }

        return count;

        List<bool> getDiagonalList(int xInput, int yInput)
        {
            List<Vector2Int> diagonalBasis = new();
            for (int rowColumn = 0; rowColumn < board.Length; rowColumn++)
            {
                diagonalBasis.Add(new Vector2Int(rowColumn, rowColumn));
            }

            List<Vector2Int> returnListPositions = diagonalBasis.Select(v => new Vector2Int(v.x + xInput - yInput, v.y)).ToList();
            returnListPositions.AddRange(diagonalBasis.Select(v => new Vector2Int(v.x + xInput - yInput, yInput - v.y)).ToList());

            returnListPositions = returnListPositions.Where(isWithinBounds).ToList();

            Console.WriteLine("Mask for diagonals on position: " + xInput + ", " + yInput);
            displayMask(returnListPositions);

            List<bool> returnListValues = returnListPositions.Select(getValueAtTile).ToList(); // getValueAtTile(x) for every x in returnListPosition
            return returnListValues;
        }
    }

    bool isWithinBounds(Vector2Int vector)
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