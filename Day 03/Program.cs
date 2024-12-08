using System.Text.RegularExpressions;

var input = ReadInput("input.txt");
var mulCommands = CleanInput(input);
var output = 0;

foreach (Match mulCommand in mulCommands)
{
    output += ProcessMulCommand(mulCommand);
}

Console.WriteLine($"The sum of all the mul commands is: {output}");
return;


string ReadInput(string filename)
{
    using var reader = new StreamReader(filename);
    var input = reader.ReadToEnd();
    return input.Replace(Environment.NewLine, "");
}

MatchCollection CleanInput(string input)
{
    var mulRegex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    var dontDoRegex = new Regex(@"don't\(\).*?do\(\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    input = dontDoRegex.Replace(input, "");
    return mulRegex.Matches(input);
}

int ProcessMulCommand(Match mulCommand)
{
    var groups = mulCommand.Groups;
    return int.Parse(groups[1].Value) * int.Parse(groups[2].Value);
}
