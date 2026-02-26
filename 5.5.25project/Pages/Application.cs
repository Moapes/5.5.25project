using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Server_temp.Pages
{
    public static class Application
    {
        private static int _visitorCounter = 0;

        public static int VisitorCounter
        {
            get
            {
                return _visitorCounter;
            }
        }

        public static void IncrementVisitor()
        {
            _visitorCounter++;
        }

        public static void DecrementVisitor()
        {
            if (_visitorCounter > 0)
                _visitorCounter--;
        }
    }
}

