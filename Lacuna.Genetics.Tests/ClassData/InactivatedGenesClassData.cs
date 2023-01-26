using System.Collections;

namespace Lacuna.Genetics.Tests.ClassData;

internal class InactivatedGenesClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "xZ9MBZyH", "TdLXHAdmCdMcnHAWch5ONkycnGc=" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}