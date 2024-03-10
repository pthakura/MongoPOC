#Build Stage

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS Build
WORKDIR /Source
COPY . .
RUN dotnet restore "TutorialMongo.csproj" 
RUN dotnet publish "TutorialMongo.csproj" -c release -o /app --no-restore

#Deployment Stage