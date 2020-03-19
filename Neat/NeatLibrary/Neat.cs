using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Neat
    {
        public static readonly int MAX_NODES = (int)Math.Pow(2, 20);

        public double C1 = 1, C2 = 1, C3 = 1;
        public double CP = 4;


        public double WEIGHT_SHIFT_STRENGTH = 0.3;
        public double WEIGHT_RANDOM_STRENGTH = 1;

        public double SURVIVORS = 0.8;

        public double PROBABILITY_MUTATE_LINK = 0.01;
        public double PROBABILITY_MUTATE_NODE = 0.003;
        public double PROBABILITY_MUTATE_WEIGHT_SHIFT = 0.002;
        public double PROBABILITY_MUTATE_WEIGHT_RANDOM = 0.002;
        public double PROBABILITY_MUTATE_TOGGLE_LINK = 0.0;

        public Dictionary<int, ConnectionGene> allConnections = new Dictionary<int, ConnectionGene>();
        public RandomHashSet<NodeGene> allNodes = new RandomHashSet<NodeGene>();

        public RandomHashSet<Client> clients = new RandomHashSet<Client>();
        public RandomHashSet<Species> species = new RandomHashSet<Species>();

        public int maxClients;
        public int outputSize;
        public int inputSize;

        public Neat(int inputSize, int outputSize, int clients)
        {
            Reset(inputSize, outputSize, clients);
        }

        public Genome EmptyGenome()
        {
            Genome g = new Genome(this);
            for (int i = 0; i < inputSize + outputSize; i++)
            {
                g.GetNodes().Add(GetNode(i + 1));
            }
            return g;
        }

        public void Reset(int inputSize, int outputSize, int clients)
        {
            this.inputSize = inputSize;
            this.outputSize = outputSize;
            this.maxClients = clients;

            allConnections.Clear();
            allNodes.Clear();
            this.clients.Clear();

            for (int i = 0; i < inputSize; i++)
            {
                NodeGene n = GetNode();
                n.SetX(0.1);
                n.SetY((i + 1) / (double)(inputSize + 1));
            }

            for (int i = 0; i < outputSize; i++)
            {
                NodeGene n = GetNode();
                n.SetX(0.9);
                n.SetY((i + 1) / (double)(outputSize + 1));
            }

            for (int i = 0; i < maxClients; i++)
            {
                Client c = new Client();
                c.SetGenome(EmptyGenome());
                c.GenerateCalculator();
                this.clients.Add(c);
            }
        }

        public Client GetClient(int index)
        {
            return clients.Get(index);
        }

        public static ConnectionGene GetConnection(ConnectionGene con)
        {
            ConnectionGene c = new ConnectionGene(con.GetFrom(), con.GetTo());
            c.SetInnovationNumber(con.GetInnovationNumber());
            c.SetWeight(con.GetWeight());
            c.SetEnabled(con.IsEnabled());
            return c;
        }
        public ConnectionGene GetConnection(NodeGene node1, NodeGene node2)
        {
            ConnectionGene connectionGene = new ConnectionGene(node1, node2);

            if (allConnections.ContainsKey(connectionGene.HashCode))
            {
                connectionGene.SetInnovationNumber(allConnections[connectionGene.HashCode].GetInnovationNumber());
            }
            else
            {
                connectionGene.SetInnovationNumber(allConnections.Count + 1);
                allConnections.Add(connectionGene.HashCode, connectionGene);
            }

            return connectionGene;
        }
        public void SetReplaceIndex(NodeGene node1, NodeGene node2, int index)
        {
            allConnections[new ConnectionGene(node1, node2).HashCode].SetReplaceIndex(index);
        }
        public int GetReplaceIndex(NodeGene node1, NodeGene node2)
        {
            ConnectionGene con = new ConnectionGene(node1, node2);
            ConnectionGene data = allConnections[con.HashCode];
            if (data == null) return 0;
            return data.GetReplaceIndex();
        }

        public NodeGene GetNode()
        {
            NodeGene n = new NodeGene(allNodes.Size() + 1);
            allNodes.Add(n);
            return n;
        }

        public NodeGene GetNode(int id)
        {
            if (id <= allNodes.Size())
            {
                return allNodes.Get(id - 1);
            }
            return GetNode();
        }  

        public void Evolve()
        {

            GenSpecies();
            Kill();
            RemoveExtinctSpecies();
            Reproduce();
            Mutate();
            foreach (Client c in clients.GetData())
            {
                c.GenerateCalculator();
            }
        }

        public string PrintSpecies()
        {
            string result = "##########################################\n";
            foreach (Species s in this.species.GetData())
            {
                result += s + "  " + s.GetScore() + "  " + s.Size() + "\n";
            }
            return result;
        }

        public void Reproduce()
        {
            RandomSelector<Species> selector = new RandomSelector<Species>();
            foreach (Species s in species.GetData())
            {
                selector.Add(s, s.GetScore());
            }

            foreach (Client c in clients.GetData())
            {
                if (c.GetSpecies() == null)
                {
                    Species s = selector.Random();
                    c.SetGenome(s.Breed());
                    s.ForcePut(c);
                }
            }
        }

        public void Mutate()
        {
            foreach (Client c in clients.GetData())
            {
                c.Mutate();
            }
        }

        public void RemoveExtinctSpecies()
        {
            for (int i = species.Size() - 1; i >= 0; i--)
            {
                if (species.Get(i).Size() <= 1)
                {
                    species.Get(i).GoExtinct();
                    species.Remove(i);
                }
            }
        }

        public void GenSpecies()
        {
            foreach (Species s in species.GetData())
            {
                s.Reset();
            }

            foreach (Client c in clients.GetData())
            {
                if (c.GetSpecies() != null) continue;


                bool found = false;
                foreach (Species s in species.GetData())
                {
                    if (s.Put(c))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    species.Add(new Species(c));
                }
            }

            foreach (Species s in species.GetData())
            {
                s.EvaluateScore();
            }
        }

        public void Kill()
        {
            foreach (Species s in species.GetData())
            {
                s.Kill(1 - SURVIVORS);
            }
        }

        public double GetCP()
        {
            return CP;
        }

        public void SetCP(double CP)
        {
            this.CP = CP;
        }

        public double GetC1()
        {
            return C1;
        }

        public double GetC2()
        {
            return C2;
        }

        public double GetC3()
        {
            return C3;
        }

        public double GetWEIGHT_SHIFT_STRENGTH()
        {
            return WEIGHT_SHIFT_STRENGTH;
        }

        public double GetWEIGHT_RANDOM_STRENGTH()
        {
            return WEIGHT_RANDOM_STRENGTH;
        }

        public double GetPROBABILITY_MUTATE_LINK()
        {
            return PROBABILITY_MUTATE_LINK;
        }

        public double GetPROBABILITY_MUTATE_NODE()
        {
            return PROBABILITY_MUTATE_NODE;
        }

        public double GetPROBABILITY_MUTATE_WEIGHT_SHIFT()
        {
            return PROBABILITY_MUTATE_WEIGHT_SHIFT;
        }

        public double GetPROBABILITY_MUTATE_WEIGHT_RANDOM()
        {
            return PROBABILITY_MUTATE_WEIGHT_RANDOM;
        }

        public double GetPROBABILITY_MUTATE_TOGGLE_LINK()
        {
            return PROBABILITY_MUTATE_TOGGLE_LINK;
        }

        public int GetOutputSize()
        {
            return outputSize;
        }

        public int GetInputSize()
        {
            return inputSize;
        }

    }
}
