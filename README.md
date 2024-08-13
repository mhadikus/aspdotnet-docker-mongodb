# ASP.NET with Docker and MongoDB

Containerize ASP.NET app with Docker and MongoDB CRUD (Create, Read, Update, and Delete)<br/>
Using [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Build the image and run it

The container image is built with [Ubuntu Chiseled](https://devblogs.microsoft.com/dotnet/dotnet-6-is-now-in-ubuntu-2204/#net-in-chiseled-ubuntu-containers), with [Dockerfile](Dockerfile.chiseled-composite)

```console
docker build -t aspnetapp-mongodb .
docker run --rm -it -p 8000:8080 -e ASPNETCORE_HTTP_PORTS=8080 --name my-aspnetapp aspnetapp-mongodb
```

The `--rm` option automatically removes the container and its associated anonymous volumes when it exits

Navigate to http://localhost:8000

## Notes

- Project requires [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver) NuGet package
- Use swagger to view and execute Web APIs http://localhost:8000/swagger

## References

- [Run an ASP.NET Core app in Docker containers](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/building-net-docker-images?view=aspnetcore-8.0)