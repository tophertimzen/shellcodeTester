using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shellcodeTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            disasmScLB.Items.Add("Disasembled shellcode will appear here");            
        }
        //\x31\xc9\xf7\xe1\xb0\x05\x51\x68\x6f\x73\x74\x73\x68\x2f\x2f\x2f\x68\x68\x2f\x65\x74\x63\x89\xe3\x66\xb9\x01\x04\xcd\x80\x93\x6a\x04\x58\xeb\x10\x59\x6a\x14\x5a\xcd\x80\x6a\x06\x58\xcd\x80\x6a\x01\x58\xcd\x80\xe8\xeb\xff\xff\xff\x31\x32\x37\x2e\x31\x2e\x31\x2e\x31\x20\x67\x6f\x6f\x67\x6c\x65\x2e\x63\x6f\x6d
        byte[] shellcode = new byte[0];
        uint architecture = 0;
        private void disasmBT_Click(object sender, EventArgs e)
        {
            disasmScLB.Items.Clear();
            string insertedShellcode = insertScTB.Text;
            insertedShellcode = insertedShellcode.Replace("\\x", string.Empty);
            insertedShellcode = insertedShellcode.Replace("0x", string.Empty);
            insertedShellcode = insertedShellcode.Replace(", ", string.Empty);
            insertedShellcode = System.Text.RegularExpressions.Regex.Replace(insertedShellcode, @"\W+", "");
            shellcode = new byte[insertedShellcode.Length];
            try
            {
                for (int i = 0; i < insertedShellcode.Length; i += 2)
                    shellcode[i / 2] = Convert.ToByte(insertedShellcode.Substring(i, 2), 16);

                if (architecture != 0)
                {
                    disassemble disasm = new disassemble();
                    disasm.disassembleSC(shellcode, architecture, disasmScLB);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Please select an architecutre and insert shellcode into the listbox");
                }
            }
            catch
            {
                MessageBox.Show("Invalid shellcode detected. Only use shellcode in the form of \"\\x##\" or \"0x##\"");
            }         

        }

        private void x86_CheckedChanged(object sender, EventArgs e)
        {
            architecture = 32;
        }

        private void x64_CheckedChanged(object sender, EventArgs e)
        {
            architecture = 64;
        }

        private void fireSc_Click(object sender, EventArgs e)
        {
            if (shellcode.Length > 0)
            {
                shellcodeTester.fireShellcode(architecture, shellcode);
            }
            else
                System.Windows.Forms.MessageBox.Show("Please put shellcode into the text field and disassemble before launching shellcode");
        }
    }
}
