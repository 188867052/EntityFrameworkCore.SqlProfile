CREATE TABLE [dbo].[log](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[create_time] [datetime] NOT NULL,
	[message] [text] NULL,
	[log_level] [int] NOT NULL,
	[sql_operate_type] [int] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[log] ADD  CONSTRAINT [DF__Log__CreateTime]  DEFAULT (getdate()) FOR [create_time]
GO

ALTER TABLE [dbo].[log] ADD  CONSTRAINT [DF_Log_SqlOperateType]  DEFAULT ((0)) FOR [sql_operate_type]
GO