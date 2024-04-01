using Launcher.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Windows.Input;

namespace SinRpLauncher.Classes
{
    /// <summary>
    /// Class that provide logic for handle event over hotkeys in project
    /// </summary>
    public class HotKeyEvent
    {
        private const string keyUpStr = "key_up";
        private const string keyDownStr = "key_down";

        private string _eventStr;
        public string EventStr
        {
            get { return _eventStr; }
            set { _eventStr = value; }
        }

        public HotKeyEvent(HotKeyEventArgs args)
        {
            _eventStr = args.EventStr;   
        }


        /// <summary>
        /// Parse special formated event string
        /// </summary>
        /// <param name="eventStr"></param>
        /// <returns>If event is happening</returns>
        public bool EventIsHappend()
        {
            // raw data like: key_down:THE_KEY+key_down:THE_KEY...
            // (+ is optional additional key it can be single)

            string[] keys = _eventStr.Split('+');
            for (int i = 0; i < keys.Length; i++)
            {
                // index 0 - key down or up
                // index 1 - the key
                // raw data like: key_down:THE_KEY
                string[] dataKey = keys[i].Trim().Split(':');
                Key k = Utils.GetKeyByStringName(dataKey[1]);
                if (dataKey[0].ToLower() == keyDownStr && !Keyboard.IsKeyDown(k))
                    return false;
                else if (dataKey[0].ToLower() == keyUpStr && !Keyboard.IsKeyUp(k))
                    return false;
                else
                    continue;
            }

            return true;
        }
    }
}
