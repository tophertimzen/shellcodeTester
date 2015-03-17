namespace shellcodeTester
{
    partial class shellcodeTesterGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(shellcodeTesterGUI));
            this.disasmBT = new System.Windows.Forms.Button();
            this.x64 = new System.Windows.Forms.RadioButton();
            this.x86 = new System.Windows.Forms.RadioButton();
            this.fireSc = new System.Windows.Forms.Button();
            this.showAddresses = new System.Windows.Forms.CheckBox();
            this.insertScRTB = new System.Windows.Forms.RichTextBox();
            this.disasmSc_RTB = new System.Windows.Forms.RichTextBox();
            this.targetCalc_CB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // disasmBT
            // 
            this.disasmBT.Location = new System.Drawing.Point(61, 302);
            this.disasmBT.Name = "disasmBT";
            this.disasmBT.Size = new System.Drawing.Size(184, 23);
            this.disasmBT.TabIndex = 0;
            this.disasmBT.Text = "Disassemble Shellcode";
            this.disasmBT.UseVisualStyleBackColor = true;
            this.disasmBT.Click += new System.EventHandler(this.disasmBT_Click);
            // 
            // x64
            // 
            this.x64.Location = new System.Drawing.Point(208, 344);
            this.x64.Name = "x64";
            this.x64.Size = new System.Drawing.Size(37, 17);
            this.x64.TabIndex = 4;
            this.x64.TabStop = true;
            this.x64.Text = "64";
            this.x64.UseVisualStyleBackColor = true;
            this.x64.CheckedChanged += new System.EventHandler(this.x64_CheckedChanged);
            // 
            // x86
            // 
            this.x86.Location = new System.Drawing.Point(61, 344);
            this.x86.Name = "x86";
            this.x86.Size = new System.Drawing.Size(37, 17);
            this.x86.TabIndex = 5;
            this.x86.TabStop = true;
            this.x86.Text = "32";
            this.x86.UseVisualStyleBackColor = true;
            this.x86.CheckedChanged += new System.EventHandler(this.x86_CheckedChanged);
            // 
            // fireSc
            // 
            this.fireSc.Location = new System.Drawing.Point(61, 381);
            this.fireSc.Name = "fireSc";
            this.fireSc.Size = new System.Drawing.Size(184, 23);
            this.fireSc.TabIndex = 6;
            this.fireSc.Text = "Fire Shellcode";
            this.fireSc.UseVisualStyleBackColor = true;
            this.fireSc.Click += new System.EventHandler(this.fireSc_Click);
            // 
            // showAddresses
            // 
            this.showAddresses.Location = new System.Drawing.Point(386, 397);
            this.showAddresses.Name = "showAddresses";
            this.showAddresses.Size = new System.Drawing.Size(130, 17);
            this.showAddresses.TabIndex = 7;
            this.showAddresses.Text = "Show Address Offsets";
            this.showAddresses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.showAddresses.UseVisualStyleBackColor = true;
            // 
            // insertScRTB
            // 
            this.insertScRTB.Location = new System.Drawing.Point(13, 13);
            this.insertScRTB.Name = "insertScRTB";
            this.insertScRTB.Size = new System.Drawing.Size(277, 283);
            this.insertScRTB.TabIndex = 8;
            this.insertScRTB.Text = "";
            // 
            // disasmSc_RTB
            // 
            this.disasmSc_RTB.Location = new System.Drawing.Point(308, 13);
            this.disasmSc_RTB.Name = "disasmSc_RTB";
            this.disasmSc_RTB.Size = new System.Drawing.Size(271, 378);
            this.disasmSc_RTB.TabIndex = 9;
            this.disasmSc_RTB.Text = "";
            // 
            // targetCalc_CB
            // 
            this.targetCalc_CB.AutoSize = true;
            this.targetCalc_CB.Location = new System.Drawing.Point(73, 410);
            this.targetCalc_CB.Name = "targetCalc_CB";
            this.targetCalc_CB.Size = new System.Drawing.Size(74, 17);
            this.targetCalc_CB.TabIndex = 10;
            this.targetCalc_CB.Text = "targetCalc";
            this.targetCalc_CB.UseVisualStyleBackColor = true;
            // 
            // shellcodeTesterGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(591, 445);
            this.Controls.Add(this.targetCalc_CB);
            this.Controls.Add(this.disasmSc_RTB);
            this.Controls.Add(this.insertScRTB);
            this.Controls.Add(this.showAddresses);
            this.Controls.Add(this.fireSc);
            this.Controls.Add(this.x86);
            this.Controls.Add(this.x64);
            this.Controls.Add(this.disasmBT);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "shellcodeTesterGUI";
            this.Text = "Test Shellcode!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button disasmBT;
        private System.Windows.Forms.RadioButton x64;
        private System.Windows.Forms.RadioButton x86;
        private System.Windows.Forms.Button fireSc;
        private System.Windows.Forms.CheckBox showAddresses;
        private System.Windows.Forms.RichTextBox insertScRTB;
        private System.Windows.Forms.RichTextBox disasmSc_RTB;
        private System.Windows.Forms.CheckBox targetCalc_CB;
    }
}

