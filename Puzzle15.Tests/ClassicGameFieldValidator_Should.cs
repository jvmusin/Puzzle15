using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using Puzzle15.Implementations;
using RectangularField.Core;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class ClassicGameFieldValidator_Should : TestBase
    {
        private ClassicGameFieldValidator validator;

        [SetUp]
        public void SetUp()
        {
            validator = new ClassicGameFieldValidator();
        }

        [Test]
        public void NotFail_WhenFieldIsCorrect()
        {
            var size = new Size(2, 2);
            var elements = new[] { 0, 3, 1, 2 };
            var field = CreateMutableField(size, elements);

            var validationResult = validator.Validate(field, field);

            validationResult.Successful.Should().BeTrue();
            validationResult.Cause.Should().BeNull();
        }

        [Test]
        public void Fail_WhenSomeFieldIsNull()
        {
            var values = Enumerable.Range(0, 9).ToArray();
            var field = CreateMutableField(new Size(3, 3), values);

            validator.Validate(field, null).Successful.Should().BeFalse();
            validator.Validate(null, field).Successful.Should().BeFalse();
            validator.Validate(null, null).Successful.Should().BeFalse();
        }

        [Test]
        public void Fail_WhenInitialFieldAndTargetHasDiferentSizes()
        {
            var values = Enumerable.Range(0, 9).ToArray();
            var initialField = CreateMutableField(new Size(3, 3), values);
            var target = CreateMutableField(new Size(2, 3), values.Take(6).ToArray());

            validator.Validate(initialField, target).Successful.Should().BeFalse();
        }

        [Test, TestCaseSource(nameof(FailCreatingCases))]
        public void Fail_WhenFieldIsIncorrect(IRectangularField<int> field)
        {
            var validationResult = validator.Validate(field, field);

            validationResult.Successful.Should().BeFalse();
            validationResult.Cause.Should().NotBeNullOrEmpty();
        }

        private static IEnumerable<TestCaseData> FailCreatingCases
        {
            get
            {
                yield return new TestCaseData(CreateMutableField(new Size(2, 2), 1, 2, 1, 0)).SetName("Twice appeared value");
                yield return new TestCaseData(CreateMutableField(new Size(2, 2), -1, 0, 1, 2)).SetName("Negative number");
                yield return new TestCaseData(CreateMutableField(new Size(2, 2), 1, 2, 3, 4)).SetName("Without empty cell");
                yield return new TestCaseData(CreateMutableField(new Size(2, 2), 0, 1, 2, 4)).SetName("Skipped value");

                var empty = StrictFake<IRectangularField<int>>();
                A.CallTo(() => empty.Size).Returns(new Size(5, 0));
                yield return new TestCaseData(empty).SetName("Empty field");

                var negative = StrictFake<IRectangularField<int>>();
                A.CallTo(() => negative.Size).Returns(new Size(-5, 5));
                yield return new TestCaseData(negative).SetName("Negative-size field");
            }
        }
    }
}
