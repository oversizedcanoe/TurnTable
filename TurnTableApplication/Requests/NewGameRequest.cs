using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnTableBase;

namespace TurnTableApplication.Requests
{
    public class NewGameRequest
    {
        public GameType GameType { get; set; }

        public string PlayerOneName { get; set; } = string.Empty;
    }
}
