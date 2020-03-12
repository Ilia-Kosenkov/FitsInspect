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
0001 > [fix|lgl]: SIMPLE  =                    T / file does conform to FITS standard             
0002 > [fix|int]: BITPIX  =                   16 / number of bits per data pixel                  
0003 > [fix|int]: NAXIS   =                    2 / number of data axes                            
0004 > [fix|int]: NAXIS1  =                  300 / length of data axis 1                          
0005 > [fix|int]: NAXIS2  =                  300 / length of data axis 2                          
0006 > [fix|lgl]: EXTEND  =                    T / FITS dataset may contain extensions            
0007 > [udf|spc]: COMMENT   FITS (Flexible Image Transport System) format is defined in 'Astronomy
0008 > [udf|spc]: COMMENT   and Astrophysics', volume 376, page 359; bibcode: 2001A&A...376..359H 
0009 > [fix|dbl]: CRVAL1  =   201.36506299999994 / Reference longitude                            
0010 > [fix|dbl]: CRVAL2  =  -43.019113000000004 / Reference latitude                             
0011 > [fre|str]: RADESYS = 'FK5'                / Coordinate system                              
0012 > [fix|flt]: EQUINOX =               2000.0 / Epoch of the equinox                           
0013 > [fre|str]: CTYPE1  = 'RA---TAN'           / Coordinates -- projection                      
0014 > [fre|str]: CTYPE2  = 'DEC--TAN'           / Coordinates -- projection                      
0015 > [fix|flt]: CRPIX1  =                150.5 / X reference pixel                              
0016 > [fix|flt]: CRPIX2  =                150.5 / Y reference pixel                              
0017 > [fix|dbl]: CDELT1  =        -0.0004722222 / X scale                                        
0018 > [fix|dbl]: CDELT2  =         0.0004722222 / Y scale                                        
0019 > [udf|bln]:                                                                                 
0020 > [udf|spc]: COMMENT   The following keyword uses the HIERARCH keyword convention            
0021 > [udf|spc]: HIERARCH  ESO TEL AIRM START  =        1.134 / Airmass at start                 
0022 > [udf|bln]:     
...                                                                            
```

Each key's prefix contains `FITS` key type information. 
The first column `udf|fix|fre` labels stand for the *undefined*, *fixed* and *free* `FITS` key formats, respectively.
The second column represents the data type of the key, including special `spc` (as in non-standard *special* keys) and `bln` (completely blank keys).

