using Ninject;
using Puzzle15.UI.ConsoleUI;
using Puzzle15.UI.Modules;

namespace Puzzle15.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var kernel = new StandardKernel(new GameBaseModule(), new ClassicGameModule());
            kernel.Get<ClassicGameConsoleUI>().Run();
        }
    }
}
