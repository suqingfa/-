FROM microsoft/aspnetcore:2.0
MAINTAINER suqingfa <enmoton@qq.com>
WORKDIR /app
COPY bin/Release/PublishOutput/ .
EXPOSE 80
ENTRYPOINT ["dotnet", "Server.dll"]