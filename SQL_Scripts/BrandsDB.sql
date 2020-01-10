
use master 
GO

-- erklære og sætter variablen dbname til værdien 'BrandsDB' 
DECLARE @dbname nvarchar(128)    
SET @dbname = N'BrandsDB'    
 
/* Her tjekker vi om der findes en database ved navn 'BrandsDB' allerede. 
   Hvis dette er tilfældet så dropper vi databasen.        
*/
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases   
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname))) 
BEGIN
   PRINT 'Dropping database';
   ALTER DATABASE BrandsDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
   drop database BrandsDB; 
END    
     
create database BrandsDB  
GO

use BrandsDB 
GO

CREATE TABLE brands(

    brand_id INT identity(1,1) primary key ,
    brand_name VARCHAR(30), 
	brand_banner VARCHAR(100),    
    brand_info VARCHAR(500)        
	);   
go	  

insert into brands( brand_name, brand_banner, brand_info) 
values ( 'Adidas', 'https://cdn.pixabay.com/photo/2016/10/26/19/04/universe-1772248_1280.jpg', 'Adidas is the largest sportswear manufacturer in Europe.');     

insert into brands( brand_name, brand_banner, brand_info) 
values ( 'Nike', 'https://cdn.pixabay.com/photo/2018/09/25/12/18/nike-3702117_1280.jpg', 'Nike is the world’s largest supplier of athletic shoes and apparel.'); 

insert into brands( brand_name, brand_banner, brand_info) 
values ( 'Converse', 'https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935026_1280.jpg', 'Converse is an American company that designs and distributes all types of shoes'); 

go
Create procedure GetBrandInfo 
(   
   @BrandName varchar(30)  
)   
as   
begin   
   Select *    
   from brands  
   where brand_name=@BrandName      
End
go 
Create procedure GetAllBrandInfo 
as 
begin    
   Select *     
   from brands    
End    