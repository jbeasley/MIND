#Download the latest SQL Server 2017 development docker image and run it

docker pull mcr.microsoft.com/mssql/server:2017-latest

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Trip0001!' \
   -p 1433:1433 --name sql1 \
   -d mcr.microsoft.com/mssql/server:2017-latest

