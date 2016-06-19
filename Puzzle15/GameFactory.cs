namespace Puzzle15
{
    public class GameFactory : IGameFactory
    {
        public IGameFieldValidator GameFieldValidator { get; }
        public IShiftPerformer ShiftPerformer { get; }

        public GameFactory(IGameFieldValidator gameFieldValidator, IShiftPerformer shiftPerformer)
        {
            GameFieldValidator = gameFieldValidator;
            ShiftPerformer = shiftPerformer;
        }

        public IGame Create(RectangularField<int> initialField)
        {
            var ex = GameFieldValidator.Validate(initialField);
            if (ex != null) throw ex;

            return new Game(initialField, ShiftPerformer);
        }
    }
}
