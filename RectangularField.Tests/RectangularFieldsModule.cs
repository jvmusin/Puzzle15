using Ninject.Modules;
using RectangularField.Implementations.Factories;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Tests
{
    public class RectangularFieldsModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRectangularFieldFactory<>)).To(typeof(MutableRectangularFieldFactory<>));
            Bind(typeof(IRectangularFieldFactory<>)).To(typeof(ImmutableRectangularFieldFactory<>));
            Bind(typeof(IRectangularFieldFactory<>)).To(typeof(WrappingRectangularFieldFactory<>));
        }
    }
}
