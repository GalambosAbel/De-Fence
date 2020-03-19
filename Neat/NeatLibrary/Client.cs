using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Client
    {
        private Calculator calculator;

        private Genome genome;
        private double score;
        private Species species;

        public void GenerateCalculator()
        {
            this.calculator = new Calculator(genome);
        }

        public double[] Calculate(double[] input)
        {
            if (this.calculator == null) GenerateCalculator();
            return this.calculator.Calculate(input);
        }

        public double Distance(Client other)
        {
            return this.GetGenome().Distance(other.GetGenome());
        }

        public void Mutate()
        {
            GetGenome().Mutate();
        }

        public Calculator GetCalculator()
        {
            return calculator;
        }

        public Genome GetGenome()
        {
            return genome;
        }

        public void SetGenome(Genome genome)
        {
            this.genome = genome;
        }

        public double GetScore()
        {
            return score;
        }

        public void SetScore(double score)
        {
            this.score = score;
        }

        public Species GetSpecies()
        {
            return species;
        }

        public void SetSpecies(Species species)
        {
            this.species = species;
        }

        public static int Compare(Client o1, Client o2)
        {
            if (o1.score < o2.score) return -1;
            if (o1.score > o2.score) return 1;
            return 0;
        }
    }
}
