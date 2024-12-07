CheckReports();
return;

void CheckReports()
{
    List<string> reports = ReadReports("reports.txt");

    var safeReports = 0;

    foreach (var report in reports)
    {
        var levels = report.Split(" ").ToList().Select(level => int.Parse(level)).ToList();

        if (IsReportSafe(levels) || IsReportSafeWithDampener(levels))
        {
            safeReports++;
        }
    }

    Console.WriteLine($"Number of Safe Reports: {safeReports}");
}

List<string> ReadReports(string filename)
{
    List<string> reports = [];
    using var reader = new StreamReader(filename);

    while (reader.ReadLine() is { } line)
    {
        reports.Add(line);
    }

    return reports;
}

bool IsReportSafe(List<int> levels)
{
    return (IsDecreasing(levels) || IsIncreasing(levels)) && AllAcceptableDifferences(levels);
}


bool IsReportSafeWithDampener(List<int> levels)
{
    for (var i = 0; i < levels.Count; i++)
    {
        var filteredLevels = levels.ToList();
        filteredLevels.RemoveAt(i);

        if (IsReportSafe(filteredLevels))
            return true;
    }

    return false;
}


bool IsDecreasing(List<int> levels)
{
    for (var i = 1; i < levels.Count; i++)
    {
        if (levels[i] > levels[i - 1])
            return false;
    }

    return true;
}

bool IsIncreasing(List<int> levels)
{
    for (var i = 1; i < levels.Count; i++)
    {
        if (levels[i] < levels[i - 1])
            return false;
    }

    return true;
}

bool AllAcceptableDifferences(List<int> levels)
{
    for (var i = 1; i < levels.Count; i++)
    {
        if (UnacceptableDifference(levels[i], levels[i - 1]))
            return false;
    }

    return true;
}

bool UnacceptableDifference(int x, int y)
{
    var difference = Math.Abs(x - y);
    return (difference < 1 || difference > 3);
}