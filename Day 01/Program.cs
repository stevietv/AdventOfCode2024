using System.Text.RegularExpressions;

const string filename = "list.txt";

using var reader = new StreamReader(filename);

List<int> leftList = [];
List<int> rightList = [];

while (reader.ReadLine() is { } line)
{
    var lineItems = Regex.Split(line, @"\s+");
    leftList.Add(Int32.Parse(lineItems[0]));
    rightList.Add(Int32.Parse(lineItems[1]));
}

leftList.Sort();
rightList.Sort();

var distanceAccumulator = leftList.Zip(rightList, (a, b) => Math.Abs(a - b)).Sum();

Console.WriteLine($"Distance Score: {distanceAccumulator}");

var similarityScore = 0;

foreach (var item in leftList)
{
    similarityScore += item * rightList.FindAll(x => x == item).Count;
}

Console.WriteLine($"Similarity Score: {similarityScore}");
