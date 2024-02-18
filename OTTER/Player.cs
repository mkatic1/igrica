using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    public class Player
    {
        private string ime;

        public string Ime
        {
            get { return ime; }
            set
            {
                if (value == "")
                    ime = "Nepoznat";
                else
                    ime = value;
            }
        }
        private int sc;

        public int Sc
        {
            get { return sc; }
            set { sc = value; }
        }
        public Player(string i_,int s_)
        {
            this.ime = i_;
            this.sc = s_;
        }

    }

}
