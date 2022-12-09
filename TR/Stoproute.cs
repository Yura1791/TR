using System;
using System.Collections.Generic;

namespace TR
{
    public class Stoproute
    {
        public int Id { get; set; }
        public string NameStop { get; set; }
        public int Routeid { get; set; }

        public Route Route { get; set; }
    }
}
