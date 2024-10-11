// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

class Program
{
    static void Main()
    {
        // Lees de invoerwaarden in als BigInteger
        BigInteger m = BigInteger.Parse(Console.ReadLine());
        BigInteger p = BigInteger.Parse(Console.ReadLine());
        BigInteger q = BigInteger.Parse(Console.ReadLine()); 
        BigInteger a =  BigInteger.Parse(Console.ReadLine());

        // Bereken de wortels van a modulo p en q
        BigInteger r = BigInteger.ModPow(a, ((p + 1) / 4), p); // Dit is een wortel van a modulo p
        BigInteger s = BigInteger.ModPow(a, ((q + 1) / 4), q); // Dit is een wortel van a modulo q
        
        // Bereken de waarden van wp en wq met EE
        var wgroups = ExtendedEuclides(p, q);
        BigInteger wq = (wgroups.Item1 % m + m) % m; //ik wil geen negatieve waardes dus ik doe eerst +m om de negatieve waardes weg te werken.
        BigInteger wp = (wgroups.Item2 % m + m) % m; //ik wil geen negatieve waardes dus ik doe eerst +m om de negatieve waardes weg te werken.
        
        //chinese reststelling 
        List<BigInteger> wortels = new List<BigInteger>
        {
            ((r*wp + s*wq) % m + m) % m, 
            ((-r*wp + s*wq) % m + m)% m,
            ((r*wp - s*wq) % m + m) % m,
            ((-r*wp - s*wq) % m + m) % m
        };
        
        //sorteer en print de wortels
        wortels.Sort();
        foreach (BigInteger wortel in wortels)
        {
            Console.WriteLine(wortel);
        }
    }
    static Tuple<BigInteger, BigInteger> ExtendedEuclides(BigInteger a, BigInteger b)
    {
        //volgensmij hoeft dit niet maar als de volgorde verkeerd is dan wissel ik ze om
        if (a < b)
        {
            (b, a) = (a, b);
        }

        //beginwaarden
        BigInteger a0 = a;
        BigInteger b0 = b;
        
        //restwaarde als deze 0 is dan is de vorige waarde de gcd
        BigInteger c = a % b;
        
        //lijsten om de waarden van x en y op te slaan
        List<BigInteger> xs = new List<BigInteger>() { 1, 0 };
        List<BigInteger> ys = new List<BigInteger>() { 0, 1 };
        
        //quotient
        BigInteger q = a / b;
        
        //bereken de waarden van x en y
        for (int i = 2; c != 0; i++)
        {
            q = a / b;
            xs.Add(xs[i - 2] - ((int)q * xs[i - 1]));
            ys.Add(ys[i - 2] - ((int)q * ys[i - 1]));
            
            a = b;
            b = c;
            c = a % b;
        }
        //geef de laats berekende waarden factoren terug dit zijn Wq en Wp
        return Tuple.Create(b0 * ys.Last(), a0 * xs.Last());
    }
}






