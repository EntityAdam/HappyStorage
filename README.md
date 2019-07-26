# Happy Storage

1. Overview
1. Running Locally
1. License

# 1. Overview

> **TL;DR:** Happy Storage is a POC application for educational purposes.

### A) HappyStorage.Core
The core of the application was forked from [GitHub/dlinkous](https://github.com/dlinkous/trinug-pluginarchitecture)

The user story is as a manager of a self-store business I want to be able to allow *customers* to reserve *units* on a month to month basis to store their ~~junk~~ stuff in. 
 - Customers can be added and deleted.
 - Customers can occupy zero or many units
 - Customers that have occupied a unit for a month incur a cost of the unit's monthly cost.
 - Payments can be accepted from a customer and that amount is applied to their total owed amount.
 - Units can be created and decommissioned. 
 - A user must be able to search for available units (not occupied by a customer) and filter that list by **X** criteria.
 - Units can be reserved by a customer. Once the unit is occupied it is no longer available to be reserved.
 - Units can be released from being occupied by a customer, and it is again available.
 
 You get the idea..
 
 The educational concept behind the core project is the 
'Plugin', 'Hexagonal' or 'Onion' Architectures.  Personally, I like onions because like onions, ogres have layers.

### B) Additions
Added to the project are:
 - `HappyStorage.Web` which is an ASP.NET Core Razor Pages application
 - `HappyStorage.BlazorWeb` which is an ASP.NET Core Server Side Blazor Application
 - `HappyStorage.Common.Ui` is the shared code for both UI projects

# 2. Running Locally

### A) Prerequisites
If you want to run the Blazor Project locally, you will need:
 - .NET Core SDK 3.0.0 Preview 7 -> [Link](https://dotnet.microsoft.com/download/dotnet-core/3.0)
 - Visual Studio 2019 Preview 1 or Visual Studio 2019 with "Use Preview Versions" enabled.

### B) SQL Database
1. Create an SQL Database; ex: "HappyStorage"
1. Run the SQL script located in the `HappyStorage/SqlScripts/` folder to create the table required to persist units.
1. If not using `(localdb)\\MSSQLLocalDB` ensure you update the connection string in your `appsettings.Development.json` for both the `*.Web` and `*.BlazorWeb` project.

```
  "ConnectionStrings": {
    "SqlUnitStore": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HappyStorage;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  }
```

# 3. License
MIT License