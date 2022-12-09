using System;
using System.Collections.Generic;

namespace TR
{
    public class Bus
    {

        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Number { get; set; } = null!;
        public string Driver { get; set; } = null!;
        public int Seats { get; set; }

        public ICollection<Runs> Runs { get; set; }
    }
}
