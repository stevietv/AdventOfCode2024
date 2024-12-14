using System.Numerics;

namespace Day_07;

public class Calibration(long result, List<long> operands)
{
    public long Result { get; set; } = result;
    public List<long> Operands { get; set; } = operands;
    public bool Valid { get; set; }
}