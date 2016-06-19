namespace Puzzle15
{
    public class ImmutableGame : Game
    {
        public ImmutableGame(RectangularField<int> field) : base(field)
        {
        }

        protected ImmutableGame(RectangularField<int> field, bool needCheck = true, bool needClone = true)
            : base(field, needCheck, needClone)
        {
        }

        public override GameBase Shift(int value)
            => new ImmutableGame(Field.Clone(), false, false).Shift0(value);

        private GameBase Shift0(int value) => base.Shift(value);
    }
}
