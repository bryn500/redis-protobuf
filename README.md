# redis-protobuf

Trying Redis using StackExchange.Redis and protobuf-net

## Setup

Use Docker to get and run a local redis client with browser gui

Redis help: 
<https://redis.io/docs/stack/get-started/install/docker/>


```sh
docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest
```

Run the app:


```sh
dotnet build
dotnet run
```