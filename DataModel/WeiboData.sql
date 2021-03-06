USE [MyTestDB]
GO
/****** Object:  StoredProcedure [dbo].[deleteData_MyWeibo]    Script Date: 2013/9/17 17:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[deleteData_MyWeibo] 
	-- Add the parameters for the stored procedure here
	@weiboID bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	Delete MyWeibo where WeiboID = @weiboID
END
GO
/****** Object:  StoredProcedure [dbo].[insertData_MyWeibo]    Script Date: 2013/9/17 17:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[insertData_MyWeibo]
	-- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@weiboID bigint OUT,
	@weiboDescription nvarchar(140),
	@imageUrl nvarchar(380),
	@createdBy nvarchar(40),
	@createdOn datetime,
	@likerate int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
insert into MyWeibo (WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate) values (@weiboDescription,@imageUrl,@createdBy,@createdOn,@likerate)
END

set @weiboID = SCOPE_IDENTITY()


GO
/****** Object:  StoredProcedure [dbo].[query_MyWeibo]    Script Date: 2013/9/17 17:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[query_MyWeibo]
as 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	select weiboID,WeiboDescription,ImageUrl,CreatedBy,CreatedOn,likerate from MyWeibo
END
GO
/****** Object:  StoredProcedure [dbo].[updateData_MyWeibo]    Script Date: 2013/9/17 17:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateData_MyWeibo] 
	-- Add the parameters for the stored procedure here
	@weiboID bigint,
	@weiboDescription nvarchar(140),
	@imageUrl nvarchar(380),
	@createdBy nvarchar(40),
	@createdOn datetime,
	@likerate int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT off;

    -- Insert statements for procedure here
	UPDATE MyWeibo SET WeiboDescription = @weiboDescription,ImageUrl = @imageUrl,CreatedBy = @createdBy,CreatedOn=@createdOn,likerate =@likerate WHERE WeiboID =@weiboID
END

GO
/****** Object:  Table [dbo].[MyWeibo]    Script Date: 2013/9/17 17:33:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MyWeibo](
	[WeiboID] [bigint] IDENTITY(1,1) NOT NULL,
	[WeiboDescription] [nvarchar](140) NULL,
	[ImageUrl] [nvarchar](380) NULL,
	[CreatedBy] [nvarchar](40) NULL,
	[CreatedOn] [datetime] NULL,
	[likerate] [int] NOT NULL,
 CONSTRAINT [PK__MyWeibo__512995157E7D24E9] PRIMARY KEY CLUSTERED 
(
	[WeiboID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[MyWeibo] ON 

INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10004, N'【小编在现场】现在，正在召开上海轨道交通11号线二期通车试运营新闻通气会。11号线二期将于本周六8月31日通车试运营啦。', N'http://ww4.sinaimg.cn/square/734b2017jw1e83k4r04j8j209g0dw74t.jpg', N'上海地铁shmetro', CAST(0x0000A22900000000 AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10005, N'可怜之人必有可恨之处。除去自然灾害所导致的可怜情形，这句话总是成立。好些人四肢健全，确终日立于街头行乞。好些人能说会道，确始终不愿融入社会。究其根本原因，生理和心理上的懒惰是这些“可怜”之人的“可恨”之处。当然，你可能会施舍前者或者开导后者，但他们的生活最终还是要他们自己来决定。', NULL, N'Qingyu', CAST(0x0000A22800000000 AS DateTime), 1)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10006, N'今天有“秋高气爽”的感觉', N'http://ww4.sinaimg.cn/thumbnail/6628711bgw1e83e5e9gf1j20c82f8dpy.jpg', N'袁斌_AgileDo', CAST(0x0000A22900000000 AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10007, N'还有什么事是他们做不出的？！！！[怒]', NULL, N'上海最资讯', CAST(0x0000A22900000000 AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10008, N'i love SQL', N'lkajsdf', N'ijf', CAST(0x0000A23400ED959B AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10018, N'ad my d', N'kd', N'', CAST(0x0000A23400EE8596 AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10020, N'I love', N'image', N'qing', CAST(0x0000A236010CCD20 AS DateTime), 0)
INSERT [dbo].[MyWeibo] ([WeiboID], [WeiboDescription], [ImageUrl], [CreatedBy], [CreatedOn], [likerate]) VALUES (10021, N'hello my data', N'lijd', N'ijfke', CAST(0x0000A236010CB78C AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[MyWeibo] OFF
ALTER TABLE [dbo].[MyWeibo] ADD  CONSTRAINT [DF__MyWeibo__likerat__3A81B327]  DEFAULT ((0)) FOR [likerate]
GO
