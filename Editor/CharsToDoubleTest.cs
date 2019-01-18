// =============================================================================
// forked from samcragg/StringToDouble.cs
// =============================================================================

using System;
using NUnit.Framework;

namespace Plugins.VARP.StringTools.Tests
{
    using StringTools;
    
    [TestFixture]
    public sealed class CharsToDoubleTest
    {
        private static char[] Empty = new char [0];
        
        [Test]
        public void TestEmptyAndNullStrings()
        {
            int end = 1;
            Assert.That(() => CharsParser.Parse(null, 0, 0, out end),
                        Throws.TypeOf<ArgumentNullException>());
            Assert.That(end, Is.EqualTo(1)); // Make sure end wasn't touched

            //Assert.That(CharsToDouble.Parse(string.Empty, 0, out end), Is.EqualTo(default(double)));
            Assert.That(() => CharsParser.Parse(Empty, 0, 0, out end), 
                Throws.TypeOf<ArgumentOutOfRangeException>());
            //Assert.That(end, Is.EqualTo(0));
        }

        [Test]
        public void TestInvalidStartIndex()
        {
            int end = 1;
            Assert.That(() => CharsParser.Parse(Empty, 1, 0, out end),
                        Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(end, Is.EqualTo(1)); // Make sure end wasn't touched

            Assert.That(() => CharsParser.Parse(Empty, -1, 0, out end),
                        Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(end, Is.EqualTo(1)); // Make sure end wasn't touched
        }

        [Test]
        public void TestValidFormats()
        {
            TestCompleteParse("1", 1.0);
            TestCompleteParse(".2", 0.2);
            TestCompleteParse("1e1", 10.0);
            TestCompleteParse(".2e1", 2.0);
            TestCompleteParse("1.2e1", 12.0);
            TestCompleteParse("+1", 1.0);
            TestCompleteParse("+.2", 0.2);
            TestCompleteParse("+1e1", 10.0);
            TestCompleteParse("+.2e1", 2.0);
            TestCompleteParse("+1.2e1", 12.0);
            TestCompleteParse("+1e+1", 10.0);
            TestCompleteParse("+.2e+1", 2.0);
            TestCompleteParse("+1.2e+1", 12.0);
        }

        [Test]
        public void TestValidRange()
        {
            TestCompleteParse("+0", 0.0);
            TestCompleteParse("+4.9406564584124654E-324", double.Epsilon);
            TestCompleteParse("+1.7976931348623157E+308", double.MaxValue);
            TestCompleteParse("-0", -0.0);
            TestCompleteParse("-4.9406564584124654E-324", -double.Epsilon);
            TestCompleteParse("-1.7976931348623157E+308", double.MinValue);
        }

        [Test]
        public void TestInfinityAndNaN()
        {
            TestCompleteParse("+infinity", double.PositiveInfinity);
            TestCompleteParse("-INF", double.NegativeInfinity);
            TestCompleteParse("NAN", double.NaN);
        }

        [Test]
        public void TestOutsideOfRange()
        {
            TestCompleteParse("+1E-325", 0.0);
            TestCompleteParse("+1E+309", double.PositiveInfinity);
            TestCompleteParse("-1E-325", 0.0);
            TestCompleteParse("-1E+309", double.NegativeInfinity);
        }

        [Test]
        public void TestInvalidFormats()
        {
            // Put the spaces at the start to make sure it doesn't skip anything.
            var formats = new[]
            {
                " .",
                " ++0",
                " +"
            };

            foreach (var format in formats)
            {
                int position;
                var formatArray = format.ToCharArray();
                double value = CharsParser.Parse(formatArray, 0, formatArray.Length,out position);
                Assert.That(value, Is.EqualTo(default(double)));
                Assert.That(position, Is.EqualTo(0)); // Make sure it read nothing
            }
        }

        [Test]
        public void TestPartiallyValid()
        {
            var formats = new[]
            {
                " 1e",
                " 1e.0",
                "1.e",
                " 1e++0",
                " 1ee0"
            };

            foreach (var format in formats)
            {
                int position;
                var formatArray = format.ToCharArray();
                double value = CharsParser.Parse(formatArray, 0,formatArray.Length,out position);
                Assert.That(value, Is.EqualTo(1.0));
                Assert.That(position, Is.EqualTo(2)); // Make sure it read as much as it could
            }
        }

        [Test]
        public void TestStartIndex()
        {
            var formats = new[]
            {
                "###0.00###",
                "###+0.0###",
                "###-0.0###",
                "###+0e0###",
                "###-0e0###",
                "###0e-0###",
                "###.0e0.##",
            };

            foreach (var format in formats)
            {
                int position;
                var formatArray = format.ToCharArray();
                double value = CharsParser.Parse(formatArray, 3,formatArray.Length, out position);
                Assert.That(value, Is.EqualTo(0.0));
                Assert.That(position, Is.EqualTo(7)); // Make sure it read as much as it could
            }
        }

        private static void TestCompleteParse(string input, double expected)
        {
            int position = 0;
            double parsed = CharsParser.Parse(input.ToCharArray(), 0, input.Length, out position);
            Assert.That(parsed, Is.EqualTo(expected));
            Assert.That(position, Is.EqualTo(input.Length)); // Make sure it read everything
        }
    }
}