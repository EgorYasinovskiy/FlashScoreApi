using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_FlashScore.Extensions
{
    public static class StringToCommandExtensions
    {
        public static Enums.Command ToCommand(this string recievedMessage)
        {
            if(recievedMessage.Equals("/start"))
            {
                return Enums.Command.start;
            }
            else if(recievedMessage.Equals("/stop"))
            {
                return Enums.Command.stop;
            }
            return Enums.Command.unknown;
        }
    }
}
