using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using Point = System.Windows.Point;

namespace OpenHab.Wpf.View.Resources.Helpers.ElysiumDragFix
{
    public static class MouseHelper
    {
        public static Point GetMousePosition(bool scaled = true)
        {
            var w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);

            if (!scaled)
                return new Point(w32Mouse.X, w32Mouse.Y);
            var scale = GetCurrentDisplayScaling();
            return new Point(w32Mouse.X / scale.X, w32Mouse.Y / scale.Y);
        }

        private static Vector GetCurrentDisplayScaling()
        {
            using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                return new Vector(graphics.DpiX / 96.0, graphics.DpiY / 96.0);
            }
        }

        #region p/Invoke

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public int X;
            public int Y;
        }

        #endregion p/Invoke
    }
}