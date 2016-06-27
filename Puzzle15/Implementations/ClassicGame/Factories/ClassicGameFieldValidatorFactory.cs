using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;

namespace Puzzle15.Implementations.ClassicGame.Factories
{
    public class ClassicGameFieldValidatorFactory : IGameFieldValidatorFactory<int>
    {
        public IGameFieldValidator<int> Create()
        {
            return new ClassicGameFieldValidator();
        }
    }
}
