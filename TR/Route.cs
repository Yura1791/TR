using System;
using System.Collections.Generic;

namespace TR
{
    public class Route
    {

        public int Id { get; set; }
        public string RouteStart { get; set; } = null!;
        public string? RouteEnd { get; set; }
        public TimeOnly TravelTime { get; set; }

        public ICollection<Runs> Runs { get; set; }
        public ICollection<Stoproute> Stoproute { get; set; }
    }
}
