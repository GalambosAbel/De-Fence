using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Connection
    {
        private Node from;
        private Node to;

        private double weight;
        private bool enabled = true;

        public Connection(Node from, Node to)
        {
            this.from = from;
            this.to = to;
        }

        public Node GetFrom()
        {
            return from;
        }

        public void SetFrom(Node from)
        {
            this.from = from;
        }

        public Node GetTo()
        {
            return to;
        }

        public void SetTo(Node to)
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
    }
}
