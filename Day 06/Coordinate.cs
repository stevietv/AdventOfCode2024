namespace Day_06;

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Visited { get; set; } = false;
    public bool IsWall { get; set; } = false;
}