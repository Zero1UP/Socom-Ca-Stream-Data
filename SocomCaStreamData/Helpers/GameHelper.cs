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
        public const int  PLAYER_NAME_OFFSET =20;
        public const int PLAYER_TEAMID_OFFSET = 216;
        public const int  PLAYER_DEATH_CAUSE_ID_OFFSET = 2216; //FFFF = alive
        public const int PLAYER_HEALTH_OFFSET = 2364;

        //Player index
        public const string PLAYER_INDEX_POINTER_ADDRESS = "206FE508";
        public const int PLAYER_INDEX_PLAYER_POINTER_OFFSET = 8;
        //Notes (Thanks to harry for the original code from S2)
        //Functions the same as SOCOM 2
        //0x0 = Next player in the index (if the index = memory address then we are done)
        //0x8 = Player pointer

        //Game information
        public const string SEAL_WIN_COUNTER_ADDRESS = "20CCDDAC";
        public const string MERCS_WIN_COUNTER_ADDRESS = "20CCDDB4";
        public const string GAME_ENDED_ADDRESS = "20948204";
        public const string SEALS_ALIVE_COUNTER_ADDRESS = "20948264";
        public const string MERCS_ALIVE_COUNTER_ADDRESS = "2094825C";

        public const string ROUND_TIMER_POINTER = "20795990";
        public const string ROUND_TIMER_RESPAWN_ADDRESS = "20C73D60";

        //Spectator Camera
        //This should get around the loading spinning wheel, needs to be tested in a large room to see exactly what happens
        // CA allows you to change to any person at any time, no check on team / alive status (have to check for this manually)
        public const string SPECTATOR_CAMERA_POINTER_ADDRESS = "20705FD8";
        public const int SPECTATOR_CAMERA_POINTER_OFFSET1 = 220; //Write player pointer
        public const int SPECTATOR_CAMERA_POINTER_OFFSET2 = 224; //Write player pointer


        public static string GetTeamName(string value)
        {
            if (value == "40000001")
            {
                return "SEALS";
            }
            else if (value == "80000100")
            {
                return "TERRORISTS";
            }
            else if (value == "00010000")
            {
                return "SPECTATOR";

            }
            else
            {
                return "N/A";
            }
        }
    }
}
