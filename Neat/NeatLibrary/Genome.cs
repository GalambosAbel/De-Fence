using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Genome
    {
        private RandomHashSet<ConnectionGene> connections = new RandomHashSet<ConnectionGene>();
        private RandomHashSet<NodeGene> nodes = new RandomHashSet<NodeGene>();

        private Neat neat;

        public Genome(Neat neat)
        {
            this.neat = neat;
        }

        public double Distance(Genome g2)
        {

            Genome g1 = this;
            int highestInnovationGene1 = 0;
            if (g1.GetConnections().Size() != 0)
            {
                highestInnovationGene1 = g1.GetConnections().Get(g1.GetConnections().Size() - 1).GetInnovationNumber();
            }

            int highestInnovationGene2 = 0;
            if (g2.GetConnections().Size() != 0)
            {
                highestInnovationGene2 = g2.GetConnections().Get(g2.GetConnections().Size() - 1).GetInnovationNumber();
            }

            if (highestInnovationGene1 < highestInnovationGene2)
            {
                Genome g = g1;
                g1 = g2;
                g2 = g;
            }

            int indexG1 = 0;
            int indexG2 = 0;

            int disjoint = 0;
            int excess = 0;
            double weightDiff = 0;
            int similar = 0;


            while (indexG1 < g1.GetConnections().Size() && indexG2 < g2.GetConnections().Size())
            {

                ConnectionGene gene1 = g1.GetConnections().Get(indexG1);
                ConnectionGene gene2 = g2.GetConnections().Get(indexG2);

                int in1 = gene1.GetInnovationNumber();
                int in2 = gene2.GetInnovationNumber();

                if (in1 == in2)
                {
                    //similargene
                    similar++;
                    weightDiff += Math.Abs(gene1.GetWeight() - gene2.GetWeight());
                    indexG1++;
                    indexG2++;
                }
                else if (in1 > in2)
                {
                    //disjoint gene of b
                    disjoint++;
                    indexG2++;
                }
                else
                {
                    //disjoint gene of a
                    disjoint++;
                    indexG1++;
                }
            }

            weightDiff /= Math.Max(1, similar);
            excess = g1.GetConnections().Size() - indexG1;

            double N = Math.Max(g1.GetConnections().Size(), g2.GetConnections().Size());
            if (N < 20)
            {
                N = 1;
            }

            return neat.GetC1() * disjoint / N + neat.GetC2() * excess / N + neat.GetC3() * weightDiff / N;

        }

        public static Genome CrossOver(Genome g1, Genome g2)
        {
            Neat neat = g1.GetNeat();

            Genome genome = neat.EmptyGenome();

            int indexG1 = 0;
            int indexG2 = 0;

            while (indexG1 < g1.GetConnections().Size() && indexG2 < g2.GetConnections().Size())
            {

                ConnectionGene gene1 = g1.GetConnections().Get(indexG1);
                ConnectionGene gene2 = g2.GetConnections().Get(indexG2);

                int in1 = gene1.GetInnovationNumber();
                int in2 = gene2.GetInnovationNumber();

                if (in1 == in2)
                {
                    if (new Random().NextDouble() > 0.5)
                    {
                        genome.GetConnections().Add(Neat.GetConnection(gene1));
                    }
                    else
                    {
                        genome.GetConnections().Add(Neat.GetConnection(gene2));
                    }
                    indexG1++;
                    indexG2++;
                }
                else if (in1 > in2)
                {

                    indexG2++;
                }
                else
                {

                    genome.GetConnections().Add(Neat.GetConnection(gene1));
                    indexG1++;
                }
            }

            while (indexG1 < g1.GetConnections().Size())
            {
                ConnectionGene gene1 = g1.GetConnections().Get(indexG1);
                genome.GetConnections().Add(Neat.GetConnection(gene1));
                indexG1++;
            }

            foreach (ConnectionGene c in genome.GetConnections().GetData())
            {
                genome.GetNodes().Add(c.GetFrom());
                genome.GetNodes().Add(c.GetTo());
            }

            return genome;
        }

        public void Mutate()
        {
            if (neat.GetPROBABILITY_MUTATE_LINK() > new Random().NextDouble())
            {
                MutateLink();
            }
            if (neat.GetPROBABILITY_MUTATE_NODE() > new Random().NextDouble())
            {
                MutateNode();
            }
            if (neat.GetPROBABILITY_MUTATE_WEIGHT_SHIFT() > new Random().NextDouble())
            {
                MutateWeightShift();
            }
            if (neat.GetPROBABILITY_MUTATE_WEIGHT_RANDOM() > new Random().NextDouble())
            {
                MutateWeightRandom();
            }
            if (neat.GetPROBABILITY_MUTATE_TOGGLE_LINK() > new Random().NextDouble())
            {
                MutateLinkToggle();
            }
        }

        public void MutateLink()
        {

            for (int i = 0; i < 100; i++)
            {

                NodeGene a = nodes.RandomElement();
                NodeGene b = nodes.RandomElement();

                if (a == null || b == null) continue;
                if (a.GetX() == b.GetX())
                {
                    continue;
                }

                ConnectionGene con;
                if (a.GetX() < b.GetX())
                {
                    con = new ConnectionGene(a, b);
                }
                else
                {
                    con = new ConnectionGene(b, a);
                }

                if (connections.Contains(con))
                {
                    continue;
                }

                con = neat.GetConnection(con.GetFrom(), con.GetTo());
                con.SetWeight((new Random().NextDouble() * 2 - 1) * neat.GetWEIGHT_RANDOM_STRENGTH());

                connections.AddSorted(con);
                return;
            }
        }

        public void MutateNode()
        {
            ConnectionGene con = connections.RandomElement();
            if (con == null) return;

            NodeGene from = con.GetFrom();
            NodeGene to = con.GetTo();

            int replaceIndex = neat.GetReplaceIndex(from, to);
            NodeGene middle;
            if (replaceIndex == 0)
            {
                middle = neat.GetNode();
                middle.SetX((from.GetX() + to.GetX()) / 2);
                middle.SetY((from.GetY() + to.GetY()) / 2 + new Random().NextDouble() * 0.1 - 0.05);
                neat.SetReplaceIndex(from, to, middle.GetInnovationNumber());
            }
            else
            {
                middle = neat.GetNode(replaceIndex);
            }

            ConnectionGene con1 = neat.GetConnection(from, middle);
            ConnectionGene con2 = neat.GetConnection(middle, to);

            con1.SetWeight(1);
            con2.SetWeight(con.GetWeight());
            con2.SetEnabled(con.IsEnabled());

            connections.Remove(con);
            connections.Add(con1);
            connections.Add(con2);

            nodes.Add(middle);
        }

        public void MutateWeightShift()
        {
            ConnectionGene con = connections.RandomElement();
            if (con != null)
            {
                con.SetWeight(con.GetWeight() + (new Random().NextDouble() * 2 - 1) * neat.GetWEIGHT_SHIFT_STRENGTH());
            }
        }

        public void MutateWeightRandom()
        {
            ConnectionGene con = connections.RandomElement();
            if (con != null)
            {
                con.SetWeight((new Random().NextDouble() * 2 - 1) * neat.GetWEIGHT_RANDOM_STRENGTH());
            }
        }

        public void MutateLinkToggle()
        {
            ConnectionGene con = connections.RandomElement();
            if (con != null)
            {
                con.SetEnabled(!con.IsEnabled());
            }
        }

        public RandomHashSet<ConnectionGene> GetConnections()
        {
            return connections;
        }

        public RandomHashSet<NodeGene> GetNodes()
        {
            return nodes;
        }

        public Neat GetNeat()
        {
            return neat;
        }
    }
}
