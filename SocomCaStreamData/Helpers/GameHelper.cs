using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocomCaStreamData
{
    public static class GameHelper
    {
        //Player Object Data
        public const string PLAYER_POINTER_ADDRESS = "206FEB08";
        public const string PLAYER_NAME_OFFSET = "14";
        public const string PLAYER_TEAMID_OFFSET = "D8";
        public const string PLAYER_DEATH_CAUSE_ID_OFFSET = "8A8"; //FFFF = alive

        //Player index
        public const string PLAYER_INDEX_POINTER_ADDRESS = "206FE508";
        //Notes (Thanks to harry for the original code from S2)
        //Functions the same as SOCOM 2
        //0x0 = Next player in the index (if the index = memory address then we are done)
        //0x4 = Player pointer

        //Game information
        public const string SEAL_WIN_COUNTER_ADDRESS = "20CCDDAC";
        public const string MERCS_WIN_COUNTER_ADDRESS = "20CCDDB4";

        //Spectator Camera
        //This should get around the loading spinning wheel, needs to be tested in a large room to see exactly what happens
        public const string SPECTATOR_CAMERA_POINTER_ADDRESS = "20705FD8";
        public const string SPECTATOR_CAMERA_POINTER_OFFSET1 = "DC"; //Write player pointer
        public const string SPECTATOR_CAMERA_POINTER_OFFSET2 = "E0"; //Write player pointer
    }
}
