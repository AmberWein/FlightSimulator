
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
        /*
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
        private delegate int MultiplyByTen(int numberToMultiply);

        static void Main(string[] args)
        {
            IntPtr pDll = NativeMethods.LoadLibrary(@"PathToYourDll.DLL");
            //oh dear, error handling here
            //if (pDll == IntPtr.Zero)

            IntPtr detectorAdress = NativeMethods.GetProcAddress(pDll, "Create");
            //oh dear, error handling here
            //if(pAddressOfFunctionToCall == IntPtr.Zero)

            IntPtr detectAdress = NativeMethods.GetProcAddress(pDll, "detect");

            detect d = (detect)Marshal.GetDelegateForFunctionPointer(detectorAdress, typeof(detect));

            //int theResult = multiplyByTen(10);

            bool result = NativeMethods.FreeLibrary(pDll);
            //remaining code here

            Console.WriteLine(theResult);
        }
        */
    }
}
