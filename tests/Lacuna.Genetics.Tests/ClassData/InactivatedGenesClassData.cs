using System.Collections;

namespace Lacuna.Genetics.Tests.ClassData;

internal class InactivatedGenesClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "QkcERG5RTkpZdVSoI9BMs4JVpL5m2SIP1PbC06pdwt0/NCUE2utrsfHgOIXKnEj2Ns9GAoN8+2gl9HB3jnuf+7J2aO+rSNiX6vLM",
            "TCKaLCHoZSgJ29Li04IoKqpKw/psN/yEiT89EXj/ps+j4EdGgAGqA5Z0kpGbJyUzIRpQKBkVM1ElFaLiqlOwCTUDP/I64qmpoL9j96Rr6GILOCVaS+ZtkiD9T2wtOqhCxIlbmOgy+D9wp4hasTysui1Ax6FjUlq4TbFGRME+StdnITIbD0955xCx7Qf0W9xvSWU9HQC33nIuo0s37LR9MV2x8pAKkavdx+CQsmRTxPHU1vpbACzWr0K/7WwEQftcM7uqQmhMZbi6X1YgJiwO295ZYxW5ITAw2kJNSqQ/2YblM36oqIGIiHFBoaHWtogVsRGNqt7lohKKeWgl/hsUeobQO/OuZTJd0dukd4CPWiO5N5iSsi6n/5FrHS4YYolVLSYaBnuVZ0zeL9kDQCuS+x9EbMaCgshXKDth"
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}