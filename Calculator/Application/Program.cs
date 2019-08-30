using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            c.Run();
        }
    }

    class Calculator
    {
        public Calculator()
        {
            Console.WriteLine("===== simple console calculator initiated");
        }

        ~Calculator()
        {
            Console.WriteLine("===== good bye :D");
        }

        public void Run()
        {
            Console.WriteLine("===== enter \"Q\" to quit the program");
            Console.WriteLine("===== usage example #1: enter \"1 + 2\" for addition");
            Console.WriteLine("===== usage example #2: enter \"3 - 1\" for subtraction");
            Console.WriteLine("===== usage example #3: enter \"2 * 3\" for multiplication");
            Console.WriteLine("===== usage example #4: enter \"5 / 1\" for division");

            while (true)
            {
                Console.Write("enter your input : ");
                string command = Console.ReadLine();

                if(command.Equals("Q"))
                {
                    break;
                }
                else
                {
                    Evaluation e = this.Parse(command);
                    if(e.Op != null)
                    {
                        int result = e.Op.Operate(e.First, e.Later);
                        Console.WriteLine("result is : " + result.ToString());
                    }
                }
            }
        }

        private Evaluation Parse(string command)
        {
            try
            {
                Evaluation e = new Evaluation();
                string[] parts = command.Split(' ');

                e.First = int.Parse(parts[0]);
                e.Later = int.Parse(parts[2]);
                switch (parts[1])
                {
                    case "+":
                        e.Op = new OperatorAdd();
                        break;

                    case "-":
                        e.Op = new OperatorSub();
                        break;

                    case "*":
                        e.Op = new OperatorMul();
                        break;

                    case "/":
                        e.Op = new OperatorDIv();
                        break;
                }

                return e;
            }
            catch
            {
                Console.WriteLine("wrong input !");
                return new Evaluation();
            }
        }

        struct Evaluation
        {
            public int First;
            public int Later;
            public Operator Op;
        }

        abstract class Operator
        {
            public abstract int Operate(int a, int b);
        }

        class OperatorAdd : Operator
        {
            public override int Operate(int a, int b)
            {
                return a + b;
            }
        }

        class OperatorSub : Operator
        {
            public override int Operate(int a, int b)
            {
                return a - b;
            }
        }

        class OperatorMul : Operator
        {
            public override int Operate(int a, int b)
            {
                return a * b;
            }
        }

        class OperatorDIv : Operator
        {
            public override int Operate(int a, int b)
            {
                try
                {
                    return a / b;
                }
                catch (System.DivideByZeroException e)
                {
                    Console.WriteLine("failed to divide for the denominator zero, error:" + e.ToString());
                    return 0;
                }
            }
        }
    }
}
