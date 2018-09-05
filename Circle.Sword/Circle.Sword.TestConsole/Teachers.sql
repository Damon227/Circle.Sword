create table dbo.[Circle.Sword.Teachers]
(
Id int not null identity(1,1),
TeacherId nvarchar(255) not null,
Name nvarchar(255) null,
Enabled bit not null,
CreateTime datetimeoffset not null,
Amount bigint not null
)