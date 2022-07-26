using System;
using System.Collections.Generic;
using System.Text;

namespace ATS
{
   
    class Abonent
    {
        public delegate void AbonentHandler(string message);
        public event AbonentHandler Notify;
        public Guid Id { get; set; }
        public Number Number { get; set; }
        public string  FirstName { get; set; }
        public string SecondName { get; set; }
        public Terminal AbonentTerminal { get; set; }
        public Port AbonentPort { get; set; }
        public Tariff AbonentTariff { get; set; }
        public double Balance { get; set; }
     
        private void Busy()
        {
            this.AbonentPort.Connection = false;
            Notify?.Invoke($"Subscriber number {this.Number.NumberPhone} is is offline");
        }
            private void Free()
        {
            this.AbonentPort.Connection = true;
            Notify?.Invoke($"Subscriber number {this.Number.NumberPhone} is online again");
        }
       

        public void Calling(Abonent incomingAbonent, DateTime dateTime)
        {
            if (incomingAbonent.AbonentPort.Connection)
            {
                Busy();
                var allCalls = new List<Calls> { };
                allCalls = DBFile<Calls>.Read("Calls");
                var minutes = new Random().Next(1, 60);
                var sum = Math.Round(AbonentTariff.Price * minutes,2);
                Balance -= sum;
                var call = new Calls()
                {
                    Abonent = this,
                    IncomingAbonent = incomingAbonent,
                    Date = dateTime,
                    Price = sum,
                    Time = minutes
                };
                allCalls.Add(call);
                DBFile<Calls>.Write(allCalls, "Calls");
                Notify?.Invoke($"The call to the subscriber number {this.Number.NumberPhone} is completed, the conversation time is {minutes} minutes and the cost is ${sum}, the debt is ${Balance}");
                this.AbonentPort.Connection = true;
                Free();
            }
            
        }
        public string Info() => $"ID:{this.Id} First Name:{this.FirstName} Second Name:{this.SecondName} Number:{this.Number.NumberPhone} Port:{this.AbonentPort.NumberPort} Terminal:{this.AbonentTerminal.Name}  Balance:{this.Balance}";
    }
}
