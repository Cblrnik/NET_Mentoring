using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas
{
    public static class WordWrap
    {
        public static string Wrap(string stringToWrap, params string[] splitters)
        {
            if (stringToWrap == null)
            {
                throw new ArgumentNullException(nameof(stringToWrap));
            }
            if (splitters == null)
            {
                return stringToWrap;
            }

            var count = GetCountOfNonSpaceSplitters(ref splitters);

            var wrapedString = stringToWrap;
            for (int i = 0; i < count; i++)
            {
                wrapedString = wrapedString.Replace($"{splitters[i]} ", $"{splitters[i]}\n");
            }

            return wrapedString;
        }

        public static int GetCountOfNonSpaceSplitters(ref string[] splitters)
        {
            if (splitters == null)
            {
                return 0;
            }

            var newSplitters = new List<string>();
            for (int i = 0; i < splitters.Length; i++)
            {
                if (!splitters[i].Contains(' ') && !splitters[i].Contains('\n'))
                {
                    newSplitters.Add(splitters[i]);
                }
            }

            splitters = newSplitters.ToArray();

            return splitters.Length;
        }
    }
}
