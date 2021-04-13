
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
    }

    class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr Create();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Detect(IntPtr d);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Free(IntPtr d);

        public static void OperateDLL(string path)
        {
            IntPtr pDll = NativeMethods.LoadLibrary(path);
            //oh dear, error handling here
            if (pDll == IntPtr.Zero)
            {
                Console.WriteLine("Error in loading dll");
            }

            IntPtr CreateFuncAddress= NativeMethods.GetProcAddress(pDll, "Create");
            //oh dear, error handling here
            if (CreateFuncAddress == IntPtr.Zero)
            {
                Console.WriteLine("Error in loading create func");
                // should print an error accured and exit the app
            }
            IntPtr DetectFuncAddress = NativeMethods.GetProcAddress(pDll, "Detect");
            //oh dear, error handling here
            if (CreateFuncAddress == IntPtr.Zero)
            {
                Console.WriteLine("Error in loading detect func");
                // should print an error accured and exit the app
            }
            IntPtr FreeFuncAddress = NativeMethods.GetProcAddress(pDll, "Free");
            //oh dear, error handling here
            if (CreateFuncAddress == IntPtr.Zero)
            {
                Console.WriteLine("Error in loading free func");
                // should print an error accured and exit the app
            }
            Create c = (Create)Marshal.GetDelegateForFunctionPointer(CreateFuncAddress, typeof(Create));
            Detect d = (Detect)Marshal.GetDelegateForFunctionPointer(DetectFuncAddress, typeof(Detect));
            Free f = (Free)Marshal.GetDelegateForFunctionPointer(FreeFuncAddress, typeof(Free));

            IntPtr p = c();
            d(p);
            f(p);
            
            bool result = NativeMethods.FreeLibrary(pDll);
            //remaining code here
        }
    }
}
