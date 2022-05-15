using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceP2.Services
{
    public class FindPrimes : IFindPrimes
    {
        public string GenerateHash(int n, int code)
        {
            long lastPrime = FindPrimeAfterCode(n, code);
            long firstPrime = FindPrimeBeforeCode(n, code);
            long hash = firstPrime * lastPrime;

            return hash.ToString();
        }
        public bool isPrime(int number)
        {
            // Corner case
            if (number <= 1)
                return false;

            // Check from 2 to sqrt(n)
            for (int i = 2; i < Math.Sqrt(number); i++)
                if (number % i == 0)
                    return false;

            return true;
        }
        public long FindPrimeAfterCode(int n, int code)
        {
            int count = 0;
            int prime = code;
            while (count < n)
            {
                if (isPrime(code + count) && count != 0)
                    prime = code + count;

                count++;
            }
            return prime;
        }
        public long FindPrimeBeforeCode(int n, int code)
        {
            int count = 0;
            int prime = code;
            while (count < n)
            {
                if (isPrime(code - count) && count != 0)
                    prime = code - count;

                count++;
            }
            return prime;
        }
    }
}
