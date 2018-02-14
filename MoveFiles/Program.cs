using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            Projects s = new Projects();
            s.RunAndMove();
        }
    }
}
