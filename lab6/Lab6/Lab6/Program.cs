using System;

namespace Lab6
{

    class Program
    {
        static void Main(string[] args)
        {
            Zad2();
        }


        static void Zad1()
        {
            Console.WriteLine("Zad1:");
            var krotka = (Imie: "Filip", Nazwisko: "Strózik", Wiek: 22, Płaca: 3000);
            WriteTuple(krotka);
            Console.WriteLine("------------------");
        }

        //TODO no chyba sie nie da bo to zarezerwowane słowo kluczowe?
        // moze chodzi o stworzenie zwyklej klasy...
        static void Zad2()
        {
            Console.WriteLine("Zad2:");
            /*string class = "hhe";*/
            /*            Console.WriteLine(class);
            */
            //var test = new {dynamic class = "filip"};

            //lub
            DataKeeper myDataKeeper = new DataKeeper();
            myDataKeeper.Data = "1234567890";
            Console.WriteLine(myDataKeeper.Data);
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

        static void Zad6()
        {
            Console.WriteLine("Zad6:");
            Console.WriteLine("Should be: (1, 0, 0, 0,");
            CountMyTypes(1, 2, 3, 5, 7, -2.4, -3.4, -4.5, "1234", "soss");
            Console.WriteLine("Should be: (2, 3, 0, 2,");
            CountMyTypes(1, 2, 3, 4, 5, 0.3, 3.4, 5.5, "sos", "what", false, true);
            Console.WriteLine("Should be: (0, 0, 0, 3,");
            CountMyTypes(new object[] { true, false, new int[] { 1, 2, 3 } });
            Console.WriteLine("Should be: (0, 0, 1, 0,");
            CountMyTypes("12345");
            Console.WriteLine("------------------");
        }



        /*Określenie typu zmiennej jako dynamic oznacza, że wszelkie
        operacje na danej zmiennej będą sprawdzane dopiero
        podczas wykonania*/

        private static void WriteAnonymousType(dynamic anon)
        {
            //tak sie nie da w łatwy sposob Cannot deconstruct dynamic type
            //(string imie1, string nazwisko1, int wiek1, int placa1) = anon; zatem ODP: da się ale nie jest to takie łatwe jak w dekonstrukcji krotki
            //tutaj trzeba niebezpiecznie dostać się do każdego atrybutu typu aninimowego osobno.

            //z przekazanego parametru:
            System.Console.WriteLine($"1) Pracownik: {anon.imie} {anon.nazwisko} w wieku {anon.wiek} lat, zarabia {anon.placa}");

            //do innego typu anonimowego
            var anonTypeInside = new { anon.imie, anon.nazwisko, anon.wiek, anon.placa };
            System.Console.WriteLine($"1) Pracownik: {anonTypeInside.imie} {anonTypeInside.nazwisko} w wieku {anonTypeInside.wiek} lat, zarabia {anonTypeInside.placa}");

        }
        private static (int, int, int, int) CountMyTypes(params object[] tab)
        {
            int evenInt = 0, positiveDouble = 0, atLeast5String = 0, others = 0;
            foreach (var elem in tab)
            {
                switch (elem)
                {
                    case int number when number % 2 == 0:
                        evenInt++;
                        break;
                    case int number when true:
                        break;
                    case double real when real > 0:
                        positiveDouble++;
                        break;
                    case string word when word.Length >= 5:
                        atLeast5String++;
                        break;
                    case object obj when (obj is not int) && (obj is not double) && (obj is not string):
                        others++;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine((evenInt, positiveDouble, atLeast5String, others));
            return (evenInt, positiveDouble, atLeast5String, others);
        }

        private static void UseArrays()
        {
            int[] nums = new int[10] { 10, 15, 16, 8, 6, 25, 27, 37, 48, -2 };
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
