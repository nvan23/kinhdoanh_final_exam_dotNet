CREATE TABLE [dbo].[khach_hang] (
    [MAKH]   NCHAR (10)     NOT NULL,
    [TENKH]  NVARCHAR (50)  NULL,
    [DIACHI] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([MAKH] ASC)
);

CREATE TABLE [dbo].[su_dung] (
    [MASD]   NCHAR (10)    NOT NULL,
    [LOAISD] NVARCHAR (50) NULL,
    [DONGIA] INT           NULL,
    PRIMARY KEY CLUSTERED ([MASD] ASC)
);

CREATE TABLE [dbo].[chi_tiet] (
    [MASD]      NCHAR (10) NOT NULL,
    [MAKH]      NCHAR (10) NOT NULL,
    [SoKW]      INT        NULL,
    [ThanhTien] INT        NULL,
    PRIMARY KEY CLUSTERED ([MAKH] ASC, [MASD] ASC),
    FOREIGN KEY ([MAKH]) REFERENCES [dbo].[khach_hang] ([MAKH]),
    FOREIGN KEY ([MASD]) REFERENCES [dbo].[su_dung] ([MASD])
);