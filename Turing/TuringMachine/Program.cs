using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuringMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Maquina maquina = new Maquina();
            maquina.start(args[0], args[1], args[2]);
            //maquina.start("C:\\Users\\Matheus Bento\\Desktop\\Turing Machine\\Turing Machine\\Turing\\TuringMachine\\obj\\Release\\entrada\\duplobal.mt", "C:\\Users\\Matheus Bento\\Desktop\\Turing Machine\\Turing Machine\\Turing\\TuringMachine\\obj\\Release\\entrada\\duplobal.in", "C:\\Users\\Matheus Bento\\Desktop\\Turing Machine\\Turing Machine\\Turing\\TuringMachine\\obj\\Release\\entrada\\duplobal.out");
            Console.ReadLine();
        }
    }
}
