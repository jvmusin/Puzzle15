﻿namespace Puzzle15
{
    public interface IGame
    {
        IGame Shift(int value);
        
        int this[CellLocation location] { get; }
    }
}