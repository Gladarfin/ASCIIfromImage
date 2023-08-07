module ASCIIfromImageFSharpProject.ConsoleHelper
open System
open System.Runtime.InteropServices

module Kernel =      
    [<Struct>]
    [<StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)>]
    type CONSOLE_FONT_INFO_EX =
        val mutable cbSize: int
        val mutable FontIndex: int
        val mutable FontWidth: uint16
        val mutable FontSize: uint16
        val mutable FontFamily: int
        val mutable FontWeight: int
        [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)>]
        val mutable FontName: string
    
    [<DllImport("kernel32.dll", ExactSpelling = true)>]
    extern IntPtr GetConsoleWindow()
    [<DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)>]
    extern bool ShowWindow(IntPtr hWnd, int nCmdShow)
    [<DllImport("kernel32.dll", SetLastError = true)>]
    extern IntPtr GetStdHandle(int nStdHandle);

    [<DllImport("kernel32.dll", SetLastError = true)>]
    extern bool SetCurrentConsoleFontEx(IntPtr consoleOutput, bool maximumWindow, CONSOLE_FONT_INFO_EX consoleCurrentFontEx) 

let FixedWidthTrueType = 54
let StandardOutputHandle = -11

let setCurrentFont fontSize =
    let mutable before = Kernel.CONSOLE_FONT_INFO_EX()
    before.cbSize <- Marshal.SizeOf(typeof<Kernel.CONSOLE_FONT_INFO_EX>)
    
    let mutable set = Kernel.CONSOLE_FONT_INFO_EX()
    set.cbSize <- Marshal.SizeOf(typeof<Kernel.CONSOLE_FONT_INFO_EX>)
    set.FontIndex <- 0
    set.FontFamily <- FixedWidthTrueType
    set.FontName <- "Consolas"
    set.FontWeight <- 400
    set.FontSize <- if fontSize > 0 then uint16 fontSize else uint16 before.cbSize
    let ConsoleOutputHandle = Kernel.GetStdHandle(StandardOutputHandle)
    Kernel.SetCurrentConsoleFontEx(ConsoleOutputHandle, false, set)

let printImageIntoConsole (outputImage: char[][]) =
    outputImage |> Array.iter Console.WriteLine
    //If we set the cursor after the image output, console doesn't show the full image
    //Console.SetCursorPosition(0, 0)
    Console.ReadKey() 
    