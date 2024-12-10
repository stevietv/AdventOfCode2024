var instructions = new List<List<int>>();
var updates = new List<List<int>>();
var incorrectUpdates = new List<List<int>>();

const string filename = "input.txt";

ReadInput(filename);

var sumOfMiddlePagesOfCorrectUpdates = CheckInstructions(instructions, updates);
Console.WriteLine($"The sum of the middle pages of the acceptable updates is: {sumOfMiddlePagesOfCorrectUpdates}");

var sumOfMiddlePagesOfFixedInstructions = FixUpdates(instructions, incorrectUpdates);
Console.WriteLine($"The sum of the middle pages of the fixed updates is: {sumOfMiddlePagesOfFixedInstructions}");
return;

void ReadInput(string input)
{
    using var reader = new StreamReader(input);

    while (reader.ReadLine() is { } line)
    {
        if (line.Contains("|"))
        {
            var instruction = line.Split("|").Select(int.Parse).ToList();
            instructions.Add(instruction);
        }
        
        else if (line.Contains(","))
        {
            var update = line.Split(",").Select(int.Parse).ToList();
            updates.Add(update);
        }
    }
}

int CheckInstructions(List<List<int>> instructionsList, List<List<int>> updatesList)
{
    var accumulator = 0;

    foreach (var update in updatesList)
    {
        var instructionIsCorrect = true;

        foreach (var instruction in instructionsList)
        {
            if (UpdateContainsBothInstructions(instruction, update) &&
                !CheckSingleInstructionOrder(instruction, update))
            {
                instructionIsCorrect = false;
                break;
            }
        }

        if (instructionIsCorrect)
            accumulator += GetMiddlePage(update);
        else
            incorrectUpdates.Add(update);
    }

    return accumulator;
}

bool CheckAllInstructions(List<List<int>> instructionsList, List<List<int>> updatesList)
{
    foreach (var update in updatesList)
    {
        foreach (var instruction in instructionsList)
        {
            if (UpdateContainsBothInstructions(instruction, update) &&
                !CheckSingleInstructionOrder(instruction, update))
            {
                return false;
            }
        }
    }
    return true;
}


int FixUpdates(List<List<int>> instructionsList, List<List<int>> updatesList)
{
    while (!CheckAllInstructions(instructionsList, updatesList))
    {
        foreach (var update in updatesList)
        {
            foreach (var instruction in instructionsList)
            {
                if (!UpdateContainsBothInstructions(instruction, update))
                    continue;

                if (CheckSingleInstructionOrder(instruction, update))
                    continue;
                
                update.Remove(instruction[0]);
                update.Insert(update.IndexOf(instruction[1]),instruction[0]);
            }
        }
    }

    return updatesList.Sum(update => GetMiddlePage(update));
}



bool CheckSingleInstructionOrder(List<int> instruction, List<int> update)
{
    return update.IndexOf(instruction[0]) < update.IndexOf(instruction[1]);
}

bool UpdateContainsBothInstructions(List<int> instruction, List<int> update)
{
    return update.Contains(instruction[0]) && update.Contains(instruction[1]);
}

int GetMiddlePage(List<int> update)
{
    return update[update.Count / 2];
}

