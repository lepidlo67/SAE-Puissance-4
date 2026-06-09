using System;
using System.Collections.Generic;
using System.Text;

namespace Systeme_Puissance_4
{
    public class Case
    {
        public bool Libre { get; set; } = true;


        
        public bool EstLibre()
        {
            return Libre;
        }
    }
}
