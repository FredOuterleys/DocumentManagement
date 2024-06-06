<h1>Document Management Application</h1>
<h3>Developed by Fred Outerleys
<br>Competition 42057</h3>
<p>This application consists of an API back-end and a Blazor web front end used together to as a document management system.  The application will allow user's to upload and save documents from their location, then be able to view, query, delete and download uploaded documents based on Property ID and/or case number.</p>
<p>The Web API is coded in C# with .Net 8 using dependency injection, entity framework, swagger and a SQL Lite database.  The front end is a Blazor web application also using dependency injection and RESTful services to connect with the back end.</p>
<p>Also included is a Unit Test project running tests against the Web API.</p>
<h3>Requirements</h3>
<li>.Net 8.0 SDK available <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">here</a></li>
<h3>Running The Application</h3>
<ol>
<li>Clone the solution into Visual Studio.</li>
<li>The appsettings.json file in the DocumentManagement-API contains the Web API's listening port of 5000 under AllowedHosts, change this if necessary</li>
<li>If the DocumentManagement-API requires a change from port 5000 under AllowedHosts, also change the AllowedHosts in the DocumentManagement-WebApp to match</li>
<li>The appsettings.json file in the DocumentManagement-WebApp contains the ports used for the API as 5050 under CSDSDocumentManagementAPI, change this if necessary</li>
<li>Build and publish the DocumentManagement-API to a folder on your machine.</li>
<li>Build and publish the DocumentManagement-WebApp to a folder on your machine.</li>
<li>From the published folder of the DocumentManagement-API,  find and execute the DocumentManagement-API.exe file</li>
<li>From the published folder of the DocumentManagement-WebApp,  find and execute the DocumentManagment-WebApp.exe file</li>
<li>Open a web browser and go to http://localhost:5050 to open the web application</li>
</ol>
<h3>Using The Application</h3>
<p>The application will first open with a completely clear database and no files uploaded, therefore you will need to upload some documents in order to use the "Query" features</p>
<p><b>Uploading A Document</b></p>
<ol>
  <li>Click on the "Add Document" link in the nav bar</li>
  <li>Enter a "Property ID" and a "Case Number" (both are required)</li>
  <li>Click on the "Choose Files" button to select a single, or multiple files. Files selected will show next to the "Choose Files" button</li>
  <li>Click the "Upload Document" button to upload the files</li>
</ol>
<p>This will have entered the file information into the database and physically moved the files into the CSDSDocuments folder within a property id and case number folder found in the published DocumentManagement-API folder</p>
<br><br>
<p><b>Querying Documents</b></p>
<ol>
  <li>Click on the "Query Documents" in the nav bar</li>
  <li>Enter either a "Property ID", "Case Number" or both from files you have uploaded into the text fields.<br> <b>NOTE</b>, you can also use "*" for wildcard searches.</li>
  <li>Click on the "Search" button</li>
  <li>All documents for the requested Property Id or Case Number will show below the search form.</li>
  <li>To preview a file, click on the row the file is located in and a preview will appear below the query results<br><b>NOTE</b> the preview will only work with certain tyes of files iin it's current state, if it can't be rendered, it will automatically download the file instead.  For preview purposes please use simple file types such as text documents or images.</li>
  <li>To "Delete" a file, click on the "Delete" button located at the end of the row of the desired file to delete.</li>
  <li>To "Download a file, select a file from the query results and above the preview window, you will see a "Download" button</li>
</ol>
