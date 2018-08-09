# ShavedIce_ClassViewer
It is an application that visualizes functions and variables inside the DLL file and EXE file(.NET framework)

## Description
Using this application, you can view DLL file and EXE file.

⚠This library can load only DLL or EXE which has manifest.

## Demo
If you had following code and built,
``` C#
// The file name is "Test.dll"
namespace Test
{
    public class Class1
    {
        public int a = 0;
        private void b() { }
        public string c() { return null; }
        private static int d(byte b) { return 0; }
        private short e = 0;
    }
}
```
You can see members of "Test.dll" without code.
![Run](https://github.com/capra314cabra/ImageForREADME/blob/master/ShavedIce_ClassViewer/Command.gif)

## Requirement
.NET Framework

## Usage
After add the path to ShavedIce to your Path environment variable on Windows,  
You can use following command.
```
shavedice [DLL file's path] 
```
Using following command, this application make a html file with the result written.
```
shavedice [DLL file's path]  -f [output file path] -html
```

## Install
Go to [ReleasePage](https://github.com/capra314cabra/ShavedIce_ClassViewer/releases) or  Download [ZIP](https://github.com/capra314cabra/ShavedIce_ClassViewer/archive/master.zip)

## Contribution
1. Fork it
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create new Pull Request

## Licence
[MIT](https://github.com/capra314cabra/ShavedIce_ClassViewer/blob/master/LICENSE)

## Author
[capra314cabra](https://github.com/capra314cabra)
