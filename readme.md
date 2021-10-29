# ORA-02292

## Introduction

This project was created to demonstrate an issue related in the scenario:

- Entity Framework
- Oracle
- Deleting an Entity in a certain scenario.

The issue is:

```
ORA-02292: integrity constraint (IA_DEV.FK_Cars_Vehicles_Id) violated - child record found
```

This error does not appear when using another databases. In this project I added Sqlite to prove it.

## Scenario

As you can see in Entities.cs, we have:

- Vehicle, class which has a property, Driver.
- Car, child of Vehicle.
- Driver, which has a list of Vehicles.
  - Driver has the AddCar method, but not an AddVehicle method.

Migration is already generated. There are two, Oracle and Sqlite.

## Reproduce the issue

In the controller we have 3 endpoints: addDriverAndCar, removeDriver and worksOk.

To reproduce the issue you have to execute the addDriverAndCar method, retrieve the returned Id and use it to call removeDriver method. Then, the ORA-02292 error will pop up.

The worksOk method performs the same actions as addDriverAndCard and removeDriver methods, but this case always works.

To switch between databases, change Database > Provider in appsettings.json.

## Dev

### Database commands from Package Manager Console

When executing database commands in Package Manager Console, you need to:

1. Select the Persistence\<PROVIDER> project

2. Execute the command with:

```
-Args "--provider <PROVIDER>"
```

Where <PROVIDER> can be:

- Oracle
- Sqlite
