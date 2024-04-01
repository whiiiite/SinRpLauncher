using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinRpLauncher.PetternsInterfaces;

namespace SinRpLauncher.DebugTools
{
    internal class CommandPromptMemento : IMemento
    {
        private readonly string _state;

        public CommandPromptMemento(string state)
        {
            _state = state;
        }

        public string GetState()
        {
            return _state;
        }
    }
}
