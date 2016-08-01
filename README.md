#Plexo 
##<sub>~ the personal finance manager ~</sub>

================

Plexo is an ASP.NET website using Angular and Bootstrap built using the WebAPI framework with a RESTful architecture. 

###Features
####Current
- Secure login using OAuth
- The ability to create finance accounts
- Households where multiple users can manage and update the household finances

####Future 
- The ability to create budgets to track income and expenses
- Provides future estimations based on data provided
- Reports on avaiable funds after expenses 

###Setup
The project is setup to run using a localhost server.
Tested and working with SQL Server Management Studio 2012 with Visual Studio 2015

1. Pull or Fork this repo 
2. Open the solution with visual studio
3. Build the solution the NuGet packages should restore
4. Go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution...
5. Select all NuGet packages and update them
6. Set the Plexo.Database package as your startup project
7. Run the project (This will create the required tables and fill them will test data)
8. Set the Plexo.Web package as your startup project
9. Run the application

&nbsp;
###### Based on the personal finance project by Adam Eury
