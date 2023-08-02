open System
open ASCIIfromImageFSharpProject.ConsoleHelper
open ASCIIfromImageFSharpProject.Extensions
open ASCIIfromImageFSharpProject.BitmapToASCIIConverter
open System
open System.Drawing

let consoleWindow = Kernel.GetConsoleWindow()
Kernel.ShowWindow(consoleWindow, 3) |> ignore

let consoleFontSize = setCurrentFont 26
    
//let image = char[][]

let printImg = printImageIntoConsole image

Console.ReadKey() |> ignore
