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

var accumulator = leftList.Zip(rightList, (a, b) => Math.Abs(a - b)).Sum();

Console.WriteLine(accumulator);
