using System.Text;
using Lacuna.Genetics.Core.Extensions;
using Lacuna.Genetics.Core.Interfaces;

namespace Lacuna.Genetics.Core;

public class Laboratory : ILaboratory
{
    /// <summary>
    ///    Decode a strand from the Base64 to the String format.
    /// </summary>
    /// <param name="strand">A Strand in the Base64 format.</param>
    /// <returns>The strand in the String format.</returns>
    public string DecodeStrand(string strand)
    {
        var encodedBytes = Convert.FromBase64String(strand);
        var encodedBits = new StringBuilder();

        foreach (var encodedByte in encodedBytes)
        {
            encodedBits.Append(Convert.ToString(encodedByte, 2).PadLeft(8, '0'));
        }

        var decodedStrand = new StringBuilder();

        for (var i = 0; i < encodedBits.Length; i += 2)
        {
            var bit = encodedBits.ToString(i, 2);
            switch (bit)
            {
                case "00":
                    decodedStrand.Append('A');
                    break;
                case "01":
                    decodedStrand.Append('C');
                    break;
                case "10":
                    decodedStrand.Append('G');
                    break;
                case "11":
                    decodedStrand.Append('T');
                    break;
            }
        }

        return decodedStrand.ToString();
    }

    /// <summary>
    ///    Encode a strand from the String to the Base64 format.
    /// </summary>
    /// <param name="strand">A strand in the String format.</param>
    /// <returns>The strand in the Base64 format.</returns>
    public string EncodeStrand(string strand)
    {
        var sb = new StringBuilder();

        foreach (var c in strand)
        {
            switch (c)
            {
                case 'A':
                    sb.Append("00");
                    break;
                case 'C':
                    sb.Append("01");
                    break;
                case 'G':
                    sb.Append("10");
                    break;
                case 'T':
                    sb.Append("11");
                    break;
            }
        }

        var byteArray = new byte[sb.Length / 8];

        for (var i = 0; i < sb.Length; i += 8)
        {
            byteArray[i / 8] = Convert.ToByte(sb.ToString().Substring(i, 8), 2);
        }

        return Convert.ToBase64String(byteArray);
    }

    /// <summary>
    ///     Check whether a gene is activated (if more than 50% of the gene
    ///     is present on the template strand.).
    /// </summary>
    /// <param name="strandEncoded">A strand in the Base64 format.</param>
    /// <param name="geneEncoded">A gene in the Base64 format.</param>
    /// <returns></returns>
    public bool CheckGene(string strandEncoded, string geneEncoded)
    {
        var strand = DecodeStrand(strandEncoded);
        var gene = DecodeStrand(geneEncoded);
        var templateStrand = GetTemplateStrand(strand);
        var lcs = templateStrand.FindLongestCommonSubstring(gene);
        var matchRate = (double)lcs.Length / gene.Length;

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

        var outputStrand = new StringBuilder();

        foreach (var c in inputStrand)
        {
            switch (c)
            {
                case 'A':
                    outputStrand.Append('T');
                    break;
                case 'T':
                    outputStrand.Append('A');
                    break;
                case 'C':
                    outputStrand.Append('G');
                    break;
                case 'G':
                    outputStrand.Append('C');
                    break;
            }
        }

        return outputStrand.ToString();
    }
}