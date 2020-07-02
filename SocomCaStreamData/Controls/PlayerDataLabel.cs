using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Memory;

namespace SocomCaStreamData.Controls
{
    public partial class PlayerDataLabel : UserControl
    {
        private Mem _m = null;
        private PlayerDataModel _pdm;
        public Color livingStatus
        {
            set { this.BackColor = value;}
        }

        public string playerName
        {
            get { return lbl_PlayerName.Text; }
            set {
                lbl_PlayerName.Text = value;
          
                }
        }

        public Color playerNameColor
        {
            set { lbl_PlayerName.ForeColor = value; }
        }

        public bool healthSet
        {
            set { hb_PlayerHealth.healthSet = value; }
        }

        public Color healthBarColor
        {
            set { hb_PlayerHealth.healthBarColor = value; }
        }

        public int playerHealth
        {
            set { hb_PlayerHealth.playerHealth = value; }
        }

        public PlayerDataModel PDM
        {
            get { return (PlayerDataModel)lbl_PlayerName.Tag; }
            set { lbl_PlayerName.Tag = (PlayerDataModel)value; }
        }
        public PlayerDataLabel()
        {
            InitializeComponent();
        }

        public Mem m
        {
            set { _m = value; }
        }
        private void lbl_PlayerName_Click(object sender, EventArgs e)
        {
          

            if (lbl_PlayerName.Tag != null)
            {
           
                string camera1PointerAddress = ByteConverstionHelper.byteArrayHexToAddressString(_m.ReadBytes(GameHelper.SPECTATOR_CAMERA_POINTER_ADDRESS, 4));

                string playerPointer = "0" + this.PDM._pointerAddress.Substring(1, 7);

                if (this.PDM._LivingStatus != "DEAD")
                {
                    _m.WriteMemory((int.Parse(camera1PointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.SPECTATOR_CAMERA_POINTER_OFFSET1).ToString("X4"), "bytes", ByteConverstionHelper.formatBytesToLittleEndian(int.Parse(playerPointer, System.Globalization.NumberStyles.HexNumber), 8));
                    _m.WriteMemory((int.Parse(camera1PointerAddress, System.Globalization.NumberStyles.HexNumber) + GameHelper.SPECTATOR_CAMERA_POINTER_OFFSET2).ToString("X4"), "bytes", ByteConverstionHelper.formatBytesToLittleEndian(int.Parse(playerPointer, System.Globalization.NumberStyles.HexNumber), 8));

                }
            }
        }
    }
}
