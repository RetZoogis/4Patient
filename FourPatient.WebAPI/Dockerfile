FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["FourPatient.WebAPI/FourPatient.WebAPI.csproj", "FourPatient.WebAPI/"]
COPY ["FourPatient.Domain/FourPatient.Domain.csproj", "FourPatient.Domain/"]
COPY ["FourPatient.DataAccess/FourPatient.DataAccess.csproj", "FourPatient.DataAccess/"]
RUN dotnet restore "FourPatient.WebAPI/FourPatient.WebAPI.csproj"
COPY . .
WORKDIR "/src/FourPatient.WebAPI"
RUN dotnet build "FourPatient.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FourPatient.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FourPatient.WebAPI.dll"]
