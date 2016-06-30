using Ninject.Modules;
using Puzzle15.Implementations.Factories;
using Puzzle15.Interfaces.Factories;
using RectangularField.Implementations.Factories;
using RectangularField.Interfaces;

namespace Puzzle15.Tests.Modules
{
    public class GameBaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IFieldFactory<>)).To(typeof(ImmutableFieldFactory<>));
            Bind(typeof(IGameFactory<>)).To(typeof(GameFactory<>));
        }
    }
}
