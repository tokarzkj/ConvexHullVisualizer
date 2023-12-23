using ConvexHullVisualizer.Domain;

namespace ConvexHullVisualizer.Tests
{
    public class ConvexHullTests
    {
        private ConvexHull _convexHull;

        public ConvexHullTests()
        {
            _convexHull = new ConvexHull();
        }

        [Fact]
        public void NullPoints_ThrowException()
        {
            IEnumerable<Point>? points = null;

#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => { _convexHull.QuickHull(points); });
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void LessThan3Points_ThrowException()
        {
            IEnumerable<Point> points = new[] { new Point(1, 1) };
            Assert.Throws<ArgumentException>(() => { _convexHull.QuickHull(points); });
        }

        [Fact]
        public void AllPointsAreHullPoints_QuickHullReturns3Points()
        {
            var points = new[] { new Point(1, 2), new Point(3, 6), new Point(4, 1) };
            var hull = _convexHull.QuickHull(points);

            Assert.Equal(points, hull);
        }

        [Fact]
        public void SomePointsAreHullPoints_QuickHullReturns3Points()
        {
            var points = new[] { new Point(1, 2), new Point(3, 3), new Point(5, 6), new Point(6, 1) };
            var hull = _convexHull.QuickHull(points);

            var answer = new[] { points[0], points[2], points[3] };
            Assert.Equal(answer, hull);
        }

        [Fact]
        public void CollinearPointsWithFurthestPointTestedFirst_QuickHullReturns3Points()
        {
            var points = new[] { new Point(1, 2), new Point(3, 6), new Point(2, 4), new Point(4, 1) };
            var hull = _convexHull.QuickHull(points);

            var answer = new[] { points[0], points[1], points[3] };
            Assert.Equal(answer, hull);
        }

        [Fact]
        public void CollinearPointsWithCloserPointTestedFirst_QuickHullReturns3Points()
        {
            var points = new[] { new Point(1, 2), new Point(2, 4), new Point(3, 6), new Point(4, 1) };
            var hull = _convexHull.QuickHull(points);

            var answer = new[] { points[0], points[2], points[3] };
            Assert.Equal(answer, hull);
        }

        [Fact]
        public void PositiveAndNegativePoints_QuickHullReturns3Points()
        {
            var points = new[] { new Point(1, 2), new Point(2, -4), new Point(-3, -6) };
            var hull = _convexHull.QuickHull(points);
            var answer = new[] { points[2], points[0], points[1] };
            Assert.Equal(answer, hull);
        }
    }
}