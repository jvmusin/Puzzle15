using Ninject.Modules;
using Puzzle15.Implementations.ClassicGame.Factories;
using Puzzle15.Interfaces.Factories;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.Tests.Modules
{
    public class ClassicGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFieldShufflerFactory<int>>().To<ClassicGameFieldShufflerFactory>();
            Bind<IGameFieldValidatorFactory<int>>().To<ClassicGameFieldValidatorFactory>();
            Bind<IShiftPerformerFactory<int>>().To<ClassicGameShiftPerformerFactory>();
        }
    }
}
