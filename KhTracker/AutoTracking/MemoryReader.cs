using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace KhTracker
{
    public class MemoryReader
    {
        const int PROCESS_WM_READ = 0x0010;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, Int64 lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        Process process;
        IntPtr processHandle;
        public bool Hooked;
        private bool PCSX2;
        public MemoryReader(bool ps2)
        {
            PCSX2 = ps2;
            try
            {
                if (PCSX2)
                    process = Process.GetProcessesByName("pcsx2")[0];
                else
                    process = Process.GetProcessesByName("KINGDOM HEARTS II FINAL MIX")[0];
                processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
            }
            catch (IndexOutOfRangeException)
            {
                Hooked = false;
                return;
            }
            Hooked = true;

        }

        public byte[] ReadMemory(Int32 address, int bytesToRead)
        {
            if (process.HasExited)
            {
                throw new Exception();
            }
            int bytesRead = 0;
            byte[] buffer = new byte[bytesToRead];

            ProcessModule processModule = process.MainModule;

            if (PCSX2)
                ReadProcessMemory((int)processHandle, address, buffer, buffer.Length, ref bytesRead);
            else
                ReadProcessMemory((int)processHandle, processModule.BaseAddress.ToInt64() + address, buffer, buffer.Length, ref bytesRead);

            return buffer;
        }
    }
}
