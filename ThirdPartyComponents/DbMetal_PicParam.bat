@echo off
del "..\Pic.DAL\Pic.DAL.SQLite.DataContext.Designer.cs"
REM Generate file
DbMetal.exe /provider=SQLite "/conn:Data Source=K:\Codesion\PicSharp\PicSharpDb\PicParamData\Database\PicParam.db" /namespace:Pic.DAL.SQLite /code:"temp1.cs" /sprocs /language=C# /pluralize /case:leave "--with-dbconnection=System.Data.SQLite.SQLiteConnection, System.Data.SQLite, Version=1.0.66.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139"
cmd /C BatchSubstitute.bat main PPDataContext temp1.cs > temp2.cs
REM temp2.cs -> Pic.DAL.SQLite.DataContext.Designer.cs
copy /Y "temp2.cs" "..\Pic.DAL\Pic.DAL.SQLite.DataContext.Designer.cs"
del temp1.cs
del temp2.cs

