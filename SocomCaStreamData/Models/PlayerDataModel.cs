using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocomCaStreamData
{
    public class PlayerDataModel
    {
        public string _PlayerName { get; set; }
        public string _Team { get; set; }
        public string _LivingStatus { get; set; }
        public decimal _PlayerHealth { get; set; }    
        public string _pointerAddress { get; set; }
        public int _hasMPBomb { get; set; }
    }
}
