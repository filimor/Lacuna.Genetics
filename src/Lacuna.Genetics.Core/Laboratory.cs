using System.Text;
using Lacuna.Genetics.Core.Extensions;
using Lacuna.Genetics.Core.Interfaces;

namespace Lacuna.Genetics.Core;

public class Laboratory : ILaboratory
{
    /// <summary>
    ///     Encode a strand from the String to the Base64 format.
    /// </summary>
    /// <param name="strand">A strand in the String format.</param>
    /// <returns>The strand in the Base64 format.</returns>
    public string? EncodeStrand(string strand)
    {
        if (string.IsNullOrEmpty(strand))
        {
            return null;
        }

        var encodingDict = new Dictionary<char, byte>
        {
            { 'A', 0b00 },
            { 'C', 0b01 },
            { 'G', 0b10 },
            { 'T', 0b11 }
        };

        var byteArray = new byte[strand.Length / 4];

        for (var i = 0; i < strand.Length; i += 4)
        {
            var bit = strand.Substring(i, 4);
            var encodedByte = (byte)((encodingDict[bit[0]] << 6) |
                                     (encodingDict[bit[1]] << 4) |
                                     (encodingDict[bit[2]] << 2) |
                                     encodingDict[bit[3]]);
            byteArray[i / 4] = encodedByte;
        }

        return Convert.ToBase64String(byteArray);
    }

    /// <summary>
    ///     Decode a strand from the Base64 to the String format.
    /// </summary>
    /// <param name="strand">A Strand in the Base64 format.</param>
    /// <returns>The strand in the String format.</returns>
    public string? DecodeStrand(string strand)
    {
        if (string.IsNullOrEmpty(strand))
        {
            return null;
        }
        
        var decodingDict = new Dictionary<byte, char>
        {
            { 0b00, 'A' },
            { 0b01, 'C' },
            { 0b10, 'G' },
            { 0b11, 'T' }
        };

        var byteArray = Convert.FromBase64String(strand);
        var stringBuilder = new StringBuilder();

        foreach (var encodedByte in byteArray)
        {
            stringBuilder.Append(decodingDict[Convert.ToByte(encodedByte >> 6)]);
            stringBuilder.Append(decodingDict[Convert.ToByte((encodedByte >> 4) & 0b11)]);
            stringBuilder.Append(decodingDict[Convert.ToByte((encodedByte >> 2) & 0b11)]);
            stringBuilder.Append(decodingDict[Convert.ToByte(encodedByte & 0b11)]);
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     Check whether a gene is activated (if more than 50% of the gene
    ///     is present on the template strand.).
    /// </summary>
    /// <param name="strandEncoded">A strand in the Base64 format.</param>
    /// <param name="geneEncoded">A gene in the Base64 format.</param>
    /// <returns></returns>
    public bool? CheckGene(string strandEncoded, string geneEncoded)
    {
        if (string.IsNullOrEmpty(strandEncoded) || string.IsNullOrEmpty(geneEncoded))
        {
            return null;
        }

        var strand = DecodeStrand(strandEncoded);
        var gene = DecodeStrand(geneEncoded);
        var templateStrand = GetTemplateStrand(strand!);
        var lcs = templateStrand.FindLongestCommonSubstring(gene!);
        var matchRate = (double)lcs.Length / gene!.Length;

        return matchRate >= 0.5;
    }

    /// <summary>
    ///     Returns the same strand if it is a template strand,
    ///     otherwise returns the complementary strand.
    /// </summary>
    /// <param name="inputStrand">A strand in the String format.</param>
    /// <returns>The corresponding template strand.</returns>
    private static string GetTemplateStrand(string inputStrand)
    {
        if (inputStrand.StartsWith("CAT"))
        {
            return inputStrand;
        }

        var translationDict = new Dictionary<char, char>
        {
            { 'A', 'T' },
            { 'C', 'G' },
            { 'G', 'C' },
            { 'T', 'A' }
        };

        var sb = new StringBuilder();

        foreach (var nucleotide in inputStrand)
        {
            sb.Append(translationDict[nucleotide]);
        }

        return sb.ToString();
    }
}