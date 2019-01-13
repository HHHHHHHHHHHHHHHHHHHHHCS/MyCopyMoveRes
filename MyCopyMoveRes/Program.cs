using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CopyMoveRes
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new CopyMoveDir();
            program.CopyAndMove();

            Thread.Sleep(5*1000);
        }
    }
}
