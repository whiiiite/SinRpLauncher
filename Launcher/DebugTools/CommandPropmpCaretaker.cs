using SinRpLauncher.Debug;
using SinRpLauncher.PetternsInterfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SinRpLauncher.DebugTools
{
    /// <summary>
    /// Class for pattern 'Snapshot'
    /// <br/>
    /// Contains state of class <see cref="DeveloperConsole"/>, command prompt
    /// </summary>
    public class CommandPromptCaretaker
    {
        private List<IMemento> _states;
        private int currentPtr;

        private DeveloperConsole _devConsole;

        public CommandPromptCaretaker(DeveloperConsole devConsole)
        {
            _states = new List<IMemento>();
            _devConsole = devConsole;
            currentPtr = 0;
        }


        /// <summary>
        /// Save the current state
        /// </summary>
        public void BackUpState()
        {
            _states.Add(_devConsole.SavePromptState());
            currentPtr = _states.Count - 1;
        }


        /// <summary>
        /// Restore stated from down to up
        /// </summary>
        public void UndoStateUp()
        {
            if (currentPtr < 0) return;

            IMemento memento = _states[currentPtr];
            currentPtr--;

            try
            {
                _devConsole.RestorePromptState(memento);
            }
            catch (Exception)
            {
                UndoStateUp();
            }
        }


        /// <summary>
        /// Restore states from up to down
        /// </summary>
        public void UndoStateDown()
        {
            if (currentPtr >= _states.Count - 1) return;

            currentPtr++;
            IMemento memento = _states[currentPtr];

            try
            {
                _devConsole.RestorePromptState(memento);
            }
            catch (Exception)
            {
                UndoStateDown();
            }
        }
    }
}
