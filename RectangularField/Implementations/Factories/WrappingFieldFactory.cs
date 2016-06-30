using System.Drawing;
using RectangularField.Interfaces;
using RectangularField.Interfaces.Factories;

namespace RectangularField.Implementations.Factories
{
    public class WrappingFieldFactory<T> : IFieldFactory<T>
    {
        //TODO Make with Ninject
        private readonly IFieldFactory<T> wrappingFieldFactory = new ImmutableFieldFactory<T>();

//        public WrappingFieldFactory(IFieldFactory<T> wrappingFieldFactory)
//        {
//            if (wrappingFieldFactory == null)
//                throw new ArgumentNullException(nameof(wrappingFieldFactory));
//
//            this.wrappingFieldFactory = wrappingFieldFactory;
//        }

        public IField<T> Create(Size size)
        {
            return new WrappingField<T>(wrappingFieldFactory.Create(size));
        }
    }
}
