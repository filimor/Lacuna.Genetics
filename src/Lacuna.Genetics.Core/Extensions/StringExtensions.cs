namespace Lacuna.Genetics.Core.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Compare two strings and look for the longest common substring.
    /// </summary>
    /// <param name="string1">The first string.</param>
    /// <param name="string2">The second string.</param>
    /// <returns>The longest common substring.</returns>
    public static string FindLongestCommonSubstring(this string string1, string string2)
    {
        var m = string1.Length;
        var n = string2.Length;
        var longest = 0;
        var lcs = string.Empty;
        var table = new int[m, n];

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (string1[i] != string2[j])
                {
                    continue;
                }

                table[i, j] = i == 0 || j == 0
                    ? 1
                    : 1 + table[i - 1, j - 1];

                if (table[i, j] <= longest)
                {
                    continue;
                }

                longest = table[i, j];
                var start = i - table[i, j] + 1;
                lcs = string1.Substring(start, longest);
            }
        }

        return lcs;
    }
}