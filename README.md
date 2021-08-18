# ConcreteProducts
Softuni ASP.NET Core MVC course final project which uses best practices and architecture for web application.

Project is deployed in Azure on address: https://concreteproducts.azurewebsites.net/

# Used tehnologies:
<ul>
  <li>1..NET 5</li>
  <li>2.ASP.NET Core 5 MVC</li>
  <li>3.Entity Framework Core</li>
  <li>4.MSSQL Server</li>
  <li>5.SignalR</li>
  <li>6.Auto-Mapper</li>
  <li>7.Bootstrap</li>
  <li>8.NUnit</li>
  <li>9.MyTested.AspNetCore.Mvc</li>
  <li>10.FluentAssertions</li>
</ul>

# Functionality:
Project:
 - 3 base role models - "Administrator", "Employee", "Basic"
    - Administrator:
       -  Has its own Area and own Layout page
       -  Add/Edit/Delete and see All Products,Shapes,Warehouses,Colors,Categories
       -  Add color to product from Product/Details page
       -  Promote registered user to "Employee" role and can Demote "Employee" user to "Basic" role
       -  Increase and decrease products quantity to warehouse where all users can view available quantity to all products anytime
    -  Employee:
       -  View products with their Details
       -  Increase products quantity to warehouse
    - Basic:
       -  View products with their Details
       -  View available products in warehouse
    - Unregistered
       -  View products with their Details
  - Data is separated from web via class library
  - Chat functionality with saving and retrieving messages from database
  - Cached latest 6 products in homepage
  - Uses Areas and Partial views where necessary
  - Written 172 tests with [MyTested.AspNetCore.Mvc](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc) library by [Ivaylo Kenov](https://github.com/ivaylokenov)
  
# Quick Start:
  - Administrator user is seeded by default and can use it to explore project with administrator functionality:
    -  **Username:** admin
    -  **Password:** admin12
