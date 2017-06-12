﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;
using ProjectEuler;
using ProjectEuler.Resources;

namespace projectEuler
{
    /// <summary>
    /// The sequence of triangle numbers is generated by adding the natural numbers. So the 7th triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. The first ten terms would be:
    // 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...

    // Let us list the factors of the first seven triangle numbers:
    //triangle number => adds to -> factors
    //1 - 1: 1
    //2 - 3: 1,3
    //3 - 6: 1,2,3,6
    //4 - 10: 1,2,5,10
    //5 - 15: 1,3,5,15
    //6 - 21: 1,3,7,21
    //7 - 28: 1,2,4,7,14,28 - 2^2*7^1 - multiply exponents (adding 1 to each exponent) 3*2 = 6
    // We can see that 28 is the first triangle number to have over five divisors.
    // What is the value of the first triangle number to have over five hundred divisors?

    // Answer is 76576500
    /// </summary>

    public class Problem12 : Problem
    {

        public override Answer GetAnswer()
        {
            const int num = 500;
            return new Answer
            {
                Description = ThirdAttempt(num).ToString()
            };
        }

        private int FirstAttempt(int limit = 0)
        {
            int triangleSum = 0;
            int triangleNth = 1;
            while (CountDivisors(triangleSum) <= limit)
            {
                triangleSum += triangleNth;
                triangleNth++;
            }
            return triangleSum;
        }

        /// <summary>
        /// For every divisor up to sqrt(num) there is a corresponding divisor above the square root
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private int CountDivisors(int num)
        {
            // Includes one and the number
            int count = 2;
            for (int i = 2; i <= Math.Sqrt(num); i++)
            {
                if (num%i == 0)
                {
                    // cases for i == sqrt(num) they only appear once.
                    // Whereas normally we have a divisor either side of the sqrt(num).
                    if (i != (num/i))
                        count++;
                    count++;
                }
            }
            return count;
        }


        /// <summary>
        /// The highest prime factor is pow(1)
        /// </summary>
        /// <param name="nthTriangle"></param>
        /// <returns></returns>
        private long SecondAttempt(int tri)
        {
            long NOD = 1;
            long nthTriangle = 1;
            while (NOD < tri)
            {
                var triangleSum = Util.NthTriangle(nthTriangle++);
                var primeFactors = Util.PrimeFactors(triangleSum);
                NOD = PrimeFactorisationNoD(triangleSum, primeFactors.ToArray());
            }


            return NOD;

            long product = 1;
            var exp_idx = 0;
            var idx = 0;

            //put in a method pass each primefactor if sequentially product not equal trianglesum 
//            //then subtract 1 from first exp 
//            for (int i = 0; i < primeFactors.Count() - 1; i++)
//            {
//                product = 1;
//                while (product < triangleSum)
//                {
//                    exp_idx = 0;
//                    foreach (var pf in primeFactors)
//                    {
//                        product *= (long) Math.Pow(pf, exponents[exp_idx]);
//                        exp_idx++;
//                    }
//                    if (product < triangleSum)
//                        exponents[i] += 1;
//                }
//            }
//            return -1;
        }

        /// <summary>
        /// 26160327 ticks
        /// </summary>
        /// <param name="nod"></param>
        /// <returns></returns>
        private long ThirdAttempt(long nod)
        {
            var primes = Util.SieveOfEratosthenes(75000);
            long number = 1;
            long i = 2;
            long cnt = 0;
            long Dn1 = 2;
            long Dn = 2;
            while (cnt < 500)
            {
                if (i % 2 == 0)
                {
                    Dn = PrimeFactorisationNoD(i + 1, primes);
                    cnt = Dn * Dn1;
                }
                else
                {
                    Dn1 = PrimeFactorisationNoD((i + 1) / 2, primes);
                    cnt = Dn * Dn1;
                }
                i++;
            }
            number = i * (i - 1) / 2;
            return number;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>>http://www.mathblog.dk/triangle-number-with-more-than-500-divisors</remarks>
        /// <param name="number"></param>
        /// <param name="primelist"></param>
        /// <returns></returns>
        private long PrimeFactorisationNoD(long number, IEnumerable<long> primelist)
        {
            int nod = 1;
            long remain = number;

            foreach (long prime in primelist)
            {
                int exponent = 1;
                while (remain%prime == 0)
                {
                    exponent++;
                    remain /= prime;
                }
                nod *= exponent;

                //If there is no remainder, return the count
                if (remain == 1)
                {
                    return nod;
                }
            }
            return nod;
            
        }
    }
}