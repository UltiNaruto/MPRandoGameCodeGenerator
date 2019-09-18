using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MPRandoGameCodeGenerator
{
    public partial class Form1 : Form
    {
        private Random rand = null;
        private const String AllowedCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const String MP1BaseString = "GM8";
        private const String MP2BaseString = "G2M";
        public Form1()
        {
            rand = new Random((int)(new DateTime().Ticks / TimeSpan.TicksPerMillisecond));
            InitializeComponent();
        }

        private IEnumerable<string> MultiEnumerateFiles(string path, string patterns, SearchOption option)
        {
            foreach (var pattern in patterns.Split('|'))
                foreach (var file in Directory.EnumerateFiles(path, pattern, option))
                    yield return file;
        }

        private void ISOtoPatch_btn_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "GC ISO File|*.iso|Compressed GC ISO File|*.ciso";
                openFileDialog.FileName = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.ISOtoPatch_txtBox.Text = openFileDialog.SafeFileName;
                }
            }
        }

        private void Patch_btn_Click(object sender, EventArgs e)
        {
            String GameCode = String.Empty;
            String TargetGameCode = String.Empty;
            String SaveStateFolder = String.Empty;
            String CurGameCode = String.Empty;
            String GameSettingsFolder = String.Empty;
            String DolphinConfigFolder = String.Empty;
            MPType GameType = MPType.UNK;
            List<String> usedGameCodes = new List<String>();

            usedGameCodes.Add("01");
            DolphinConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Dolphin Emulator";
            if (this.ISOtoPatch_txtBox.Text == "")
            {
                MessageBox.Show("Set the path of the ISO you want to generate a game code");
                return;
            }
            GameCode = ISOManager.GetISOGameCode(this.ISOtoPatch_txtBox.Text);
            if (GameCode.Substring(0, 3) == MP1BaseString)
                GameType = MPType.PRIME;
            else if (GameCode.Substring(0, 3) == MP2BaseString)
                GameType = MPType.PRIME_2;
            else
            {
                MessageBox.Show("This game is not Metroid Prime 1 or 2!");
                return;
            }
            if (Directory.Exists(DolphinConfigFolder))
            {
                SaveStateFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Dolphin Emulator\\StateSaves";
                foreach (String file in Directory.EnumerateFiles(SaveStateFolder, GameType == MPType.PRIME ? MP1BaseString + "*.s*" : MP2BaseString + "*.s*", SearchOption.TopDirectoryOnly))
                {
                    CurGameCode = Path.GetFileNameWithoutExtension(file);
                    if (CurGameCode != GameCode)
                        if (!usedGameCodes.Contains(CurGameCode.Substring(4)))
                            usedGameCodes.Add(CurGameCode.Substring(4));
                }
            }
            TargetGameCode = GameCode.Substring(0, 4) + AllowedCharacters[rand.Next(AllowedCharacters.Length)] + "" + AllowedCharacters[rand.Next(AllowedCharacters.Length)];
            while (usedGameCodes.Contains(TargetGameCode.Substring(4)))
                TargetGameCode = GameCode.Substring(0, 4) + AllowedCharacters[rand.Next(AllowedCharacters.Length)] + "" + AllowedCharacters[rand.Next(AllowedCharacters.Length)];
            if (Directory.Exists(DolphinConfigFolder))
            {
                GameSettingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar + "Dolphin Emulator\\GameSettings";
                if (File.Exists(GameSettingsFolder + "\\" + GameCode + ".ini"))
                    File.Copy(GameSettingsFolder + "\\" + GameCode + ".ini", GameSettingsFolder + "\\" + TargetGameCode + ".ini");
            }
            ISOManager.PatchGameCode(this.ISOtoPatch_txtBox.Text, TargetGameCode);
            ISOManager.PatchGameTitle(this.ISOtoPatch_txtBox.Text, Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text));
            MessageBox.Show("Game patched with Game Code \"" + TargetGameCode + "\"\r\nand with Game Title \"" + Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text) + "\"");
        }
    }
}
