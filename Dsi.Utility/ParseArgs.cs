using System;
using System.Collections.Generic;

namespace Dsi.Utility {

    public class ProcessArgs {

        private static string FlagSwitch = "--";

        public static IDictionary<string, string> Parse(string[] args) {
            var dictionary = new Dictionary<string, string>();
            var argList = new List<string>(args);
            string key = string.Empty;
            foreach(var arg in argList) {
                if(arg.StartsWith(FlagSwitch)) {
                    key = arg;
                    dictionary.Add(arg, null);
                    continue;
                }
                // not a switch
                if(!dictionary.ContainsKey(key)) {
                    throw new Exception($"No key for value {arg}");
                }
                dictionary[key] = arg;
            }
            return dictionary;
        }
    }
}
