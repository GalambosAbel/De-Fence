using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Species
    {
        private RandomHashSet<Client> clients = new RandomHashSet<Client>();
        private Client representative;
        private double score;

        public Species(Client representative)
        {
            this.representative = representative;
            this.representative.SetSpecies(this);
            clients.Add(representative);
        }

        public bool Put(Client client)
        {
            if (client.Distance(representative) < representative.GetGenome().GetNeat().GetCP())
            {
                client.SetSpecies(this);
                clients.Add(client);
                return true;
            }
            return false;
        }

        public void ForcePut(Client client)
        {
            client.SetSpecies(this);
            clients.Add(client);
        }

        public void GoExtinct()
        {
            foreach (Client c in clients.GetData())
            {
                c.SetSpecies(null);
            }
        }

        public void EvaluateScore()
        {
            double v = 0;
            foreach (Client c in clients.GetData())
            {
                v += c.GetScore();
            }
            score = v / clients.Size();
        }

        public void Reset()
        {
            representative = clients.RandomElement();
            foreach (Client c in clients.GetData())
            {
                c.SetSpecies(null);
            }
            clients.Clear();

            clients.Add(representative);
            representative.SetSpecies(this);
            score = 0;
        }

        public void Kill(double percentage)
        {
            clients.GetData().Sort(Client.Compare);

            double amount = percentage * this.clients.Size();
            for(int i = 0;i<amount; i++)
            {
                clients.Get(0).SetSpecies(null);
                clients.Remove(0);
            }
        }

        public Genome Breed()
        {
            Client c1 = clients.RandomElement();
            Client c2 = clients.RandomElement();

            if (c1.GetScore() > c2.GetScore()) return Genome.CrossOver(c1.GetGenome(), c2.GetGenome());
            return Genome.CrossOver(c2.GetGenome(), c1.GetGenome());
        }

        public int Size()
        {
            return clients.Size();
        }

        public RandomHashSet<Client> GetClients()
        {
            return clients;
        }

        public Client GetRepresentative()
        {
            return representative;
        }

        public double GetScore()
        {
            return score;
        }
    }
}
