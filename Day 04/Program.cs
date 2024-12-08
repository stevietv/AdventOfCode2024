var inputGrid = ReadInput("input.txt");

Console.WriteLine($"XMAS has been found {CountOfXmas(inputGrid)} times.");
Console.WriteLine("----------------");
Console.WriteLine($"""
                   M S
                    A  has been found {CountOfMasCross(inputGrid)} times
                   M S
                   """);
Console.WriteLine("----------------");
return;

List<List<char>> ReadInput(string filename)
{
    using var reader = new StreamReader(filename);

    List<List<char>> output = [];

    while (reader.ReadLine() is { } line)
    {
        output.Add(line.ToCharArray().ToList());
    }

    return output;
}

int CountOfXmas(List<List<char>> grid)
{
    var countOfXmas = 0;

    for (var i = 0; i < grid.Count; i++)
    {
        for (var j = 0; j < grid[i].Count; j++)
        {
            if (grid[i][j] == 'X')
                countOfXmas += CountOfXmasFromLetter(grid, j, i);
        }
    }

    return countOfXmas;
}

int CountOfXmasFromLetter(List<List<char>> grid, int x, int y)
{
    var xMax = grid[0].Count;
    var yMax = grid.Count;
    
    var countOfXmasFromLetter = 0;

    //check W
    if (x >= 3 && grid[y][x - 1] == 'M' && grid[y][x - 2] == 'A' && grid[y][x - 3] == 'S')
        countOfXmasFromLetter++;
    
    //check S
    if (y < yMax-3 && grid[y + 1][x] == 'M' && grid[y + 2][x] == 'A' && grid[y + 3][x] == 'S')
        countOfXmasFromLetter++;

    //check E
    if (x < xMax-3 && grid[y][x + 1] == 'M' && grid[y][x + 2] == 'A' && grid[y][x + 3] == 'S')
        countOfXmasFromLetter++;

    //check N
    if (y >= 3 && grid[y - 1][x] == 'M' && grid[y - 2][x] == 'A' && grid[y - 3][x] == 'S')
        countOfXmasFromLetter++;
 
    //check NW
    if (y >= 3 && x >= 3 && grid[y - 1][x - 1] == 'M' && grid[y - 2][x - 2] == 'A' && grid[y - 3][x - 3] == 'S')
        countOfXmasFromLetter++;
     
    //check NE
    if (y >= 3 && x < xMax - 3 && grid[y - 1][x + 1] == 'M' && grid[y - 2][x + 2] == 'A' && grid[y - 3][x + 3] == 'S')
        countOfXmasFromLetter++;    
     
    //check SW
    if (y < yMax - 3 && x >= 3 && grid[y + 1][x - 1] == 'M' && grid[y + 2][x - 2] == 'A' && grid[y + 3][x - 3] == 'S')
        countOfXmasFromLetter++;
     
    //check SE
    if (y < yMax - 3 && x < xMax - 3 && grid[y + 1][x + 1] == 'M' && grid[y + 2][x + 2] == 'A' && grid[y + 3][x + 3] == 'S')
        countOfXmasFromLetter++;

    return countOfXmasFromLetter;
}

int CountOfMasCross(List<List<char>> list)
{
    var countOfMasCross = 0;

    for (var i = 1; i < list.Count - 1; i++)
    {
        for (var j = 1; j < list[i].Count - 1; j++)
        {
            if (list[i][j] == 'A')
                countOfMasCross += CountOfMasCrossFromLetter(list, j, i);
        }
    }

    return countOfMasCross;
}

int CountOfMasCrossFromLetter(List<List<char>> grid, int x, int y)
{
    var countOfMassCrossFromLetter = 0;
    
    //check Left-to-Right
    if (grid[y-1][x-1] == 'M' && grid[y+1][x+1] == 'S')
        if (grid[y + 1][x - 1] == 'M' && grid[y - 1][x + 1] == 'S')
            countOfMassCrossFromLetter++;
    
    //check Top-to-Bottom
    if (grid[y-1][x-1] == 'M' && grid[y+1][x+1] == 'S')
        if (grid[y + 1][x - 1] == 'S' && grid[y - 1][x + 1] == 'M')
            countOfMassCrossFromLetter++;
    
    //check Right-to-Left
    if (grid[y+1][x+1] == 'M' && grid[y-1][x-1] == 'S')
        if (grid[y - 1][x + 1] == 'M' && grid[y + 1][x - 1] == 'S')
            countOfMassCrossFromLetter++;
    
    //check Bottom-to-Top
    if (grid[y+1][x+1] == 'M' && grid[y-1][x-1] == 'S')
        if (grid[y + 1][x - 1] == 'M' && grid[y - 1][x + 1] == 'S')
            countOfMassCrossFromLetter++;
    return countOfMassCrossFromLetter;
}
