# AT Hike Log
## By: Jeff Erdman

AT Hike Log is a ASP.NET Web MVC app that allows Appalachian Trail hikers to keep track of their daily mileage (including notes) and keep basic stats to help plan.

The app consists of a database with tables for:
- Profile
- Section
- DailyLog


In Version 1.0 (MVP), users will have the ability to:
- Create, edit, and delete a profile for their logged in user
- Create, edit, delete, and view details for Sections
- Create, edit, delete, and view details for Daily Log entries
- Search for Sections and Daily Log entries by various criteria
- View statistics for a chosen section:
    -Planned Average Miles Per Day
    -Average Miles Hiked Per Day
    -Average Miles Hiked Per Day excluding Zero Days
    -Estimated days to complete Section based on Average Miles Hiked Per Day

The app has been deployed to Azure at this link:
- [AT Hike Log](https://hikelogwebmvc.azurewebsites.net)

For Version 2.0 and beyond, planned features include:
- Mapping.  Implement feature to display miles hiked on a map of the AT footpath using GoogleMaps API
- Implement Photo/Video attachment to logs/notes using GooglePhotos API
- Expand Statistics
- Create "Friends & Tramily" table.  Add other users to view their progress / stats
- Implement user roles.  Admins will have full permissions while standard users will be limited.



The main goal of this exercise is to gain practice using:
- Web MVC with n-tier architecture
- Practice branching and pull requests in Git
- Practice building an app individually
- Generally apply knowledge gained throughout the ElevenFifty Software Development program

The following external resources were used in planning this project:
- [Trello Board](https://trello.com/b/rWJj8JsR/at-hike-log-agile-ticket-system)
- [Google Doc](https://docs.google.com/document/d/1dB5GzbSkNkMl5lUTypUcJDO9YdZ9-P-F4LhSeWWPLEI/edit?usp=sharing)
- [DBDiagram.io](https://dbdiagram.io/d/620d0564485e433543c1fd1a)

