namespace Day_06;

public class Coordinate
{
    public int X { get; init; }
    public int Y { get; init; }
    public bool Visited { get; set; }
    public bool IsWall { get; set; }
    public Coordinate Clone()
    {
        return new Coordinate
        {
            X = this.X,
            Y = this.Y,
            Visited = this.Visited,
            IsWall = this.IsWall
        };
    }
}