using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using Memory;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using SocomCaStreamData.Controls;

namespace SocomCaStreamData
{
    public partial class frm_Scoreboard : Form
    {
        Mem m = new Mem();
        private const string PCSX2PROCESSNAME = "pcsx2";
        bool pcsx2Running;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public frm_Scoreboard()
        {
            InitializeComponent();
         
            
            this.TransparencyKey = (BackColor); // make GUI form transparent, also removes the border

            // initialize labels to be transparent on picturebox
            lbl_T_Alive.Parent = pictureBox1;
            lbl_T_Alive.Location = new Point(20, 20);//point
            lbl_T_Alive.BackColor = Color.Transparent;

            lbl_S_Alive.Parent = pictureBox1;
            lbl_S_Alive.Location = new Point(172, 20);//point
            lbl_S_Alive.BackColor = Color.Transparent;

            lbl_GameTime.Parent = pictureBox1;
            lbl_GameTime.Location = new Point(68, 0);//point
            lbl_GameTime.BackColor = Color.Transparent;

            lbl_T_Rounds.Parent = pictureBox1;
            lbl_T_Rounds.Location = new Point(74, 37);//point
            lbl_T_Rounds.BackColor = Color.Transparent;

            lbl_S_Rounds.Parent = pictureBox1;
            lbl_S_Rounds.Location = new Point(120, 37);//point
            lbl_S_Rounds.BackColor = Color.Transparent;

        }

        private void pnl_Background_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pnl_Background_MouseLeave(object sender, EventArgs e)
        {
            pnl_Background.BackColor = Color.FromArgb(20, 20, 20);
        }

        private void pnl_Background_MouseEnter(object sender, EventArgs e)
        {
            pnl_Background.BackColor = Color.FromArgb(25, 25, 25);

        }

        public void resetPlayers()
        {
            foreach (var label in pnl_Background.Controls.OfType<PlayerDataLabel>())
            {
                label.playerName = "";
                label.playerNameColor = Color.FromArgb(175, 175, 175);
                label.BackColor = Color.Transparent;

                label.healthSet = false;
                label.healthBarColor = Color.FromArgb(20, 20, 20);
                label.playerHealth = 0;
            }
        }

        public void setLabel(PlayerDataLabel label, PlayerDataModel player)
        {
            label.playerName = player._PlayerName;
            label.healthBarColor = Color.FromArgb(25, 140, 25);
            label.playerHealth = (int)player._PlayerHealth;
            label.PDM = player;
            
            label.m = m;
            if (player._LivingStatus == "DEAD")
            {
               
                label.playerNameColor = Color.FromArgb(175, 175, 175);
          
            }
            else
            {
                label.playerNameColor = Color.FromArgb(255, 255, 255);
              
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmr_PCSX2Check_Tick(object sender, EventArgs e)
        {
            Process[] pcsx2 = Process.GetProcessesByName(PCSX2PROCESSNAME);

            if (pcsx2.Length > 0)
            {
                pnl_PCSX2Detected.BackColor = Color.FromArgb(23,42,44);
                pcsx2Running = true;
                return;
            }
            pnl_PCSX2Detected.BackColor = Color.FromArgb(55,27,27);
            pcsx2Running = false;
        }

        private void tmr_GetGameData_Tick(object sender, EventArgs e)
        {
            List<PlayerDataModel> playerData = new List<PlayerDataModel>();

            if (pcsx2Running)
            {
                m.OpenProcess(PCSX2PROCESSNAME + ".exe");

                if ((m.ReadBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4) != null) && (!m.ReadBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4).SequenceEqual(new byte[] { 0, 0, 0, 0 })))
                {
                    if (m.ReadByte(GameHelper.GAME_ENDED_ADDRESS) == 0)
                    {
                        string playerDataLocationAddress = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4));
                        string playerTeam = GameHelper.GetTeamName(ByteConverstionHelper.byteArrayHexToHexString(m.ReadBytes((int.Parse(playerDataLocationAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_TEAMID_OFFSET).ToString("X4"), 4)));
                       
                        //Get Room specific data
                        int sealsRoundsWon = m.ReadByte(GameHelper.SEAL_WIN_COUNTER_ADDRESS);
                        int mercsRoundsWon = m.ReadByte(GameHelper.MERCS_WIN_COUNTER_ADDRESS);
                        int sealsAlive = m.ReadByte(GameHelper.SEALS_ALIVE_COUNTER_ADDRESS);
                        int mercsAlive = m.ReadByte(GameHelper.MERCS_ALIVE_COUNTER_ADDRESS);

                        string roundTimePointerAdddress = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes(GameHelper.ROUND_TIMER_POINTER, 4));
                        string roundTime = ByteConverstionHelper.convertBytesToString(m.ReadBytes((int.Parse(roundTimePointerAdddress, System.Globalization.NumberStyles.HexNumber) + 12).ToString("X4"), 5));

                        //string roundTime = ByteConverstionHelper.convertBytesToString(m.ReadBytes(GameHelper.ROUND_TIMER_RESPAWN_ADDRESS, 5));
                        playerData = processPlayers();

                        resetPlayers();

                            foreach (var item in playerData)
                            {

                                if (item._Team == "SEALS")
                                {

                                var playerDataLabel = pnl_Background.Controls
                               .OfType<PlayerDataLabel>()
                               .Where(pdl => pdl.Name.Contains("lbl_Seal_") && pdl.Text == "")
                               .OrderBy(label => label.Name); ;

                                if (item._PlayerName == "DroPPinCliPz")
                                {

                                }
                                setLabel(GetLabels("lbl_Seal_").First(), item);

                            }
                                else if (item._Team == "TERRORISTS")
                                {
                                var playerDataLabel = pnl_Background.Controls
                                   .OfType<PlayerDataLabel>()
                                   .Where(pdl => pdl.Name.Contains("lbl_Terr_") && pdl.Text == "")
                                   .OrderBy(label => label.Name);
                                    setLabel(GetLabels("lbl_Terr_").First(), item);


                            }


                       }
                        
                            

                        lbl_S_Alive.Text = sealsAlive.ToString();
                        lbl_T_Alive.Text = mercsAlive.ToString();
                        lbl_S_Rounds.Text = sealsRoundsWon.ToString();
                        lbl_T_Rounds.Text = mercsRoundsWon.ToString();
                        lbl_GameTime.Text = roundTime.ToString();


                    }
                }
                else
                {
                    resetPlayers();
                    lbl_S_Rounds.Text = "0";
                    lbl_T_Rounds.Text = "0";
                    lbl_S_Alive.Text = "0";
                    lbl_T_Alive.Text = "0";
                    lbl_GameTime.Text = "00:00";
                }
            }
        }

        private List<PlayerDataLabel> GetLabels(string labelNameContents)
        {
            return pnl_Background.Controls
                                    .OfType<PlayerDataLabel>()
                                    .Where(label => label.Name.Contains(labelNameContents) && label.playerName == "")
                                    .OrderBy(label => label.Name).ToList(); ;
        }
        private List<PlayerDataModel> processPlayers()
        {
            List<PlayerDataModel> playerData = new List<PlayerDataModel>();
            //Make sure we are in a game.
            if (m.ReadBytes(GameHelper.PLAYER_POINTER_ADDRESS, 4) != new byte[] { 0x00, 0x00, 0x00, 0x00 })
            {

                string objectPtr = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes(GameHelper.PLAYER_INDEX_POINTER_ADDRESS, 4));
                do
                {
                    string playerPointerAddress = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes((int.Parse(objectPtr, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_INDEX_PLAYER_POINTER_OFFSET).ToString("X4"), 4));
                    string playerNamePointerAddress = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes((int.Parse(playerPointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_NAME_OFFSET).ToString("X4"), 4));
                    string teamID = ByteConverstionHelper.byteArrayHexToHexString(m.ReadBytes((int.Parse(playerPointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_TEAMID_OFFSET).ToString("X4"), 4));
                    string teamName = GameHelper.GetTeamName(teamID);


                    if (teamName == "SEALS" || teamName == "TERRORISTS")
                    {
                        PlayerDataModel PD = new PlayerDataModel();
                        PD._pointerAddress = playerPointerAddress;
                        PD._Team = teamName;
                        PD._PlayerHealth = ByteConverstionHelper.byteHexFloatToDecimal(m.ReadBytes((int.Parse(playerPointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_HEALTH_OFFSET).ToString("X4"), 4));
                       
                        PD._PlayerName = ByteConverstionHelper.convertBytesToString(m.ReadBytes(playerNamePointerAddress, 20));
                        //PD._hasMPBomb = m.readByte((int.Parse(playerPointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.ENTITY_HAS_MPBOMB).ToString("X4"));
                        int livingStatus = m.ReadByte((int.Parse(playerPointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.PLAYER_DEATH_CAUSE_ID_OFFSET).ToString("X4"));

                        if (livingStatus == 255)
                        {
                            PD._LivingStatus = "ALIVE";
                        }
                        else
                        {
                            PD._LivingStatus = "DEAD";
                        }

                        playerData.Add(PD);

                    }

                    objectPtr = ByteConverstionHelper.byteArrayHexToAddressString(m.ReadBytes(objectPtr, 4)); // Get the next pointer in the list

                } while (objectPtr.ToUpper() != "206FE508");
            }

            return playerData;

        }
    }
}
