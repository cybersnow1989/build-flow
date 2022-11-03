using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow.Helpers
{
    public static class Validators
    {
        public static bool CheckIfZeroOrNegative(int value)
        {
            if (value <= 0)
                return false;
            else
                return true;
        }

        public static bool CheckIfZeroOrNegative(decimal value)
        {
            if (value <= 0)
                return false;
            else
                return true;
        }
    }
}
