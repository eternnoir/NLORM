# NLORM - A Lightweight ORM For C# .Net 

## Introduction

NLORM(dotNet Lightweight ORM) is a lightweight ORM Framework built on C# .Net implementations.
The primary purpose is to ease the burden of programmers converting Table into objects.

NLORM base on the concept of 1-class to 1-tables ,which attempt to user-friendly and highly expandability .
And every feature can be used independently .

## Download

## Examples

### Get NLORMDb
NLROM get started with manipulating the database by acquiring a NLROMDb . You could acquire a NLROMDb supported by NLORMFactroy. 

```csharp
//Get a NLORMDb which supported MsSql Server
var connectionStr = "IamConnectionString";
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.MSSQL);
```

```csharp
//Get a NLORMDb which supported Sqlite
var connectionStr = "Data Source=C:\\test.sqlite";
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
```

### Model
A rule of correspondence between NLROM such that every table’s column have to correspond 
to the property in Model Class . The rule can be set on lots of different Attribute .


The following example define the User Model, these attributes contains Id、Name、Email、Birth.

```csharp
//An User Model Class
public class User
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime Birth { get; set; }
}
```
However Class Name doesn’t mean Table Name in sometimes , 
property name in Model class is not necessarily in accordance with column name in DB either . 
Here providing some attributes to correspond naming in NLORM 


```csharp
//Naming Attribute
[TableName("UUSER")]
public class User
{
    [ColumnName("UID")]
    public string ID { get; set; }

    [ColumnName("UNAME")]
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime Birth { get; set; }
}
```

You could also use ColumnType Attribute assisting with set up DB’s attributes when creating Table for Model Class .

```csharp
[TableName("UUSER")]
public class User
{
    [ColumnName("UID")]
    [ColumnType(DbType.String, "10", false, "User Id")]
    public string ID { get; set; }

    [ColumnName("UNAME")]
    [ColumnType(DbType.String, "30", false, "User Email")]
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime Birth { get; set; }
}
```

### Create Table

Use NLORMDb create an User Table.

```csharp
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.CreateTable<User>();
```

### Insert Model


```csharp
var user1 = new User { ID = "135", Name = "Nlorm", Email = "nlrom@is.good", Birth = DateTime.Now };
db.Insert<User>(user1);
```

### Query
In Query , NLORM will pull back a set of results from FilterBy . This list can be extract as filtering through those results .

```csharp
//Select All
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.Query<User>();
```

```csharp
//Select Id is 135
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Query<User>();
```

```csharp
//Select Id is 135 and Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135",Name="Nlorm"}).Query<User>();
```

```
//Select Id is 135 OR Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_OR,new { ID="135",Name="Nlorm"}).Query<User>();
```

```csharp
//Select Id is 135 AND Id is 136
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND, new { ID = "135" })
    .And().FilterBy(FilterType.EQUAL_AND, new { ID="136"})
    .Query<User>();
```

### Delete
Delete data. Similar to Query , working with FilterBy to filter out those data which is going to delete . 


```csharp
//Delete Id is 135
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Delete<User>();
```

```csharp
//Select Id is 135 and Name is "Nlorm"
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
var users = db.FilterBy(FilterType.EQUAL_AND,new { ID="135",Name="Nlorm"}).Delete<User>();
```

### Update
Update data. Work with FilterBy to select Model which is going to update . 

```csharp
//Update Id is 135 to New Name
var newUser= new User { ID = "135", Name = "Nlorm New", Email = "nlrom@is.good", Birth = DateTime.Now };
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Update<User>(newUser);
```
Or using [Anonymous Types](http://msdn.microsoft.com/en-us/library/bb397696.aspx)

```csharp
//Update Id is 135 to New Name
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.FilterBy(FilterType.EQUAL_AND,new { ID="135"}).Update<User>(new {Name = "Nlorm New"});
```

### Transaction
Use Transaction in NLROM.

```csharp
INLORMDb db = NLORM.Manager.GetDb(connectionStr, SupportedDb.SQLITE);
db.CreateTable<TestClassUser>();
var trans = sqliteDbc.BeginTransaction();
var testObj = new TestClassUser();
testObj.ID = 1;
testObj.Name = "Name " + 1;
testObj.CreateTime = DateTime.Now;
sqliteDbc.Insert<TestClassUser>(testObj);
trans.Commit();  // or Rollback()
sqliteDbc.Close();
```

## Contributing

Feel free to folk,and send me pull request.

## LICENSE
MIT License


