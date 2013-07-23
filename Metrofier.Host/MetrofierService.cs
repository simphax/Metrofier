using Metrofier.Metadata;
using Metrofier.Host.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Metrofier.Host
{
    public class MetrofierService : IMetrofierService
    {

        Dictionary<uint, IntPtr> launchedWindows;

        public MetrofierService()
        {
            launchedWindows = new Dictionary<uint, IntPtr>();
        }

        private bool BringForward(IntPtr window)
        {
            
	        User32.ShowWindow(window, ShowWindowCommands.SW_HIDE);
	        uint lStyle = (uint)User32.GetWindowLong(window, User32.GWL_STYLE);
            lStyle &= ~(User32.WS_CAPTION | User32.WS_THICKFRAME | User32.WS_MINIMIZE | User32.WS_MAXIMIZE | User32.WS_SYSMENU);
            User32.SetWindowLong(window, User32.GWL_STYLE, (int)lStyle);

            uint lExStyle = (uint)User32.GetWindowLong(window, User32.GWL_EXSTYLE);
            lExStyle &= ~(User32.WS_EX_DLGMODALFRAME | User32.WS_EX_CLIENTEDGE | User32.WS_EX_STATICEDGE | User32.WS_EX_APPWINDOW);
            lExStyle |= User32.WS_EX_TOOLWINDOW;

            User32.SetWindowLong(window, User32.GWL_EXSTYLE, (int)lExStyle);
	        User32.ShowWindow(window, ShowWindowCommands.SW_SHOW);
            
            return User32.SetWindowPos(window, HWND.TopMost, 0, 20, 500, 500, SetWindowPosFlags.FrameChanged);
        }

        private bool enumWindowsProc(IntPtr hWnd, IntPtr lParam)
        {
            uint processId;

            User32.GetWindowThreadProcessId(hWnd, out processId);

            if (processId == (uint)lParam)
            {
                launchedWindows[processId] = hWnd;

                bool result = BringForward(hWnd);

                return false;
            }

            return true;
        }

        public uint Start(string file, string directory)
        {

            IntPtr callingWindow = User32.GetForegroundWindow();

            SHELLEXECUTEINFO execInfo = new SHELLEXECUTEINFO();
            execInfo.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(execInfo);
            execInfo.fMask = SEEMasks.SEE_MASK_NOCLOSEPROCESS;
            execInfo.hwnd = callingWindow;
            execInfo.lpFile = file;
            execInfo.lpDirectory = directory;
            execInfo.nShow = ShowWindowCommands.SW_SHOWNOACTIVATE;

            if (!Shell32.ShellExecuteEx(ref execInfo))
            {
                return 100001;// throw new Exception("Could not execute");
            }

            if (execInfo.hProcess.ToInt64() > 0)
            {
                User32.SetForegroundWindow(callingWindow);

                Thread.Sleep(1000);

                User32.SetForegroundWindow(callingWindow);

                uint procId = Kernel32.GetProcessId(execInfo.hProcess);

                Thread.Sleep(2000);

                User32.SetForegroundWindow(callingWindow);

                User32.EnumWindows(enumWindowsProc, (IntPtr)procId);

                return procId;

            }
            else
            {
                return 100002;// throw new Exception("Could not get process handle");
            }
        }

        public bool Show(uint processId)
        {
            return User32.ShowWindow(launchedWindows[processId], ShowWindowCommands.SW_SHOW);
        }

        public bool Hide(uint processId)
        {
            return User32.ShowWindow(launchedWindows[processId], ShowWindowCommands.SW_HIDE);
        }

        public bool Close(uint processId)
        {
            return false;//PostMessage(launchedWindow, WM_CLOSE, 0, 0);
        }

        public bool Resize(uint processId, int width, int height)
        {
            return false;
        }
    }
}
