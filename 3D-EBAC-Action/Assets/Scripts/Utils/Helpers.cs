using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Helpers
    {
        public static string ConvertSnakeCaseToPascalCase(string snakeCaseString)
        {
            string[] words = snakeCaseString.Split('_');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
            return string.Join("", words);
        }
    }
    
}
