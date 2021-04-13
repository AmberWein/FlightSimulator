
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator
{
    class NativeMethods
    {
        
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Func();
    }

    class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Detect();

        public static void OperateDLL()
        {
            IntPtr pDll = NativeMethods.LoadLibrary("plugins/SimpleAnomalyDetectorDll.dll");
            //oh dear, error handling here
            if (pDll == IntPtr.Zero)
            {

            }

            IntPtr detectorAddress = NativeMethods.GetProcAddress(pDll, "Create");
            //oh dear, error handling here
            if (detectorAddress == IntPtr.Zero)
            {
                // should print an error accured and exit the app
            }

            IntPtr detectFuncAddress = NativeMethods.GetProcAddress(pDll, "Detect");
            //oh dear, error handling here
            if (detectFuncAddress == IntPtr.Zero)
            {
                // should print an error accured and exit the app
            }

            Detect d = (Detect)Marshal.GetDelegateForFunctionPointer(detectFuncAddress, typeof(Detect));
            d();
            //int theResult = multiplyByTen(10);

            bool result = NativeMethods.FreeLibrary(pDll);
            //remaining code here
        }
    }
}
