using Ninject;
using Puzzle15.Implementations;
using Puzzle15.Implementations.ClassicGame;
using Puzzle15.Implementations.ClassicGame.Factories;
using Puzzle15.Implementations.Factories;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using Puzzle15.UI.ConsoleUI;
using RectangularField.Implementations.Factories;
using RectangularField.Interfaces.Factories;

namespace Puzzle15.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IGameFieldValidator<int>>().To<ClassicGameFieldValidator>();
            kernel.Bind<IShiftPerformerFactory<int>>().To<ClassicShiftPerformerFactory>();
            kernel.Bind<IGameFieldShuffler<int>>().To<ClassicGameFieldShuffler>();

            kernel.Bind(typeof(IGameFactory<>)).To(typeof(GameFactory<>));
            kernel.Bind(typeof(IRectangularFieldFactory<>)).To(typeof(WrappingRectangularFieldFactory<>));
            
            kernel.Get<ClassicGameConsoleUI>().Run();



            kernel.Bind<IGameFieldValidatorFactory<int>>().To<ClassicGameFieldValidatorFactory>();
            kernel.Bind<IShiftPerformerFactory<int>>().To<ClassicShiftPerformerFactory>();

            kernel.Bind(typeof(IGameFieldFactory<>)).To(typeof(GameFieldFactory<>));
            kernel.Bind(typeof(IGameFactory<>)).To(typeof(GameFactory<>));
        }
    }
}
