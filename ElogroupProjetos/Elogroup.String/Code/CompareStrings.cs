using Elogroup.String.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elogroup.String.Code
{
    public class CompareStrings
    {
        public CompareStrings()
        {

        }

        public double Execute(
            string source, 
            string target, 
            bool removeSpecialCharacters,
            bool ignoreBlankSpaces,
            bool replaceAccents)
        {
            source = source.ToUpper();
            target = target.ToUpper();

            if (replaceAccents)
            {
                var replace = new ReplaceAccentedCharacters();
                source = replace.Execute(source);
                target = replace.Execute(target);
            }

            if (removeSpecialCharacters)
            {
                var remove = new RemoveSpecialCharacters();
                remove.SetOptions(ignoreBlankSpaces);

                source = remove.Execute(source);
                target = remove.Execute(target);
            }           

            var result = CalculateSimilarity(source, target);

            return result;
        }

        /// <summary>
        /// Calculate percentage similarity of two strings
        /// <param name="source">Source String to Compare with</param>
        /// <param name="target">Targeted String to Compare</param>
        /// <returns>Return Similarity between two strings from 0 to 1.0</returns>
        /// </summary>
        private double CalculateSimilarity(string source, string target)
        {
            if ((source == null) || (target == null)) return 0.0;
            if ((source.Length == 0) || (target.Length == 0)) return 0.0;
            if (source == target) return 1.0;

            int stepsToSame = LevenshteinDistance(source, target);
            return (1.0 - ((double)stepsToSame / (double)Math.Max(source.Length, target.Length)));
        }

        private int LevenshteinDistance(string source, string target)
        {
            if (source == target) return 0;
            if (source.Length == 0) return target.Length;
            if (target.Length == 0) return source.Length;

            int[] v0 = new int[target.Length + 1];
            int[] v1 = new int[target.Length + 1];

            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < source.Length; i++)
            {
                v1[0] = i + 1;

                for (int j = 0; j < target.Length; j++)
                {
                    var cost = (source[i] == target[j]) ? 0 : 1;
                    v1[j + 1] = Math.Min(v1[j] + 1, Math.Min(v0[j + 1] + 1, v0[j] + cost));
                }

                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[target.Length];
        }

    }
}
