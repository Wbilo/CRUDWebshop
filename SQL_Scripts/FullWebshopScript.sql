/*  
 I customer tabellen har vi CustomerID kollonen, som vi sætter til primary key, hvilket sørger for at kunderne
 kommer til at have et unikt id, som man kan refere til. 
 Yderligere benytter vi "identity(1,1)", hvilket sørger for at der bliver auto incrementet,
 hver gang der bliver tilføjet en ny kunde til tabbellen og vi starter ved tallet 1, så den første kunde får id = 1.
 Fornavn, Efternavn og Email er varchar, fordi de skal indholde bogstaver.   
      
   
 I Orders tabellen har vi OrderId , som vi sætter til primary key, hvilket sørger for at ordre entries
 kommer til at have et unikt id, som man kan refere til. 
 Og så har vi CustomerID, som vi sætter til foreign key, med reference til customer tabellen's primary key "CustomerID".
 Dette linker de to tabeller sammen og skaber en relation mellem dem.     

  
 I orderEntry tabellen har vi order_detail_entry_ID, som vi sætter til primary key, hvilket sørger for at ordredetaljer entries
 kommer til at have et unikt id, som man kan refere til. 
 Og så har vi OrdreID, som vi sætter til foreign key, med reference til ordre tabellen's primary key "OrdreID". 
 Og så har vi CustomerID, som vi sætter til foreign key, med reference til customer tabellen's primary key "CustomerID".
 Og så har vi ProductID, som vi sætter til foreign key, med reference til Product tabellen's primary key "ProductID".
 Disse forign keys skaber et link til den tilhørende tabel og skaber en relation mellem dem.  
       

 I Products tabellen har vi ProducID  som vi sætter til primary key, hvilket sørger for at Products entires
 kommer til at have et unikt id, som man kan refere til.  
  
 */        

use master 
GO

-- erklære og sætter variablen dbname til værdien 'Shop' 
DECLARE @dbname nvarchar(128) 
SET @dbname = N'Shop'  
 
/* Her tjekker vi om der findes en database ved navn 'Shop' allerede. 
   Hvis dette er tilfældet så dropper vi databasen.        
*/
IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases   
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname))) 
BEGIN
   PRINT 'Dropping database';
   ALTER DATABASE Shop SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
   drop database Shop; 
END    
     
create database Shop  
GO

use Shop 
GO

CREATE TABLE customer(
    customer_id INT identity(1,1) primary key ,
    customer_fname VARCHAR(30),
    customer_lname VARCHAR(30), 
    customer_email VARCHAR(30),
	customer_address VARCHAR(30),
	customer_city VARCHAR(30),
	customer_password VARCHAR(30) 
	);

	CREATE TABLE orders(
    order_id INT identity(1,1) primary key,
    customer_id int FOREIGN KEY REFERENCES customer(customer_id),
    order_date DATETIME 
	); 
	     
	CREATE TABLE brands(
    brand_id INT identity(1,1) primary key, 
    brand_name VARCHAR(40));    
	 
	
    CREATE TABLE products(
    product_id INT identity(1,1) primary key,
	brand_id int FOREIGN KEY REFERENCES brands(brand_id),  
    product_name VARCHAR(40),   
    product_price float,     
	product_info text,  
	product_img text);   
	
	 CREATE TABLE stock (
	 stock_id INT identity(1,1) primary key, 
	 product_id INT FOREIGN KEY REFERENCES products(product_id),
	 stock_amount INT,
	 in_stock bit 
	);
 
	 CREATE TABLE orderEntry(
    order_detail_entry_ID int identity(1,1) primary key,
    order_id int FOREIGN KEY REFERENCES orders(order_id),  
    product_id int FOREIGN KEY REFERENCES products(product_id),
    quantity int,
    combined_price float
    );    

	CREATE TABLE cartTable(
    cartID int identity(1,1) primary key,
    customer_id int FOREIGN KEY REFERENCES customer(customer_id), 
    );  
	/* Hver gang der bliver tilføjet produkt til cart
	skal det gemmes her i denne tabel*/  
	CREATE TABLE cartTableEntry(
    cartDetailID int identity(1,1) primary key,
    cartID int FOREIGN KEY REFERENCES cartTable(cartID),  
	product_id int FOREIGN KEY REFERENCES products(product_id),
	quantity int, 
	dateAdded DATETIME  
    );     
	
/* Følgende skaber en entry i stock tabellen når
	der bliver tilføjet et nyt product i product tabellen */
	GO 
	CREATE TRIGGER TR_AddToStockTable
	ON products     
	AFTER INSERT  
	AS
	BEGIN
	SET NOCOUNT ON;
	DECLARE @ProdID nvarchar(128) 
	SELECT @ProdID = product_id FROM INSERTED
	insert into stock(product_id, stock_amount, in_stock)
    values (@ProdID,100, 1); 
	END     
GO  
 



insert into customer (customer_fname, customer_lname, customer_email, customer_address, customer_city, customer_password)
values ('Jan', 'Jensen', 'janJe@mail.com', 'Eksempel vej 1', 'Aarhus', 'jan1');  

insert into customer (customer_fname, customer_lname, customer_email, customer_address, customer_city, customer_password)
values ('Jan', 'Karlsen', 'janK@mail.com', 'Eksempel vej 2', 'Aarhus', 'jan2');   

insert into customer (customer_fname, customer_lname, customer_email, customer_address, customer_city, customer_password)
values ('Jan', 'Jon', 'janJo@mail.com', 'Eksempel vej 3', 'Aarhus', 'jan3'); 

insert into brands(brand_name) 
values ('Nike');

insert into brands(brand_name) 
values ('Converse');
insert into brands(brand_name)
values ('Adidas');   

insert into products(product_price, brand_id, product_name, product_info, product_img)
values (700, 1, 'Rox S2',              
'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2016/11/19/18/06/feet-1840619_960_720.jpg');

insert into products(product_price, brand_id, product_name, product_info, product_img)  
values (1200, 1, 'Force S3', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2014/04/05/11/38/nike-316449_960_720.jpg');     

insert into products(product_price, brand_id, product_name, product_info, product_img)  
values (900, 1, 'Speed S4', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2012/12/21/10/07/sneakers-71623_960_720.jpg');  


insert into products(product_price, brand_id, product_name, product_info, product_img)
values (1000, 2, 'Chuck 60', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2013/07/12/18/20/chucks-153310_960_720.png');     
insert into products(product_price, brand_id, product_name, product_info, product_img)
values (1015, 2, 'Chuck 70', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2016/06/03/17/35/shoe-1433925_960_720.jpg');  

insert into products(product_price, brand_id, product_name, product_info, product_img)
values (1015, 2, 'Chuck Taylor', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2016/04/04/04/26/converse-1306118_1280.jpg');  

insert into products(product_price, brand_id, product_name, product_info, product_img)
values (1300, 3, 'Shox R7','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ',
'https://cdn.pixabay.com/photo/2017/07/30/15/49/adidas-2554690_1280.jpg');       
  
insert into products(product_price, brand_id, product_name, product_info, product_img)
values (800, 3, 'Shoxies R1', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. '
,'https://cdn.pixabay.com/photo/2019/07/21/14/14/mother-pass-4352838__340.jpg');  


 
insert into orders(customer_id, order_date) 
values (1, GETDATE()); 

insert into cartTable(customer_id)
values (1); 
  
insert into cartTable(customer_id)
values (2);  
 
go

Create procedure GetAllBrandsInfo    
as
begin     
   Select brand_id, brand_name 
   from brands   
End 
go
Create procedure GetProductsByBrand  
(   
   @BrandName varchar(40)
)    
as  
begin 
   Select products.product_id, product_name, product_price, stock.in_stock as product_inStock, brands.brand_name as product_brand, product_img     
   from products    
   inner join brands on products.brand_id = brands.brand_id
   inner join stock on products.product_id = stock.product_id
   where brands.brand_name = @BrandName;    
End
go
Create procedure GetProdInfo
(  
   @ProdId int  
)   
as  
begin   
   Select *    
   from products  
   where product_id=@ProdId    
End
go 
Create procedure GetAllProdInfo 
as
begin    
    Select products.product_id, product_name, product_price, stock.in_stock as product_inStock, brands.brand_name as product_brand, product_img     
   from products    
   inner join brands on products.brand_id = brands.brand_id
   inner join stock on products.product_id = stock.product_id;
End
  
go  
Create procedure GetAllCartItems 
(   
   @CustomerID int,
   @jsonOutput NVARCHAR(MAX) OUTPUT
)   
as  
begin   
   /* sætter jsonOutput variablen lig med resulatat fra følgende query*/
    SET @jsonOutput = (Select cartTableEntry.cartDetailID, 
   cartTableEntry.quantity, 
   cartTableEntry.product_id,
   products.product_img, 
   brands.brand_name, 
   products.product_name,
   products.product_price       
   from cartTable   
   INNER JOIN cartTableEntry ON cartTable.CartID=cartTableEntry.CartID
   INNER JOIN products ON cartTableEntry.product_id = products.product_id
   INNER JOIN brands on products.brand_id = brands.brand_id
   /* "FOR JSON" Formatere query resultatet i JSON */
   where customer_id=@CustomerID FOR JSON PATH)              
End       
go 
/* "Update" procedure */ 
Create procedure UpdateCartItemQuan   
(   
   @Quan int,
   @cartEntryID int  
)      
as  
begin   
   
   UPDATE cartTableEntry
SET cartTableEntry.quantity = cartTableEntry.quantity + @Quan
WHERE cartTableEntry.cartDetailID = @cartEntryID; 
Select cartTableEntry.quantity from cartTableEntry 
where cartTableEntry.cartDetailID = @cartEntryID;
End    
Go  
Create procedure GetQuanAndStockAmount  
(   
   @cartDetailID int
)   
as  
begin 
       select cartTableEntry.quantity,
       stock.stock_amount 
	   from cartTableEntry
	   INNER JOIN stock ON cartTableEntry.product_id=stock.product_id
	   where cartTableEntry.cartDetailID = @cartDetailID;   
End;  
go 
/* "Delete" procedure*/
Create procedure RemoveItemFromCart  
(   
   @cartDetailID int
)   
as  
begin 
       Delete from cartTableEntry
	   where cartTableEntry.cartDetailID = @cartDetailID;     
End; 
go 
Create procedure CheckIfItemExistsInCart   
(    
   @cartID int,
   @productID int
)        
as  
begin 
       select cartTableEntry.cartDetailID
	   from cartTable
	   INNER JOIN cartTableEntry ON cartTableEntry.cartID =cartTable.cartID
	   Where cartTable.cartID = @cartID and cartTableEntry.product_id = @productID;
End;     
go  
/* "Insert" procedure */
Create procedure AddItemToCart   
(   
   @cartID int, 
   @productID int,
   @quan int,
   @dateAdded DATETIME 

)    
as  
begin 
       INSERT INTO [dbo].[cartTableEntry]
           ([cartID]
           ,[product_id]
           ,[quantity]
           ,[dateAdded]) 
     VALUES
           (@cartID, @productID, @quan, @dateAdded) 
End;     
go  
Create procedure GetSumOfCartItems   
(    
   @cartID int
)    
as  
begin 
       SELECT SUM(quantity)
       FROM cartTableEntry
       WHERE cartTableEntry.cartID = 1; 
End;   
   
GO

PRINT 'Done';          
 