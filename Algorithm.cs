class Algorithm
{
    Board? currentState;
    List<Board> nextStates;
    int restarts;
    int stateChanges;
    public Algorithm()
    {
        reset();
        restarts = 0;
        stateChanges = 0;
    }

    public int getRestarts()
    {
        return restarts;
    }
    public int getStateChanges()
    {
        return stateChanges;
    }

    public void reset()
    {
        currentState = new();
        nextStates = new();
        restarts++;
        fillNextStates();
    }

    public void fillNextStates()
    {
        List<Vector2Int> possibleMoves = currentState.getAllPossibleMoves();
        foreach (Vector2Int move in possibleMoves)
        {
            Board state = currentState.getBoardAfterMovePiece(move);
            nextStates.Add(state);
        }
    }

    public Board? getBestStateFromNextStates(out int counter)
    {
        Board? currentBest = currentState;
        counter = 0;
        foreach (Board state in nextStates)
        {
            if (currentBest == null)
            {
                currentBest = state;
            }
            else if (state.getConflicts() < currentBest.getConflicts())
            {
                counter = 0;
                currentBest = state;
            }
            else if (state.getConflicts() == currentBest.getConflicts())
            {
                counter++;
            }
        }

        if (currentBest == currentState)
        {
            counter = 0;
            return null;
        }

        return currentBest;
    }

    public void displayCurrentState()
    {
        Console.WriteLine(currentState);
    }


    public bool doNextCalculationStep()
    {
        Console.WriteLine("Current h: " + currentState.getConflicts());
        Console.WriteLine(currentState.ToString());
        int counter = 0;
        Board? betterState = getBestStateFromNextStates(out counter);
        currentState = betterState;
        stateChanges++;
        Console.WriteLine("Neighbors found with better h: " + counter);

        if (currentState == null)
        {
            Console.WriteLine("Restart.");
            reset();
        }
        else
        {
            Console.WriteLine("\nGetting better state.");
            fillNextStates();
        }

        return checkWinningState();
    }

    public bool checkWinningState()
    {
        if (currentState.getConflicts() == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }





}