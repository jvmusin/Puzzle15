using Ninject.Modules;
using Puzzle15.Implementations.Factories;
using Puzzle15.Interfaces.Factories;
using RectangularField.Implementations.Factories;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.UI.Modules
{
    public class GameBaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRectangularFieldFactory<>)).To(typeof(WrappingRectangularFieldFactory<>));
            Bind(typeof(IGameFieldFactory<>)).To(typeof(GameFieldFactory<>));
            Bind(typeof(IGameFactory<>)).To(typeof(GameFactory<>));
        }
    }
}
