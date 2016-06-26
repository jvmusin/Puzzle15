using Ninject;
using Puzzle15.Implementations;
using Puzzle15.Implementations.ClassicGame;
using Puzzle15.Interfaces;
using RectangularField.Factories;

namespace Puzzle15ConsoleUI
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
        }
    }
}
