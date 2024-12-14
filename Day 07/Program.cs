using System.Numerics;

namespace Day_07;

 internal static class Program
 {
     private const string Filename = "calibration.txt";

     private static List<Calibration>? _calibrations;

     private static void Main()
     {
         _calibrations = ReadInput(Filename);
         ProcessCalibrations(_calibrations);

         var sumOfCorrectCalibrations = SumOfCorrectCalibrations(_calibrations);
         var countOfCorrectCalibrations = _calibrations.Where(c => c.Valid).ToList().Count;

         Console.WriteLine($"Total sum of correct calibrations: {sumOfCorrectCalibrations}");
         Console.WriteLine($"Number of correct calibrations: {countOfCorrectCalibrations}");
         
         ProcessCalibrations(_calibrations, true);
         
         sumOfCorrectCalibrations = SumOfCorrectCalibrations(_calibrations);
         countOfCorrectCalibrations = _calibrations.Where(c => c.Valid).ToList().Count;

         Console.WriteLine($"Total sum of correct calibrations including `||` operand: {sumOfCorrectCalibrations}");
         Console.WriteLine($"Number of correct calibrations including `||` operand: {countOfCorrectCalibrations}");
     }

     private static void ProcessCalibrations(List<Calibration> calibrations, bool includeOrOperand = false)
     {
         foreach (var calibration in calibrations)
         {
                 var possibleResults = GetAllResults(calibration.Operands, includeOrOperand);

                 if (possibleResults.Contains(calibration.Result))
                 {
                     calibration.Valid = true;
                 }
         }
     }

     private static HashSet<long> GetAllResults(List<long> calibrationOperands, bool includeOrOperand)
     {
         var results = new HashSet<long>();

         Calculate(calibrationOperands, 1, calibrationOperands[0], results, includeOrOperand);

         return results;
     }

     private static void Calculate(List<long> calibrationOperands, int index, long currentResult, HashSet<long> results, bool includeOrOperand)
     {
         if (index == calibrationOperands.Count)
         {
             results.Add(currentResult);
             return;
         }

         var nextOperand = calibrationOperands[index];
         Calculate(calibrationOperands, index + 1, currentResult + nextOperand, results, includeOrOperand);
         Calculate(calibrationOperands, index + 1, currentResult * nextOperand, results, includeOrOperand);

         if (includeOrOperand)
         {
             var concatenatedOperand = long.Parse($"{currentResult}{nextOperand}");
             Calculate(calibrationOperands, index + 1, concatenatedOperand, results, includeOrOperand);         
         }
         
     }

     private static long SumOfCorrectCalibrations(List<Calibration> calibrations)
     {
         var result = 0L;

         foreach (var calibration in calibrations)
         {
             if (calibration.Valid)
                 result += calibration.Result;
         }

         return result;
     } 
     private static List<Calibration> ReadInput(string filename)
     {
         var inputCalibrations = new List<Calibration>();

         using var reader = new StreamReader(filename);

         while (reader.ReadLine() is { } line)
         {
             var splitLine = line.Split(":");
             var result = long.Parse(splitLine[0]);
             var operands = splitLine[1].Trim().Split(" ").Select(long.Parse).ToList();
             
             inputCalibrations.Add(new Calibration(result, operands));
         }
         
         return inputCalibrations;
     }

 }