using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooBoo
{
    internal class Location
    {
        public Location(Position start, Position end)
        {
            this.start = start;
            this.end = end;
        }

        Position start;
        Position end;
    }
}
