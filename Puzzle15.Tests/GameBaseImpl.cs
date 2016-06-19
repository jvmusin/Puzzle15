using System;

namespace Puzzle15.Tests
{
    public class GameBaseImpl : GameBase
    {
        public GameBaseImpl(RectangularField<int> field, bool needCheck = true, bool needClone = true)
            : base(field, needCheck, needClone)
        {
        }

        public override GameBase Shift(int value)
        {
            throw new NotImplementedException();
        }
    }
}
