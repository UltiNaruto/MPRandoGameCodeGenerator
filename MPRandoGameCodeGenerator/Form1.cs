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
        private const String BaseString = "GM8E";
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

        private void ISOFolder_btn_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    this.ISOFolder_txtBox.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void ISOtoPatch_btn_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "GC ISO File|*.iso";
                openFileDialog.DefaultExt = "iso";
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
            String ISOFolder = String.Empty;
            List<String> usedGameCodes = new List<String>();
            usedGameCodes.Add("01");
            if (this.ISOFolder_txtBox.Text == "")
            {
                if (MessageBox.Show("Are you sure you don't want to set the ISO folder?\r\nSetting the ISO folder will make sure that you don't have an unused game code.", this.Text, MessageBoxButtons.YesNo) == DialogResult.No)
                    return;
            }
            else
                ISOFolder = this.ISOFolder_txtBox.Text;
            if (this.ISOtoPatch_txtBox.Text == "")
            {
                MessageBox.Show("Set the path of the ISO you want to generate a game code");
                return;
            }
            if (ISOFolder != String.Empty)
            {
                foreach (String file in MultiEnumerateFiles(ISOFolder, "*.iso|*.ciso", SearchOption.TopDirectoryOnly))
                {
                    if (file.ToLower() == this.ISOtoPatch_txtBox.Text.ToLower())
                        continue;
                    GameCode = ISOManager.GetISOGameCode(file);
                    if (GameCode.Substring(0, 4) != "GM8E")
                        continue;
                    if (!usedGameCodes.Contains(GameCode.Substring(4)))
                        usedGameCodes.Add(GameCode.Substring(4));
                }
            }
            TargetGameCode = BaseString + AllowedCharacters[rand.Next(AllowedCharacters.Length)] + "" + AllowedCharacters[rand.Next(AllowedCharacters.Length)];
            while (usedGameCodes.Contains(TargetGameCode.Substring(4)))
                TargetGameCode = BaseString + AllowedCharacters[rand.Next(AllowedCharacters.Length)] + "" + AllowedCharacters[rand.Next(AllowedCharacters.Length)];
            ISOManager.PatchGameCode(this.ISOtoPatch_txtBox.Text, TargetGameCode);
            ISOManager.PatchGameTitle(this.ISOtoPatch_txtBox.Text, Path.GetFileNameWithoutExtension(this.ISOtoPatch_txtBox.Text));
        }
    }
}
