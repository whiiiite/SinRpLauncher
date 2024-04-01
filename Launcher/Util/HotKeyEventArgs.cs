using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SinRpLauncher.Classes
{
    public class HotKeyEventArgs
    {
        private int _opcode;
        public int Opcode 
        {
            get { return _opcode; } 
        }


        private string _eventStr;
        public string EventStr
        {
            get { return _eventStr; }
        }

        public HotKeyEventArgs(int opcode, string eventStr) 
        {
            _opcode = opcode;
            _eventStr = eventStr;
        }


        /// <summary>
        /// Get specified list of args for hot key event and parse it
        /// </summary>
        /// <param name="toParseData"></param>
        /// <exception cref="ArgumentException"></exception>
        public HotKeyEventArgs(List<string> toParseData)
        {
            if (toParseData.Count != 2)
                throw new ArgumentException("List for parse is not valid");

            _eventStr = toParseData[0].Replace(" ", "");
            _opcode = int.Parse(toParseData[1]);
        }
    }
}
