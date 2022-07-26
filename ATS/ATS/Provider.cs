using System;
using System.Collections.Generic;
using System.Text;

namespace ATS
{
    class Provider
    {
        public delegate void ProviderHandler(string message);
        public static event ProviderHandler Notify;
     //   public string NameTreaty { get; set; }
        static List<string> FirstName = new List<string> { "Alex", "Bob", "Alice", "Mari" };
        static List<string> SecondName = new List<string> { "SMITH", "JOHNSON", "WILLIAMS", "BROWN" };
        static List<string> Terminals = new List<string> { "Apple", "Samsung", "Sony", "LG" };
        static List<string> Tariff = new List<string> { "Economy", "Everyday", "Children's" };


        public static void Creation()
        {
            var allAbonents = new List<Abonent> { };       
            allAbonents = DBFile<Abonent>.Read("Abonents");  
            var _random = new Random();
            var _numberPhone = new Number() { NumberPhone = _random.Next(10000000, 99999999) };
            var abonent = new Abonent()
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName[_random.Next(FirstName.Count)],
                SecondName = SecondName[_random.Next(SecondName.Count)],
                AbonentPort = new Port()                
                {
                    NumberPort=_random.Next(1000, 9999),
                    Connection=true
                },
                AbonentTerminal = new Terminal() 
                {
                    Name = Terminals[_random.Next(Terminals.Count)],
                    NumberInTerminal = _numberPhone 
                },
                Number = _numberPhone,
                AbonentTariff=new Tariff()
                {
                    Name= SecondName[_random.Next(SecondName.Count)],
                    Price= Math.Round(_random.NextDouble()/10,2)
                },
                Balance=0

            };
            allAbonents.Add(abonent);
            DBFile<Abonent>.Write(allAbonents, "Abonents");
            Notify?.Invoke($"Subscriber created: ID:{ abonent.Id}  First Name:{ abonent.FirstName} Second Name:{ abonent.SecondName} Number: { abonent.Number.NumberPhone} Port: { abonent.AbonentPort.NumberPort} Terminal: { abonent.AbonentTerminal.Name}  Balance:{abonent.Balance}");


        }
    }
}
