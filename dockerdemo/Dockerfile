FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY buildoutput/. /app/

EXPOSE 80

CMD ["dotnet","dockerdemo.dll"]