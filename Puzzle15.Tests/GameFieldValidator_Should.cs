using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace Puzzle15.Tests
{
    [TestFixture]
    public class GameFieldValidator_Should : TestBase
    {
        private GameFieldValidator gameFieldValidator;

        [SetUp]
        public void SetUp()
        {
            gameFieldValidator = new GameFieldValidator();
        }

        [Test]
        public void CreateFieldCorrectly()
        {
            var size = new Size(2, 2);
            var elements = new[]{0, 3, 1, 2};
            var field = FieldFromArray(size, elements);

            var validationResult = gameFieldValidator.Validate(field);

            validationResult.Successful.Should().BeTrue();
            validationResult.Cause.Should().BeNull();
        }

        [Test, TestCaseSource(nameof(FailCreatingCases))]
        public void FailCreating_WhenFieldIsIncorrect(RectangularField<int> field)
        {
            var validationResult = gameFieldValidator.Validate(field);

            validationResult.Successful.Should().BeFalse();
            validationResult.Cause.Should().NotBeNullOrEmpty();
        }

        private static IEnumerable<TestCaseData> FailCreatingCases
        {
            get
            {
                yield return new TestCaseData(null).SetName("Null field");
                yield return new TestCaseData(FieldFromArray<int>(new Size(5, 0))).SetName("Empty Field");
                yield return new TestCaseData(FieldFromArray(new Size(2, 2), 1, 2, 1, 0)).SetName("Twice appeared value");
                yield return new TestCaseData(FieldFromArray(new Size(2, 2), -1, 0, 1, 2)).SetName("Negative number");
                yield return new TestCaseData(FieldFromArray(new Size(2, 2), 1, 2, 3, 4)).SetName("Without empty cell");
                yield return new TestCaseData(FieldFromArray(new Size(2, 2), 0, 1, 2, 4)).SetName("Skipped value");
            }
        }
    }
}
