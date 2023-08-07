module ASCIIfromImageFSharpProject.Extensions

open System
open System.IO
open System.Drawing

let toGrayscale (originalImage: Bitmap) =
    for y in 0 .. originalImage.Height - 1 do
        for x in 0 .. originalImage.Width - 1 do
            let currentPixel = originalImage.GetPixel(x, y)
            let averageColor = int (currentPixel.R + currentPixel.G + currentPixel.B) / 3
            originalImage.SetPixel(x, y, Color.FromArgb(int currentPixel.A, averageColor, averageColor, averageColor))
    originalImage
    
let resizeBitmap (originalImage: Bitmap) =
    let maxWidth = 350
    let widthOffset = 2
    let mutable resizedImage = originalImage
    let newHeight = originalImage.Height / widthOffset * maxWidth / originalImage.Width
    if ((originalImage.Height > newHeight) || (originalImage.Width > maxWidth)) then
        resizedImage <- new Bitmap(originalImage, maxWidth, newHeight)
    resizedImage
    
let mutable imageName = Guid.NewGuid().ToString()   
let proceedImage() =
    Console.WriteLine "Enter the full path to the image:"
    let pathToImage = Console.ReadLine()
    let mutable bitmap = null
    if String.IsNullOrEmpty(pathToImage) then
        Console.WriteLine("Invalid input. Path cannot be empty.")
    else
        if File.Exists(pathToImage) then
            try
                let img = Image.FromFile(pathToImage)
                bitmap <- resizeBitmap(new Bitmap(img))
                bitmap <- toGrayscale(bitmap)
                imageName <- Path.GetFileNameWithoutExtension(pathToImage)
            with
                | :? Exception as ex -> Console.WriteLine(ex.Message)
        else
            Console.WriteLine "The file doesn't exist."
    bitmap        
            
        
    
        

        
    
    
        
        
     
        
        
    
    
    