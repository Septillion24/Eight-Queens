using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

class Program
{

    static void Main()
    {
        Board board = new();

        board.restart();
        board.displayBoard();
        Console.WriteLine(board.getConflicts());


    }
}