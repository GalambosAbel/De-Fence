using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class ConnectionGene : Gene
    {
        private NodeGene from;
        private NodeGene to;

        private double weight;
        private bool enabled = true;

        private int replaceIndex;

        public ConnectionGene(NodeGene from, NodeGene to)
        {
            this.from = from;
            this.to = to;
        }

        public NodeGene GetFrom()
        {
            return from;
        }

        public void SetFrom(NodeGene from)
        {
            this.from = from;
        }

        public NodeGene GetTo()
        {
            return to;
        }

        public void SetTo(NodeGene to)
        {
            this.to = to;
        }

        public double GetWeight()
        {
            return weight;
        }

        public void SetWeight(double weight)
        {
            this.weight = weight;
        }

        public bool IsEnabled()
        {
            return enabled;
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }
        public bool Equals(Object o)
        {
            if (!(o is ConnectionGene)) return false;
            ConnectionGene c = (ConnectionGene)o;
            return (from.Equals(c.from) && to.Equals(c.to));
        }

        public int HashCode
        {
            get
            {
                return from.GetInnovationNumber() * Neat.MAX_NODES + to.GetInnovationNumber();
            }
        }

        public int GetReplaceIndex()
        {
            return replaceIndex;
        }

        public void SetReplaceIndex(int replaceIndex)
        {
            this.replaceIndex = replaceIndex;
        }
    }
}
