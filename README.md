# NationsBenefits

In order to run the Redis Cache, please go to this link and download the zip file: Redis-x64-3.0.504.zip
https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504

Then extract all the content and run these two exe files:
* redis-server.exe

* redis-cli.exe
* => here take note of the Ip address (for example: 127.0.0.1:6379) and go to appsettings.json inside the NationsBenefits.API project and replace the entry for "RedisUrl"
* for example:  "RedisUrl": "127.0.0.1:6379"

