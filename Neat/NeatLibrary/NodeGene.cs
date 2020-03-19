using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class NodeGene : Gene
    {
        private double x,y;

        public NodeGene(int innovationNumber)
        {
            this.innovationNumber = innovationNumber;
        }

        public double GetX()
        {
            return x;
        }

        public void SetX(double x)
        {
            this.x = x;
        }

        public double GetY()
        {
            return y;
        }

        public void SetY(double y)
        {
            this.y = y;
        }

        public bool Equals(object obj)
        {
            if (!(obj is NodeGene)) return false;
            return innovationNumber == ((NodeGene)obj).GetInnovationNumber();
        }

        public int HashCode()
        {
            return innovationNumber;
        }
    }
}
