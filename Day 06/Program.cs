namespace Day_06;

class Program
{

    private static int currentX;
    private static int currentY;
    private static Direction direction;
    private static List<Coordinate> map;

    private const string Filename = "map.txt";
    
    private static void Main()
    {
        map = ReadInput(Filename);
        currentX = map.Find(coord => coord.Visited).X;
        currentY = map.Find(coord => coord.Visited).Y;

        Console.WriteLine($"Starting location is: ({currentX},{currentY})");
        Console.WriteLine($"Amount if walls is: {map.Count(coord => coord.IsWall)}");
        Console.WriteLine($"Starting direction is: {direction}");

        ProcessMap();

        Console.WriteLine($"Amount of visited squares: {map.Count(coord => coord.Visited)}");
    }

    private static void ProcessMap()
    {
        Console.WriteLine("Processing Map movements");
        var nextCoord = map.Find(coord => coord.X == currentX && coord.Y == currentY);

        while (nextCoord != null)
        {
            var currentLocation = map.Find(coord => coord.X == currentX && coord.Y == currentY);
            if (currentLocation != null)
                currentLocation.Visited = true;
            
            switch (direction)
            {
                case Direction.North:
                    nextCoord = map.Find(coord => coord.X == currentX && coord.Y == currentY - 1);

                    if (nextCoord != null && nextCoord.IsWall)
                        direction = Direction.East;
                    
                    else if (nextCoord != null)
                        currentY--;

                    break;
                
                case Direction.West:
                    nextCoord = map.Find(coord => coord.X == currentX -1 && coord.Y == currentY);
                    
                    if (nextCoord != null && nextCoord.IsWall)
                        direction = Direction.North;
                    
                    else if (nextCoord != null)
                        currentX--;

                    break;
                
                case Direction.South:
                    nextCoord = map.Find(coord => coord.X == currentX && coord.Y == currentY + 1);
                    
                    if (nextCoord != null && nextCoord.IsWall)
                        direction = Direction.West;
                    
                    else if (nextCoord != null)
                        currentY++;

                    break;            
                
                case Direction.East:
                    nextCoord = map.Find(coord => coord.X == currentX + 1 && coord.Y == currentY);
                    
                    if (nextCoord != null && nextCoord.IsWall)
                        direction = Direction.South;
                    
                    else if (nextCoord != null)
                        currentX++;

                    break;
            }
        }
        Console.WriteLine("Exited Map");
    }

    private static List<Coordinate> ReadInput(string input)
    {
        var inputMap = new List<Coordinate>();

        using var reader = new StreamReader(input);
        
        var y = 1;
        while (reader.ReadLine() is { } line)
        {
            var x = 1;
            foreach (var character in line)
            {
                inputMap.Add(new Coordinate
                {
                    X = x,
                    Y = y,
                    IsWall = character == '#',
                    Visited = character == '^' || character == '>' || character == '<' || character == 'v'
                });
                
                x++;
            }

            if (line.Contains('^'))
                direction = Direction.North;
            
            if (line.Contains('>'))
                direction = Direction.East;
            
            if (line.Contains('<'))
                direction = Direction.West;
            
            if (line.Contains('v'))
                direction = Direction.South;
            
            y++;
        }

        return inputMap;
    }
}