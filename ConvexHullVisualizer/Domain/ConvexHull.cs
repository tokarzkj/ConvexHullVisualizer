namespace ConvexHullVisualizer.Domain
{
    public class ConvexHull
    {
        public IEnumerable<Point> QuickHull(IEnumerable<Point> points)
        {
            if (points is null) throw new ArgumentNullException(nameof(points));
            if (points.Count() < 3) throw new ArgumentException($"{nameof(points)} requires at least 3 points");

            var sortedPoints = points.OrderBy(p => p.X);
            var leftMostPoint = points.First();
            var rightMostPoint = points.Last();

            var leftPoints = new List<Point>();
            var rightPoints = new List<Point>();

            foreach (var point in sortedPoints)
            {
                int polarAngle = GetPolarAngleUsingCrossProduct(leftMostPoint, point, rightMostPoint);
                if (point.Equals(leftMostPoint) || point.Equals(rightMostPoint)) continue;

                if (polarAngle > 0)
                {
                    leftPoints.Add(point);
                }
                else if (polarAngle < 0)
                {
                    rightPoints.Add(point);
                }
            }

            var leftHull = FindPointsOnHull(leftPoints, leftMostPoint, rightMostPoint);
            var rightHull = FindPointsOnHull(rightPoints, rightMostPoint, leftMostPoint);

            var hull = new List<Point>() { leftMostPoint };
            if (leftHull != null)
            {
                hull.AddRange(leftHull);
            }

            hull.Add(rightMostPoint);

            if (rightHull != null)
            {
                hull.AddRange(rightHull);
            }

            return hull;
        }

        private static int GetPolarAngleUsingCrossProduct(Point p1, Point p2, Point p3)
        {
            var p3p1 = new Point(p3.X - p1.X, p3.Y - p1.Y);
            var p2p1 = new Point(p2.X - p1.X, p2.Y - p1.Y);

            return (p3p1.X * p2p1.Y) - (p3p1.Y * p2p1.X);
        }

        private static IEnumerable<Point>? FindPointsOnHull(IEnumerable<Point> points, Point left, Point right)
        {
            if (points == null || !points.Any())
            {
                return null;
            }

            (int Distance, Point point) distanceAndPoint = (0, left);

            foreach (var point in points)
            {
                int distance = GetPointsDistanceFromLine(left, point, right);
                if (distance > distanceAndPoint.Distance)
                {
                    distanceAndPoint = (distance, point);
                }
            }

            var leftPoints = new List<Point>();
            var rightPoints = new List<Point>();
            var furthestPoint = distanceAndPoint.point;

            foreach (var point in points)
            {
                if (point == left || point == right ||point == furthestPoint)
                {
                    continue;
                }

                int leftSidePolarAngle = GetPolarAngleUsingCrossProduct(left, point, furthestPoint);
                int rightSidePolarAngle = GetPolarAngleUsingCrossProduct(right, point, furthestPoint);

                if (leftSidePolarAngle > 0)
                {
                    leftPoints.Add(point);
                }
                else if (rightSidePolarAngle < 0)
                {
                    rightPoints.Add(point);
                }
            }

            var leftHull = FindPointsOnHull(leftPoints, left, furthestPoint);
            var rightHull = FindPointsOnHull(rightPoints, furthestPoint, right);

            var hull = new List<Point>();
            if (leftHull != null)
            {
                hull.AddRange(leftHull);
            }

            hull.Add(furthestPoint);

            if (rightHull != null)
            {
                hull.AddRange(rightHull);
            }

            return hull;
        }

        private static int GetPointsDistanceFromLine(Point p1, Point p2, Point p3)
        {
            return Math.Abs((p1.Y - p2.Y) * (p3.X - p1.X) - (p3.Y - p1.Y) * (p1.X - p2.X));
        }
    }
}
