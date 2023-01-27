using System.Collections;

namespace Lacuna.Genetics.Tests.ClassData;

internal class ActivatedGenesClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "xZ9MBZyHk42r", "TdLXHAdmCdMcnHAWch5ONkycnGc=" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}