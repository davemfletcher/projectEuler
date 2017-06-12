﻿using System;
using System.Linq;

namespace ProjectEuler
{
    /** The four adjacent digits in the 1000-digit number that have the greatest product are 9 × 9 × 8 × 9 = 5832.
     * Find the thirteen adjacent digits in the 1000-digit number that have the greatest product. What is the value of this 
     * product? 
     * 
     * Answer
     * ======
     *  for n(13) = 23514624000
     **/
    class Problem08 : Problem
    {
        private const string SourceString = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";



        /// <summary>
        /// 60,000 ticks to complete
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        private long FirstAttempt(int limit=13)
        {
            long highestScore = 0;
            long tally = 1;
            int[] source = SourceString.Select(c => c - '0').ToArray();
            for (var i = 0; i < source.Length - limit + 1; i++, tally = 1)
            {
                for (int j = 0; j < limit; j++)
                {
                    tally *= source[j+i];
                    // if a zero is encountered reset and move to next iteration
                    if (tally == 0)
                    {
                        i += j;
                        break;
                    }
                }
                if (tally > highestScore)
                {
                    highestScore = tally;
                }

            }

           //todo use Linq?
//            source.ToList().ForEach(Console.WriteLine);
//            Console.WriteLine("highest score " + highestScore + " - start pos - " + startPos + " - " + source[startPos] + source[startPos + 1] + source[startPos + 2] + source[startPos + 3] + source[startPos + 4] + source[startPos + 5] + source[startPos + 6] + source[startPos + 7] + source[startPos + 8] + source[startPos + 9]);

            return highestScore;
        }

        /// <summary>
        /// 60,000 ticks as well
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        private long SecondAttempt(int digits=13)
        {
            return (from i in Enumerable.Range(0, SourceString.Length - digits + 1)
                    select (Enumerable.Range(i, digits).Select(n => Convert.ToInt64(SourceString[n] - '0')))
                     .Aggregate<long, long>(1, (c, n) => c * n)).Max();
        }

        public override Answer GetAnswer()
        {
            return new Answer
            {
                Description = (SecondAttempt(13)).ToString()
            };
        }
    }
}