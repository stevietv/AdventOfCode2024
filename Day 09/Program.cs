namespace Day_09;

internal class Block
{
    public int BlockId { get; set; }
    public int? FileId { get; set; }
}

internal class Program
{
    private const string Filename = "input.txt";
    
    private static void Main()
    {
        var blocks = ReadInput(Filename);
        // var output = "";
        //
        // foreach (var block in blocks)
        // {
        //     if (block.FileId == null)
        //         output += ".";
        //     else
        //         output += block.FileId;
        // }
        // Console.WriteLine(output);

        var reorganisedDisk = ReorganiseDisk(blocks);
        
        // var output2 = "";
        //
        // foreach (var block in reorganisedDisk)
        // {
        //     if (block.FileId == null)
        //         output2 += ".";
        //     else
        //         output2 += block.FileId;
        // }
        // Console.WriteLine(output2);

        var checksum = 0L;

        foreach (var block in reorganisedDisk.Where(b => b.FileId.HasValue))
        {
            if (block.FileId != null)
                 checksum += (long)block.BlockId * (long)block.FileId;
        }
        
        Console.WriteLine(checksum);

    }
    private static List<Block> ReadInput(string filename)
    {

        var blocks = new List<Block>();

        using var reader = new StreamReader(filename);
        var diskMap = reader.ReadToEnd().Select(c => int.Parse(c.ToString())).ToList();

        var isFile = true;
        var blockId = 0;
        var fileId = 0;
        foreach (var item in diskMap)
        {
            for (var i = 0; i < item; i++)
            {
                blocks.Add(new Block
                {
                    BlockId = blockId,
                    FileId = isFile ? fileId : null
                });

                blockId++;
            }

            if (isFile) fileId++;
            isFile = !isFile;
        }

        return blocks;
    }

    private static List<Block> ReorganiseDisk(List<Block> blocks)
    {
        var lastNoneNull = blocks.FindLast(b => b.FileId.HasValue);
        var firstNull = blocks.Find(b => !b.FileId.HasValue);

        foreach (var block in blocks)
        {
            if (firstNull.BlockId > lastNoneNull.BlockId)
                return blocks;

            if (block.FileId == null)
            {
                block.FileId = lastNoneNull.FileId;
                lastNoneNull.FileId = null;
            }

            lastNoneNull = blocks.FindLast(b => b.FileId.HasValue);
            firstNull = blocks.Find(b => !b.FileId.HasValue);
        }

        return blocks;
    }
}