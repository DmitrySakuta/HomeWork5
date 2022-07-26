using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class Calls
    {
        public Abonent Abonent { get; set; }
        public Abonent IncomingAbonent { get; set; }
        public DateTime Date { get; set; }
        public double  Price { get; set; }
        public int Time { get; set; }
        public static void Info(Abonent abonent)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            select p;
            ConsoleOut(findCalls);
        }
        public static void InfoByDate(Abonent abonent, DateTime date1, DateTime date2) 
        {
            var listCalls= DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                          where p.Abonent.Id==abonent.Id
                          where p.Date >= date1
                          where p.Date <= date2
                          select p; 
                ConsoleOut(findCalls);
        }     
        public static void InfoByAbonent(Abonent abonent, Abonent incomingAbonent)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            where p.IncomingAbonent.Id == incomingAbonent.Id                          
                            select p;           
                ConsoleOut(findCalls);
        }
        public static void InfoByCost(Abonent abonent, double min, double max)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            where p.Price<= max
                            where p.Price >= min
                            select p;
                ConsoleOut(findCalls);
        }
        public static void InfoByDate(Abonent abonent)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            orderby p.Date
                            select p;            
                ConsoleOut(findCalls);
        }
        public static void InfoByAbonent(Abonent abonent)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            orderby p.IncomingAbonent.Id
                            select p;            
                ConsoleOut(findCalls);
        }
        public static void InfoByCost(Abonent abonent)
        {
            var listCalls = DBFile<Calls>.Read("Calls");
            var findCalls = from p in listCalls
                            where p.Abonent.Id == abonent.Id
                            orderby p.Price
                            select p;             
                ConsoleOut(findCalls);
        }
        public static void ConsoleOut(  IEnumerable<Calls> findCalls)
        {
            var flag = false;
            foreach (var call in findCalls)
            {
                Console.WriteLine($"Сall {call.IncomingAbonent.FirstName} Date: {call.Date} Duration:{call.Time} min Cost:{call.Price}$ ");
                flag = true;
            }
          if(!flag)
                Console.WriteLine("No calls!");
        }



    }
}
