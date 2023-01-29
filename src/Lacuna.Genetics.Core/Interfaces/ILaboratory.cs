namespace Lacuna.Genetics.Core.Interfaces;

public interface ILaboratory
{
    string? EncodeStrand(string strand);
    string? DecodeStrand(string strand);
    bool? CheckGene(string strandEncoded, string geneEncoded);
}