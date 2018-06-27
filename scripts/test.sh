dotnet restore ../test/server/
dotnet build ../test/server/
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov ../test/server/