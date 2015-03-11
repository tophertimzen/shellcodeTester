using System;
using System.Collections;
using System.Linq;
using System.Text;
using shellcodeTester.bea;
using System.Runtime.InteropServices;

namespace shellcodeTester
{
    class disassemble
    {
        /// <summary>
        /// Disassemble a given piece of shellcode. 
        /// </summary>
        /// <param name="disasmBytes">The byte array to disassemble</param>
        /// <param name="architecture">The architecture to target. Either 32 or 64 bit Windows</param>
        /// <param name="disasmBox">The listbox to place the disassembly</param>
        /// <param name="showOffsets">Boolean to determine whether or not to display address offsets in the disassembly listing.</param>
        public void disassembleSC(byte[] disasmBytes, uint architecture, System.Windows.Forms.RichTextBox disasmBox, bool showOffsets)
        {            
            var disasm = new Disasm();
            disasm.Options = 0x200;//display in NASM syntax 

            if (architecture == 32)
            {
                disasm.Archi = 32;
            }
            else if (architecture == 64)
            {
                disasm.Archi = 64;
            }

            int size = disasmBytes.Length;
            IntPtr executionPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.Copy(disasmBytes, 0, executionPointer, size);
            disasm.EIP = new IntPtr(executionPointer.ToInt64());        

            int result;
            var disasmPtr = Marshal.AllocHGlobal(Marshal.SizeOf(disasm));
            disasmBox.AppendText("Disassembled shellcode with " + architecture + "bit\n");
            var EIPrange = (executionPointer.ToInt64() + size/2);

            try
            {               
                while (disasm.EIP.ToInt64()  < EIPrange)
                {                  
                    System.Runtime.InteropServices.Marshal.StructureToPtr(disasm, disasmPtr, false);
                    result = BeaEngine.Disasm(disasmPtr);
                    Marshal.PtrToStructure(disasmPtr, disasm);
                    if (result == (int)BeaConstants.SpecialInfo.UNKNOWN_OPCODE)
                    {
                        disasmBox.AppendText("Unknown opcode error @ " + disasm.EIP.ToString("X") + "\n");
                        break;
                    }

                    if (showOffsets)
                        disasmBox.AppendText(disasm.EIP.ToString("X") + "h : " + disasm.CompleteInstr.ToString() + "\n");
                    else
                        disasmBox.AppendText(disasm.CompleteInstr.ToString() + "\n");

                    if (disasm.Instruction.Opcode.ToString("X") == "C3")
                        break;
                    disasm.EIP = new IntPtr(disasm.EIP.ToInt64() + result);
                  
                }
            }
            catch
            {
                disasmBox.AppendText("Something went wrong with disassembly\n");
            }
        }
    }
}
