using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace shellcodeTester
{
    public class shellcodeTester
    {
        #region dll imports
        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName,
            string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
            uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public unsafe byte* lpSecurityDescriptor;
            public int bInheritHandle;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [Flags]
        public enum AllocationType
        {
            COMMIT = 0x00001000
        }

        [Flags]
        public enum MemoryProtection
        {
            EXECUTE_READWRITE = 0x0040,
        }

        [DllImport("kernel32.dll")]
        public static extern void WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

        [DllImport("kernel32.dll")]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern bool Wow64DisableWow64FsRedirection(out IntPtr oldValue);

        [DllImport("kernel32.dll")]
        static extern bool Wow64RevertWow64FsRedirection(IntPtr oldValue);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        public static extern bool VirtualFree(IntPtr lpAddress, UInt32 dwSize, UInt32 dwFreeType);

        #endregion

        #region fire shellcode
        public static void fireShellcode(uint architecture, byte[] shellcode, bool isCalcTarget)
        {
            if (isCalcTarget)
            {
                fireAgainstCalc(architecture, shellcode);
            }
            else
            {
                IntPtr virtualMemory = VirtualAlloc(shellcode);
                callATrueIntPtr(virtualMemory);
                VirtualFree(virtualMemory, 0, 0x8000);
            }
        }


        public static void fireAgainstCalc(uint architecture, byte[] shellcode)
        {
            IntPtr oldValue = IntPtr.Zero;
            bool disabledWOW = false;
            string file = string.Empty;
            if (architecture == 32)
            {
                file = "C:\\Windows\\SysWOW64\\calc.exe";
            }
            else if (architecture == 64)
            {
                disabledWOW = Wow64DisableWow64FsRedirection(out oldValue);
                file = "C:\\Windows\\System32\\calc.exe";
            }

            PROCESS_INFORMATION pInfo = new PROCESS_INFORMATION();
            STARTUPINFO sInfo = new STARTUPINFO();
            SECURITY_ATTRIBUTES pSec = new SECURITY_ATTRIBUTES();
            SECURITY_ATTRIBUTES tSec = new SECURITY_ATTRIBUTES();
            pSec.nLength = Marshal.SizeOf(pSec);
            tSec.nLength = Marshal.SizeOf(tSec);

            if (!CreateProcess(file, null, ref pSec, ref tSec, false, 0x0020, IntPtr.Zero, null, ref sInfo, out pInfo))
            {
                System.Windows.Forms.MessageBox.Show("Couldn't create process");
                return;
            }
            if (disabledWOW == true)
            {
                Wow64RevertWow64FsRedirection(oldValue);
            }

            System.Threading.Thread.Sleep(2000);
            IntPtr hHandle = pInfo.hProcess;
            IntPtr hAlloc = VirtualAllocEx(hHandle, IntPtr.Zero, (uint)shellcode.Length, AllocationType.COMMIT, MemoryProtection.EXECUTE_READWRITE);
            UIntPtr bytesWritten = UIntPtr.Zero;
            WriteProcessMemory(hHandle, hAlloc, shellcode, (uint)shellcode.Length, out bytesWritten);
            uint iThreadId = 0;
            IntPtr hThread = CreateRemoteThread(hHandle, IntPtr.Zero, 0, hAlloc, IntPtr.Zero, 0, out iThreadId);
            System.Threading.Thread.Sleep(1000);
            CloseHandle(hThread);
            CloseHandle(hHandle);
        }
        #endregion fire shellcode

        #region target against self
        public delegate void launchShellCodeIntPtr(IntPtr target);

        //Virtual alloc and marshal copy shellcode to an IntPtr. 
        public static IntPtr VirtualAlloc(byte[] shellcodeIN)
        {
            IntPtr virtualMemory = VirtualAlloc(IntPtr.Zero, new UIntPtr((uint)shellcodeIN.Length), AllocationType.COMMIT, MemoryProtection.EXECUTE_READWRITE);
            System.Runtime.InteropServices.Marshal.Copy(shellcodeIN, 0, virtualMemory, shellcodeIN.Length);
            return virtualMemory;
        }

        public static void callATrueIntPtr(IntPtr intPtrToFire)
        {
            IntPtr p = VirtualAlloc(call_a_fun_ptr);

            launchShellCodeIntPtr fireShellcode = (launchShellCodeIntPtr)System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer(p, typeof(launchShellCodeIntPtr));
            try
            {
                fireShellcode(intPtrToFire);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Failed in callATrueIntPtr because of " + ex.Message);
            }

            VirtualFree(p, 0, 0x8000);
        }

        /// <summary>
        /// Takes an IntPtr as an argument and will call it. 
        /// </summary>
        static public byte[] call_a_fun_ptr = new byte[]
        {          
            0x60, //pushad
            0x55,//push ebp
            0x89, 0xe5, //mov ebp, esp
            0x8b, 0x44, 0x24, 0x28,  //mov eax, [esp + 28]
            0xff, 0xd0, //call eax
            0x89, 0xec,//mov esp, ebp
            0x5d, //pop ebp
            0x61, //popad
            0xc3//ret
        };
        #endregion target against self
    }
}

