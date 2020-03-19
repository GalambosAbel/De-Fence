using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Node
    {
        private double x;
        private double output;
        private List<Connection> connections = new List<Connection>();

        public Node(double x)
        {
            this.x = x;
        }

        public void Calculate()
        {
            double s = 0;
            foreach (Connection c in connections)
            {
                if (c.IsEnabled())
                {
                    s += c.GetWeight() * c.GetFrom().GetOutput();
                }
            }
            output = ActivationFunction(s);
        }

        private double ActivationFunction(double x)
        {
            return 1d / (1 + Math.Exp(-x));
        }

        public void SetX(double x)
        {
            this.x = x;
        }

        public void SetOutput(double output)
        {
            this.output = output;
        }

        public void SetConnections(List<Connection> connections)
        {
            this.connections = connections;
        }

        public double GetX()
        {
            return x;
        }

        public double GetOutput()
        {
            return output;
        }

        public List<Connection> GetConnections()
        {
            return connections;
        }

        public static int Compare(Node o1, Node o2)
        {
            if (o1.x > o2.x) return -1;
            if (o1.x < o2.x) return 1;
            return 0;
        }
    }
}
