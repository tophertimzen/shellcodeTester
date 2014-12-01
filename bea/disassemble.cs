using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shellcodeTester.bea;
using System.Runtime.InteropServices;

namespace shellcodeTester
{
    class disassemble
    {
        public void disassembleSC(byte[] disasmBytes, uint architecture, System.Windows.Forms.ListBox disasmBox)
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
            //System.Runtime.InteropServices.Marshal.SizeOf(disasmBytes[0]) * disasmBytes.Length;
            IntPtr executionPointer = System.Runtime.InteropServices.Marshal.AllocHGlobal(size);
            System.Runtime.InteropServices.Marshal.Copy(disasmBytes, 0, executionPointer, size);
            IntPtr startingEip = executionPointer;
            disasm.EIP = new IntPtr(executionPointer.ToInt64());        

            int result;
            var disasmPtr = Marshal.AllocHGlobal(Marshal.SizeOf(disasm));
            disasmBox.Items.Add("Disassembled shellcode with " + architecture + "bit");
            var EIPrange = (executionPointer.ToInt64() + size/2);
            try
            {               
                while (disasm.EIP.ToInt64()  < EIPrange)
                {                  
                    System.Runtime.InteropServices.Marshal.StructureToPtr(disasm, disasmPtr, false);
                    result = BeaEngine.Disasm(disasmPtr);
                    Marshal.PtrToStructure(disasmPtr, disasm);
                   // if (result == (int)BeaConstants.SpecialInfo.UNKNOWN_OPCODE)
                   //     break;

                    disasmBox.Items.Add(disasm.CompleteInstr.ToString());

                    disasm.EIP = new IntPtr(disasm.EIP.ToInt64() + result);
                }
            }
            catch
            {
                disasmBox.Items.Add("Something went wrong with disassembly");
            }
        }
    }
}
