/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
*/
//namespace FlightSimulator.plugins
//{
    //class AnomalyDetector
   // {
        /*
        [DllImport(".dll")]
        public static extern IntPtr Create(int x);
        [DllImport("AnomalyDetector.dll")]
        public static extern int AnomalyDetectorAdd(IntPtr s, int y);
        static void Main(string[] args)
        {
            IntPtr a = Create(5);
            Console.WriteLine(AnomalyDetectorAdd(a, 11));
            Console.WriteLine("Hello");
        }
        */
        /*
        static class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern IntPtr LoadLibrary(string dllToLoad);

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

            [DllImport("kernel32.dll")]
            public static extern bool FreeLibrary(IntPtr hModule);
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

                IntPtr pAddressOfLearnNormal = NativeMethods.GetProcAddress(pDll, "learnNormal");
                //oh dear, error handling here
                //if(pAddressOfFunctionToCall == IntPtr.Zero)
                IntPtr pAddressOfDetect = NativeMethods.GetProcAddress(pDll, "detect");

                learnNormal ln = (learnNormal)Marshal.GetDelegateForFunctionPointer
                    (pAddressOfLearnNormal,typeof(learnNormal));
                detect d = (detect)Marshal.GetDelegateForFunctionPointer
                    (pAddressOfLearnNormal, typeof(detect));

                int theResult = multiplyByTen(10);

                bool result = NativeMethods.FreeLibrary(pDll);
                //remaining code here

                Console.WriteLine(theResult);
            }
        }
    }
}
*/