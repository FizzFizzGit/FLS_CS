using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace GD_URIConvert {
    public class ClipboardHelper{

        [DllImport ("user32.dll", SetLastError = true)]
        private extern static void AddClipboardFormatListener (IntPtr hwnd);

        [DllImport ("user32.dll", SetLastError = true)]
        private extern static void RemoveClipboardFormatListener (IntPtr hwnd);

        public static readonly int ClipboardUpdate = 0x31d;
        IntPtr handle;
        HwndSource hwndSource = null;
        public event EventHandler DrawClipboard;

        public ClipboardHelper(IntPtr handle){
            hwndSource = HwndSource.FromHwnd(handle);
            hwndSource.AddHook(WndProc);
            this.handle = handle;
        }

        private void raiseDrawClipboard(){
            DrawClipboard?.Invoke(this, EventArgs.Empty);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParaml, ref bool handled){
            if(msg == ClipboardUpdate){
                this.raiseDrawClipboard();
                handled = true;
            }

            return IntPtr.Zero;

        }

        public void Start(){
            AddClipboardFormatListener(handle);
        }
        
        public void Stop(){
            RemoveClipboardFormatListener(handle);
        }
    }

}