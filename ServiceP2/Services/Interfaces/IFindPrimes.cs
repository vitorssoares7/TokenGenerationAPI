using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceP2.Services.Interfaces
{
    public interface IFindPrimes
    {
        public string GenerateHash(int n, int code);
        public bool isPrime(int number);
        public long FindPrimeAfterCode(int n, int code);
        public long FindPrimeBeforeCode(int n, int code);
    }
}
