using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Ninject;
using NUnit.Framework;
using Puzzle15.Implementations.ClassicGame;
using Puzzle15.Interfaces;
using Puzzle15.Interfaces.Factories;
using Puzzle15.Tests.Modules;
using RectangularField.Interfaces;
using RectangularField.Utils;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ClassicGameFieldValidator_Should
    {
        //TODO Two kernels or one static?
        private static readonly IKernel Kernel = new StandardKernel(new GameBaseModule(), new ClassicGameModule());

        private IFieldFactory<int> gameFieldFactory;
        private ClassicGameFieldValidator gameFieldValidator;

        [SetUp]
        public void SetUp()
        {
            gameFieldFactory = Kernel.Get<IFieldFactory<int>>();
            gameFieldValidator = new ClassicGameFieldValidator();
        }

        [Test]
        public void NotFail_WhenFieldIsCorrect()
        {
            var size = new Size(2, 2);
            var elements = new[] { 0, 3, 1, 2 };
            var field = gameFieldFactory.Create(size, elements);

            var validationResult = gameFieldValidator.Validate(field, field);

            validationResult.Successful.Should().BeTrue();
            validationResult.Cause.Should().BeNull();
        }

        [Test]
        public void Fail_WhenSomeFieldIsNull()
        {
            var values = Enumerable.Range(0, 9).ToArray();
            var field = gameFieldFactory.Create(new Size(3, 3), values);

            gameFieldValidator.Validate(field, null).Successful.Should().BeFalse();
            gameFieldValidator.Validate(null, field).Successful.Should().BeFalse();
            gameFieldValidator.Validate(null, null).Successful.Should().BeFalse();
        }

        [Test]
        public void Fail_WhenInitialFieldAndTargetHasDiferentSizes()
        {
            var values = Enumerable.Range(0, 9).ToArray();
            var initialField = gameFieldFactory.Create(new Size(3, 3), values);
            var target = gameFieldFactory.Create(new Size(2, 3), values.Take(6).ToArray());

            gameFieldValidator.Validate(initialField, target).Successful.Should().BeFalse();
        }

        [Test, TestCaseSource(nameof(FailCreatingCases))]
        public void Fail_WhenFieldIsIncorrect(IField<int> field)
        {
            var validationResult = gameFieldValidator.Validate(field, field);

            validationResult.Successful.Should().BeFalse();
            validationResult.Cause.Should().NotBeNullOrEmpty();
        }

        private static IEnumerable<TestCaseData> FailCreatingCases
        {
            get
            {
                var fact = Kernel.Get<IFieldFactory<int>>();
                yield return new TestCaseData(fact.Create(new Size(2, 2), 1, 2, 1, 0)).SetName("Twice appeared value");
                yield return new TestCaseData(fact.Create(new Size(2, 2), -1, 0, 1, 2)).SetName("Negative number");
                yield return new TestCaseData(fact.Create(new Size(2, 2), 1, 2, 3, 4)).SetName("Without empty cell");
                yield return new TestCaseData(fact.Create(new Size(2, 2), 0, 1, 2, 4)).SetName("Skipped value");

                var empty = A.Fake<IField<int>>(x => x.Strict());
                A.CallTo(() => empty.Size).Returns(new Size(5, 0));
                yield return new TestCaseData(empty).SetName("Empty field");

                var negative = A.Fake<IField<int>>(x => x.Strict());
                A.CallTo(() => negative.Size).Returns(new Size(-5, 5));
                yield return new TestCaseData(negative).SetName("Negative-size field");
            }
        }
    }
}
