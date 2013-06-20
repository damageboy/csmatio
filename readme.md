# CSMatIO #

CSMatIO is a .NET Library to read, write, and manipulate Matlab binary MAT-files
written by David Zier. He published his code on
[Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319)

I found his work really useful. However it has some bugs and is somehow incomplete. I try to provide some fixes and enhancements here.

toaaot


------------------------------------------------------------------------

# Original Readme from David Zier #

CSMatIO is a .NET Library to read, write, and manipulate Matlab binary
MAT-files.

CSMatIO is a managed version of the Matlab MAT-File I/O API written entirly in C#.  As is with
most of my projects, CSMatIO was originally intended as a small support application for my Ph.D.
research, but quickly grew out of control into a full API.  CSMatIO was orinally based off of 
converting the Java source code to C# from the JMatIO library written by Wojciech Gradkowski, 
but eventually grew morphed into its current version.  Mr. Gradkowski's CSMatIO is, I am sure,
an excellent utility, but unfortionately is imcompatable with Microsoft's .NET Architecture.

This library was written for people who develope .NET applications and would like to import or
export there data into Matlab for further processing or to use Matlabs superior graphing 
capabilities.  It was written with Visual Studio .NET 2005 and requires the .NET 2.0 
Architecture set to be installed.

If you would like to send comments, improvement requests, or to criticize the project please 
email me: david.zier@gmail.com 

Enjoy!

David A. Zier

------------------------------------------------------------------------

# CHANGE LOG #

## r7, 2013-06-20 ##

fixed MLNumericArray.Flags: should return

~~~~~~~
(int)((uint)(base._type & MLArray.mtFLAG_TYPE) | (uint)(base._attributes & 0xFFFFFF00))
~~~~~~~

or it will make all the numberic data written as Double.

(contributed 2013-06-19 by Anton on [Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319))

## r6, 2012-08-29 ##

support for reading matrix types added: mxUINT16, mxINT16, mxUINT32, mxINT32
readme.txt -> readme.md

## r5, 2012-03-27 ##

support for reading "single" arrays added (tip on [Matlab Central](http://www.mathworks.com/matlabcentral/fileexchange/16319))

## r4, 2011-10-01 ##

FIX: saving empty strings was broken.

## r3, 2011-09-30 ##

FIX: name-based access to struct-array fields was partially broken due to string handling bug in MatFileReader.cs

## r2, 2007-09-10 ##

checked in Davids original code.

Currently supported data types are:

+ Double array
+ Single array
+ Char array
+ Structure
+ Cell array
+ Sparse array
+ Int8 array
+ UInt8 array
+ Int16 array
+ UInt16 array
+ Int32 array
+ UInt32 array
+ Int64 array
+ UInt64 array
