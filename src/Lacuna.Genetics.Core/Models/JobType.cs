namespace Lacuna.Genetics.Core.Models;

public class JobType
{
    public const string EncodeStrand = "EncodeStrand";
    public const string DecodeStrand ="DecodeStrand";
    public const string CheckGene = "CheckGene";

    // TODO: It isn't a problem a so short endpoint instead of a full name?
    public static string GetEndpoint(string type)
    {
        return type switch
        {
            EncodeStrand => "encode",
            DecodeStrand => "decode",
            CheckGene=> "gene",
            _ => throw new Exception("Unknown job type.")
        };
    }
}