namespace MPRandoGameCodeGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ISOtoPatch_lbl = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ISOtoPatch_btn = new System.Windows.Forms.Button();
            this.ISOtoPatch_txtBox = new System.Windows.Forms.TextBox();
            this.Patch_btn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.ISOtoPatch_lbl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Patch_btn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 91);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ISOtoPatch_lbl
            // 
            this.ISOtoPatch_lbl.AutoSize = true;
            this.ISOtoPatch_lbl.Location = new System.Drawing.Point(3, 3);
            this.ISOtoPatch_lbl.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.ISOtoPatch_lbl.Name = "ISOtoPatch_lbl";
            this.ISOtoPatch_lbl.Size = new System.Drawing.Size(94, 17);
            this.ISOtoPatch_lbl.TabIndex = 3;
            this.ISOtoPatch_lbl.Text = "ISO to patch :";
            this.toolTip1.SetToolTip(this.ISOtoPatch_lbl, resources.GetString("ISOtoPatch_lbl.ToolTip"));
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.Controls.Add(this.ISOtoPatch_btn, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.ISOtoPatch_txtBox, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(482, 32);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // ISOtoPatch_btn
            // 
            this.ISOtoPatch_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ISOtoPatch_btn.Location = new System.Drawing.Point(445, 3);
            this.ISOtoPatch_btn.Name = "ISOtoPatch_btn";
            this.ISOtoPatch_btn.Size = new System.Drawing.Size(34, 26);
            this.ISOtoPatch_btn.TabIndex = 0;
            this.ISOtoPatch_btn.Text = "...";
            this.ISOtoPatch_btn.UseVisualStyleBackColor = true;
            this.ISOtoPatch_btn.Click += new System.EventHandler(this.ISOtoPatch_btn_Click);
            // 
            // ISOtoPatch_txtBox
            // 
            this.ISOtoPatch_txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ISOtoPatch_txtBox.Location = new System.Drawing.Point(3, 6);
            this.ISOtoPatch_txtBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.ISOtoPatch_txtBox.MaxLength = 260;
            this.ISOtoPatch_txtBox.Name = "ISOtoPatch_txtBox";
            this.ISOtoPatch_txtBox.Size = new System.Drawing.Size(436, 22);
            this.ISOtoPatch_txtBox.TabIndex = 1;
            // 
            // Patch_btn
            // 
            this.Patch_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Patch_btn.Location = new System.Drawing.Point(3, 59);
            this.Patch_btn.Name = "Patch_btn";
            this.Patch_btn.Size = new System.Drawing.Size(476, 29);
            this.Patch_btn.TabIndex = 4;
            this.Patch_btn.Text = "Patch";
            this.Patch_btn.UseVisualStyleBackColor = true;
            this.Patch_btn.Click += new System.EventHandler(this.Patch_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 91);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 138);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 138);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Prime Rando Game Code Generator";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button ISOtoPatch_btn;
        private System.Windows.Forms.TextBox ISOtoPatch_txtBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label ISOtoPatch_lbl;
        private System.Windows.Forms.Button Patch_btn;
    }
}

