using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLibrary
{
    public class Gene
    {
        protected int innovationNumber;

        public Gene(int innovationNumber)
        {
            this.innovationNumber = innovationNumber;
        }

        public Gene()
        {

        }

        public int GetInnovationNumber()
        {
            return innovationNumber;
        }

        public void SetInnovationNumber(int innovationNumber)
        {
            this.innovationNumber = innovationNumber;
        }
    }
}
