REM this should generate the PicParamDb.dbml file
"K:\Codesion\PicSharp\ThirdPartyComponents\DbMetal.exe"  /pluralize /namespace:Pic.DAL /provider:"SQLite" /conn:"Data Source=K:\Codesion\PicSharp\PicSharpDb\PicParamData\Database\PicParam.db" /dbml "K:\Codesion\PicSharp\Pic.DAL\PicParamDb.dbml"
REM
"K:\Codesion\PicSharp\ThirdPartyComponents\DbMetal.exe"  "K:\Codesion\PicSharp\Pic.DAL\PicParamDb.dbml" /pluralize /namespace:Pic.DAL.SQLite /provider:"SQLite" /conn:"Data Source=K:\Codesion\PicSharp\PicSharpDb\PicParamData\Database\PicParam.db" /code:"K:\Codesion\PicSharp\Pic.DAL\Pic.DAL.SQLite.DataContext.designer.cs" /language:C#
