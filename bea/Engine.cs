using System.Runtime.InteropServices;
using System;
namespace shellcodeTester.bea
{
    public class BeaEngine
    {
        [DllImport("BeaEngine.dll")]
        public static extern int Disasm([In, Out] IntPtr diasm);
    }
}
