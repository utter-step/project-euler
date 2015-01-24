using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools;

namespace _60
{
    class Program
    {
        private const bool VERBOSE = false;
        private const int LENGTH = 5;
        private const int LIMIT = 10000;

        static void Main(string[] args)
        {
            Decorators.Benchmark(Solve, LIMIT);
        }

        private static int[] _primes;

        private static bool IsPrime(int n)
        {
            if ((n & 1) == 0 && n != 2)
            {
                return false;
            }
            foreach (var prime in _primes)
            {
                if (n % prime == 0)
                {
                    return false;
                }
                if (prime * prime > n)
                {
                    break;
                }
            }
            return true;
        }

        private static int Solve(int limit)
        {
            var primes = NumUtils.EratospheneSeive(limit);

            _primes = primes.ToArray();

            var primesArray = primes.ToArray();

            var pairs = new SortedDictionary<int, HashSet<int>>();

            for (int i = 0; i < primesArray.Length; i++)
            {
                for (int j = i; j < primesArray.Length; j++)
                {
                    int I = primesArray[i];
                    int J = primesArray[j];
                    if (IsPrime(Concatenate(I, J)) && IsPrime(Concatenate(J, I)))
                    {
                        if (!pairs.ContainsKey(I))
                        {
                            pairs.Add(I, new HashSet<int>());
                        }
                        if (!pairs.ContainsKey(J))
                        {
                            pairs.Add(J, new HashSet<int>());
                        }
                        pairs[I].Add(J);
                        pairs[J].Add(I);
                    }
                }
            }

            foreach (var prime in primes)
            {
                if (pairs.ContainsKey(prime) && pairs[prime].Count < LENGTH)
                {
                    pairs.Remove(prime);
                }
            }

            var r = FindCliques(LENGTH, 
                                pairs.ToList(),
                                new List<KeyValuePair<int, HashSet<int>>>(),
                                new List<KeyValuePair<int, HashSet<int>>>());

            if (r == null)
            {
                return 0;
            }

            if (VERBOSE)
            {
                Console.WriteLine(String.Join(", ", r.Select(x => x.Key)));
            }

            return r.Sum(x => x.Key);
        }

        private static int Concatenate(int a, int b)
        {
            return a * ((int)Math.Pow(10, (int)(Math.Log10(b) + 1))) + b;
        }

        private static List<KeyValuePair<int, HashSet<int>>> FindCliques(
            int length,
            List<KeyValuePair<int, HashSet<int>>> remainingNodes, 
            List<KeyValuePair<int, HashSet<int>>> potentialClique,
            List<KeyValuePair<int, HashSet<int>>> skipNodes)
        {
            if (remainingNodes.Count == 0 && skipNodes.Count == 0 && potentialClique.Count == length)
            {
                return potentialClique;
            }

            var nodesToLookAt = new List<KeyValuePair<int, HashSet<int>>>(remainingNodes);

            foreach (var node in remainingNodes)
            {
                var nPotentialClique = new List<KeyValuePair<int, HashSet<int>>>(potentialClique);
                nPotentialClique.Add(node);

                var nRemainingNodes = remainingNodes.Where(x => node.Value.Contains(x.Key)).ToList();
                var nSkipNodes = skipNodes.Where(x => node.Value.Contains(x.Key)).ToList();

                var res = FindCliques(length, nRemainingNodes, nPotentialClique, nSkipNodes);
                if (res != null)
                {
                    return res;
                }

                nodesToLookAt.Remove(node);
                skipNodes.Add(node);
            }

            return null;
        }
    }
}
