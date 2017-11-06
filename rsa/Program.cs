using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rsa
{
    class Program
    { 
        public struct Key
        {
            public uint keynum;
            public ulong n;
        }
        static void Main(string[] args)
        {
            //2 больших и простых числа p и q, и d 
            uint p = 7;
            uint q = 4;
            Random rnd = new Random();
            uint d = Rsa.Calculate_d(p, q);
            Console.WriteLine("d : " + d);
            ulong n = p * q;

            //шифруемый текст 
            Console.Write("Type text: ");
            string startText = Console.ReadLine();
            string[] M = new string[n - 1];
            string[] C = new string[n - 1];
            string[] M1 = new string[n - 1];

            //генерация ключей
            Key openkey = Rsa.GenerateOpenKey(n, d, p, q);
            Key secretkey = Rsa.GenerateSecretKey(n, d);

            ulong lengthOfBlock = (ulong)(startText.Length) / n + 1;

            //разделение текста на блоки
                if(startText.Length<M.Length)
                {
                    for (int i = 0; i < startText.Length - 1; i++)
                    {
                        M[i] = startText.Substring(i, (int)lengthOfBlock);
                    }
                Array.Resize<string>(ref M, startText.Length);
                }
                else
                {
                    //пока не рабоатет
                    for (int i = 0; i < M.Length - 2; i++)
                    {
                        M[i] = startText.Substring(i, (int)lengthOfBlock);
                    }
                }


            //шифрование
            Console.Write("Encode: ");
            C = Rsa.Encode(M, openkey);
            for(int i = 0; i<C.Length-1; i++)
            {
                if (C[i] != null)
                {
                    Console.Write(C[i]+" ");
                }
            }

            //Дешифровка
            Console.Write("Decode: ");
            M1 = Rsa.Decode(C, secretkey);
            for (int i = 0; i < M1.Length - 1; i++)
            {
                if (M1[i] != null)
                {
                    Console.Write(M1[i]+ " ");
                }
            }

            Console.ReadKey();
        }
    }
}
