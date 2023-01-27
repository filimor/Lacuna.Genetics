using System.Text;

namespace Lacuna.Genetics.Core;

public static class LabModule
{
    public static string DecodeStrand(string strand)
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

    public static string EncodeStrand(string strand)
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

    public static bool CheckGene(string strandEncoded, string geneEncoded)
    {
        var strand = DecodeStrand(strandEncoded);
        var gene = DecodeStrand(geneEncoded);
        var templateStrand = GetTemplateStrand(strand);
        var lcs = FindLongestCommonSubstring(templateStrand, gene);
        var matchRate = (double)lcs.Length / gene.Length;

        return matchRate >= 0.5;
    }

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

    // TODO: Extract this method
    private static string FindLongestCommonSubstring(string s1, string s2)
    {
        var m = s1.Length;
        var n = s2.Length;
        var longest = 0;
        var lcs = string.Empty;
        var table = new int[m, n];

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (s1[i] != s2[j])
                {
                    continue;
                }

                table[i, j] = i == 0 || j == 0 ? 1 : 1 + table[i - 1, j - 1];

                if (table[i, j] <= longest)
                {
                    continue;
                }

                longest = table[i, j];
                var start = i - table[i, j] + 1;
                lcs = s1.Substring(start, longest);
            }
        }

        return lcs;
    }
}