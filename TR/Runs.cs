using System;
using System.Collections.Generic;

namespace TR
{
    public class Runs
    {
        public int Id { get; set; }
        public TimeOnly RunStart { get; set; }
        public TimeOnly RunEnd { get; set; }
        public int Routeid { get; set; }
        public int Busid { get; set; }
        public int TicketPrice { get; set; }
        public DateOnly Date { get; set; }

        public Bus Bus { get; set; }
        public Route Route { get; set; }
    }
}
