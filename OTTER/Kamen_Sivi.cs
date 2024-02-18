using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTTER
{
    //prepreke koje lik mora preskakati klikom na space preskočiti:sivi kamen, smeđi kamen i grm
    class Kamen_Sivi:Sprite
    {
        public Kamen_Sivi(string slika,int x,int y)
            : base(slika, x, y)
        {
                        
        }
       

     
    }
    class Kamen_Smedi : Sprite
    {
        public Kamen_Smedi(string slika,int x,int y)
            : base(slika, x, y)
        {

        }
    }
    class Grm : Sprite
    {
        public Grm(string slika,int x,int y)
            :base(slika,x,y)
        {

        }
    }
    //coinse lik skuplja kako bi postizao veći score sto je i cilj igre
    class Coin : Sprite
    {
        public Coin(string slika,int x,int y)
            : base(slika, x, y)
        {

        }
    }
    //objekt bomb smanjuje score igracu(prepolavlja ga)
    class Bomb : Sprite
    {
        public Bomb(string slika,int x,int y)
            :base(slika,x,y)
        {

        }
    }
    class Power : Sprite
    {
        public Power(string slika, int x, int y)
            : base(slika, x, y)
        {

        }
    }
}
