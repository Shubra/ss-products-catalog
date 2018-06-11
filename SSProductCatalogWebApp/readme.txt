This application is used to create, edit, delete and view details of the Products.
Two project an ASP.NET MVC web app and Unit Test projects are created.

Sql scripts:
-----------------------
The Web app is created using database first approach, thus the scripts to create new Database are found in the SqlScripts folder under the main solution folder

Execute the CreateProductDB followed by CreatProductTable sql scripts.

Update the connectionstrings in web.config in WebApp and app.config with the appropriate datasource name.


Installed Nuget packages:
------------------------------
Entity framework
AutoMapper - For mapping the dtos
ClosedXML -  For exporting the data to excel sheet


Known Issues / Pending changes:
----------------------------------
Swagger implementation.
