// See https://aka.ms/new-console-template for more information

using System.Numerics;

BigInteger m = BigInteger.Parse(Console.ReadLine());
BigInteger p = BigInteger.Parse(Console.ReadLine());
BigInteger q = BigInteger.Parse(Console.ReadLine()); 
int a = int.Parse(Console.ReadLine());

List<BigInteger> alleWortels = Wortels(m, p, q, a);
alleWortels.Sort();
foreach (BigInteger wortel in alleWortels)
{
    Console.WriteLine(wortel);
}
//BigInteger modp = BigInteger.ModPow(a, (p+1)/4,   m);
//Console.WriteLine($"modp: {modp}");
//BigInteger modq = BigInteger.ModPow(a, (q+1)/4,   m);
//Console.WriteLine($"modq: {modq}");

//Tuple <BigInteger,BigInteger> wgroups = ExtendedEuclides(p,q);
//BigInteger wp = (wgroups.Item1 % m + m) % m; //ik wil geen negatieve waardes dus ik doe eerst +m om de negatieve waardes weg te werken.
//BigInteger wq = (wgroups.Item2 % m + m) % m;

//Console.WriteLine("Wp = "+ wp);
//Console.WriteLine("Wq = "+ wq);



//Euclides(p, q);

static List<BigInteger> Wortels(BigInteger m, BigInteger p, BigInteger q, BigInteger a)
    {
        // Bereken de wortels modulo p en q
        BigInteger modp = BigInteger.ModPow(a, (p + 1) / 4, p);
        BigInteger modq = BigInteger.ModPow(a, (q + 1) / 4, q);

        // Bereken de multiplicatieve inversen van p en q
        var wgroups = ExtendedEuclides(p, q);
        BigInteger wp = (wgroups.Item1 % p + p) % p;
        BigInteger wq = (wgroups.Item2 % q + q) % q;

        // Bereken en voeg de vier wortels toe
        List<BigInteger> roots = new List<BigInteger>
        {
            MaakWortels(modp, modq, wp, wq, p, q, m),
            MaakWortels(modp, -modq, wp, wq, p, q, m),
            MaakWortels(-modp, modq, wp, wq, p, q, m),
            MaakWortels(-modp, -modq, wp, wq, p, q, m)
        };
        return roots;
    }

    static BigInteger MaakWortels(BigInteger modp, BigInteger modq, BigInteger wp, BigInteger wq, BigInteger p, BigInteger q, BigInteger m)
    {
        BigInteger x = (modp * wq * q + modq * wp * p) % m;
        return (x + m) % m; // Zorg ervoor dat x positief is
    }

    static Tuple<BigInteger, BigInteger> ExtendedEuclides(BigInteger a, BigInteger b)
    {
        if (a < b) (b, a) = (a, b);

        BigInteger x0 = 1, y0 = 0, x1 = 0, y1 = 1;
        while (b != 0)
        {
            BigInteger q = a / b;
            (a, b) = (b, a % b);
            (x0, x1) = (x1, x0 - q * x1);
            (y0, y1) = (y1, y0 - q * y1);
        }
        return Tuple.Create(x0, y0);
    }

static BigInteger Euclides(BigInteger a, BigInteger b)
{
    if (a < b)
    {
        (b, a) = (a, b);
    }

    BigInteger c = a % b;
    while (c != 0)
    {
        a = b;
        b = c;
        c = a % b;
    }

    Console.WriteLine($"De GGD is : {b}");
    return b;
}


