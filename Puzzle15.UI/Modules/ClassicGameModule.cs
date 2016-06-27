using Ninject.Modules;
using Puzzle15.Implementations.ClassicGame.Factories;
using Puzzle15.Interfaces.Factories;

namespace Puzzle15.UI.Modules
{
    public class ClassicGameModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IGameFieldShufflerFactory<int>)).To(typeof(ClassicGameFieldShufflerFactory));
            Bind(typeof(IGameFieldValidatorFactory<int>)).To(typeof(ClassicGameFieldValidatorFactory));
            Bind(typeof(IShiftPerformerFactory<int>)).To(typeof(ClassicShiftPerformerFactory));
        }
    }
}
