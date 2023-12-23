using ConvexHullVisualizer.Domain;

namespace ConvexHullVisualizer.Tests
{
    public class ConvexHullTests
    {
        [Fact]
        public void NullPoints_ThrowException()
        {
            IEnumerable<Point>? points = null;
            var hull = new ConvexHull();

#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => { hull.QuickHull(points); });
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void LessThan3Points_ThrowException()
        {
            IEnumerable<Point> points = new[] { new Point(1, 1) };
            var hull = new ConvexHull();
            Assert.Throws<ArgumentException>(() => { hull.QuickHull(points); });
        }
    }
}