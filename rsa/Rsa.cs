using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace rsa
{
    class Rsa
    {
        static public Program.Key GenerateOpenKey(ulong n, uint d, uint p, uint q)
        {
            Program.Key key;
            key.n = n;
            key.keynum = Calculate_e(d, p, q);
            return key;
        }

        static public Program.Key GenerateSecretKey(ulong n, uint d)
        {
            Program.Key key;
            key.n = n;
            key.keynum = d;
            return key;
        }
        static public string[] Encode(string[] M, Program.Key openkey)
        {
            //C(i)=(M(I)^e)mod n, где bi=M(i)^e
            Console.Write("Open: " + openkey.keynum + "|");
            string[] C = new string[openkey.n-1];
            BigInteger bi;
            BigInteger n_ = (BigInteger)openkey.n;
            for(int i = 0; i < M.Length; i++)
            {
                bi = (BigInteger)Math.Pow(i, openkey.keynum);
                C[i] = (bi % n_).ToString();
            }
            return C ;
        }

        static public string[] Decode(string[] C, Program.Key secretkey)
        {
            Console.Write("Secret: " + secretkey.keynum +"|");
            string[] M = new string[secretkey.n - 1];
            BigInteger bi;
            BigInteger n_ = (BigInteger)secretkey.n;
            for (int i = 0; i < M.Length; i++)
            {
                bi = (BigInteger)Math.Pow((Convert.ToInt32(C[i])), secretkey.keynum);
                M[i] = (bi % n_).ToString();
            }
            return M;
        }
        static uint Calculate_e(uint d, uint p, uint q)
        {
            uint e = 0;
            while (true)
            {
                if ((e * d) % ((d-1)*(q-1)) == 1)
                    break; 
                else
                    e++;
            }
            return e;
        }
        static public uint Calculate_d(uint p, uint q)
        {
            uint d = (p-1)*(q-1) - 1;

            for (uint i = 2; i <= (p - 1) * (q - 1); i++)
                if (((p - 1) * (q - 1) % i == 0) && (d % i == 0)) //если имеют общие делители
                {
                    d--;  //iter
                    i = 1;
                }

            return d;
        }
    }
}
