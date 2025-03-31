# NationsBenefits

*** DATABASE ***
-----------------
* crete database with name: NationsBenefits
* Run this script into your database to create the tables:

CREATE TABLE product.products ( 
  id int identity(1,1) NOT NULL, 
  subcategory_id int NOT NULL, 
  ski varchar(15) NOT NULL, 
  name varchar(150) NULL, 
  description varchar(1000) NULL, 
  created_at datetime DEFAULT getdate() NOT NULL, 
  updated_at datetime DEFAULT getdate() NOT NULL, 
  CONSTRAINT pk_product PRIMARY KEY (id) 
); 
GO

CREATE TABLE product.subcategories ( 
  id int identity(1,1) NOT NULL, 
  code varchar(50) NOT NULL, 
  description varchar(250) NOT NULL, 
  category_id int NOT NULL, 
  created_at datetime DEFAULT getdate() NOT NULL, 
  updated_at datetime DEFAULT getdate() NOT NULL, 
  CONSTRAINT pk_subcategory PRIMARY KEY (id) 
);
GO

ALTER TABLE product.products ADD CONSTRAINT fk_product_subcategory_subcategory_id  
FOREIGN KEY (subcategory_id) REFERENCES product.subcategories(id) ; 
GO


*** API PROJECT ***
---------------------
Go to file: appsettings.json and change connection string accordingly


*** REDIS CACHE ***
---------------------
In order to run the Redis Cache, please go to this link and download the zip file: Redis-x64-3.0.504.zip
https://github.com/microsoftarchive/redis/releases/tag/win-3.0.504

Then extract all the content and run these two exe files:
1. redis-server.exe

2. redis-cli.exe
* => here take note of the Ip address (for example: 127.0.0.1:6379) and go to appsettings.json inside the NationsBenefits.API project and replace the entry for "RedisUrl"
* for example:  "RedisUrl": "127.0.0.1:6379"


*** LOGS ***
---------------------
Logs are saved into folder: "Logs" one file per day.