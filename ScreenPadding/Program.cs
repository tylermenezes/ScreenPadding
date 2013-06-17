using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ScreenPadding
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(
                                                        int uiAction,
                                                        int uiParam,
                                                        ref RECT pvParam,
                                                        int fWinIni);

        private const Int32 SPIF_SENDWININICHANGE = 2;
        private const Int32 SPIF_UPDATEINIFILE = 1;
        private const Int32 SPIF_change = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;
        private const Int32 SPI_SETWORKAREA = 47;
        private const Int32 SPI_GETWORKAREA = 48;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Int32 Left;
            public Int32 Top;
            public Int32 Right;
            public Int32 Bottom;
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoA")]
        private static extern Int32 SystemParametersInfo(Int32 uAction, Int32 uParam, IntPtr lpvParam, Int32 fuWinIni);


        private static void SetWorkspace(Rectangle r)
        {
            RECT rect = new RECT
            {
                Left = r.Left,
                Right = r.Right,
                Top = r.Top,
                Bottom = r.Bottom
            };

            SystemParametersInfo(SPI_SETWORKAREA, 0, ref rect, SPIF_change);
        }

        static void Main(string[] args)
        {
            bool parsedCorrectly = false;
            int top = 0,
                bottom = 0,
                left = 0,
                right = 0;

            if (args.Length == 4) {
                parsedCorrectly = true;
                
                parsedCorrectly = parsedCorrectly && int.TryParse(args[0], out top);
                parsedCorrectly = parsedCorrectly && int.TryParse(args[1], out right);
                parsedCorrectly = parsedCorrectly && int.TryParse(args[2], out bottom);
                parsedCorrectly = parsedCorrectly && int.TryParse(args[3], out left);
            }


            if (!parsedCorrectly) // The number of arguments was wrong or one of the arguments wasn't an integer
            {
                Console.WriteLine("Usage: " + Path.GetFileNameWithoutExtension(Application.ExecutablePath) + " top right bottom left");
            }
            else // Pad the screen
            {
                var bounds = Screen.PrimaryScreen.Bounds;
                var newWorkingArea = new Rectangle(left, top, bounds.Width - left - right, bounds.Height - top - bottom);
                SetWorkspace(newWorkingArea);
            }
        }
    }
}
