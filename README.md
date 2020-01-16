# FitsInspect
A test F# tool to inspect FITS file headers powered by [`Fits-Cs`](https://github.com/Ilia-Kosenkov/Fits-Cs).

## Instalation
The tool is compiled into a [`.NET Core` global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
and can be pulled from this repository's package registry.
(Assuming the `GitHub` package registry is added to the `nuget` sources)
```
> dotnet tool install fitsinspect --global
> fits-inspect *fits ..\my_image.fits
```

## Usage
Using a sample *experimantal* `FITS` [file](http://fits.gsfc.nasa.gov/samples/testkeys.fits) (from [here](https://fits.gsfc.nasa.gov/fits_samples.html)),
the tool produces something like this:
```
> fits-inspect testkeys.fits

Inspecting file testkeys.fits

---------
Block #1:
---------
[fix|  bool]: SIMPLE  =                    T / file does conform to FITS standard             
[fix|   int]: BITPIX  =                   16 / number of bits per data pixel                  
[fix|   int]: NAXIS   =                    2 / number of data axes                            
[fix|   int]: NAXIS1  =                  300 / length of data axis 1                          
[fix|   int]: NAXIS2  =                  300 / length of data axis 2                          
[fix|  bool]: EXTEND  =                    T / FITS dataset may contain extensions            
[udf|  spcl]: COMMENT   FITS (Flexible Image Transport System) format is defined in 'Astronomy
[udf|  spcl]: COMMENT   and Astrophysics', volume 376, page 359; bibcode: 2001A&A...376..359H 
[fix| float]: CRVAL1  =           201.365067 / Reference longitude                            
[fix| float]: CRVAL2  =          -43.0191116 / Reference latitude                             
[fre|string]: RADESYS = 'FK5'                / Coordinate system                              
[fix| float]: EQUINOX =                 2000 / Epoch of the equinox                           
[fre|string]: CTYPE1  = 'RA---TAN'           / Coordinates -- projection                      
[fre|string]: CTYPE2  = 'DEC--TAN'           / Coordinates -- projection                      
[fix| float]: CRPIX1  =                150.5 / X reference pixel                              
[fix| float]: CRPIX2  =                150.5 / Y reference pixel                              
[fix| float]: CDELT1  =      -0.000472222193 / X scale                                        
[fix| float]: CDELT2  =       0.000472222193 / Y scale                                        
[udf| blank]:                                                                                 
[udf|  spcl]: COMMENT   The following keyword uses the HIERARCH keyword convention            
[udf|  spcl]: HIERARCH  ESO TEL AIRM START  =        1.134 / Airmass at start                 
[udf| blank]:     
...                                                                            
```

Each key's prefox contains `FITS` key type information. 
First column `udf|fix|fre` stand for *undefined*, *fixed* and *free* `FITS` key formats.
Second column represents the data type of the key, including special `spcl` (as in non-standard *special* keys) and `blank` (completely blank keys).

