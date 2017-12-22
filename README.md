<p align="center">
    <img src="https://i.imgur.com/C8cdJ7x.png" alt="NetBarcode">
</p>

# NetBarcode

Barcode generation library written in .NET Core compatible with .NET Standard 2.

## Supported barcodes:

* CODE128
  * CODE128 (automatic mode switching)
  * CODE128 A/B/C
* EAN
  * EAN-13
  * EAN-8
* CODE11
* CODE39
* CODE93

## Install

On Nuget:
```
PM> Install-Package FixerIoCore
```

## Using

``` c#
var barcode = new Barcode("543534"); // default: Code128
```
Change barcode type
``` c#
var barcode = new Barcode("543534", Type.Code93);
```
Show label
``` c#
var barcode = new Barcode("543534", Type.Code128, true);
```
Saving in a image file
``` c#
var value = barcode.SaveImageFile("./path"); // default: ImageFormat.Jpeg
```
Change image format
``` c#
var value = barcode.SaveImageFile("./path", ImageFormat.Png); // formats: Bmp, Gif, Jpeg, Png...
```
Get string with base64 image to use in HTML
``` c#
var value = barcode.GetBase64Image();
```

## License

NetBarcode is shared under the MIT license. This means you can modify and use it however you want, even for comercial use. But please give this the Github repo a ⭐️.