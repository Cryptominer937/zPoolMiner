namespace zPoolMiner
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="HashKingsProcess" />
    /// </summary>
    public class HashKingsProcess
    {
        /// <summary>
        /// Defines the CREATE_NEW_CONSOLE
        /// </summary>
        private const uint CREATE_NEW_CONSOLE = 0x00000010;

        /// <summary>
        /// Defines the NORMAL_PRIORITY_CLASS
        /// </summary>
        private const uint NORMAL_PRIORITY_CLASS = 0x0020;

        /// <summary>
        /// Defines the CREATE_NO_WINDOW
        /// </summary>
        private const uint CREATE_NO_WINDOW = 0x08000000;

        /// <summary>
        /// Defines the STARTF_USESHOWWINDOW
        /// </summary>
        private const int STARTF_USESHOWWINDOW = 0x00000001;

        /// <summary>
        /// Defines the SW_SHOWMINNOACTIVE
        /// </summary>
        private const short SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// Defines the INFINITE
        /// </summary>
        private const uint INFINITE = 0xFFFFFFFF;

        /// <summary>
        /// Defines the STILL_ACTIVE
        /// </summary>
        private const uint STILL_ACTIVE = 259;

        /// <summary>
        /// Defines the <see cref="PROCESS_INFORMATION" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            /// <summary>
            /// Defines the hProcess
            /// </summary>
            public IntPtr hProcess;

            /// <summary>
            /// Defines the hThread
            /// </summary>
            public IntPtr hThread;

            /// <summary>
            /// Defines the ProcessId
            /// </summary>
            public int ProcessId;

            /// <summary>
            /// Defines the ThreadId
            /// </summary>
            public int ThreadId;
        }

        /// <summary>
        /// Defines the <see cref="SECURITY_ATTRIBUTES" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            /// <summary>
            /// Defines the nLength
            /// </summary>
            public int nLength;

            /// <summary>
            /// Defines the lpSecurityDescriptor
            /// </summary>
            public IntPtr lpSecurityDescriptor;

            /// <summary>
            /// Defines the bInheritHandle
            /// </summary>
            public int bInheritHandle;
        }

        /// <summary>
        /// Defines the <see cref="STARTUPINFO" />
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO
        {
            /// <summary>
            /// Defines the cb
            /// </summary>
            public int cb;

            /// <summary>
            /// Defines the lpReserved
            /// </summary>
            public string lpReserved;

            /// <summary>
            /// Defines the lpDesktop
            /// </summary>
            public string lpDesktop;

            /// <summary>
            /// Defines the lpTitle
            /// </summary>
            public string lpTitle;

            /// <summary>
            /// Defines the dwX
            /// </summary>
            public int dwX;

            /// <summary>
            /// Defines the dwY
            /// </summary>
            public int dwY;

            /// <summary>
            /// Defines the dwXSize
            /// </summary>
            public int dwXSize;

            /// <summary>
            /// Defines the dwYSize
            /// </summary>
            public int dwYSize;

            /// <summary>
            /// Defines the dwXCountChars
            /// </summary>
            public int dwXCountChars;

            /// <summary>
            /// Defines the dwYCountChars
            /// </summary>
            public int dwYCountChars;

            /// <summary>
            /// Defines the dwFillAttribute
            /// </summary>
            public int dwFillAttribute;

            /// <summary>
            /// Defines the dwFlags
            /// </summary>
            public int dwFlags;

            /// <summary>
            /// Defines the wShowWindow
            /// </summary>
            public short wShowWindow;

            /// <summary>
            /// Defines the cbReserved2
            /// </summary>
            public short cbReserved2;

            /// <summary>
            /// Defines the lpReserved2
            /// </summary>
            public IntPtr lpReserved2;

            /// <summary>
            /// Defines the hStdInput
            /// </summary>
            public IntPtr hStdInput;

            /// <summary>
            /// Defines the hStdOutput
            /// </summary>
            public IntPtr hStdOutput;

            /// <summary>
            /// Defines the hStdError
            /// </summary>
            public IntPtr hStdError;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashKingsProcess"/> class.
        /// </summary>
        public HashKingsProcess() => StartInfo = new ProcessStartInfo();

        /// <summary>
        /// The CreateProcess
        /// </summary>
        /// <param name="lpApplicationName">The <see cref="string"/></param>
        /// <param name="lpCommandLine">The <see cref="string"/></param>
        /// <param name="lpProcessAttributes">The <see cref="SECURITY_ATTRIBUTES"/></param>
        /// <param name="lpThreadAttributes">The <see cref="SECURITY_ATTRIBUTES"/></param>
        /// <param name="bInheritHandles">The <see cref="bool"/></param>
        /// <param name="dwCreationFlags">The <see cref="uint"/></param>
        /// <param name="lpEnvironment">The <see cref="IntPtr"/></param>
        /// <param name="lpCurrentDirectory">The <see cref="string"/></param>
        /// <param name="lpStartupInfo">The <see cref="STARTUPINFO"/></param>
        /// <param name="lpProcessInformation">The <see cref="PROCESS_INFORMATION"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);

        /// <summary>
        /// The TerminateProcess
        /// </summary>
        /// <param name="hProcess">The <see cref="IntPtr"/></param>
        /// <param name="uExitCode">The <see cref="uint"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        /// <summary>
        /// The CloseHandle
        /// </summary>
        /// <param name="hObject">The <see cref="IntPtr"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// The GetExitCodeProcess
        /// </summary>
        /// <param name="hProcess">The <see cref="IntPtr"/></param>
        /// <param name="lpExitCode">The <see cref="uint"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

        /// <summary>
        /// The WaitForSingleObject
        /// </summary>
        /// <param name="hHandle">The <see cref="IntPtr"/></param>
        /// <param name="dwMilliseconds">The <see cref="UInt32"/></param>
        /// <returns>The <see cref="UInt32"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        /// <summary>
        /// The GetStdHandle
        /// </summary>
        /// <param name="nStdHandle">The <see cref="int"/></param>
        /// <returns>The <see cref="IntPtr"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        /// <summary>
        /// The CreatePipe
        /// </summary>
        /// <param name="hReadPipe">The <see cref="IntPtr"/></param>
        /// <param name="hWritePipe">The <see cref="IntPtr"/></param>
        /// <param name="lpPipeAttributes">The <see cref="SECURITY_ATTRIBUTES"/></param>
        /// <param name="nSize">The <see cref="uint"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll")]
        private static extern bool CreatePipe(out IntPtr hReadPipe, out IntPtr hWritePipe,
           ref SECURITY_ATTRIBUTES lpPipeAttributes, uint nSize);

        // ctrl+c
        /// <summary>
        /// The AttachConsole
        /// </summary>
        /// <param name="dwProcessId">The <see cref="uint"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        /// <summary>
        /// The FreeConsole
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();

        /// <summary>
        /// The GenerateConsoleCtrlEvent
        /// </summary>
        /// <param name="dwCtrlEvent">The <see cref="CtrlTypes"/></param>
        /// <param name="dwProcessGroupId">The <see cref="uint"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

        /// <summary>
        /// The SetConsoleCtrlHandler
        /// </summary>
        /// <param name="handler">The <see cref="HandlerRoutine"/></param>
        /// <param name="add">The <see cref="bool"/></param>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("Kernel32", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);

        /// <summary>
        /// The AllocConsole
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        [DllImport("kernel32.dll", EntryPoint = "AllocConsole")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        /// <summary>
        /// The GetLastError
        /// </summary>
        /// <returns>The <see cref="uint"/></returns>
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();

        /// <summary>
        /// Defines the CtrlTypes
        /// </summary>
        private enum CtrlTypes
        {
            /// <summary>
            /// Defines the CTRL_C_EVENT
            /// </summary>
            CTRL_C_EVENT = 0,

            /// <summary>
            /// Defines the CTRL_BREAK_EVENT
            /// </summary>
            CTRL_BREAK_EVENT,

            /// <summary>
            /// Defines the CTRL_CLOSE_EVENT
            /// </summary>
            CTRL_CLOSE_EVENT,

            /// <summary>
            /// Defines the CTRL_LOGOFF_EVENT
            /// </summary>
            CTRL_LOGOFF_EVENT = 5,

            /// <summary>
            /// Defines the CTRL_SHUTDOWN_EVENT
            /// </summary>
            CTRL_SHUTDOWN_EVENT
        }

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.        /// <summary>
        /// The HandlerRoutine
        /// </summary>
        /// <param name="CtrlType">The <see cref="CtrlTypes"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private delegate bool HandlerRoutine(CtrlTypes CtrlType);

        /// <summary>
        /// The ExitEventDelegate
        /// </summary>
        public delegate void ExitEventDelegate();

        /// <summary>
        /// Defines the StartInfo
        /// </summary>
        public ProcessStartInfo StartInfo;

        /// <summary>
        /// Defines the ExitEvent
        /// </summary>
        public ExitEventDelegate ExitEvent;

        /// <summary>
        /// Defines the ExitCode
        /// </summary>
        public uint ExitCode;

        /// <summary>
        /// Defines the Id
        /// </summary>
        public int Id;

        /// <summary>
        /// Defines the tHandle
        /// </summary>
        private Thread tHandle;

        /// <summary>
        /// Defines the bRunning
        /// </summary>
        private bool bRunning;

        /// <summary>
        /// Defines the pHandle
        /// </summary>
        private IntPtr pHandle;

        /// <summary>
        /// The Start
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool Start()
        {
            var pInfo = new PROCESS_INFORMATION();
            var sInfo = new STARTUPINFO();
            var pSec = new SECURITY_ATTRIBUTES();
            var tSec = new SECURITY_ATTRIBUTES();
            pSec.nLength = Marshal.SizeOf(pSec);
            tSec.nLength = Marshal.SizeOf(tSec);

            uint sflags = 0;

            if (StartInfo.CreateNoWindow)
            {
                sflags = CREATE_NO_WINDOW;
            }
            else if (StartInfo.WindowStyle == ProcessWindowStyle.Minimized)
            {
                sInfo.dwFlags = STARTF_USESHOWWINDOW;
                sInfo.wShowWindow = SW_SHOWMINNOACTIVE;
                sflags = CREATE_NEW_CONSOLE;
            }
            else
            {
                sflags = CREATE_NEW_CONSOLE;
            }

            string workDir = null;

            if (StartInfo.WorkingDirectory != null && StartInfo.WorkingDirectory.Length > 0)
                workDir = StartInfo.WorkingDirectory;

            var res = CreateProcess(StartInfo.FileName,
                " " + StartInfo.Arguments,
                ref pSec,
                ref tSec,
                false,
                sflags | NORMAL_PRIORITY_CLASS,
                IntPtr.Zero,
                workDir,
                ref sInfo,
                out pInfo);

            if (!res)
            {
                var err = Marshal.GetLastWin32Error();
                throw new Exception("Failed to start process, err=" + err.ToString());
            }

            CloseHandle(sInfo.hStdError);
            CloseHandle(sInfo.hStdInput);
            CloseHandle(sInfo.hStdOutput);

            pHandle = pInfo.hProcess;
            CloseHandle(pInfo.hThread);

            Id = pInfo.ProcessId;

            if (ExitEvent != null)
            {
                bRunning = true;
                tHandle = new Thread(CThread);
                tHandle.Start();
            }

            return true;
        }

        /// <summary>
        /// The Kill
        /// </summary>
        public void Kill()
        {
            if (pHandle == IntPtr.Zero) return;

            if (tHandle != null)
            {
                bRunning = false;
                tHandle.Join();
            }

            TerminateProcess(pHandle, 0);
            CloseHandle(pHandle);
            pHandle = IntPtr.Zero;
        }

        /// <summary>
        /// The Close
        /// </summary>
        public void Close()
        {
            if (pHandle == IntPtr.Zero) return;

            if (tHandle != null)
            {
                bRunning = false;
                tHandle.Join();
            }

            CloseHandle(pHandle);
            pHandle = IntPtr.Zero;
        }

        /// <summary>
        /// The SignalCtrl
        /// </summary>
        /// <param name="thisConsoleId">The <see cref="uint"/></param>
        /// <param name="dwProcessId">The <see cref="uint"/></param>
        /// <param name="dwCtrlEvent">The <see cref="CtrlTypes"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private bool SignalCtrl(uint thisConsoleId, uint dwProcessId, CtrlTypes dwCtrlEvent)
        {
            var success = false;
            // uint thisConsoleId = GetCurrentProcessId();
            // Leave current console if it exists
            // (otherwise AttachConsole will return ERROR_ACCESS_DENIED)
            var consoleDetached = (FreeConsole() != false);

            if (AttachConsole(dwProcessId) != false)
            {
                // Add a fake Ctrl-C handler for avoid instant kill is this console
                // WARNING: do not revert it or current program will be also killed
                SetConsoleCtrlHandler(null, true);
                success = (GenerateConsoleCtrlEvent(dwCtrlEvent, 0) != false);
                FreeConsole();
                // wait for termination so we don't terminate NHM
                WaitForSingleObject(pHandle, 10000);
            }

            if (consoleDetached)
            {
                // Create a new console if previous was deleted by OS
                if (AttachConsole(thisConsoleId) == false)
                {
                    var errorCode = GetLastError();

                    if (errorCode == 31)
                    { // 31=ERROR_GEN_FAILURE
                        AllocConsole();
                    }
                }

                SetConsoleCtrlHandler(null, false);
            }

            return success;
        }

        /// <summary>
        /// The SendCtrlC
        /// </summary>
        /// <param name="thisConsoleId">The <see cref="uint"/></param>
        public void SendCtrlC(uint thisConsoleId)
        {
            if (pHandle == IntPtr.Zero) return;

            if (tHandle != null)
            {
                bRunning = false;
                tHandle.Join();
            }

            SignalCtrl(thisConsoleId, (uint)Id, CtrlTypes.CTRL_C_EVENT);
            pHandle = IntPtr.Zero;
        }

        /// <summary>
        /// The CThread
        /// </summary>
        private void CThread()
        {
            while (bRunning)
            {
                if (WaitForSingleObject(pHandle, 10) == 0)
                {
                    GetExitCodeProcess(pHandle, out ExitCode);

                    if (ExitCode != STILL_ACTIVE)
                    {
                        CloseHandle(pHandle);
                        pHandle = IntPtr.Zero;
                        ExitEvent();
                        return;
                    }
                }
            }
        }
    }
}