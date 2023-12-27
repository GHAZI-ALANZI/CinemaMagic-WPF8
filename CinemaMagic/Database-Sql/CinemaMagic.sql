--create database
go
create database CinemaMagic
go

--use database
go
use CineMajesticImage
go


-- staff table
go
create table Staff
(
	Id int primary key identity(1, 1),
	FullName nvarchar(100) not null,
	Birth smalldatetime not null,
	Gender nvarchar(20) not null,
	Email varchar(100) not null,
	PhoneNumber varchar(20) not null,
	Salary int not null,
	Role nvarchar(30) not null,
	NgayVaolam smalldatetime not null,
	ImageSource varbinary(max),
)
go



-- ACCOUNTS
go
create table ACCOUNTS
(
	id INT PRIMARY KEY IDENTITY(1, 1),
    Username VARCHAR(40) not null unique,
    Password VARCHAR(40) not null,
	Staff_Id int not null,
	Constraint FK_StaffId foreign key(Staff_Id) references Staff(Id)
)
go




--voucher
go
CREATE TABLE VOUCHER
(   
    ID INT IDENTITY (1,1) PRIMARY KEY,
    NAME NVARCHAR(50) NOT NULL,
    CODE VARCHAR(10) NOT NULL UNIQUE,
    VOUCHERDETAIL NTEXT NOT NULL,
    TYPE varchar(10) NOT NULL,
    STARTDATE SMALLDATETIME NOT NULL,
    FINDATE SMALLDATETIME NOT NULL
)
go




--customer
go
CREATE TABLE CUSTOMER
(
    Id int identity(1,1) primary key ,
    FullName nvarchar(50) not null,
    PhoneNumber varchar(10) not null unique,
    Email varchar(50) not null,
    Point int not null,
    Birth smalldatetime not null,
    RegDate smalldatetime not null,
    Gender nvarchar(20) not null,
)   
go



--MOVIES
GO
CREATE TABLE MOVIE
(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(100) not null,
    Description NVARCHAR(500) not null,
    Genre nvarchar(100) not null,
    Director NVARCHAR(50) not null,
    ReleaseYear int not null,
    Language NVARCHAR(20) not null,
    Country NVARCHAR(20) not null,
    Length INT not null,
    Trailer NVARCHAR(200) not null,
    StartDate SMALLDATETIME not null,
    Status nvarchar(50) not null,
	ImportPrice int not null,
    ImageSource varbinary(max) not null,
);
GO






--product
go
create table Product
(
	ID int identity(1,1) primary key,
	Name nvarchar(100) not null,
	ImageSource varbinary(max) not null,
	Quantity int not null,
	PurchasePrice int not null,
	Price int null,
	Type int not null,
)
go



--room
create table Auditorium
(
	Id int identity(1,1) primary key,
	Name nvarchar(50) not null,
	NumberOfSeats int not null,
)



insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 1',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 2',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 3',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 4',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 5',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 6',176)

insert into Auditorium(Name,NumberOfSeats)
values(N'Phòng 7',176)





--Seat
go
create table Seat
(
	Id int identity(1,1) primary key,
	Location varchar(3),
	Auditorium_Id int not null,
	Constraint FK_Auditorium_Id_BySeat foreign key(Auditorium_Id) references Auditorium(Id)
)
go

go
create procedure sp_generate_seat_for_firstName
(
	@firstName char(1),
	@Auditorium_Id int
)
as
begin
	declare @Location char(3)
	declare @i int=1
	while(@i<=9)
	begin
		set @Location=@firstName+'0'+cast(@i as char(1)) 
		set @i=@i+1
		insert into Seat(Location,Auditorium_Id)
		values(@Location,@Auditorium_Id)
	end
	while(@i<=16)
	begin
		set @Location=@firstName+cast(@i as char(2)) 
		set @i=@i+1
		insert into Seat(Location,Auditorium_Id)
		values(@Location,@Auditorium_Id)
	end
end
go




go
create procedure sp_generate_seat_for_auditorium
(
	@Auditorium_Id int
)
as
begin
	execute sp_generate_seat_for_firstName 'A',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'B',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'C',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'D',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'E',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'F',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'G',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'H',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'I',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'K',@Auditorium_Id
	execute sp_generate_seat_for_firstName 'L',@Auditorium_Id
end
go


--seat for room 1,2,3,4,5,6,7
go
execute sp_generate_seat_for_auditorium 1
execute sp_generate_seat_for_auditorium 2
execute sp_generate_seat_for_auditorium 3
execute sp_generate_seat_for_auditorium 4
execute sp_generate_seat_for_auditorium 5
execute sp_generate_seat_for_auditorium 6
execute sp_generate_seat_for_auditorium 7
go








--show time
go
create table ShowTime
(
	Id int identity(1,1) primary key,
	StartTime smalldatetime not null,
	EndTime smalldatetime,
	PerSeatTicketPrice int not null,
	Movie_Id int not null,
	Auditorium_Id int not null,
    Constraint FK_Auditorium_Id_ByShowTime foreign key(Auditorium_Id) references Auditorium(Id),
	Constraint FK_Movie_Id_ByShowTime foreign key(Movie_Id) references Movie(Id),
)
go





go
create table SeatForShowtime
(
    Id int identity(1,1) primary key,
    Seat_Id int not null,
    ShowTime_Id int not null,
    Condition bit,
    Constraint FK_Seat_Id_BySeatForShowtime foreign key(Seat_Id) references Seat(Id),
    Constraint FK_ShowTime_Id_BySeatForShowtime foreign key(ShowTime_Id) references ShowTime(Id)
)
go




--bill
go
create table Bill
(
	Id int identity(1,1) primary key,
	Staff_Id int,
	Customer_Id int,
	ShowTime_Id int,
	BillDate smalldatetime not null,
	QuantityTicket int not null,
	PerSeatTicketPrice int not null,
	Discount int,
	Note nvarchar(300),
	Total int not null,
    Constraint FK_StaffId_ByBill foreign key(Staff_Id) references Staff(Id),
    Constraint FK_ShowTime_IdByBill foreign key(ShowTime_Id) references ShowTime(Id),
	Constraint FK_Customer_IdByBill foreign key(Customer_Id) references Customer(Id),
)
go






go
create table BillDetail
(
	Id int identity(1,1) primary key,
    Bill_Id int not null,
	Product_Id int,
	Quantity int not null,
	UnitPrice int not null,
	Total as (Quantity * UnitPrice),
	Constraint FK_BillId_ByBillDeTail foreign key(Bill_Id) references Bill(Id),
    Constraint FK_ProductId_ByBillDeTail foreign key(Product_Id) references Product(ID)
)
go





go
create table Bill_AddMovie
(
	Id int identity(1,1) primary key,
	Movie_Id int,
	BillDate smalldatetime not null,
	Total int not null,
	Constraint FK_Movie_Id_ByBill_AddMovie foreign key(Movie_Id) references Movie(Id),
)
go




go
create table Bill_AddProduct
(
	Id int identity(1,1) primary key,
	Product_Id int,
	BillDate smalldatetime not null,
	Quantity int not null,
	UnitPurchasePrice int not null,
	Total as (Quantity * UnitPurchasePrice),
	Constraint FK_ProductId_ByBill_AddProduct foreign key(Product_Id) references Product(ID)
)
go


go
create table Bill_ImportProduct
(
	Id int identity(1,1) primary key,
	Product_Id int,
	BillDate smalldatetime not null,
	Quantity int not null,
	UnitPurchasePrice int not null,
	Total as (Quantity * UnitPurchasePrice),
	Constraint FK_ProductId_ByBill_ImportProduct foreign key(Product_Id) references Product(ID)
)
go



--error
go
CREATE TABLE ERRORS(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    DESCRIPTION NVARCHAR(200) NOT NULL,
    DATEADDED SMALLDATETIME DEFAULT GETDATE(),
    STATUS NVARCHAR(50) DEFAULT N'Chờ tiếp nhận',
    ENDDATE SMALLDATETIME,
    COST INT,
    STAFF_id INT CONSTRAINT FK_ERR_STAFF FOREIGN KEY REFERENCES STAFF(Id),
    ImageSource varbinary(max) not null,
)
go




go
create table Staff_Salary (
	Id int primary key IDENTITY(1, 1),
	Staff_Id INT,
	BillDate DATETIME,
	Total INT,
	CONSTRAINT FK_Salary_Staff_Id FOREIGN KEY (Staff_Id) REFERENCES Staff(Id)
)
go

go
create procedure sp_phatluong_staff_salary
as
begin
    declare @BillDate smalldatetime
    set @BillDate= GETDATE()

    insert into Staff_Salary(Staff_Id, BillDate, Total)
    select Id, @BillDate, Salary
    from Staff
end
go



insert into Staff(FullName,Birth,Gender,Email,PhoneNumber,Salary,Role,NgayVaolam)
values('admin','1950-01-01','Nam','hihi@gmail.com','43302043',2000000000,N'Quản lý','2000-01-01')



insert into ACCOUNTS(Username,Password,Staff_Id)
values('admin','e10adc3949ba59abbe56e057f20f883e',1)




go
create trigger trg_DeleteStaff
on staff
instead of delete
as
begin
	declare @staffid int

	select @staffid=id
	from deleted

	delete ACCOUNTS
	where Staff_Id=@staffid

	update Staff_Salary
	set Staff_Id=null
	where Staff_Id=@staffid

	update ERRORS
	set STAFF_id=null
	where STAFF_id=@staffid

	update Bill
	set Staff_Id=null
	where Staff_Id=@staffid

	delete Staff
	where id=@staffid

end
go



go
create trigger trg_Customer_Delete
on customer
instead of delete
as
begin
	declare @Customer_Id int

	select @Customer_Id=Id
	from deleted

	update Bill
	set Customer_Id=null
	where Customer_Id=@Customer_Id

	delete CUSTOMER
	where Id=@Customer_Id
end
go



go
create trigger trg_Delete_Movie
on Movie
instead of delete
as
begin
	declare @Movie_Id int

	select @Movie_Id=id
	from deleted

	update Bill_AddMovie
	set Movie_Id=null
	where Movie_Id=@Movie_Id

	delete ShowTime
	where Movie_Id=@Movie_Id

	delete MOVIE
	where id=@Movie_Id
end
go



go
create trigger trg_setPrice_Product
on Product
for insert
as
begin
	declare @ID int
	declare @PurchasePrice int

	select @ID=ID, @PurchasePrice=PurchasePrice
	from inserted

	update Product
	set Price=@PurchasePrice+0.2*@PurchasePrice
	where ID=@ID
end
go


go
create trigger trg_autoAddBill_AddProduct
on Product
after insert
as
begin
	declare @Product_Id int
	declare @Quantity int
	declare @BillDate smalldatetime
	declare @UnitPurchasePrice int

	select @Product_Id=ID,@Quantity=Quantity,@UnitPurchasePrice=PurchasePrice
	from inserted

	set @BillDate= GETDATE()

	insert into Bill_AddProduct(Product_Id,BillDate,Quantity,UnitPurchasePrice)
	values(@Product_Id,@BillDate,@Quantity,@UnitPurchasePrice)
end
go


go
create trigger trg_autoAddBill_ImportProduct
on product
after update
as
begin
	declare @Id int
	declare @QuantityNew int
	declare @UnitPuchasePrice int
	declare @QuantityOld int

	select @Id=ID,@QuantityOld=Quantity
	from deleted

	select @QuantityNew=Quantity,@UnitPuchasePrice=PurchasePrice
	from inserted

	declare @denta int
	set @denta=@QuantityNew-@QuantityOld

	insert into Bill_ImportProduct(Product_Id,BillDate,Quantity,UnitPurchasePrice)
	values(@Id,GETDATE(),@denta,@UnitPuchasePrice)
end
go


go
create trigger trg_deleteProduct
on product
instead of delete
as
begin
	declare @Product_Id int

	select @Product_Id=id
	from deleted

	update Bill_ImportProduct
	set Product_Id=null
	where Product_Id=@Product_Id

	update BillDetail
	set Product_Id=null
	where Product_Id=@Product_Id

	update Bill_AddProduct
	set Product_Id=null
	where Product_Id=@Product_Id

	delete Product
	where ID=@Product_Id
end
go



go
create trigger trg_createSeatForShowtimeByShowtime_insert
on ShowTime
after insert
as
begin
	declare @ShowtimeId int
	declare @Auditorium_Id int

	select @ShowtimeId=Id,@Auditorium_Id=Auditorium_Id
	from inserted

	insert into SeatForShowtime(Seat_Id,ShowTime_Id,Condition)
	select Id, @ShowtimeId, 0
    from Seat
    where Auditorium_Id = @Auditorium_Id
end
go



go
create trigger trg_deleteShowtime
on ShowTime
instead of delete
as
begin
	declare @ShowtimeId int
	select @ShowtimeId=Id
	from deleted

	delete SeatForShowtime
	where ShowTime_Id=@ShowtimeId

	update Bill
	set ShowTime_Id=null
	where ShowTime_Id=@ShowtimeId

	delete ShowTime
	where Id=@ShowtimeId
end
go



go
create trigger trg_updateBillAddmovie_update
on Movie
after update
as
begin
	declare @MovieId int
	declare @ImportPrice int

	select @MovieId=Id,@ImportPrice=ImportPrice
	from inserted

	update Bill_AddMovie
	set Total=@ImportPrice
	where Id=@MovieId
end
go

