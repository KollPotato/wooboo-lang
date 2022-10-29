using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooBoo
{
    internal class Position
    {
        public Position(int Index, int Column, int Line, string FilePath)
        {
            this.Index = Index;
            this.Column = Column;
            this.Line = Line;
            this.FilePath = FilePath;
        }

        public Position Advance(string? character = null) {
            this.Index += 1;
            this.Column += 1;


            if (character == "\n") {
                this.Line += 1;
                this.Column = 0;
            }

            return this;
        }

        public Position Copy() {
            return new Position(this.Index, this.Column, this.Line, this.FilePath);
        }

        public int Index;
        public int Column;
        public int Line;
        public string FilePath;
    }
}
