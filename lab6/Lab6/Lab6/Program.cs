using System;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Zad4();
        }




        static void Zad1()
        {
            Console.WriteLine("Zad1:");
            var krotka = (Imie: "Filip", Nazwisko: "Strózik", Wiek: 22, Płaca: 3000);
            WriteTuple(krotka);
            Console.WriteLine("------------------");
        }

        //TODO no chyba sie nie da bo to zarezerwowane słowo kluczowe?
        static void Zad2()
        {
            Console.WriteLine("Zad2:");
            /*string class = "hhe";*/
/*            Console.WriteLine(class);
*/
            Console.WriteLine("------------------");
        }


        static void Zad3()
        {
            Console.WriteLine("Zad3:");
            UseArrays();
            Console.WriteLine("------------------");
        }

        static void Zad4()
        {
            Console.WriteLine("Zad4:");

            var anonTyp = new { imie = "Filip", nazwisko = "Strózik", wiek = 22, placa = 3000 };
            WriteAnonymousType(anonTyp);


            Console.WriteLine("------------------");
        }

        /*Określenie typu zmiennej jako dynamic oznacza, że wszelkie
        operacje na danej zmiennej będą sprawdzane dopiero
        podczas wykonania*/

        //TODO czy to odpowiada na pytanie?
        private static void WriteAnonymousType(dynamic anon) 
        {
            var anonTypeInside = new { anon.imie, anon.nazwisko, anon.wiek, anon.placa };

            System.Console.WriteLine($"1) Pracownik: {anonTypeInside.imie} {anonTypeInside.nazwisko} w wieku {anonTypeInside.wiek} lat, zarabia {anonTypeInside.placa}");
        }

        static void Zad5()
        {
            Console.WriteLine("Zad5:");
            DrawCard("Kuba", "Rama", "?", 2, 20);
            DrawCard("Kuba");
            DrawCard(imie: "Filip", znak: "@", border: 2, width: 30, nazwisko: "Strózik");
            DrawCard("Jan", znak: "#", width: 30);
            DrawCard("Olek", "Lenski", width: 14, znak: "O", border: 2);
            Console.WriteLine("------------------");
        }

        //TODO implement this
        static void Zad6()
        {
            Console.WriteLine("Zad6:");
            UseArrays();
            Console.WriteLine("------------------");
        }



        private static void UseArrays()
        {
            int[] nums = new int[10] { 10, 15, 16, 8, 6, 25, 27, 37, 48, -2};
            Array.ForEach(nums, n => Console.Write($"{n} "));
            Console.WriteLine();

            Array.Reverse(nums);
            Array.ForEach(nums, n => Console.Write($"{n} "));
            Console.WriteLine();

            Array.Sort(nums);
            Array.ForEach(nums, n => Console.Write($"{n} "));
            Console.WriteLine();

            int SearchedIndex = Array.BinarySearch(nums, 10);
            Console.WriteLine(SearchedIndex);

            bool allPositive = Array.TrueForAll(nums, n => n > 0);
            Console.WriteLine(allPositive);

            Array.Fill(nums, 1);
            Array.ForEach(nums, n => Console.Write($"{n} "));
            Console.WriteLine();

            allPositive = Array.TrueForAll(nums, n => n > 0);
            Console.WriteLine(allPositive);
        }

        private static void WriteTuple((string Imie, string Nazwisko, int Wiek, int Płaca) krotka)
        {
            /* 1) Przepisywanie krotki do osobno deklarowanych zmiennych*/
            (string imie1, string nazwisko1, int wiek1, int płaca1) = krotka;
            System.Console.WriteLine($"1) Pracownik: {imie1} {nazwisko1} w wieku {wiek1} lat, zarabia {płaca1}");

            /*2) Przepisywanie krotki do osobno deklarowanych wcześniej zmiennych*/
            string imie2, nazwisko2;
            int wiek2, placa2;
            (imie2, nazwisko2, wiek2, placa2) = krotka;
            System.Console.WriteLine($"2) Pracownik: {imie2} {nazwisko2} w wieku {wiek2} lat, zarabia {placa2}");

            /*3) Przepisywanie krotki do osobno deklarowanych zmiennych z niejawnie określonym typem*/
            (var imie3, var nazwisko3, var wiek3, var placa3) = krotka;
            System.Console.WriteLine($"3) Pracownik: {imie3} {nazwisko3} w wieku {wiek3} lat, zarabia {placa3}");

            (string Im, string Nazw, int Wi, int Pl) krotka4 = krotka;
            System.Console.WriteLine($"4) Pracownik: {krotka4.Im} {krotka4.Nazw} w wieku {krotka4.Wi} lat, zarabia {krotka4.Pl}");


            System.Console.WriteLine($"4) Pracownik: {krotka.Imie} {krotka.Nazwisko} w wieku {krotka.Wiek} lat, zarabia {krotka.Płaca}");
        }

        private static void DrawCard(string imie, string nazwisko = "Kowalski", string znak = "X", int border = 1, int width = 12)
        {
            if (border > (width / 2) - (imie.Length / 2) || (width - border) <= (width / 2) + (imie.Length / 2)) return; // nie narysuje sie
            DrawHorizontalLines(border, width, znak);
            DrawCenter(imie, border, width, znak);
            DrawCenter(nazwisko, border, width, znak);
            DrawHorizontalLines(border, width, znak);
        }

        private static void DrawCenter(string text, int border, int width, string znak)
        {
            int putTextIndex = (width / 2) - (text.Length / 2);
            for (int i = 0; i < width; i++)
            {
                if (i < border)
                {
                    Console.Write(znak);
                }
                else if (i < putTextIndex)
                {
                    Console.Write(" ");
                }
                else if (i == putTextIndex)
                {
                    Console.Write(text);
                    i += (text.Length - 1);
                }
                else if (i < width - border)
                {
                    Console.Write(" ");
                }
                else if (i < width)
                {
                    Console.Write(znak);
                }
            }
            Console.WriteLine();
        }

        private static void DrawHorizontalLines(int border, int width, string znak)
        {
            for (int i = 0; i < border; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(znak);
                }
                Console.WriteLine();
            }
        }

    }


}
