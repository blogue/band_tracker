# Band Tracker

#### A web app created as an exercise in using C# and databases.

#### By Ben Logue, current as of 7/22/16

## Description

Band tracker web app allows a user to create, edit, and delete venues as well as add bands to a venue.

## Technologies Used

* C#
* Nancy and Razor View Engines
* XUnit
* MSSQL

## Instructions

* Clone the repository
* In PowerShell, navigate to the project directory
* In PowerShell type sqlcmd -S "(localdb)\mssqllocaldb" -i C:\[YOURFILEPATH]\band_tracker\band_tracker.sql
* In PowerShell, enter '>dnu restore' and then '>dnx kestrel'.
* Navigate your web browser to http://localhost:5004

## Known Bugs

None

## Contacts

benjamin.logue73@gmail.com

### License

Licensed under the MIT License.

&copy; Ben Logue 2016
