FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["User.Api/User.Api.csproj", "User.Api/"]
COPY ["Address.Api/Address.Api.csproj", "Address.Api/"]
COPY ["Utils/Utils.csproj", "Utils/"]
COPY ["CityServiceImpl/CityServiceImpl.csproj", "CityServiceImpl/"]
COPY ["City.Tools/City.Tools.csproj", "City.Tools/"]
COPY ["CityService/CityService.csproj", "CityService/"]
COPY ["City.Domain/City.Domain.csproj", "City.Domain/"]
COPY ["IAddressService/AddressService.csproj", "IAddressService/"]
COPY ["AddressDto/AddressDto.csproj", "AddressDto/"]
COPY ["City.EFRepository/City.EFRepository.csproj", "City.EFRepository/"]
COPY ["AddressEFRepository/AddressEFRepository.csproj", "AddressEFRepository/"]
COPY ["Address.Tools/Address.Tools.csproj", "Address.Tools/"]
COPY ["Address.Domain/Address.Domain.csproj", "Address.Domain/"]
COPY ["AddressServiceImpl/AddressServiceImpl.csproj", "AddressServiceImpl/"]
COPY ["ProvinceServiceImpl/ProvinceServiceImpl.csproj", "ProvinceServiceImpl/"]
COPY ["ProvinceService/ProvinceService.csproj", "ProvinceService/"]
RUN dotnet restore "User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]