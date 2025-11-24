namespace Day3;

public static class Directions
{
  public static Point[] WithoutDiagonals { get; } =
  [
    (0, 1),
    (1, 0),
    (0, -1),
    (-1, 0),
  ];

  public static Point[] WithDiagonals { get; } =
  [
    (0, 1),
    (1, 0),
    (0, -1),
    (-1, 0),
    (1, 1),
    (-1, 1),
    (1, -1),
    (-1, -1),
  ];

  public record struct Point(int X, int Y)
  {
    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public Point Normalize() => new Point(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);
    public static implicit operator Point((int X, int Y) tuple) => new Point(tuple.X, tuple.Y);
    public int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
  }
}