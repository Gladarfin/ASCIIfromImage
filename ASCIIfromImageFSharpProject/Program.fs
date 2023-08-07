open System
open System.Collections.Generic
open System.IO
open ASCIIfromImageFSharpProject.ConsoleHelper
open ASCIIfromImageFSharpProject.Extensions
open ASCIIfromImageFSharpProject.BitmapToASCIIConverter

let window = Kernel.GetConsoleWindow()
let fullWindow = Kernel.ShowWindow(window, 3)
let mutable isMainLoopContinue = true
   
let saveResultToFile (fileName : string, outputImage : IEnumerable<char[]>, outputImageNegative : IEnumerable<char[]>) =
    File.WriteAllLines($"{fileName}ASCIINegative.txt", outputImage |> Seq.map (fun r -> new string(r)))
    File.WriteAllLines($"{fileName}ASCII.txt", outputImageNegative |> Seq.map (fun r -> new string(r)))

let GetUserInputAndContinue() =
    let mutable innerLoop = true   
    while innerLoop do
        Console.Write("Want to convert an image into ASCII? (y/n): ")
        let answer = Console.ReadLine().ToLower()
        match answer with
        | "n" -> 
            isMainLoopContinue <- false
            innerLoop <- false
        | "y" -> innerLoop <- false
        | _ -> Console.WriteLine("Incorrect input, try again...")   
    isMainLoopContinue
    
let rec mainLoop() =
    setCurrentFont 26 |> ignore
    let input = GetUserInputAndContinue()
    if  input = false then
        Console.WriteLine("Press any key to exit...")
        Environment.Exit(0)
    else
        let imageToProceed = proceedImage()
        let imageName = imageName
        
        if (imageToProceed = null) then
            mainLoop()

        Console.WriteLine("After the image is generated, press any button to save it into a local TXT file.")
        Console.WriteLine("Press any key to continue...")
        Console.ReadKey() |> ignore
        Console.Clear()
        setCurrentFont 8 |> ignore     
        let outputImage = convertImageTo(false, imageToProceed)
        let outputImageNegative = convertImageTo(true, imageToProceed)
        printImageIntoConsole(outputImageNegative) |> ignore
        saveResultToFile(imageName,outputImage, outputImageNegative)
        Console.Clear()
        mainLoop() 

[<EntryPoint>]
let main argv =
    mainLoop()
    0



