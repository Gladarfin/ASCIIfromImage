module ASCIIfromImageFSharpProject.BitmapToASCIIConverter
open System.Drawing
let asciiTable = [|'.'; ','; ':'; '+'; '*'; '?'; '%'; '$'; '#'; '@'|]
let asciiTableReversed = [|'@'; '#'; '$'; '%'; '?'; '*'; '+'; ':'; ','; '.' |]

let map (valueToMap:float, originalRangeStart:float, originalRangeStop:float, rangeToMapToStart:float, rangeToMapToStop:float) : int =
    let result = (int) ((valueToMap - originalRangeStart) / (originalRangeStop - originalRangeStart)
                            * (rangeToMapToStop - rangeToMapToStart) + rangeToMapToStart)
    result
                
let private convert (table:char[], imageToConvert: Bitmap): char[][] =          
  let result = Array.zeroCreate<char[]> imageToConvert.Height
  for y in 0 .. imageToConvert.Height - 1 do
      result.[y] <- Array.zeroCreate<char> imageToConvert.Width
      for x in 0 .. imageToConvert.Width - 1 do
          let mappedPixel = map (float (imageToConvert.GetPixel(x, y).R), 0, 255, 0, float (table.Length - 1))
          result.[y].[x] <- asciiTable.[mappedPixel]
  result   
let image = new Bitmap(800,800)
let convertImageTo (isNegative:bool): char[][] =
    let table = if isNegative then asciiTableReversed else asciiTable
    convert (table, image)
    
    