using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticis_Project
{
    class Program
    {
        static void read(double[,] a)
        {
            for(int i = 0; i < a.GetLength(0); i++)
            {
                Console.WriteLine("Equation {0}..",i+1);
                for (int k = 0; k < a.GetLength(1); k++)
                {
                    R:
                    if(k < a.GetLength(1)-1)
                        Console.Write(" X{0}..",k+1);
                    else
                        Console.Write(" a{0}..", i+1);
                    try
                    {
                        a[i, k] = double.Parse(Console.ReadLine());
                    }
                    catch { goto R; }
                }

            }
        }

        static void print(double[,] p)
        {
            int n = p.GetLength(0),
                m = p.GetLength(1);
            string s;
            for (int i = 0; i < n; i++)
            {
                Console.Write("|");
                for (int j = 0; j < m; j++)
                {
                    s = Convert.ToString(p[i, j]);
                    for (int q = 0; q < 4 - s.Length; q++)
                        Console.Write(" ");

                    Console.Write(p[i, j] + " ");
                }
                Console.WriteLine("  |");
            }
            Console.WriteLine("\n");
        }

        static void Excange(double[,] a, int f, int s)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                swap(ref a[f, j], ref a[s, j]);
            }

            Console.WriteLine("Swap R{0} , R{1}", f+1, s+1);
            print(a);
        }

        static void swap(ref double x,ref double y)
        {
            double temp = x;
            x = y;
            y = temp;
        }

        static void div_rowbycons(double[,] a, int r, double c)
        {
                if (c != 0)
                {
                  for (int j = 0; j < a.GetLength(1); j++)
                  {
                      a[r, j] *= (1/c);
                  }
                }
                Console.WriteLine("R{0} = R{0} / {1}",r+1 , c+1);
            print(a);
        }

        static void Add_muledrowTOother(double[,] a, int sender, double c, int resever)
        {
                for (int j = 0; j < a.GetLength(1); j++)
                a[resever, j] += a[sender, j] * c;

            Console.WriteLine("R{0} = {1}R{2} +  R{0}", resever+1, c+1, sender+1);
            print(a);
        }

        static void Nosolution(double[,] a, int r)
        {
            for(int i=0; i< a.GetLength(1); i++)
            {
                if (i < a.GetLength(1) - 1)
                    Console.Write("+ ("+a[r, i] + ")X{0} ", i+1);
                else
                    Console.Write(" = "+a[r, i]);
            }
            Console.WriteLine("  Is Impossible\n");
        }

        static void printsolution(double[,] a, int c)
        {
            for(int i = 0; i < a.GetLength(0); i++)
            {
                Console.WriteLine("x{0} = {1} .",i+1,a[i,c]);
            }
        }

        static void Solver()
        {
            int equations;
            do
            {
            w:
                Console.Write("Number of Equations  ");
                try { equations = int.Parse(Console.ReadLine()); } catch { goto w; }
            } while (equations <= 0);

            double[,] test = new double[equations, equations + 1];
            read(test);
            Console.WriteLine("\nyour system is...");
            print(test);

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            int c = 0;
            do
            {
                bool nonzero = true;

                if (test[c, c] == 0)
                {
                    nonzero = false;
                    //find non zero elemnt
                    for (int f = c + 1; f < test.GetLength(0); f++)
                    {
                        if (test[f, c] != 0)
                        {
                            nonzero = true;
                            Excange(test, f, c);
                        }
                    }
                }

                if (nonzero)
                {
                    div_rowbycons(test, c, test[c, c]);

                    // make down zeroooos
                    for (int i = c + 1; i < test.GetLength(0); i++)
                    {
                        if (test[i, c] != 0)
                        {
                            Add_muledrowTOother(test, c, -test[i, c], i);
                        }
                    }
                }
                c++;
            } while (c < test.GetLength(1) - 1);
            Console.WriteLine("^_^  ^_^  ^_^\n\n");
            //////////////////////////////////////////////////////////////////////////////////////////////////////

            do
            {
                c--;
                // make up zeroooos
                for (int i = c - 1; i >= 0; i--)
                {
                    if (test[c, c] == 0 && test[c,test.GetLength(0)] != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\aSystem have no solution since");
                        Nosolution(test, c);    //////NO SOLUTION
                        Console.ForegroundColor = ConsoleColor.White;
                        c = -5;
                        break;
                    }
                    else if(test[c, c] == 0 && test[c, test.GetLength(0)] == 0)
                    {
                        Console.WriteLine("system have infinity numer of solutions");
                        c = -5;
                        break;
                    }
                    else if(c > 0 && test[i, c] != 0)
                    {
                        Add_muledrowTOother(test, c, -test[i, c], i);
                    }
                }
            } while (c >= 0);
            print(test);

            if (c == -1)
            {
                c = test.GetLength(1) - 1;
                printsolution(test, c);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Linear Equation System Solver by Gauss Gurdan Method");
            Solver();

            char k = 'a';
            do
            {
                if (k == 's')
                    Solver();

               t: Console.WriteLine("Press:\n(S) solve ather system\n(E) exit");
                try { k = char.Parse(Console.ReadLine()); } catch { goto t; }

            } while (k != 'e');
        }
    }
}
