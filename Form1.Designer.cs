namespace shellcodeTester
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.disasmBT = new System.Windows.Forms.Button();
            this.insertScTB = new System.Windows.Forms.TextBox();
            this.disasmScLB = new System.Windows.Forms.ListBox();
            this.x64 = new System.Windows.Forms.RadioButton();
            this.x86 = new System.Windows.Forms.RadioButton();
            this.fireSc = new System.Windows.Forms.Button();
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
            // insertScTB
            // 
            this.insertScTB.Location = new System.Drawing.Point(7, 12);
            this.insertScTB.Multiline = true;
            this.insertScTB.Name = "insertScTB";
            this.insertScTB.Size = new System.Drawing.Size(283, 284);
            this.insertScTB.TabIndex = 1;
            // 
            // disasmScLB
            // 
            this.disasmScLB.FormattingEnabled = true;
            this.disasmScLB.Location = new System.Drawing.Point(296, 10);
            this.disasmScLB.Name = "disasmScLB";
            this.disasmScLB.Size = new System.Drawing.Size(283, 394);
            this.disasmScLB.TabIndex = 2;
            // 
            // x64
            // 
            this.x64.AutoSize = true;
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
            this.x86.AutoSize = true;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(591, 416);
            this.Controls.Add(this.fireSc);
            this.Controls.Add(this.x86);
            this.Controls.Add(this.x64);
            this.Controls.Add(this.disasmScLB);
            this.Controls.Add(this.insertScTB);
            this.Controls.Add(this.disasmBT);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Test Shellcode!";
            this.TransparencyKey = System.Drawing.Color.Black;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button disasmBT;
        private System.Windows.Forms.TextBox insertScTB;
        private System.Windows.Forms.ListBox disasmScLB;
        private System.Windows.Forms.RadioButton x64;
        private System.Windows.Forms.RadioButton x86;
        private System.Windows.Forms.Button fireSc;
    }
}

