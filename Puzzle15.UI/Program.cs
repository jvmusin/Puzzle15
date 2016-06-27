using Ninject;
using Puzzle15.Implementations;
using Puzzle15.Implementations.ClassicGame;
using Puzzle15.Interfaces;
using Puzzle15.UI.ConsoleUI;
using RectangularField.Core;
using RectangularField.Factories;

namespace Puzzle15.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IFieldValidator<int>>().To<ClassicGameFieldValidator>();
            kernel.Bind<IShiftPerformerFactory<int>>().To<ClassicGameShiftPerformerFactory>();
            kernel.Bind<IFieldShuffler<int>>().To<ClassicGameFieldShuffler>();

            kernel.Bind(typeof(IGameFactory<>)).To(typeof(GameFactory<>));
            kernel.Bind(typeof(IRectangularFieldFactory<>)).To(typeof(WrappingRectangularFieldFactory<>));
            
            kernel.Get<ClassicGameConsoleUI>().Run();
        }
    }
}
