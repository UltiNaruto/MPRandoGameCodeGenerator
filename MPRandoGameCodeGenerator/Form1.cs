using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MPRandoGameCodeGenerator
{
    public partial class Form1 : Form
    {
        Random rand = null;
        const String MP1BaseString = "GM8";
        const String MP2BaseString = "G2M";
        readonly String DolphinConfigFolder;
        readonly String GameSettingsFolder;
        public Form1()
        {
            InitializeComponent();
            DolphinConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Dolphin Emulator";
            GameSettingsFolder = DolphinConfigFolder + Path.DirectorySeparatorChar + "GameSettings";
        }

        List<String> GetAllGeneratedDevCodes(MPType type, String GameRegion)
        {
            var Dir = default(String);

            if (type == MPType.PRIME)
            {
                if (File.Exists("ugc_mp1.list"))
                    return File.ReadAllLines("ugc_mp1.list").ToList();

                Dir = DolphinConfigFolder + Path.DirectorySeparatorChar + "GameSettings";

                if (!Directory.Exists(Dir))
                    return new List<String>();
                return Directory.EnumerateFiles(
                    Dir,
                    MP1BaseString + GameRegion + "*.ini"
                ).Select((str) =>
                {
                    var match = Regex.Match(str, @"[A-Z0-9]{6}\.ini");
                    if (match.Success)
                        return match.Value.Substring(4, 2);
                    return str;
                }).ToList();
            }

            if (type == MPType.PRIME_2)
            {
                if (File.Exists("ugc_mp2.list"))
                    return File.ReadAllLines("ugc_mp2.list").ToList();

                Dir = DolphinConfigFolder + Path.DirectorySeparatorChar + "GameSettings";

                if (!Directory.Exists(Dir))
                    return new List<String>();
                return Directory.EnumerateFiles(
                    Dir,
                    MP2BaseString + GameRegion + "*.ini"
                ).Select((str) =>
                {
                    var match = Regex.Match(str, @"[A-Z0-9]{6}\.ini");
                    if (match.Success)
                        return match.Value.Substring(4, 2);
                    return str;
                }).ToList();
            }

            throw new Exception("Game unsupported");
        }

        String RandomizeGameCode(MPType type, String GameRegion, bool saveToList=false)
        {
            const String AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const int AllowedCharsLen = 36;

            var devCode = default(String);
            var usedGameCodes = default(List<String>);

            devCode = "01";
            if (saveToList)
                usedGameCodes = GetAllGeneratedDevCodes(type, GameRegion);
            else
                usedGameCodes = new List<String>();

            // Reserved for vanilla game
            if (!usedGameCodes.Contains("01"))
                usedGameCodes.Add("01");

            switch (type)
            {
                case MPType.PRIME:
                    if (GameRegion == "E")
                    {
                        // Reserved for Practice Mod
                        if (!usedGameCodes.Contains("PM"))
                            usedGameCodes.Add("PM");
                    }
                    if (GameRegion == "P")
                    {
                        // Reserved for Scan Dash Enabled version
                        if (!usedGameCodes.Contains("SD"))
                            usedGameCodes.Add("SD");
                    }
                    break;
                case MPType.PRIME_2:
                default:
                    break;
            }

            while(usedGameCodes.Contains(devCode))
                devCode = new String(new char[] { AllowedChars[rand.Next(AllowedCharsLen)], AllowedChars[rand.Next(AllowedCharsLen)] });
            usedGameCodes.Add(devCode);

            if (usedGameCodes.Contains("01"))
                usedGameCodes.Remove("01");

            switch (type)
            {
                case MPType.PRIME:
                    if (GameRegion == "E")
                    {
                        if (usedGameCodes.Contains("PM"))
                            usedGameCodes.Remove("PM");
                    }
                    if (GameRegion == "P")
                    {
                        if (usedGameCodes.Contains("SD"))
                            usedGameCodes.Remove("SD");
                    }
                    if(saveToList)
                        File.WriteAllLines("ugc_mp1_"+GameRegion.ToLower()+".list", usedGameCodes);
                    return MP1BaseString + GameRegion + devCode;
                case MPType.PRIME_2:
                    if(saveToList)
                        File.WriteAllLines("ugc_mp2_" + GameRegion.ToLower() + ".list", usedGameCodes);
                    return MP2BaseString + GameRegion + devCode;
                default:
                    throw new Exception("Game unsupported");
            }
        }

        private void ISOtoPatch_btn_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "GC ISO File|*.iso|Compressed GC ISO File|*.ciso";
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.ISOtoPatch_txtBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void Patch_btn_Click(object sender, EventArgs e)
        {
            String GameCode = String.Empty;
            String GameRegion = String.Empty;
            String TargetGameCode = String.Empty;
            MPType GameType = MPType.UNK;
            String TargetConfigFile = String.Empty;

            if (this.ISOtoPatch_txtBox.Text == "")
            {
                MessageBox.Show("Set the path of the ISO you want to generate a game code");
                return;
            }
            GameCode = ISOManager.GetISOGameCode(this.ISOtoPatch_txtBox.Text);
            if (GameCode == "ERROR")
            {
                MessageBox.Show("This is not a GC ISO");
                return;
            }
            GameRegion = GameCode.Substring(3, 1);
            if (GameCode.Substring(0, 3) == MP1BaseString)
                GameType = MPType.PRIME;
            else if (GameCode.Substring(0, 3) == MP2BaseString)
                GameType = MPType.PRIME_2;
            else
            {
                MessageBox.Show("This game is not Metroid Prime 1 or 2!");
                return;
            }
            TargetGameCode = RandomizeGameCode(GameType, GameRegion, comboBox1.Text == comboBox1.Items[1].ToString());
            if (comboBox1.Text != comboBox1.Items[0].ToString())
            {
                if (Directory.Exists(GameSettingsFolder))
                {
                    if (File.Exists(GameSettingsFolder + Path.DirectorySeparatorChar + GameCode.Substring(0, 4) + "01.ini"))
                    {
                        if (comboBox1.Text == comboBox1.Items[1].ToString())
                        {
                            TargetConfigFile = GameSettingsFolder + "\\" + TargetGameCode + ".ini";
                            File.Copy(GameSettingsFolder + Path.DirectorySeparatorChar + GameCode.Substring(0, 4) + "01.ini", TargetConfigFile);
                            File.WriteAllText(TargetConfigFile, File.ReadAllText(TargetConfigFile) + "\r\n\r\n# " + Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text));
                        }
                        if (comboBox1.Text == comboBox1.Items[2].ToString())
                        {
                            TargetConfigFile = Path.GetDirectoryName(this.ISOtoPatch_txtBox.Text) + Path.DirectorySeparatorChar + TargetGameCode + ".ini";
                            File.Copy(GameSettingsFolder + Path.DirectorySeparatorChar + GameCode.Substring(0, 4) + "01.ini", TargetConfigFile);
                            File.WriteAllText(TargetConfigFile, File.ReadAllText(TargetConfigFile) + "\r\n\r\n# " + Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text));
                        }
                    }
                }
            }
            ISOManager.PatchGameCode(this.ISOtoPatch_txtBox.Text, TargetGameCode);
            ISOManager.PatchGameTitle(this.ISOtoPatch_txtBox.Text, Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text));
            MessageBox.Show("Game patched with Game Code \"" + TargetGameCode + "\"\r\nand with Game Title \"" + Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text) + "\"");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rand = new Random((int)(DateTimeOffset.Now.ToUnixTimeSeconds()));
        }
    }
}
