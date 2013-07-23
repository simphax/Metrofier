using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Metrofier.Host.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public SEEMasks fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;
        public ShowWindowCommands nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon;
        public IntPtr hProcess;
    }

    public enum ShowWindowCommands : int
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_FORCEMINIMIZE = 11,
        SW_MAX = 11
    }

    public enum SEEMasks : int
    {
        SEE_MASK_DEFAULT = 0x00000000,
        // Note CLASSKEY overrides CLASSNAME
        SEE_MASK_CLASSNAME = 0x00000001,
        SEE_MASK_CLASSKEY = 0x00000003,
        // Note INVOKEIDLIST overrides IDLIST
        SEE_MASK_IDLIST = 0x00000004,
        SEE_MASK_INVOKEIDLIST = 0x0000000c,
        SEE_MASK_ICON = 0x00000010,
        SEE_MASK_HOTKEY = 0x00000020,
        SEE_MASK_NOCLOSEPROCESS = 0x00000040,
        SEE_MASK_CONNECTNETDRV = 0x00000080,
        SEE_MASK_FLAG_DDEWAIT = 0x00000100,
        SEE_MASK_DOENVSUBST = 0x00000200,
        SEE_MASK_FLAG_NO_UI = 0x00000400,
        SEE_MASK_UNICODE = 0x00004000,
        SEE_MASK_NO_CONSOLE = 0x00008000,
        SEE_MASK_ASYNCOK = 0x00100000,
        SEE_MASK_HMONITOR = 0x00200000,
        // #if (_WIN32_IE >= 0x0560)
        SEE_MASK_NOZONECHECKS = 0x00800000,
        // #endif // (_WIN32_IE >= 0x560)
        // #if (_WIN32_IE >= 0x0500)
        SEE_MASK_NOQUERYCLASSSTORE = 0x01000000,
        SEE_MASK_WAITFORINPUTIDLE = 0x02000000,
        // #endif // (_WIN32_IE >= 0x500)
        // #if (_WIN32_IE >= 0x0560)
        SEE_MASK_FLAG_LOG_USAGE = 0x04000000
        // #endif // (_WIN32_IE >= 0x560)
    }

    [Flags()]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues, 
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from 
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        AsynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to 
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE 
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the 
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter 
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid 
        /// contents of the client area are saved and copied back into the client area after the window is sized or 
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to 
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent 
        /// window uncovered as a result of the window being moved. When this flag is set, the application must 
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }

    /// <summary>
    /// Window handles (HWND) used for hWndInsertAfter
    /// </summary>
    public static class HWND
    {
        public static IntPtr
        NoTopMost = new IntPtr(-2),
        TopMost = new IntPtr(-1),
        Top = new IntPtr(0),
        Bottom = new IntPtr(1);
    }

    public class Shell32
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);
    }

    public class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetProcessId(IntPtr Process);

        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();
    }

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    
    public class User32
    {
       public const int GWL_HWNDPARENT = (-8);

       public const int GWL_ID = (-12);
       public const int GWL_STYLE = (-16);
       public const int GWL_EXSTYLE = (-20);

// Window Styles 
       public const UInt32 WS_OVERLAPPED = 0;
       public const UInt32 WS_POPUP = 0x80000000;
       public const UInt32 WS_CHILD = 0x40000000;
       public const UInt32 WS_MINIMIZE = 0x20000000;
       public const UInt32 WS_VISIBLE = 0x10000000;
       public const UInt32 WS_DISABLED = 0x8000000;
       public const UInt32 WS_CLIPSIBLINGS = 0x4000000;
       public const UInt32 WS_CLIPCHILDREN = 0x2000000;
       public const UInt32 WS_MAXIMIZE = 0x1000000;
       public const UInt32 WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
       public const UInt32 WS_BORDER = 0x800000;
       public const UInt32 WS_DLGFRAME = 0x400000;
       public const UInt32 WS_VSCROLL = 0x200000;
       public const UInt32 WS_HSCROLL = 0x100000;
       public const UInt32 WS_SYSMENU = 0x80000;
       public const UInt32 WS_THICKFRAME = 0x40000;
       public const UInt32 WS_GROUP = 0x20000;
       public const UInt32 WS_TABSTOP = 0x10000;
       public const UInt32 WS_MINIMIZEBOX = 0x20000;
       public const UInt32 WS_MAXIMIZEBOX = 0x10000;
       public const UInt32 WS_TILED = WS_OVERLAPPED;
       public const UInt32 WS_ICONIC = WS_MINIMIZE;
       public const UInt32 WS_SIZEBOX = WS_THICKFRAME;

// Extended Window Styles 
       public const UInt32 WS_EX_DLGMODALFRAME = 0x0001;
       public const UInt32 WS_EX_NOPARENTNOTIFY = 0x0004;
       public const UInt32 WS_EX_TOPMOST = 0x0008;
       public const UInt32 WS_EX_ACCEPTFILES = 0x0010;
       public const UInt32 WS_EX_TRANSPARENT = 0x0020;
       public const UInt32 WS_EX_MDICHILD = 0x0040;
       public const UInt32 WS_EX_TOOLWINDOW = 0x0080;
       public const UInt32 WS_EX_WINDOWEDGE = 0x0100;
       public const UInt32 WS_EX_CLIENTEDGE = 0x0200;
       public const UInt32 WS_EX_CONTEXTHELP = 0x0400;
       public const UInt32 WS_EX_RIGHT = 0x1000;
       public const UInt32 WS_EX_LEFT = 0x0000;
       public const UInt32 WS_EX_RTLREADING = 0x2000;
       public const UInt32 WS_EX_LTRREADING = 0x0000;
       public const UInt32 WS_EX_LEFTSCROLLBAR = 0x4000;
       public const UInt32 WS_EX_RIGHTSCROLLBAR = 0x0000;
       public const UInt32 WS_EX_CONTROLPARENT = 0x10000;
       public const UInt32 WS_EX_STATICEDGE = 0x20000;
       public const UInt32 WS_EX_APPWINDOW = 0x40000;
       public const UInt32 WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
       public const UInt32 WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
       public const UInt32 WS_EX_LAYERED = 0x00080000;
       public const UInt32 WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
       public const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
       public const UInt32 WS_EX_COMPOSITED = 0x02000000;
       public const UInt32 WS_EX_NOACTIVATE = 0x08000000;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr OpenDesktop(string lpszDesktop, uint dwFlags,
           bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll")]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop,
           EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit,
           uint dwDesiredAccess);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeWindowStationHandle GetProcessWindowStation();

        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(uint dwThreadId);

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeWindowStationHandle OpenWindowStation(
            [MarshalAs(UnmanagedType.LPTStr)]
         string lpszWinSta,
            [MarshalAs(UnmanagedType.Bool)]
         bool fInherit,
            AccessMask dwDesiredAccess
        );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessWindowStation(IntPtr hWinSta);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetThreadDesktop(IntPtr hDesktop);

        /// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    }

    [Flags]
    public enum AccessMask : uint
    {
        DELETE = 0x00010000,
        READ_CONTROL = 0x00020000,
        WRITE_DAC = 0x00040000,
        WRITE_OWNER = 0x00080000,
        SYNCHRONIZE = 0x00100000,

        STANDARD_RIGHTS_REQUIRED = 0x000f0000,

        STANDARD_RIGHTS_READ = 0x00020000,
        STANDARD_RIGHTS_WRITE = 0x00020000,
        STANDARD_RIGHTS_EXECUTE = 0x00020000,

        STANDARD_RIGHTS_ALL = 0x001f0000,

        SPECIFIC_RIGHTS_ALL = 0x0000ffff,

        ACCESS_SYSTEM_SECURITY = 0x01000000,

        MAXIMUM_ALLOWED = 0x02000000,

        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,

        DESKTOP_READOBJECTS = 0x00000001,
        DESKTOP_CREATEWINDOW = 0x00000002,
        DESKTOP_CREATEMENU = 0x00000004,
        DESKTOP_HOOKCONTROL = 0x00000008,
        DESKTOP_JOURNALRECORD = 0x00000010,
        DESKTOP_JOURNALPLAYBACK = 0x00000020,
        DESKTOP_ENUMERATE = 0x00000040,
        DESKTOP_WRITEOBJECTS = 0x00000080,
        DESKTOP_SWITCHDESKTOP = 0x00000100,

        WINSTA_ENUMDESKTOPS = 0x00000001,
        WINSTA_READATTRIBUTES = 0x00000002,
        WINSTA_ACCESSCLIPBOARD = 0x00000004,
        WINSTA_CREATEDESKTOP = 0x00000008,
        WINSTA_WRITEATTRIBUTES = 0x00000010,
        WINSTA_ACCESSGLOBALATOMS = 0x00000020,
        WINSTA_EXITWINDOWS = 0x00000040,
        WINSTA_ENUMERATE = 0x00000100,
        WINSTA_READSCREEN = 0x00000200,

        WINSTA_ALL_ACCESS = 0x0000037f
    }
    public sealed class SafeWindowStationHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeWindowStationHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return User32.CloseWindowStation(handle);
        }
    }
}
