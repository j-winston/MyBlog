FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY /bin/Release/net8.0/publish/ MyBlog/ 


ENV ASPNETCORE_ENVIRONMENT Production
ENV Logging_Console_Formatting_Name simple 


EXPOSE 5000
WORKDIR /MyBlog
ENTRYPOINT ["dotnet", "MyBlog.dll", "--urls=http://0.0.0.0:5000"]

