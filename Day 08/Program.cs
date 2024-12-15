using System.Numerics;

namespace Day_08;

internal class Program
{
    private const string Filename = "input.txt";
    private static readonly Dictionary<char, List<Vector2>> Antennas = new();
    private static int _maxX;
    private static int _maxY;

    private static void Main()
    {
        ReadInput(Filename);

        var totalAntinodes = new HashSet<Vector2>();
        var totalAntinodesInLine = new HashSet<Vector2>();

        foreach (var antenna in Antennas.Values)
        {
            totalAntinodes.UnionWith(FindAntinodes(antenna));
            totalAntinodesInLine.UnionWith(FindAntinodes(antenna, true));
        }

        Console.WriteLine($"There are {totalAntinodes.Count} antinodes");
        Console.WriteLine($"There are {totalAntinodesInLine.Count} antinodes");
    }


    static void ReadInput(string filename)
    {
        using var reader = new StreamReader(Filename);
        var lineCounter = 0;

        while (reader.ReadLine() is { } line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                var currentLocation = line[i];

                if (currentLocation != '.')
                {
                    var antennaLocation = new Vector2(i, lineCounter);

                    if (Antennas.TryGetValue(currentLocation, out List<Vector2> currentList))
                        currentList.Add(antennaLocation);

                    else
                        Antennas.Add(currentLocation, new List<Vector2>
                        {
                            antennaLocation
                        });
                }

                _maxX = i;
            }
            _maxY = lineCounter;
            lineCounter++;
        }
    }

    static HashSet<Vector2> FindAntinodes(List<Vector2> nodes, bool line = false)
    {
        var antinodes = new HashSet<Vector2>();

        for (var i = 0; i < nodes.Count; i++)
        {
            for (var j = i + 1; j < nodes.Count; j++)
            {
                var node1 = nodes[i];
                var node2 = nodes[j];

                var difference = node1 - node2;

                antinodes.UnionWith(AddAntinodes(node1, node2, difference, line));
                antinodes.UnionWith(AddAntinodes(node2, node1, Vector2.Negate(difference), line));
            }
        }

        return antinodes;
    }

    static HashSet<Vector2> AddAntinodes(Vector2 node, Vector2 partnerNode, Vector2 difference, bool line = false)
    {
        var individualAntiNodes = new HashSet<Vector2>();

        if (line) individualAntiNodes.Add(partnerNode);

        do
        {
            var newNode = node + difference;
            if (newNode.X < 0 || newNode.X > _maxX || newNode.Y < 0 || newNode.Y > _maxY) return individualAntiNodes;
            individualAntiNodes.Add(newNode);
            node += difference;
        } while (line);

        return individualAntiNodes;
    }
}