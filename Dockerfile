# FROM node:8.9.3-alpine
# RUN mkdir -p /usr/src/app
# COPY ./app/* /usr/src/app/
# WORKDIR /usr/src/app
# RUN npm install
# CMD node /usr/src/app/index.js
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=https://+:443

# Copy the server certificate (.pfx) into the container
COPY hdrclucds-di.hdr.vaec.va.gov.pfx  /https/hdrclucds-di.hdr.vaec.va.gov.pfx 

# Install OpenSSL and extract root CA certificate
RUN apt-get update && apt-get install -y --no-install-recommends openssl ca-certificates && \
    chmod 644 /https/hdrclucds-di.hdr.vaec.va.gov.pfx

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy and restore the application code
COPY va-veis-healthdatarepo/va-veis-healthdatarepo.csproj ./va-veis-healthdatarepo/
RUN dotnet restore va-veis-healthdatarepo/va-veis-healthdatarepo.csproj

# Copy and publish the application
COPY . .
WORKDIR /src/va-veis-healthdatarepo
RUN dotnet publish va-veis-healthdatarepo.csproj -c Release -o /app/publish

# Final runtime stage
FROM base AS final
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Set the certificate path (password is passed securely at runtime)
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/hdrclucds-di.hdr.vaec.va.gov.pfx

# Expose HTTPS and start the application
EXPOSE 443
ENTRYPOINT ["dotnet", "va-veis-healthdatarepo.dll"]
