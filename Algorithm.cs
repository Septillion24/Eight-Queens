class Algorithm
{
    Board? currentState;
    List<Board> nextStates;

    public Algorithm()
    {
        reset();
    }

    public void reset()
    {
        currentState = new();
        nextStates = new();
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

    public Board? getBestStateFromNextStates()
    {
        Board currentBest = currentState;
        foreach (Board state in nextStates)
        {
            if(state.getConflicts() < currentBest.getConflicts())
            {
                currentBest = state;
            }
        }

        if(currentBest == currentState)
        {
            return null;
        }

        return currentBest;
    }

    public void doNextCalculationStep()
    {
        currentState = getBestStateFromNextStates();
        if(currentState == null)
        {
            //TODO
            reset();
        }
        else
        {
            fillNextStates();
            currentState = ;
        }

    }





}