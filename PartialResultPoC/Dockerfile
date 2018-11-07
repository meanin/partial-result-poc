FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["PartialResultPoC/PartialResultPoC.csproj", "PartialResultPoC/"]
RUN dotnet restore "PartialResultPoC/PartialResultPoC.csproj"
COPY . .
WORKDIR "/src/PartialResultPoC"
RUN dotnet build "PartialResultPoC.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PartialResultPoC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PartialResultPoC.dll"]