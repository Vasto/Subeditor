using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Views.Menus.Encodings
{
    /// <summary>
    /// Klasa mapująca wyświetlane nazwy kodowań, do numeru ich stron kodowych.
    /// </summary>
    public static class EncodingDisplayNameMap
    {
        private static readonly Dictionary<int, String> codepageToNameMap;
        private static readonly Dictionary<String, int> nameToCodepageMap;

        static EncodingDisplayNameMap()
        {
            codepageToNameMap = new Dictionary<int, String>();
            nameToCodepageMap = new Dictionary<String, int>();

            InitializeCodepageToNameMap();
            InitializeNameToCodepageMap();
        }

        /// <summary>
        /// Pozwala pobrać numer strony kodowej dla wskazanej nazwy kodowania.
        /// </summary>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static int GetCodepageForName(String encodingName)
        {
            if (nameToCodepageMap.ContainsKey(encodingName))
            {
                return nameToCodepageMap[encodingName];
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Pozwala pobrać nzwe wyświetlaniaj dla wskazanego numeru strony kodowej.
        /// </summary>
        /// <param name="encodingCodepage"></param>
        /// <returns></returns>
        public static String GetNameForCodepage(int encodingCodepage)
        {
            if (codepageToNameMap.ContainsKey(encodingCodepage))
            {
                return codepageToNameMap[encodingCodepage];
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Zwraca wszystkie przechowywane nazwy.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<String> GetAllNames()
        {
            return codepageToNameMap.Values;
        }

        /// <summary>
        /// Zwraca wsyzystkie przechowywane numery stron kodowych.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetAllCodepages()
        {
            return nameToCodepageMap.Values;
        }

        private static void InitializeCodepageToNameMap()
        {
            codepageToNameMap.Add(1200, Resources.Encoding1200);
            codepageToNameMap.Add(65001, Resources.Encoding65001);
            codepageToNameMap.Add(1201, Resources.Encoding1201);
            codepageToNameMap.Add(1250, Resources.Encoding1250);
            codepageToNameMap.Add(1252, Resources.Encoding1252);

        }

        private static void InitializeNameToCodepageMap()
        {
            nameToCodepageMap.Add(Resources.Encoding1200, 1200);
            nameToCodepageMap.Add(Resources.Encoding65001, 65001);
            nameToCodepageMap.Add(Resources.Encoding1201, 1201);
            nameToCodepageMap.Add(Resources.Encoding1250, 1250);
            nameToCodepageMap.Add(Resources.Encoding1252, 1252);

        }
    }
}
