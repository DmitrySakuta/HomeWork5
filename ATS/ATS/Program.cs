using System;
using System.Collections.Generic;

namespace ATS
{
    class Program
    { 
        static void Main(string[] args)
        {
            try
            {
                var listAbonents = DBFile<Abonent>.Read("Abonents");
                var randomAbonentNum = new Random().Next(listAbonents.Count);
                Abonent yourAbonent = listAbonents[randomAbonentNum];
                yourAbonent.Notify += ShowMessage;
                Provider.Notify += ShowMessage;
                StartWork(yourAbonent, randomAbonentNum, listAbonents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
       static void ShowMessage(string message) => Console.WriteLine(message);
            static byte Anser(string menuConsole, int sizeMenu)
            {
                byte numberMenu;
                var flag = false;
                do
                {
                    Console.WriteLine(menuConsole);
                    Console.WriteLine("Please, enter a number!");
                    flag = byte.TryParse(Console.ReadLine(), out numberMenu);
                }
                while (sizeMenu < numberMenu || numberMenu < 1 || !flag);
                return numberMenu;
            }

        public static void StartWork(Abonent yourAbonent, int randomAbonentNum, List<Abonent> list)
        {
            var anser = "";        
            Console.WriteLine("Your abonent: " + yourAbonent.Info());       
            var menuConsole = @$"What do you want to do?
1-Call someone
2-View calls filter by date
3-View calls, filter by cost
4-View calls, filter by subscriber
5-Create subscriber
6-Show all subscribers";


            switch (Anser(menuConsole, 6))
            {
                case 1: 
                    yourAbonent.Calling(list[new Random().Next(list.Count)], DateTime.Now);
                    list.RemoveAt(randomAbonentNum);
                    list.Add(yourAbonent);
                    DBFile<Abonent>.Write(list, "Abonents");
                    break;
                case 2:
                    Calls.InfoByDate(yourAbonent);
                    break;
                case 3:
                    Calls.InfoByCost(yourAbonent);
                    break;
                case 4:
                    Calls.InfoByAbonent(yourAbonent);
                    break;
                case 5:
                    Provider.Creation();
                    break;
                case 6:
                    foreach (var abonent in list)
                        Console.WriteLine(abonent.Info());
                    break;
            }
            do
            {
                Console.WriteLine("Do you wanna repeat? Enter Y/N.");
                anser = Console.ReadLine();
                anser = anser.ToUpper();
            }
            while (anser != "Y" && anser != "N");
            if (anser == "Y") StartWork(yourAbonent, randomAbonentNum, list);
            else Console.WriteLine("The job is done!");
        }
        }
}
