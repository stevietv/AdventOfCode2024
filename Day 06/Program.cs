namespace Day_06;

internal static class Program
{

    private static int _startingX;
    private static int _startingY;
    private static Direction _startingDirection;
    private static List<Coordinate>? _startingMap;

    private const string Filename = "map.txt";
    
    private static void Main()
    {
        _startingMap = ReadInput(Filename);
        _startingX = _startingMap.Find(coord => coord.Visited)!.X;
        _startingY = _startingMap.Find(coord => coord.Visited)!.Y;

        Console.WriteLine($"Starting location is: ({_startingX},{_startingY})");
        Console.WriteLine($"Amount if walls is: {_startingMap.Count(coord => coord.IsWall)}");
        Console.WriteLine($"Starting direction is: {_startingDirection}");

        var processedMap = ProcessMap(_startingMap.ToDictionary(coord => (coord.X, coord.Y)), _startingX, _startingY, _startingDirection);

        if (processedMap != null) Console.WriteLine($"Amount of visited squares: {processedMap.Count(coord => coord.Visited)}");

        var possibleLoops = CheckPossibleLoops(_startingMap, _startingX, _startingY, _startingDirection);
        
        Console.WriteLine($"Number of possible loop creating positions: {possibleLoops}");
    }

    private static List<Coordinate>? ProcessMap(Dictionary<(int, int), Coordinate> coordinateMap, int x, int y, Direction startDirection)
    {
        var direction = startDirection;
        var visitedWalls = new HashSet<(int X, int Y, Direction ApproachDirection)>();
        var nextCoord = coordinateMap.GetValueOrDefault((x, y));

        while (nextCoord != null)
        {
            if (!nextCoord.IsWall)
                nextCoord.Visited = true;
            
            switch (direction)
            {
                case Direction.North:
                    nextCoord = coordinateMap.GetValueOrDefault((x, y - 1));
                    
                    if (nextCoord is {IsWall: true})
                    {
                        if (!visitedWalls.Add((nextCoord.X, nextCoord.Y, direction)))
                            return null;
                        direction = Direction.East;
                    }

                    else if (nextCoord != null)
                        y--;

                    break;
                
                case Direction.West:
                    nextCoord = coordinateMap.GetValueOrDefault((x - 1, y));
                    
                    if (nextCoord is {IsWall: true})
                    {
                        if (!visitedWalls.Add((nextCoord.X, nextCoord.Y, direction)))
                            return null;
                        direction = Direction.North;
                    }

                    else if (nextCoord != null)
                        x--;

                    break;
                
                case Direction.South:
                    nextCoord = coordinateMap.GetValueOrDefault((x, y + 1));
                    
                    if (nextCoord is {IsWall: true})
                    {
                        if (!visitedWalls.Add((nextCoord.X, nextCoord.Y, direction)))
                            return null;
                        direction = Direction.West;
                    }

                    else if (nextCoord != null)
                        y++;

                    break;            
                
                case Direction.East:
                    nextCoord = coordinateMap.GetValueOrDefault((x + 1, y));
                    
                    if (nextCoord is {IsWall: true})
                    {
                        if (!visitedWalls.Add((nextCoord.X, nextCoord.Y, direction)))
                            return null;
                        direction = Direction.South;
                    }

                    else if (nextCoord != null)
                        x++;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return coordinateMap.Values.ToList();
    }

    private static int CheckPossibleLoops(List<Coordinate> inputMap, int x, int y, Direction startDirection)
    {
        var coordinateMap = inputMap.ToDictionary(coord => (coord.X, coord.Y));
        var numberOfPossibleLoops = 0;
        foreach (var coordinate in coordinateMap.Values.Where(coord => !coord.IsWall))
        {
            coordinate.IsWall = true;

            if (ProcessMap(coordinateMap, x, y, startDirection) == null)
                numberOfPossibleLoops++;

            coordinate.IsWall = false;
        }

        return numberOfPossibleLoops;
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
                _startingDirection = Direction.North;
            
            if (line.Contains('>'))
                _startingDirection = Direction.East;
            
            if (line.Contains('<'))
                _startingDirection = Direction.West;
            
            if (line.Contains('v'))
                _startingDirection = Direction.South;
            
            y++;
        }

        return inputMap;
    }
}