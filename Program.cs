using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

class Program
{

    static void Main()
    {
        Board board = new();

        // board.movePiece(3,5);
        Console.WriteLine(board);
        Console.WriteLine(board.getConflicts());
    

    }
}