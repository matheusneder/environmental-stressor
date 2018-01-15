FROM microsoft/aspnetcore:2.0.0-jessie
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS "http://*:80"
COPY . .
ENTRYPOINT ["dotnet", "EnvironmentalStressor.dll"]
