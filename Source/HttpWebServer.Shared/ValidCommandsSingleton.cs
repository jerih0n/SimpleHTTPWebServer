
namespace HttpWebServer.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ValidCommandsSingleton
    {
        private static ValidCommandsSingleton _instance;
        private SortedDictionary<string, string> _allValidCommands;

        private ValidCommandsSingleton(SortedDictionary<string,string> validCommands )
        {
            _allValidCommands = validCommands;
        }      
        public static ValidCommandsSingleton Innstance()
        {
            if(_instance == null)
            {
                SortedDictionary<string, string> commnads = new SortedDictionary<string, string>();
                commnads.Add("-help", "");
                _instance = new ValidCommandsSingleton(commnads);
                
            }
            return _instance;
                
        }
        /// <summary>
        /// Return sorted dictionary containing key - value pair. Key is command, Value is the response that should be printed on the console
        /// </summary>
        /// <returns></returns>
        public SortedDictionary<string,string> GetAllCommands()
        {
            return this._allValidCommands;
        }
            
    }
}
