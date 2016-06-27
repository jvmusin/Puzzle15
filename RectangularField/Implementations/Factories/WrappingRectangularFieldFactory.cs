using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class WrappingRectangularFieldFactory<T> : IRectangularFieldFactory<T>
    {
        //TODO Make with Ninject
        private readonly IRectangularFieldFactory<T> wrappingFieldFactory = new ImmutableRectangularFieldFactory<T>();

//        public WrappingRectangularFieldFactory(IRectangularFieldFactory<T> wrappingFieldFactory)
//        {
//            if (wrappingFieldFactory == null)
//                throw new ArgumentNullException(nameof(wrappingFieldFactory));
//
//            this.wrappingFieldFactory = wrappingFieldFactory;
//        }

        public IRectangularField<T> Create(Size size)
        {
            return new WrappingRectangularField<T>(wrappingFieldFactory.Create(size));
        }
    }
}
