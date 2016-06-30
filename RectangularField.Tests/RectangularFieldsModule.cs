using Ninject.Modules;
using RectangularField.Implementations.Factories;
using RectangularField.Interfaces;

namespace RectangularField.Tests
{
    public class RectangularFieldsModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IFieldFactory<>)).To(typeof(MutableFieldFactory<>));
            Bind(typeof(IFieldFactory<>)).To(typeof(ImmutableFieldFactory<>));
            Bind(typeof(IFieldFactory<>)).To(typeof(WrappingFieldFactory<>));
        }
    }
}
