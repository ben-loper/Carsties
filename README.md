# Overview
Learning about microservices with .NET

## Commands

### Entity Framework

#### Adding new migration
&emsp;&emsp;**dotnet ef migrations add InitialMigration --output-dir Data/Migrations**<br/><br/>
&emsp;&emsp;output-dir only needed for first migrations generation<br/>

#### Updating the migration
&emsp;&emsp;**dotnet ef database update**<br/>

### Docker

#### Updating docker service using the yml file. Must be ran from the directory with the docer-compose.yml file
&emsp;&emsp;**docker compose up -d**<br/><br/>
&emsp;&emsp;d flag runs it as detached so it doesn't spam the terminal<br/>

#### Deleting the docker services
&emsp;&emsp;**docker compose down -v**<br/><br/>
&emsp;&emsp;v flag deletes the persistent data store
