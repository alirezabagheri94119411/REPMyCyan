CREATE TABLE [dbo].[AccDocumentHeaders] (
    [AccDocumentHeaderId] [int] NOT NULL IDENTITY,
    [DocumentNumber] [bigint] NOT NULL,
    [DailyNumber] [bigint] NOT NULL,
    [SystemFixNumber] [bigint] NOT NULL,
    [DocumentDate] [datetime] NOT NULL,
    [SystemName] [nvarchar](max),
    [Exporter] [nvarchar](max),
    [Seconder] [nvarchar](max),
    [HeaderDescription] [nvarchar](max),
    [ManualDocumentNumber] [bigint] NOT NULL,
    [BaseDocument] [nvarchar](max),
    [Status] [int] NOT NULL,
    [TypeDocumentId] [int],
    [Attachment] [nvarchar](max),
    [AmountDocument] [float] NOT NULL,
    CONSTRAINT [PK_dbo.AccDocumentHeaders] PRIMARY KEY ([AccDocumentHeaderId])
)
CREATE INDEX [IX_TypeDocumentId] ON [dbo].[AccDocumentHeaders]([TypeDocumentId])
CREATE TABLE [dbo].[AccDocumentItems] (
    [AccDocumentItemId] [int] NOT NULL IDENTITY,
    [SLId] [int],
    [DL1Id] [int],
    [DL2Id] [int],
    [Description1] [nvarchar](max),
    [Description2] [nvarchar](max),
    [Debit] [float] NOT NULL,
    [Credit] [float] NOT NULL,
    [CurrencyAmount] [float] NOT NULL,
    [ExchangeRate] [float],
    [CurrencyId] [int],
    [TrackingDate] [datetime],
    [TrackingNumber] [bigint] NOT NULL,
    [AccDocumentHeaderId] [int],
    CONSTRAINT [PK_dbo.AccDocumentItems] PRIMARY KEY ([AccDocumentItemId])
)
CREATE INDEX [IX_SLId] ON [dbo].[AccDocumentItems]([SLId])
CREATE INDEX [IX_DL1Id] ON [dbo].[AccDocumentItems]([DL1Id])
CREATE INDEX [IX_DL2Id] ON [dbo].[AccDocumentItems]([DL2Id])
CREATE INDEX [IX_CurrencyId] ON [dbo].[AccDocumentItems]([CurrencyId])
CREATE INDEX [IX_AccDocumentHeaderId] ON [dbo].[AccDocumentItems]([AccDocumentHeaderId])
CREATE TABLE [dbo].[Currencies] (
    [CurrencyId] [int] NOT NULL IDENTITY,
    [CurrencyTitle] [nvarchar](max),
    [CurrencyTitle2] [nvarchar](max),
    [ExchangeUnit] [float] NOT NULL,
    [NumberDecimal] [int] NOT NULL,
    [TitleDecimal] [nvarchar](max),
    [TitleDecimal2] [nvarchar](max),
    CONSTRAINT [PK_dbo.Currencies] PRIMARY KEY ([CurrencyId])
)
CREATE TABLE [dbo].[BankAccounts] (
    [BankAccountId] [int] NOT NULL IDENTITY,
    [Dcode] [int] NOT NULL,
    [BankId] [int],
    [AccountNumber] [bigint] NOT NULL,
    [Branch] [nvarchar](max),
    [AccountTypeId] [int],
    [ShabaNumber] [nvarchar](max),
    [Addrress] [nvarchar](max),
    [PostalCode] [bigint] NOT NULL,
    [CardNumber] [bigint] NOT NULL,
    [PoseId] [nvarchar](max),
    [WebSite] [nvarchar](max),
    [OpeningDate] [datetime] NOT NULL,
    [FirstReservePeriod] [float] NOT NULL,
    [InventoryReservation] [float] NOT NULL,
    [CurrencyId] [int],
    [ExchangeRate] [bit] NOT NULL,
    [Status] [bit] NOT NULL,
    [DL_DLId] [int],
    [SL_SLId] [int],
    CONSTRAINT [PK_dbo.BankAccounts] PRIMARY KEY ([BankAccountId])
)
CREATE INDEX [IX_BankId] ON [dbo].[BankAccounts]([BankId])
CREATE INDEX [IX_AccountTypeId] ON [dbo].[BankAccounts]([AccountTypeId])
CREATE INDEX [IX_CurrencyId] ON [dbo].[BankAccounts]([CurrencyId])
CREATE INDEX [IX_DL_DLId] ON [dbo].[BankAccounts]([DL_DLId])
CREATE INDEX [IX_SL_SLId] ON [dbo].[BankAccounts]([SL_SLId])
CREATE TABLE [dbo].[AccountTypes] (
    [AccountTypeId] [int] NOT NULL IDENTITY,
    [AccountTypeTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.AccountTypes] PRIMARY KEY ([AccountTypeId])
)
CREATE TABLE [dbo].[Banks] (
    [BankId] [int] NOT NULL IDENTITY,
    [BankName] [nvarchar](max),
    [BankName2] [nvarchar](max),
    [BankTypeId] [int],
    CONSTRAINT [PK_dbo.Banks] PRIMARY KEY ([BankId])
)
CREATE INDEX [IX_BankTypeId] ON [dbo].[Banks]([BankTypeId])
CREATE TABLE [dbo].[BankTypes] (
    [BankTypeId] [int] NOT NULL IDENTITY,
    [BankTypeTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.BankTypes] PRIMARY KEY ([BankTypeId])
)
CREATE TABLE [dbo].[RelatedPersons] (
    [RelatedPersonId] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Family] [nvarchar](max),
    [Phone] [nvarchar](max),
    [CompanyId] [int],
    [PersonId] [int],
    [BankAccountId] [int],
    CONSTRAINT [PK_dbo.RelatedPersons] PRIMARY KEY ([RelatedPersonId])
)
CREATE INDEX [IX_CompanyId] ON [dbo].[RelatedPersons]([CompanyId])
CREATE INDEX [IX_PersonId] ON [dbo].[RelatedPersons]([PersonId])
CREATE INDEX [IX_BankAccountId] ON [dbo].[RelatedPersons]([BankAccountId])
CREATE TABLE [dbo].[Companies] (
    [CompanyId] [int] NOT NULL IDENTITY,
    [Dcode] [bigint] NOT NULL,
    [DateRegistration] [datetime] NOT NULL,
    [RegistrationNumber] [nvarchar](max),
    [EconomicalNumber] [nvarchar](max),
    [City] [nvarchar](max),
    [Province] [nvarchar](max),
    [Town] [nvarchar](max),
    [PostalCode] [bigint] NOT NULL,
    [PhoneManager] [nvarchar](max),
    [ManagerName] [nvarchar](max),
    [Phone1] [nvarchar](max),
    [Phone2] [nvarchar](max),
    [Phone3] [nvarchar](max),
    [Address] [nvarchar](max),
    [LatinName] [nvarchar](max),
    [FirstAccountBalance] [bigint] NOT NULL,
    [Fax] [bigint] NOT NULL,
    [Logo] [nvarchar](max),
    [WebSite] [nvarchar](max),
    [DL_DLId] [int],
    [SL_SLId] [int],
    CONSTRAINT [PK_dbo.Companies] PRIMARY KEY ([CompanyId])
)
CREATE INDEX [IX_DL_DLId] ON [dbo].[Companies]([DL_DLId])
CREATE INDEX [IX_SL_SLId] ON [dbo].[Companies]([SL_SLId])
CREATE TABLE [dbo].[People] (
    [PersonId] [int] NOT NULL IDENTITY,
    [Dcode] [bigint] NOT NULL,
    [NationalCode] [bigint] NOT NULL,
    [CertificateNumber] [bigint] NOT NULL,
    [BirthDate] [datetime] NOT NULL,
    [FatherName] [nvarchar](max),
    [NumberOfChildren] [int] NOT NULL,
    [PostalCode] [bigint] NOT NULL,
    [Sexuality] [nvarchar](max),
    [BirthPlace] [nvarchar](max),
    [CertificatePlace] [nvarchar](max),
    [CertificateSeries] [nvarchar](max),
    [Address] [nvarchar](max),
    [Address2] [nvarchar](max),
    [WebSite] [nvarchar](max),
    [FirstAccountBalance] [float] NOT NULL,
    [Fax] [bigint] NOT NULL,
    [Logo] [nvarchar](max),
    [DL_DLId] [int],
    [SL_SLId] [int],
    CONSTRAINT [PK_dbo.People] PRIMARY KEY ([PersonId])
)
CREATE INDEX [IX_DL_DLId] ON [dbo].[People]([DL_DLId])
CREATE INDEX [IX_SL_SLId] ON [dbo].[People]([SL_SLId])
CREATE TABLE [dbo].[ExchangeRates] (
    [ExchangeRateId] [int] NOT NULL IDENTITY,
    [CurrencyId] [int],
    [EffectiveDate] [datetime] NOT NULL,
    [ParityRate] [float] NOT NULL,
    [Description] [nvarchar](max),
    CONSTRAINT [PK_dbo.ExchangeRates] PRIMARY KEY ([ExchangeRateId])
)
CREATE INDEX [IX_CurrencyId] ON [dbo].[ExchangeRates]([CurrencyId])
CREATE TABLE [dbo].[DLs] (
    [DLId] [int] NOT NULL IDENTITY,
    [DLType] [int] NOT NULL,
    [DLCode] [int] NOT NULL,
    [Title] [nvarchar](max),
    [Title2] [nvarchar](max),
    [Status] [bit] NOT NULL,
    [DLAccountsNature] [int] NOT NULL,
    CONSTRAINT [PK_dbo.DLs] PRIMARY KEY ([DLId])
)
CREATE TABLE [dbo].[SLs] (
    [SLId] [int] NOT NULL IDENTITY,
    [TLId] [int],
    [SLCode] [bigint] NOT NULL,
    [Title] [nvarchar](max),
    [Title2] [nvarchar](max),
    [Status] [bit] NOT NULL,
    [Property] [int] NOT NULL,
    [AccountsNatureEnum] [int] NOT NULL,
    [DLType1] [int] NOT NULL,
    [DLType2] [int] NOT NULL,
    CONSTRAINT [PK_dbo.SLs] PRIMARY KEY ([SLId])
)
CREATE INDEX [IX_TLId] ON [dbo].[SLs]([TLId])
CREATE TABLE [dbo].[SLStandardDescriptions] (
    [SLStandardDescriptionId] [int] NOT NULL IDENTITY,
    [SLStandardDescriptionCode] [int] NOT NULL,
    [SLStandardDescriptionTitle] [nvarchar](max),
    [SLId] [int],
    CONSTRAINT [PK_dbo.SLStandardDescriptions] PRIMARY KEY ([SLStandardDescriptionId])
)
CREATE INDEX [IX_SLId] ON [dbo].[SLStandardDescriptions]([SLId])
CREATE TABLE [dbo].[Stocks] (
    [StockId] [int] NOT NULL IDENTITY,
    [StockCode] [nvarchar](max),
    [StockTitle1] [nvarchar](max),
    [StockTitle2] [nvarchar](max),
    [UserId] [int],
    [Phone] [nvarchar](max),
    [Address] [nvarchar](max),
    [ProductId] [int],
    [SLId] [int],
    CONSTRAINT [PK_dbo.Stocks] PRIMARY KEY ([StockId])
)
CREATE INDEX [IX_UserId] ON [dbo].[Stocks]([UserId])
CREATE INDEX [IX_SLId] ON [dbo].[Stocks]([SLId])
CREATE TABLE [dbo].[Products] (
    [ProductId] [int] NOT NULL IDENTITY,
    [ProductCode] [bigint] NOT NULL,
    [ProductTitle] [nvarchar](max),
    [ProductTitle2] [nvarchar](max),
    [ProductTypeEnum] [int] NOT NULL,
    [Salable] [bit] NOT NULL,
    [TaxExempt] [bit] NOT NULL,
    [ProductBarcode] [bigint] NOT NULL,
    [IranCode] [bigint] NOT NULL,
    [EquivalentUnit] [int] NOT NULL,
    [SalePrice] [float] NOT NULL,
    [InventoryControl_InventoryControlId] [int],
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY ([ProductId])
)
CREATE INDEX [IX_InventoryControl_InventoryControlId] ON [dbo].[Products]([InventoryControl_InventoryControlId])
CREATE TABLE [dbo].[ImageProducts] (
    [ImageProductId] [int] NOT NULL IDENTITY,
    [Picture] [nvarchar](max),
    [Product_ProductId] [int],
    CONSTRAINT [PK_dbo.ImageProducts] PRIMARY KEY ([ImageProductId])
)
CREATE INDEX [IX_Product_ProductId] ON [dbo].[ImageProducts]([Product_ProductId])
CREATE TABLE [dbo].[InventoryControls] (
    [InventoryControlId] [int] NOT NULL IDENTITY,
    [MinInventory] [float] NOT NULL,
    [MaxInventory] [float] NOT NULL,
    [Agent] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.InventoryControls] PRIMARY KEY ([InventoryControlId])
)
CREATE TABLE [dbo].[SimilarProducts] (
    [SimilarProductId] [int] NOT NULL IDENTITY,
    [Ratio] [nvarchar](max),
    [Order] [int] NOT NULL,
    [Product_ProductId] [int],
    CONSTRAINT [PK_dbo.SimilarProducts] PRIMARY KEY ([SimilarProductId])
)
CREATE INDEX [IX_Product_ProductId] ON [dbo].[SimilarProducts]([Product_ProductId])
CREATE TABLE [dbo].[Users] (
    [UserId] [int] NOT NULL IDENTITY,
    [FriendlyName] [nvarchar](450) NOT NULL,
    [UserName] [nvarchar](450) NOT NULL,
    [Password] [nvarchar](50) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [GroupId] [int],
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([UserId])
)
CREATE INDEX [IX_GroupId] ON [dbo].[Users]([GroupId])
CREATE TABLE [dbo].[UGDPs] (
    [UGDPId] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [GroupId] [int] NOT NULL,
    [DynamicPageId] [int] NOT NULL,
    [UserRights] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UGDPs] PRIMARY KEY ([UGDPId])
)
CREATE INDEX [IX_UserId] ON [dbo].[UGDPs]([UserId])
CREATE INDEX [IX_GroupId] ON [dbo].[UGDPs]([GroupId])
CREATE INDEX [IX_DynamicPageId] ON [dbo].[UGDPs]([DynamicPageId])
CREATE TABLE [dbo].[DynamicPages] (
    [DynamicPageId] [int] NOT NULL,
    [Name] [nvarchar](max),
    [Title] [nvarchar](max),
    [ParentId] [int],
    [Order] [int] NOT NULL,
    [IconPath] [nvarchar](max),
    CONSTRAINT [PK_dbo.DynamicPages] PRIMARY KEY ([DynamicPageId])
)
CREATE INDEX [IX_ParentId] ON [dbo].[DynamicPages]([ParentId])
CREATE TABLE [dbo].[Groups] (
    [GroupId] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max) NOT NULL,
    CONSTRAINT [PK_dbo.Groups] PRIMARY KEY ([GroupId])
)
CREATE TABLE [dbo].[TLs] (
    [TLId] [int] NOT NULL IDENTITY,
    [GLId] [int],
    [TLCode] [bigint] NOT NULL,
    [Title] [nvarchar](max),
    [Title2] [nvarchar](max),
    [Status] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.TLs] PRIMARY KEY ([TLId])
)
CREATE INDEX [IX_GLId] ON [dbo].[TLs]([GLId])
CREATE TABLE [dbo].[GLs] (
    [GLId] [int] NOT NULL IDENTITY,
    [AccountGLEnum] [int] NOT NULL,
    [GLCode] [bigint] NOT NULL,
    [Title] [nvarchar](max),
    [Title2] [nvarchar](max),
    [Status] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.GLs] PRIMARY KEY ([GLId])
)
CREATE TABLE [dbo].[TypeDocuments] (
    [TypeDocumentId] [int] NOT NULL IDENTITY,
    [TypeDocumentTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.TypeDocuments] PRIMARY KEY ([TypeDocumentId])
)
CREATE TABLE [dbo].[AccountDocuments] (
    [AccountDocumentId] [int] NOT NULL IDENTITY,
    [AccountDocumentTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.AccountDocuments] PRIMARY KEY ([AccountDocumentId])
)
CREATE TABLE [dbo].[DocumentNumberings] (
    [DocumentNumberingId] [int] NOT NULL IDENTITY,
    [AccountDocumentId] [int],
    [CountingWayId] [int],
    [StartNumber] [bigint] NOT NULL,
    [EndNumber] [bigint] NOT NULL,
    [StyleId] [int],
    CONSTRAINT [PK_dbo.DocumentNumberings] PRIMARY KEY ([DocumentNumberingId])
)
CREATE INDEX [IX_AccountDocumentId] ON [dbo].[DocumentNumberings]([AccountDocumentId])
CREATE INDEX [IX_CountingWayId] ON [dbo].[DocumentNumberings]([CountingWayId])
CREATE INDEX [IX_StyleId] ON [dbo].[DocumentNumberings]([StyleId])
CREATE TABLE [dbo].[CountingWays] (
    [CountingWayId] [int] NOT NULL IDENTITY,
    [CountingWayTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.CountingWays] PRIMARY KEY ([CountingWayId])
)
CREATE TABLE [dbo].[Styles] (
    [StyleId] [int] NOT NULL IDENTITY,
    [StyleTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.Styles] PRIMARY KEY ([StyleId])
)
CREATE TABLE [dbo].[AccountsNatures] (
    [AccountsNatureId] [int] NOT NULL IDENTITY,
    [AccountsNatureTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.AccountsNatures] PRIMARY KEY ([AccountsNatureId])
)
CREATE TABLE [dbo].[AuditEntries] (
    [AuditEntryID] [int] NOT NULL IDENTITY,
    [EntitySetName] [nvarchar](255),
    [EntityTypeName] [nvarchar](255),
    [State] [int] NOT NULL,
    [StateName] [nvarchar](255),
    [CreatedBy] [nvarchar](255),
    [CreatedDate] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.AuditEntries] PRIMARY KEY ([AuditEntryID])
)
CREATE TABLE [dbo].[AuditEntryProperties] (
    [AuditEntryPropertyID] [int] NOT NULL IDENTITY,
    [AuditEntryID] [int] NOT NULL,
    [RelationName] [nvarchar](255),
    [PropertyName] [nvarchar](255),
    [OldValue] [nvarchar](max),
    [NewValue] [nvarchar](max),
    CONSTRAINT [PK_dbo.AuditEntryProperties] PRIMARY KEY ([AuditEntryPropertyID])
)
CREATE INDEX [IX_AuditEntryID] ON [dbo].[AuditEntryProperties]([AuditEntryID])
CREATE TABLE [dbo].[Customers] (
    [Id] [int] NOT NULL IDENTITY,
    [StoreId] [int],
    [FirstName] [nvarchar](max),
    [LastName] [nvarchar](max),
    [Phone] [nvarchar](max),
    [Email] [nvarchar](max),
    [Street] [nvarchar](max),
    [City] [nvarchar](max),
    [State] [nvarchar](max),
    [Zip] [nvarchar](max),
    CONSTRAINT [PK_dbo.Customers] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Orders] (
    [Id] [bigint] NOT NULL IDENTITY,
    [StoreId] [int],
    [CustomerId] [int] NOT NULL,
    [OrderStatusId] [int] NOT NULL,
    [OrderDate] [datetime] NOT NULL,
    [DeliveryDate] [datetime] NOT NULL,
    [DeliveryCharge] [decimal](18, 2) NOT NULL,
    [ItemsTotal] [decimal](18, 2) NOT NULL,
    [Phone] [nvarchar](max),
    [DeliveryStreet] [nvarchar](max),
    [DeliveryCity] [nvarchar](max),
    [DeliveryState] [nvarchar](max),
    [DeliveryZip] [nvarchar](max),
    CONSTRAINT [PK_dbo.Orders] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CustomerId] ON [dbo].[Orders]([CustomerId])
CREATE INDEX [IX_OrderStatusId] ON [dbo].[Orders]([OrderStatusId])
CREATE TABLE [dbo].[OrderItems] (
    [Id] [bigint] NOT NULL IDENTITY,
    [StoreId] [int],
    [OrderId] [bigint] NOT NULL,
    [ProductId] [int] NOT NULL,
    [ProductSizeId] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [TotalPrice] [decimal](18, 2) NOT NULL,
    [Instructions] [nvarchar](max),
    CONSTRAINT [PK_dbo.OrderItems] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrderId] ON [dbo].[OrderItems]([OrderId])
CREATE INDEX [IX_ProductId] ON [dbo].[OrderItems]([ProductId])
CREATE INDEX [IX_ProductSizeId] ON [dbo].[OrderItems]([ProductSizeId])
CREATE TABLE [dbo].[OrderItemOptions] (
    [Id] [bigint] NOT NULL IDENTITY,
    [StoreId] [int],
    [OrderItemId] [bigint] NOT NULL,
    [ProductOptionId] [int] NOT NULL,
    [Quantity] [int] NOT NULL,
    [Price] [decimal](18, 2) NOT NULL,
    CONSTRAINT [PK_dbo.OrderItemOptions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrderItemId] ON [dbo].[OrderItemOptions]([OrderItemId])
CREATE INDEX [IX_ProductOptionId] ON [dbo].[OrderItemOptions]([ProductOptionId])
CREATE TABLE [dbo].[ProductOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Type] [nvarchar](max),
    [Name] [nvarchar](max),
    [Factor] [int] NOT NULL,
    [IsPizaaOption] [bit] NOT NULL,
    [IsSaladOption] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.ProductOptions] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Product22] (
    [Id] [int] NOT NULL IDENTITY,
    [Type] [nvarchar](max),
    [Name] [nvarchar](max),
    [Description] [nvarchar](max),
    [Image] [nvarchar](max),
    [HasOptions] [bit] NOT NULL,
    [IsVegetarian] [bit] NOT NULL,
    [WithTomatoSauce] [bit] NOT NULL,
    [SizeIds] [nvarchar](max),
    CONSTRAINT [PK_dbo.Product22] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ProductSizes] (
    [Id] [int] NOT NULL IDENTITY,
    [Type] [nvarchar](max),
    [Name] [nvarchar](max),
    [Price] [decimal](18, 2) NOT NULL,
    [PremiumPrice] [decimal](18, 2),
    [ToppingPrice] [decimal](18, 2),
    [IsGlutenFree] [bit],
    CONSTRAINT [PK_dbo.ProductSizes] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[OrderStatus] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    CONSTRAINT [PK_dbo.OrderStatus] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[DLAccountsNatures] (
    [DLAccountsNatureId] [int] NOT NULL,
    [DLAccountsNatureTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.DLAccountsNatures] PRIMARY KEY ([DLAccountsNatureId])
)
CREATE TABLE [dbo].[DLTypes] (
    [DLTypeId] [int] NOT NULL,
    [DLTypeTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.DLTypes] PRIMARY KEY ([DLTypeId])
)
CREATE TABLE [dbo].[FinancialYears] (
    [FinancialYearId] [int] NOT NULL IDENTITY,
    [YearName] [int] NOT NULL,
    [Description] [nvarchar](max),
    [StartDate] [datetime] NOT NULL,
    [EndDate] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [Selected] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.FinancialYears] PRIMARY KEY ([FinancialYearId])
)
CREATE TABLE [dbo].[KeyValues] (
    [Key] [nvarchar](128) NOT NULL,
    [Value] [nvarchar](max),
    CONSTRAINT [PK_dbo.KeyValues] PRIMARY KEY ([Key])
)
CREATE TABLE [dbo].[MeasurementUnits] (
    [MeasurementUnitId] [int] NOT NULL IDENTITY,
    [MeasurementUnitTitle] [nvarchar](max),
    [MeasurementUnitTitle2] [nvarchar](max),
    CONSTRAINT [PK_dbo.MeasurementUnits] PRIMARY KEY ([MeasurementUnitId])
)
CREATE TABLE [dbo].[OtherProducts] (
    [OtherProductId] [int] NOT NULL IDENTITY,
    [OtherProductTitle] [nvarchar](max),
    [RequiredCode] [bit] NOT NULL,
    [RequiredDocument] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.OtherProducts] PRIMARY KEY ([OtherProductId])
)
CREATE TABLE [dbo].[ProductBrands] (
    [ProductBrandId] [int] NOT NULL IDENTITY,
    [ProductBrandTitle] [nvarchar](max),
    [RequiredCode] [bit] NOT NULL,
    [RequiredDocument] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.ProductBrands] PRIMARY KEY ([ProductBrandId])
)
CREATE TABLE [dbo].[ProductModels] (
    [ProductModelId] [int] NOT NULL IDENTITY,
    [ProductModelTitle] [nvarchar](max),
    [RequiredCode] [bit] NOT NULL,
    [RequiredDocument] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.ProductModels] PRIMARY KEY ([ProductModelId])
)
CREATE TABLE [dbo].[ProductRacks] (
    [ProductRackId] [int] NOT NULL IDENTITY,
    [ProductRackTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.ProductRacks] PRIMARY KEY ([ProductRackId])
)
CREATE TABLE [dbo].[ProductStocks] (
    [ProductStockId] [int] NOT NULL IDENTITY,
    [ProductId] [int] NOT NULL,
    [StockId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProductStocks] PRIMARY KEY ([ProductStockId])
)
CREATE INDEX [IX_ProductId] ON [dbo].[ProductStocks]([ProductId])
CREATE INDEX [IX_StockId] ON [dbo].[ProductStocks]([StockId])
CREATE TABLE [dbo].[ProductTypes] (
    [ProductTypeId] [int] NOT NULL IDENTITY,
    [ProductTypeTitle] [nvarchar](max),
    [RequiredCode] [bit] NOT NULL,
    [RequiredDocument] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.ProductTypes] PRIMARY KEY ([ProductTypeId])
)
CREATE TABLE [dbo].[Properties] (
    [PropertyId] [int] NOT NULL IDENTITY,
    [PropertyTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.Properties] PRIMARY KEY ([PropertyId])
)
CREATE TABLE [dbo].[SelectAgents] (
    [SelectAgentId] [int] NOT NULL IDENTITY,
    [SelectAgentTitle] [nvarchar](max),
    CONSTRAINT [PK_dbo.SelectAgents] PRIMARY KEY ([SelectAgentId])
)
CREATE TABLE [dbo].[TLDocumentHeaders] (
    [TLDocumentHeaderId] [int] NOT NULL IDENTITY,
    [TLDocumentNumber] [int] NOT NULL,
    [TLDocumentHeaderDate] [datetime] NOT NULL,
    [TLDocumentType] [int] NOT NULL,
    [TLDocumentExport] [int] NOT NULL,
    CONSTRAINT [PK_dbo.TLDocumentHeaders] PRIMARY KEY ([TLDocumentHeaderId])
)
CREATE TABLE [dbo].[TLDocumentItems] (
    [TLDocumentItemId] [int] NOT NULL IDENTITY,
    [TLDocumentItemCode] [bigint] NOT NULL,
    [TLDocumentItemDate] [datetime] NOT NULL,
    [TLTitle] [nvarchar](max),
    [TLId] [int] NOT NULL,
    [Credit] [float] NOT NULL,
    [Debit] [float] NOT NULL,
    [TLDocumentHeaderId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.TLDocumentItems] PRIMARY KEY ([TLDocumentItemId])
)
CREATE INDEX [IX_TLDocumentHeaderId] ON [dbo].[TLDocumentItems]([TLDocumentHeaderId])
CREATE TABLE [dbo].[ProductStock1] (
    [Product_ProductId] [int] NOT NULL,
    [Stock_StockId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProductStock1] PRIMARY KEY ([Product_ProductId], [Stock_StockId])
)
CREATE INDEX [IX_Product_ProductId] ON [dbo].[ProductStock1]([Product_ProductId])
CREATE INDEX [IX_Stock_StockId] ON [dbo].[ProductStock1]([Stock_StockId])
ALTER TABLE [dbo].[AccDocumentHeaders] ADD CONSTRAINT [FK_dbo.AccDocumentHeaders_dbo.TypeDocuments_TypeDocumentId] FOREIGN KEY ([TypeDocumentId]) REFERENCES [dbo].[TypeDocuments] ([TypeDocumentId])
ALTER TABLE [dbo].[AccDocumentItems] ADD CONSTRAINT [FK_dbo.AccDocumentItems_dbo.AccDocumentHeaders_AccDocumentHeaderId] FOREIGN KEY ([AccDocumentHeaderId]) REFERENCES [dbo].[AccDocumentHeaders] ([AccDocumentHeaderId])
ALTER TABLE [dbo].[AccDocumentItems] ADD CONSTRAINT [FK_dbo.AccDocumentItems_dbo.Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[AccDocumentItems] ADD CONSTRAINT [FK_dbo.AccDocumentItems_dbo.DLs_DL1Id] FOREIGN KEY ([DL1Id]) REFERENCES [dbo].[DLs] ([DLId])
ALTER TABLE [dbo].[AccDocumentItems] ADD CONSTRAINT [FK_dbo.AccDocumentItems_dbo.DLs_DL2Id] FOREIGN KEY ([DL2Id]) REFERENCES [dbo].[DLs] ([DLId])
ALTER TABLE [dbo].[AccDocumentItems] ADD CONSTRAINT [FK_dbo.AccDocumentItems_dbo.SLs_SLId] FOREIGN KEY ([SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[BankAccounts] ADD CONSTRAINT [FK_dbo.BankAccounts_dbo.AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountTypes] ([AccountTypeId])
ALTER TABLE [dbo].[BankAccounts] ADD CONSTRAINT [FK_dbo.BankAccounts_dbo.Banks_BankId] FOREIGN KEY ([BankId]) REFERENCES [dbo].[Banks] ([BankId])
ALTER TABLE [dbo].[BankAccounts] ADD CONSTRAINT [FK_dbo.BankAccounts_dbo.Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[BankAccounts] ADD CONSTRAINT [FK_dbo.BankAccounts_dbo.DLs_DL_DLId] FOREIGN KEY ([DL_DLId]) REFERENCES [dbo].[DLs] ([DLId])
ALTER TABLE [dbo].[BankAccounts] ADD CONSTRAINT [FK_dbo.BankAccounts_dbo.SLs_SL_SLId] FOREIGN KEY ([SL_SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[Banks] ADD CONSTRAINT [FK_dbo.Banks_dbo.BankTypes_BankTypeId] FOREIGN KEY ([BankTypeId]) REFERENCES [dbo].[BankTypes] ([BankTypeId])
ALTER TABLE [dbo].[RelatedPersons] ADD CONSTRAINT [FK_dbo.RelatedPersons_dbo.BankAccounts_BankAccountId] FOREIGN KEY ([BankAccountId]) REFERENCES [dbo].[BankAccounts] ([BankAccountId])
ALTER TABLE [dbo].[RelatedPersons] ADD CONSTRAINT [FK_dbo.RelatedPersons_dbo.Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([CompanyId])
ALTER TABLE [dbo].[RelatedPersons] ADD CONSTRAINT [FK_dbo.RelatedPersons_dbo.People_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[People] ([PersonId])
ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [FK_dbo.Companies_dbo.DLs_DL_DLId] FOREIGN KEY ([DL_DLId]) REFERENCES [dbo].[DLs] ([DLId])
ALTER TABLE [dbo].[Companies] ADD CONSTRAINT [FK_dbo.Companies_dbo.SLs_SL_SLId] FOREIGN KEY ([SL_SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[People] ADD CONSTRAINT [FK_dbo.People_dbo.DLs_DL_DLId] FOREIGN KEY ([DL_DLId]) REFERENCES [dbo].[DLs] ([DLId])
ALTER TABLE [dbo].[People] ADD CONSTRAINT [FK_dbo.People_dbo.SLs_SL_SLId] FOREIGN KEY ([SL_SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[ExchangeRates] ADD CONSTRAINT [FK_dbo.ExchangeRates_dbo.Currencies_CurrencyId] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[SLs] ADD CONSTRAINT [FK_dbo.SLs_dbo.TLs_TLId] FOREIGN KEY ([TLId]) REFERENCES [dbo].[TLs] ([TLId])
ALTER TABLE [dbo].[SLStandardDescriptions] ADD CONSTRAINT [FK_dbo.SLStandardDescriptions_dbo.SLs_SLId] FOREIGN KEY ([SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[Stocks] ADD CONSTRAINT [FK_dbo.Stocks_dbo.SLs_SLId] FOREIGN KEY ([SLId]) REFERENCES [dbo].[SLs] ([SLId])
ALTER TABLE [dbo].[Stocks] ADD CONSTRAINT [FK_dbo.Stocks_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_dbo.Products_dbo.InventoryControls_InventoryControl_InventoryControlId] FOREIGN KEY ([InventoryControl_InventoryControlId]) REFERENCES [dbo].[InventoryControls] ([InventoryControlId])
ALTER TABLE [dbo].[ImageProducts] ADD CONSTRAINT [FK_dbo.ImageProducts_dbo.Products_Product_ProductId] FOREIGN KEY ([Product_ProductId]) REFERENCES [dbo].[Products] ([ProductId])
ALTER TABLE [dbo].[SimilarProducts] ADD CONSTRAINT [FK_dbo.SimilarProducts_dbo.Products_Product_ProductId] FOREIGN KEY ([Product_ProductId]) REFERENCES [dbo].[Products] ([ProductId])
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_dbo.Users_dbo.Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId])
ALTER TABLE [dbo].[UGDPs] ADD CONSTRAINT [FK_dbo.UGDPs_dbo.DynamicPages_DynamicPageId] FOREIGN KEY ([DynamicPageId]) REFERENCES [dbo].[DynamicPages] ([DynamicPageId]) ON DELETE CASCADE
ALTER TABLE [dbo].[UGDPs] ADD CONSTRAINT [FK_dbo.UGDPs_dbo.Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Groups] ([GroupId]) ON DELETE CASCADE
ALTER TABLE [dbo].[UGDPs] ADD CONSTRAINT [FK_dbo.UGDPs_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE CASCADE
ALTER TABLE [dbo].[DynamicPages] ADD CONSTRAINT [FK_dbo.DynamicPages_dbo.DynamicPages_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[DynamicPages] ([DynamicPageId])
ALTER TABLE [dbo].[TLs] ADD CONSTRAINT [FK_dbo.TLs_dbo.GLs_GLId] FOREIGN KEY ([GLId]) REFERENCES [dbo].[GLs] ([GLId])
ALTER TABLE [dbo].[DocumentNumberings] ADD CONSTRAINT [FK_dbo.DocumentNumberings_dbo.AccountDocuments_AccountDocumentId] FOREIGN KEY ([AccountDocumentId]) REFERENCES [dbo].[AccountDocuments] ([AccountDocumentId])
ALTER TABLE [dbo].[DocumentNumberings] ADD CONSTRAINT [FK_dbo.DocumentNumberings_dbo.CountingWays_CountingWayId] FOREIGN KEY ([CountingWayId]) REFERENCES [dbo].[CountingWays] ([CountingWayId])
ALTER TABLE [dbo].[DocumentNumberings] ADD CONSTRAINT [FK_dbo.DocumentNumberings_dbo.Styles_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
ALTER TABLE [dbo].[AuditEntryProperties] ADD CONSTRAINT [FK_dbo.AuditEntryProperties_dbo.AuditEntries_AuditEntryID] FOREIGN KEY ([AuditEntryID]) REFERENCES [dbo].[AuditEntries] ([AuditEntryID]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.OrderStatus_OrderStatusId] FOREIGN KEY ([OrderStatusId]) REFERENCES [dbo].[OrderStatus] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [FK_dbo.OrderItems_dbo.Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [FK_dbo.OrderItems_dbo.Product22_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product22] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [FK_dbo.OrderItems_dbo.ProductSizes_ProductSizeId] FOREIGN KEY ([ProductSizeId]) REFERENCES [dbo].[ProductSizes] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderItemOptions] ADD CONSTRAINT [FK_dbo.OrderItemOptions_dbo.OrderItems_OrderItemId] FOREIGN KEY ([OrderItemId]) REFERENCES [dbo].[OrderItems] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[OrderItemOptions] ADD CONSTRAINT [FK_dbo.OrderItemOptions_dbo.ProductOptions_ProductOptionId] FOREIGN KEY ([ProductOptionId]) REFERENCES [dbo].[ProductOptions] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[ProductStocks] ADD CONSTRAINT [FK_dbo.ProductStocks_dbo.Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE CASCADE
ALTER TABLE [dbo].[ProductStocks] ADD CONSTRAINT [FK_dbo.ProductStocks_dbo.Stocks_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stocks] ([StockId]) ON DELETE CASCADE
ALTER TABLE [dbo].[TLDocumentItems] ADD CONSTRAINT [FK_dbo.TLDocumentItems_dbo.TLDocumentHeaders_TLDocumentHeaderId] FOREIGN KEY ([TLDocumentHeaderId]) REFERENCES [dbo].[TLDocumentHeaders] ([TLDocumentHeaderId]) ON DELETE CASCADE
ALTER TABLE [dbo].[ProductStock1] ADD CONSTRAINT [FK_dbo.ProductStock1_dbo.Products_Product_ProductId] FOREIGN KEY ([Product_ProductId]) REFERENCES [dbo].[Products] ([ProductId]) ON DELETE CASCADE
ALTER TABLE [dbo].[ProductStock1] ADD CONSTRAINT [FK_dbo.ProductStock1_dbo.Stocks_Stock_StockId] FOREIGN KEY ([Stock_StockId]) REFERENCES [dbo].[Stocks] ([StockId]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201802150415495_Init', N'Saina.Data.Migrations.Configuration',  0x1F8B0800000000000400ED7D5B731C37B2E6FB46EC7F60F069F7C41C516C8FE7781CD23921B1499A312D9347DDB677E68501758364ADBBABE8AA6A999C8DFD65FBB03F69FFC20275C525714755356585236476014824901F12895BE6FFFB3FFFF7CD7F3CEDB6479F715E2459FAF6F8F4D5EBE3239CAEB34D92DEBF3DDE9777FFFADDF17FFCFB7FFD2F6FCE37BBA7A39FDB7CDFD07CA4645ABC3D7E28CBC7EF4F4E8AF503DEA1E2D52E59E75991DD95AFD6D9EE046DB293D9EBD77F3D393D3DC184C431A17574F4E6E33E2D931DAE7E909F6759BAC68FE51E6D3F641BBC2D9AEF246559513DFA11ED70F188D6F8EDF11225297A3547257A458A95F8A93C3E7AB74D10E16489B777C747284DB3129584CFEF7F2AF0B2CCB3F47EF9483EA0EDEAF911937C77685BE086FFEFFBECB64D793DA34D39E90BB6A4D6FBA2CC768E044FBF69FAE6442CEED5C3C75DDF91DE3B27BD5C3ED356573DF8F6F8DD7A3DCFD6FB1D4ECB1F30DAE0FCF848ACF5FBB36D4E4BB43DFDEEF1719BACABA4B32CC7AF2A9A092E5E115A1915637AFFAAA5C97C926AFAD351454F20F7A70E53047AF4BF3F1D9DEDB7E53EC76F53BC2F73B4FDD3D1CDFE1329F237FCBCCA7EC5E9DB74BFDDB2AD24ED2469DC07F2E926CF1E715E3E7FC477AAB65F6D8E8F4E78422722A58E8E8E48DD5D5769F9CDECF8E847C21EFAB4C51DCC98AE5D96A4C99738C5392AF1E6069525CE094AAE36B81294C48E50795BF38FFBDD272ABAAEDEBFFC19A8D7400B25DBE7188496CF45897717C95314AE9A1692E18D5B4AF4EF1551169E9CD1BF5B524419106C1E1F7D404F0B9CDE970F6F8FC99FC747847BBC69BF34B47F4A13A20649A132DF1BAB3A7F7ACCF2B26FFD60152DF13A4B37235454A37B8E8B759E3CD6DA68E01A3FA0944C003131FE1E15B8A537BC64C818DF175D35642A7A557F3A4FF73B67D62995967549C5E88BBE2B4BB47E18A5CDEF7654DD8B5D3CCF88C2B618AE3FA2CFC97DA518D54AF68A8C60D2A91FF1B6CA583C248F4DE70A996E81A9ED22CF761FB3AD4C51CE7CBB42F93DA64DC86C4B2CB37DBE7668152B52538B5AA6B8226073809C1D67405BA0EC6DD3D986BC39E96D075B8B82F6D318F604ADE790AC09CA4FA02DD19218CB92582E1C95CA7C71EA5C62E65AA29F6C4E07575D4C65B3112AFB9438AB4781C6598E37E144F6794E9677CFB5E20E2476FEB47E40E93DFEC81A6B0D293B361C01B2CAD1FA572221D83AB42B1BC3AE302F023C66BA76D21A7EAAD34C0F86C9D1B659AD7CC1D6B489B7ED1FD5E29E6D029C439AA215D95CE765A2DA403EE78BDBF9A23865196B3E499CB4DFDDAB9E69AA9EC955CF1455CF9CAB5E2EC09A978BDBE5821346FD45AAB7F90C556B6D3BF43019C868682B98DC5A60159EAB99A0549603DA076D9DABA4DC0EBF7AE66A1B7E366EE72D5228740AAC6793395E273BB43589C73041D1C60B9406EB02B6B2013A5CA975DEA3F4D7667CC2AB3A2643A7D62977AC4652E591749432A3ABB21466AA98939A38191BE63E5B8E59DB0CE698CD71DB6BE29E613083D4C770AEA06981115BC0CCF01E15C9FA2ABDCBF25D95FA8AFD9BA962F2B981E1C5677A108A8FB6174D877F98C6A39CBBEE6DD50D8DB23998A374FD30FC1659CD31ADC5B1ADCB07F409F12D1D8ECBCD868CDCA218BCA29BAC28D1F68CC78E87F4CE50BE898102C20F2397C19AFD0BFEB44CCAE12DA9EB479C2A17C88E5D7391E445F9111738FF8C6F709E649B4063E92AFD4C744B963FD744117BAA10B89FE138B4A09D8BF71999CF50EA7ECAC4EDFB5B53D16D09B40A43B519D0A6DFF2E614B70700E68176B9E18CAE16122DAC34E8947C4A89A00917C6196F45C6363945F3CD689BDAB25D314AE6659C3D6E61BEBB1C7991A55D1FF10705AA3C523F2B330659721C9687B1E4982A26B7E484B9DEE33C40692A0C68C931D50EB4DCF75F0E06293B609F53AF15BD572B432E532647756BA6FB2C4CC6C431AD6F94CB1E6D45C3EF54D19A6C560E61FB2D0ED33334DD99C7928943EDD4DC0D5789B53E05341B98E4E0ED882167B096FE410C74DFB94B89D48107FC54B3967A3459E1151A47009CBDF0CA9972438196AB6472E472DCF8C057223016864799B02ED02ED93E0FBFB7F390A5231C1665BB4794BAAEF81592352B18D5D6AAEB0CACB8F315B48813B58871B567BD66AEFB183EE9A8D36E85052A73D00165900FEFC15CAE8BFB56C9017C36BDA064134A97B80433859D79B75D3B8C5E6EC84FAE919951EA7CDEAD18E0231E6678DDA72FF1477C9F14A41BB98D4DDFED5796D848A700E74459643B0298ED48159E55B2197A5ACAB3CF49BA1E7E665A65BF0F7F4B3ED6014A35597F4029BA1F41CE4D3DA3183B55C386BF3F5A5533FC0E4055CD37A39CFF8D71FCB720EA2C1DC7E6A5A7578DF1F61E6D11A300FC06CC05652B84C022BBCF5EEE4163C04185A7C528DD8CD1DA955EC6D8B06BE403591CFBAF8AC75F0E4730C47EAC188972C780FC9DDC51A1E128174E92BC7C8873328FCA879166D3BAE1D777670FC9964C11A9090AE3182F4BFCB447DB318CC74A68375B3482F9C8C06DF40A97384FF0F0D3FF58664653CFF0E6D958F77A34D68CE7CD9903356702CC0CAF0D1FD1C8D0EE0A79D918FC5DA3811E3AB0954C6E70B0CCF8981D62F9B18C0FDFCB647777785D269F7194B9FD06E58455E8499DEB86D4904FF7FD5E7E795E3917C7A8FE62BAD7209D2F622E02DAC3F057F3C5E48371BEF0198273F97DF09056FFA23E5F6F7ABB7E01477F78B94E982F0403D3F731CE38AF708637523C6FA84ADDDAC29AACB008520571F1897682F3BFB6325F282FAD084992F610D3FDCEC612C5AB1A429DC9C071D57D8758EA135DF9A9DFA8BABD5F0518E0DEB53AD4EDFC8015AE7BE65CB7C612242465F3AFFB087110C3D05B0E33872CA79F43965E7308E06362C03964E5EAD162294E131E6BA0AFD30474DA56FDE4A687F6A3D77C0ECD2C0C6D9F8907B43F4EE31920B4E8CC9B9CFFB4B8544F8B4BC3B428A6C79D16978A6971A99B169721D3A2667A5842D3C352393D2CB5D3838E87CA93828B9305A06AD6F7827DBD642CA71B946F9845A08A1520EB2D9DCA58E6E03C804F08454667E71465B6565CB1AC9244069B6F32436D82B3AF2ED83BC64A12DC0AF68EB18AE01D03ECCD814C0CA0A603B03A00AEFC0C1105A1B16C139081F0452A48761C93C4EC3FCCDDF14C802292F5A64163F90D48AA4D02062099CC7698F059EBB0E9C717E5C26B3CB505471B3FB44276BC0C6803939AAA2134FC0DA2BEAEE16DFB9F0AB393B2496E738F7522487E6EF66B5757AA016AAEA91036629AC4DBD6CE610EA9B814F93E329F1CCBE997CEA69274AB686CD9564E3108564F1380AE603E4BFDC0A60519594D87C6D0EA0DA9C9F53A0375E70B3F8A5132A06E6FAA8C70A1B526348EFDC35636C225D0A6B66E0781DF5AE1D35C8D4854E50EDDF659A1A7F327BC7B2C430935ED798FF2F0AB60573912CD6C0F2AE7BFED93CF684BE0CC3A8CF334D9099D9B3C71BFBDA1D4AA573B748FB5930D9BE3B6D377BD9E85D225850B66729D7E3A07303470479E6DB573A39C599E25C53CF2750E5546E72D9564976C51AEED683E0FD4D5700E79DB02CE167117C5DA005175A8302F7BCDBD2CA6624CC02CBDC9676196199FA9582C3FDA7C9CACB9D3DDE1AF8C740888A5BB44CC6A159C1F7225DD1401BD02CDE9112C30E4856280C65848FE90A45DF58177A908E463917A77CF44CAB03392EC37A8B87923CA4E1547717248F2EC78ED5D4914C682E3474A65F0A5C275BEE1DE29D8D8A85E9AD9DBD891F613F43691D740A8B71AE29D94507A9383BFDDC373853CB8F73720D02FF204A79BEDB3E17DCA9FBF7D6D857747FD4A5B3B4DCD37A8287ECF729D33D1612ABE2ADE55F78F4397DE9779B67FF4DEF09C3FA76897AC6F88AD05AF397EBA9CDFDCD62393D9E46B3FCA5B7C5D8AEBBAA76A07C842955211E5D63CCC67890D362DE202CC62CF535494D07EA89F76243D1B553B127AD36B47C28497766CCA8DA51DEDB4B1CF4075BDA5D48FD75052B44D1F93FB87920F7F7783F35D5214D5AD056F1B846113BEEBD9A7D7A3F45DBAA9FA87BF7EABCC25075451678DAD88789529EA233655A196B82CAEDCA9CF63F48A5AD24B920AF77B82C10A3A9E7262C84EAEA38421E7FC28C365C49A35D68FD5F9AEE931F5086F8C473AB241B97B2C4D9FE594689DADC9C843E5009106D47731BBC7DA0685D9678455659B2E5F998732395FD1AC24128F499D26975AE2A225D93965882948D7BB8AD9CA4BC536F3513CE55A119C5CAD76E690AB42B5B4A30ECEDB630C630A86B2B35D22425763BA388D38DFE51BCC0FBFB4F31A3BAB615EF7ACA67FDDB3F27ADD03BCB71970E05CBABEEE597D7DDD13E5758F7A6D035F25BB5CDCAEF81BF2F5177901537F76BFC106AB06E5CD7C5121ACD44F2AECE7D16174C1E5F4BAE0D24B1700A373F87815970BD553B036C57557E5ABCA185665AC146357A933A4C99C57257EF3F8731FF97E30772C6C25938F699619AF995E283FDA8B5EA6DE21C6956B14A3C15123D4333970047E02421A4D011FA1EAB18344B4F5D60EF4486D8ABD0331DBAD8436660BC19459DE02319608BA3D2F911F6C6C48354D3E3A248EBCB6742122138D10C715D659239A5F90ABC32C6257E45142B89EA75162802ECBE7AD7F882569BC0E3FCAA5FD426BBD60FF9ABF13AEC2A56F977E0B693AD6B7AF2E271016429BDDFD04FE59E50480A6187857E501DE992B3206868A608430905E65EA985CA30A1AC55597EA14D2905E03FB6A0FD4C6883658658FDC5663DBF3B175357207827D457D72C077138FFB0B6C68C61AF40536A9F040E11DA4C9E5D7AD06951FB282EC3CE70DB079C8573139B4797602968E0C8591EDE2A6E669B71EF69BA4245F73AD05F08F1A20CF173929F47B96FFFAEA66BB2790E80AFFE94891E5FCE22F2C28FEEDD55F6D40F1ED5F3FFDE52F9BD7B3EFFEFA6F7F3D7DFDED272F8074CC5DCD3DC0C1951E6DAACF312DF75EE7767EF6EDB7315CB3D73545F1285C0B7E894BC3E9731CCE7B088F521DDDA3EE6BA90E013A683469CEAB411AEF6110DE750F2DE84F95F7B4BE496DB9DBF6EE0C13065B95495AADA87386057B97C806E9AC96C841EBAE362D4C87B154469BE41C34A8F1C57EF57394F1DE8E8E512AFB11FFFE33DAEEF145656E915E1EFEF9D67633748D3ED7F102159068649B5595DF7E49A56C825E7EB52426B79FBD1EDB8EEB920B30CA0D0FC3681093516E142FD048158DE324EB7C8792ED08C7FD39C6E5E0D58C12F393370B87AAE51FC9E388FAB9BA8BAED8E16BD4D66D9B87D9D2E393E43D3C213D480337D7E5BDD56F55FEE5EB5EF00C6A5ADDDB8A39F4A95725A1FA625014525156B873BC4D3EE3FC392AB1B3073A123A72789DEC1051C23739F9ABC2E1F1E977C747CB35A2943D9E85947857AC0822B6835531CEE4D4F6D748D347279E31A691BE6D634C276D6D134C2B151AC1A9A54BBE6D947B3FB50849D23E8398EE7E785BDF3F547175DB6610586ABE4B531D97183ECFD19685CE7594C6D7F96E80F9AEEE5D2393C69D0D3B2F31566496C93F835F5AFFE71E355D164285FA31E4BD10C69E7AAA896DD82AAED28228BA75A518C654979A600ADD90AE33DD326A02D09A6226B5FE9472BA6AD2467587A977509D02EADF61D35BE95DA8A77D233B16921235ACDD68DC0969E71E32580D9CD55920B6688A86A72A39CEFC731D1A1442A0F4752E1A6C2E227D1C693EBAB60C6031C6541259C79BADD42114AF72A82A55B4A38E6B87A905EB420935FB5C466313F8DC41CA47E0D05BF570745EBEE219E135CCE02BD051F6CC2FD09A7445A0538DE226F927422D061D5FA849C4A813F48D0F31D741339B850F98D9ECEB60F9C30C9641A3788B12DAA1FBE15BF4032ABA3554E8B8FD1993690CE5090AD601BF24E5C38A8CCA325BA27D6FD1F892ABD7FAB1D7A5AEDAA65EA184EA1B4AE5ABC6F9C3689C61776D089D5DB2DF395662DA6C7A7C247D1195E65571B9DD9738BDC8B1AC0CFCD7ECED8E75D87ABDA6F2754C8E3F5CEC5FC62E46B8F32F563239224486BC9EC40234421163E3EA50AC77D27BFF75B8E8814043091C0054281B7E00694B8E030B5ACBA460B820824AD709DAFE1DA3A86EED39C2932382E3C607181281B1261A5A1F3BD9783A641E71A9573DC18FF4BA24CE2B95584EEC97788BD7CCC5E9C8FB380483D505ED98C3B0A539F9082499DC475D554809D7D3D97743843C6864308D3EFE805141BA9F3E10AD230286C7D911484E8E04811F1F6D0C90184B1F0B558FE3940DAA34B68F36FBC566F98063C68162E94D0E4E96191F648AE5C782255BEF3898FC887FDB2739DEB08E0D7D27D69656EFB266C88392F7394A3711A33257F426472ECB4C407CE6AEFC58C865EBFD8A5C3D723F108EA3048464E91D0A722B660290DB951F19B955BD5F91AB47EE47B4FE35227029B943C12DE52500B66DF191514BAB9D74E7A93D01A441C72222A3A27728D0A89809C046577E647084DEFFB3E43BC66D5EB6A7A00BBD50BA2AF2389FC9F95A6F8D64138F4D360587CDBF3AFEEA7F635CB30B3C071087DE41ECFD33BC040CBC58A7008EE36EA8E3802FC592A86A1DE4E4AA257E08F8ADBE7882B72B3B2272AB9F93DA12F5C67C13FE7C007430F4270708C38B977B47BEF8583061AA9D1429AB45ABA57EC028CCA180214C8350D1E4B81119F20BCC24D318ED16DC82F79C1966A18A2D8972CED813E5AEECD53184D824AF18323D89F3A7C72C2F15F4EB44BB1A34E1543ACFEDCAD7DB7C9E5B7964B1F192B459A5D789A6FC41BEE478E2232880837888CDB31336F8A5F766A30D7D5A7384484A1CBD48437F1CBBDD26349E9EC219B1D4934E79CC33022FF7E6CEF1A7601AEE9389BBF66A55D1B0EA4B8E0367A7EEECD4D77EC75ABAD5F5DC5ABB5F15175B745FB4C019CC9D795B635C1D46704FFA62FB4CEA60A5CE8BF803A6537D0BBA1CDD915EABEE67BC3D7E2DE181CBBCC23B320F22EADCB82970AA2F704ECF049BACF2156E2EEB0D262B84B45A6C3405FE2C0BB3169B4694F5ED3F41940DD207BB9B790872ACCEAA0B5BB9DCE0EC916A173BD19C65BB47943EAB0423672F7A217E676024CFFE275EF7D94FFFA2CFFF1EA5BF36BDDF95111F745B0285BF3D3C2264E48AA7060FD1DD5BAA9DEDC0F37EFFCC64360FEB224BE9016BC0A86EA7A2D184C45638B570CE38B09B14EED3FA01A5F70E433B2D92A2C4E9DA7A782F126A69AA06B8953C271A7A8737F088C9672D5A6A63DA8AB5BE281F36E2DABDF3C8E2810E380E4114DD31979D349638FF5C3D5F830562D7C9C4E4498AA2BA3F3E1CFCFB5AA6EEE27ABFD2B6877F7ADC548B483BC45FA505CE95B6A33CEEB698A11DA0C3DAD8C5E3A9AFBAC6A965F91E6D51BAC6CB87CA87A4A5D599677749B9C88AC256AAF3A45827A48529BBE6F0D1675290097B7919031F5414878E7AE02EA19A9B779B0D336D18D67775917A706CECD778B4D0876C93DC25F633545D6A99DD953C87DFD81613B9348C7876B382AFF15BFB82629D86C54ACDECD93ECFD965EDBF79C017DAE98EA6716CB75C0F65A6E68C1B0338AF1F715A6D1A5A9AC3DBAC60B30799C2B0FA532E751D81C01E498C0E85BEF2A9C13047C9D67A3BEA4396960F4C762BA3ED5D5164EBA46A5A3FF5737B91CCEF669F93E7E13CDD1C3581344C25FB6DCFFE184A28747CF48174259D15D744BBBC3DFE17A9CD2EF575A744607DEDB62D5FE3EB57AFE48EFE88EF30D57309DAD2452591719296F2114A9292091D6D5DF9130881A7315261F94086CAB8E3414C9963A22CE8E989ABB02232D7F1209C23997AF7CD090353237AE980A602BF65B6D30A1D68C1020AACB679AD5063510B80506E17D0341A7C81A96DB3A5C85B1AE148D4F64D146E46801EE5DD0E73524E086C34DD116532DD49E0A56C9E8D2469B9404029BB21ACFE9120D48D033D7EFA6C2AF078A82981B0023D03C2466E95ADCC222822B8F5E10C8C049C06EDB7F5926C5D9D0B6B2104155081C95A63585401C0AA4D1FCE1833716323E6B65C049CE9FA3E9C95111057ED1CE04D7D02D669DBB454224E5500421C97D70573CA4ADC26C278B0333164AB5D1A460391679241146E46005F73667FDBB5A73AF0578102CC0DC1AEBB0AE03469C2E401C039A2DA0B6FDAB65A29969A4020D0B47D12CCC608086B86881DC0A0CC10BE60C91BE005129F085DBA86DA48B52E1F882D5D8784323186EE6A66EE6EEAA79FD5B63E9C1DD45E2E3694B102FDD699DD569D9F06D3367844334DDF312FC2486B2FCC7C2423A56B86126A606E08696C4617EB0CAE6092E58096951141A6EDF31781B1F9C26ED34BC807E16ABE70D45D22CDA0ED2EB736D7064AA2D1DC6CA658ADED0942A306365F839A395F14A7BA16D2F4588DAB684D36EF088DB1197BF345E0E8171A6D57E7E994439DB02B9E588A329C45C4C36C6A3CCC26C0C3CC0D0FB329F16058197539626142BDFEB15AF838346E6939AF2DCDF3DAD2B5A122CDB1E6B5A5CDBCB634CC6B3EAD1D775E5B1A71BBD4E1D6A781A3E276B9D04A8F24C76A1625359986E69B62A32C97A10A9A6F72489523A8E7E56259A27483F20DE3CC9BF0AFC1065C00460B90D76531A8AC0CC09325343D51A4E763245CE97BFEC09156C5EA6ABD60B56F0D54828732430863F3B9000BA40F295E98B48C2C976DE3A6BAABF433E9D92C7F263D5BE699F8FE9ED9DD551400B78FDDFB42491EE80F89E7981DB34C76C916E5B61881B3837A88CBE9A480E03A46464AE59B4D3D5DF3D922A3A2210A29DCDAE55C349BA47643A799799A0CA0842D7881898D3D9308F58E3373083D77E033C5FC3945BB647D4354F4EDD943B2DD10BAEAD52390195C48F6F95C5794500D006C3435C4DA6ED0B4D56A1FA02F1FBA09A1E9131B4E6E106DFC4120ECA7822CAEDEA59BCB3CDB3F6A766A95451CD1762A76D09BEBB47E567344E3C9D0B07F67A858A38DEC9AE484B0E3C11F80D59F2EE737238014EEA8E9A00A774C147E46406DC5F52DC3891AAD725608A555AE91F00970342E2ED55D6223FFAA742012D55D10C8C168D8ABC68F0174551E6BB419E65E96240497427E9B13152E5C63C6C509D7E8C307081DB915CF4A7874392070D80D7C8898252CA2AA352F44492CDB0895E60F8493D4EF61F58E81A58A59FDA297C903E209808041D7B0143D97BBDED0901B331638E4461F3C3C2EC9EA59737A5127833390EBE945430A40C34A22156BDEE1B8B7D2FBA1DB037C2B43AA1C41F82BFDD1D54A7974254BCC20FC95F2E84ADE208A24FC95F369D52A54F82BE7D32A5595E3BCDC155F4713A97411025492D41733BC38875F805BBE3987AA83B409DB8A31DE9A6BF8B28200532EDE1B738D6CE23035C6CE12E74D3C49EF6F9B0B1846901A4B82FB4C6221179C9A6B848FEDB9E60C86566BEE6CB021140DDD52B2155534D646792F553B39F9053DDF4A0DD4BC3DD016835F5075259C5F5169EB82F6E4DDC787E7932A9B5EB081034329F87D954D6F45E1698C8B2AE5F316BB005355003E3E7CDEBA1E0B29E94F0843539B6D845DD1083E70D4F74D201F63589A9D73BB96B3DBFACC4A6D64AA4A80F6A594D9C9C05456054DD85DE6C3DB123336C46A02ED885CCDC34C5193046370330276AF73EA5DA9C4BBEBFA8A58F75B895D650908BB426617E0AAEB0180DBB37D70B835B6C30629813AD628331B1EBA4293295AB119CDBDA3065BB640E24A0D0C5ABE2EF55530B8A6C3032FD89E09000CCAD0860FAEE0F440AE47A219BA753E2D58BD60DAD05569D4030624CFF99810E46561AF39A7079BE996AC947300C099AFC5CE66870C3B81FF51812748C541DF1D00F896C93FD5EFA3F86C03C0AE22ABC65CC5DC01A38E657F54C8B1E270C01B2D362DE66EEBA8617AC43599947873C65A4B5035A1364C1D26D204E6C7C2992004EB09B52E36A1DF1DEA369CB05F31A373B8C3E5833DEDD45946BA5E2972646BFD4573C80376C808705334DC6A5BBB293A9D03B146B156CF334C861C9459F3D6C7F9450C48DFFEA1D3F4FA4ED78060B3CADEF398464C2FC1B8E3F8AF41648598E6DF81F058FF6B7B2BEFC0B0C8316F8381AA404C1C72C209E460943B5EFA20BA2AC85847D4656F85F94696B08E3E0E55065F2A9A1EB7968DB28110148839E89E9A956CE371161DE7E77530A42C2D49893E241C0D1E33FF443FE3A712885CF353819BE0356D4C6611915540285CAAEEC891A5C97917045A7DBF4D02BA922C158081683D980C241BB774D48F8C4CAC77EF67A0C239E091E9703E70CC6D6C234428DAD787F6B0E04AC58E55611513BDD77E0311DE2DAD4C48F0056B9255E7F2071055EB7BC74043C98A250FAC1F44880FDEE5A481D87C0191A0EEA60C059760C1A54D41C07F074C0B749B62225FBF9700C8D526924938B5BD0011E82C7E0309D6A906448777D6612226789A00094ADE284C7DC4B973003B4B70156120583F8893C9D4CF4F4C852FE73760E1EA2D9409BFEC335000C8EC036403A9E6E5B34CA479B16728BE0207C4CA3C202EC18297E682EC4D5FB06EEE3AB59DD2D7D193EEBC9A6423DF52032424DF0933EAE0EE1A1EAC86997B954665F1BC0571D3DC84B3EBB3268EB4A6CB9A1C6682EDAD19788661EF30D9526A2FF19828F617B08CE64ABD5D045B2BED6E9F8148B3B128536876E76C8AABEC2FE638C596CCB57212922E4CD8CD1F6A82C2AD083B72B39986D44C8E65A920438F2E3484EA93229B3EABF7A7415ADC5980D1F8308F20318F055195CD58A718095C90950831C7D1F6EF188118E53218C9FD0D3F575126214A6D9A91C8078C0AD278AA287F4A13503B0B59CC822C1FB0CE0460D36D01F63E27069B066155BA2DB10FD90683F60E9B6E4BEC2382AD4226D97A14A92C4C7E8BCF8E980AAB4CB20D29B586B7D6EBCB2A28FBBB7BC5E4CF245B9842E6A5B7BC09644D54A5F8C54D2C8120B31FA15CCF03215C8F9872F00ADF14F795DB2FB38FFCDA7500B4EB206DD4B8D4D0EEC9C135743213F7D6F80EB4EB5C38C228DCA716D148C586EAE391F2ED63F634F4BDA70F3FCA10E5DB14DC5D72544CA09F0CA133B9B6A883670A8DD077893A56E6B07DD1C940D9114C0E3DFB7D4655179891A188F868EA4ACFC683210A15DD600E672835441BD0D052AC7644817E62773C833B4B195D0FE82CBB487C5CBB8CB1F89876097B8D9ADE3206DF1B6E64C101E180CEB2881CC7B5491F3B8E157FBF89AAE9227DAC38DB5EF7E820309E19D03FE6B867FC41AE2EF219D31C0BF468E39C0DD8338A785C10762C2277F1D2D6C7EEB2521E3604F5A60F685C7974141C4C0AE8278BA8535CABF471A798460987049A8ED2479A1A4A6B8BA19080BED1464BE29AA08A97C4305F9D73687A41151E69383DCC0546829BAF083021322E8798706BB81C51C24659FB35B90A9204B7568E9F2432CA4550726B23173069D8C1DF86FE51B672666CE5CCBF95B3F15AA99E1A15D16E4476D593A0455BD5D35EB4F94E8C6B03B4541BFA86635915FC86617C6968B52AD6CD704A6A695052EA283822E35A2565D1F0D194D452076C45381C915935B02D5A3A0EB02B46C0064A7EC5440659CF626E4D631D890DAB9E94015DC036DB047F11DA6208FFC2F50B7CE141DB5586802FFA6EF7E82F302C09D057E6F0255C43B4014C9846087726345DA30D59C28E1505319F05A12A5209B428B48A6AC2AFE64C714D2C5A654710E826E07E49F8D88303984023CF22D4093F30F4C14ED86121DE71D10D377D78936151D51E27A9B104B98005052EF881F5C58DE0FC95EDD486D77088B4514E205080115078818931508C3CC2C50753A760EC0EC85036C6F8E0CD5E5D940FD67CE66E45E9EC685D500F4B8281DD23049E3074922E4C85B2658A4015A11DA6882FC1906DAEB605F71710F200E827536004AE219AD0084C035AA1687A4413D160B09E68EE1BAABA00F0CF0FB0CC7BE8F76934EF909F6D6D11E700B5F7260FB455E16A9EE35376366F9289A2F8908D64DC9C43CD547941E73905FCA09B7855121872E66BBC7643D805FC79F3A0E33D7AB388352CBD0407DE4CC95594E96EA55C4F427EAA39D656CAF524C01A5472B039DCE065597F2FC4E89759BC7460E399D9E9BE864B051030F8CBC5E153BED1233034F3BBB911E6676A6B47C2AC1D005C59D65903D6AE83E5AB28313BD7E0B4163CDCB577732B1CCB5A39BAE536E7D8FBDADA135F2BBFB64EE2F25A9B285CAC826B151B77ACC2E2C3E090959B75EAFBE9DAB58CC1FFEAD0DDA576110A69483B7FA2BCEE327A1465071778EF5DA7198D4E4421EA71B69AD51E2A81AEB37467C935CEECD092699C7C1B5ED36D66179610E5213A4DB8506FEE388D6B456D1361E78AB13A1076A7286FE12868877464E3A94FD77590333FB839823B3F2B146829A9F014B70B349B940627730AE6355B93CE1D61DE90AC1E8844EC8EFA2988AE2F649F670AE639AF6721BDC0F93993BBA079DC12A7135A175BAA2E005D70C97C8B4EB88C185693500D83FE294E84EB5DBCDB27F05E97C633146FCC297C4371178ABA9764DA8B5CB04BA721F401E88948BD33AFF158046DACC33E8B00249B77B0B55E8A063DAF603DE2987A06F09BA36E05EF3927BC57785F39436CE598FCB380DB210E2E5D84DD0E3BA72EDC068AFDA3115B372E2079F71723D4510B25D4B901E9D2DE9C2CD70F78879A0F6F4E4896357E2CF7685B3F086B133EA0C747BA84E94B365F8E968F684D15C7BF2E8F8F9E76DBB4787BFC50968FDF9F9C1415E9E2D52E59E75991DD95AFD6D9EE046DB293D9EBD77F3D393D3DD9D5344ED69C6E7B2370DBD5546639BAC7422A5DC96EF0459217E51C95E8132A885CCE363B299BE0F484EFBCAEA7DBCA947E4D64A9B60FA3DAA2F46FC6CFCA2BCAD6ABA65AC0098A40B0EFDA0BD25A9AAF6A38D66D47C9340895E51AD5C7ACD53A4BD520EA87E62CDBEE77A926830865357D7ED1CB9316D31CA8A264FB0C926413ECE92D9F0B32EE2E922788A694E8DEFA79E50C046AFB1C701362E694FE0D31597FB7A776FEF498E5A5D8E0FEAB035F789DA51BA9EBBAAFF6946A94714E47589240B23DED0F28259A4C874938877D0DEF89BAE9F75459CA7C8A43CF36B630D7AFE00B6F1D15312E1D4BCD14B34E4DF55D59A2F583DC56F6BB03B51DBF23CD5114D264AA6F4E0435292AE61349330BF3A5A8F35D67847A9E8F371F40DB4B6EB3014CC1622E68A37128660255B00E35EDE54224B7042378AA29CC17A72289E6930B8D994C63E648A3573DA702292EC58BE24C49517233A1A7F8292945529F64B7043A1A6739DE8844DA6F0E549A272DF5F815A809692EF316EB648B9FBBD4EEB76CF814E1C17E77D0B6395AFF4AC6A83CEFF329EE14A1394B4C73D0B891ACBF89742FF3262A50EBAA1E7F59A85B75D171B0D63DF74DCA2D860936499E34671AA24E5AA91D9BB5831468D442AE5374146BC4CFF13AD9A12D4F5248721869B45D20493EC58FE24C4D12ECCB894696E1558DD3D8629D5FBA0F2F6D69B515DE1512079990E430A7AEB38DB888AB3FB9AC0ED25F21861CADEE9A7D682A10921C38CB51BA7E10386BBE397346E100CC246C9283E5F8803E21705DCE2638F0B8D9100556082BAAFEAB3DA59BAC28A9FF5D1116EC77079D8BF20DD44CF6BB136F9208DA6FF6547EC19F968968C2741FEDE95C3FE2143487B8047B7AD5EEDE475CE0FC33BEC17992092D85D2EDA977AF326A0A48DE8480734C656DC6B786DDF61CA65B79F74E85C257DD9D5765AF15B7BAF498BA922908988372EAC1C852E545C8D9D0F0B430DC4C8BF0299C96903771FBAFEE946630292703BDF5B104B54F0DC609211365EC77CECCFDA0E336EAFDBAD8440D18EC42D2C188CDE4B0C74974BCFB7877F919CAABBA9D2B264A524A7458CE4A1AC1551B5CA05DB27D168CA1E69B83F1F890A5A24D5B7F7230716A07529285D37F76E007EC689F1E0E59184EB5C9A5F1F0E0B6C7D54448F0D8E252951C43F831D6DD747DF111DF2734508A6CCDCBA9F694D972D0EA0D4A77B0ECD7599AED129204D196531D8667520A6AA2FEE23028F3EC7392AE453DD17D75D81FCB7E1724527F996A255EE9BA0F2845F7628FF3294EE7CEB488ACDFB904470E4F01DE9CCE9DAA1233808A93D15895F806A0F28DEBAE0CB829E3B827B320232D95FB99F9ECB8CBD04C09EF49BA04763083CB64FD24CED44F4E6DCDEE33A199D597E1F677269A02E3188BDE56A2AB7918CF5C8931F7FD58CD3D9076E4531CE60EFAB0E78ECC3B2506372CE56407032DC9CB07799F90F9EC32BEA8137D5919B0DF5DCF9AAEEF7A8712F271139B3AD5ECB5C44F7BB495A677E6B3A3346EB648547CEC772FDC0034E5542FCA4B5C3F0F53906E93C79F999A22339090D38C1B6B57FECB99DF269A977837AF81D313B757EF3E49E98BDB1C1D8813969836D9F1C6DD1D5E97C9672C4F0B429283CA4539E938F9C084FDEE75814B797FEB80603B075D423881752EF987B280285448D9A5D27DC1B9F37DC1DA693F4F030AE9A2A722CFCBED37C71B20C0CD0F671AD0F511A7B923CE6D62392C14DF3FFAA05115DD69A00F7B437182BEEC1ACD02FA5021A58882AFCAAE240A2B470A4B00F6CB3F3CECDB9FD29657F3D5F9A0B61920E7E97E071ED572E9AEAA4FBC9DDC7E74A523DE496E3F1ED0A0065DBC068F732800AECFD0B7A2A31E89407159412832B98C788004A40494D902EB0294852EDF104F0FA6C2AFCABD9A1B5EA1E7BE36F884CBA93529C92E7568FBD14523932200BEFACF8EB42A5C9C02D4DA041F7A33153DA7D9877AB713BBACFD36EEA96CACCD8C9BFAADB7B4C7D97FFE82C6E74DFB8C3F74031A765560B303AD2A39867C9A42C0FE249BE04C0FD0F77C8A1F45F13C8B4F72A749042A9B6752A203DAC9B74FD244D77E74B08BD1D3F913DE3D0A2F2998CFCE6D7D8F72F9BC414C73B8469B23C080E8BF3AEC00FDB64F3EA32D6E63EB725B40429A9324F04D9E889B9ECCE783D140BC83FE4035C412F3D045FAE24A3030A544AD24A639003759CBDB0FDDC7C3119FE4E63F548486F80636623492508A52282989134877B82A91A41D01E1AE0497E272F9E24945914B71B0A2EA38CC9C0D0585669E1072620086D0C50547CE67956120A054D25C39C95094521D6E6ED1D36FE1B256FDC9E15D4B2EB9BC683E1D0C1054DED89DC40FF939B7103A5C6CD855D2459EE074B3AD024E0AA78F5C8ADBEA4DA6D67F7539752A8ADFB35C34CABBAF0EB655F1AE3AF712346FF7D59E52E54B5FECF6EEE3E1C0B8F2961F0A6342C407C66031255C486E09C6CDB7B1B70C5C85ABDC08EEE34B48C7727C925B0B3F26F70F6521B7B2FD7E30F03384F5703B3BED69F91CA2EA4A8F293F5925BAAAC318C743B5B768699FA3FB3ACC5CAE54CBEB2CBD41A5F08EBAFF7A30785606F071427245C503C38A72436B317BC44E24153800899348563EA7D350A1E1CE962F250A97CEE7DBF2BED2EA0F733A3D95CA0807E7A50F38A142C341AB3978BF5C28CFE4DB2417C0CB70BDFC0AD72E7D185DCA45D009D5AA9A404236FA555B5C29AC415C36B2252148C9C907235229744FA0540DD18C2C046BA46050332AF102C9CE2A4C236438C7C1C8D92AA08FDB924BA4E8B3F032D3502EBFC4A2D2220CCAE02DF1A86862C242C9CF79B924A7092407DD5771090EE78329E83989F9ECC2DBF3565A24771F0F668C7061BD02470743CBEB71B8A6F498A8620A023A4F4E3D18593671C6428F682815AFFB5F60B958E3434B07BAF3C77C3F1809F1D77163191EEDF5786FBB4349C0305134E514F30493EA7BA1596D73F0190E47C06C68B950E97631ED7C24AB29ACECFBAECCD55CE8742EC54195E698FAB0792FBA88E83F3BD3921F6F71092E937DD359F2769E90E44AB3952A44B44F735B274BDACDC3131FC014F3F9F0C6101BFC31DA58EA5E59848C293511F3D86A13D4638CCDE1A039A38FDEF6A70C1B3EC5E1AE04F569453DD84814F91487BD78FCFBCF68BBC71759BE43255103C2C67C93EC7480B3DDE848B6C9073462FA106CA1167C43C8CB7DB9AAA8F2904BBCEFE57AEF5FB63CBA8F8E8FD7653C329F1D1E9A238854FF75DC3BFAE73B94089EC79B4F2EBD9C635C8A9D5C7F7398BA252F12AE4EA2624C81FF481E790AD5878319C4CA588C4E23188CF06A317C15E50E7BECB63A47F60FD07F773CB8AFCF1F44824292234DC05D75FFD9C53BC036F98CF36720081897E24EF1EC81C6058469B6690E971768F8A255568A610FD8EFE36AC2B62D903613D33C7A4FD26E7C8A0F9F4A113B6BBDB6A0A4FDB884C3D2825102602923ABDB6A43B7A05787A1116BC621EDE5F5562BE6BB2F1AD95941AF4DB2A7F99F7B54498B27D77F75B83E982625F07A87F9EC70264A951B408CFDEEA047D3A2CC49EFD481E2F997116CCAE18DDDEB382FE7057A21E35845E1258C6620841E97E03C12AFC1B7FF52E214A311183C87F6868EEBA7586F79BD116E283F0CBE658748AEEE90E455B2EB0AF902AD4957088BF6E69BCBFB879BE49F085D033EB78424179AF419EE06A6C9251D1AA667B358789ECDFCB10C95FD7271ECEBF34DD923F4112CF02ED685C60FA868D51B4788FDEE321E7EC6F7B8447982A4E1C0A6D853FC25291F56D90E95D912EDC5F9424A7498B72B3B54BC91D87E3CB4D14A198B355E292DFF110B97FE72C7AC8B95A2A68177C97E079262535C561F95B0C0F5079BE232722FB7FB12A71739965E08B229073332987DBAF0A1C110F35D7AA84A0F3334EC813D99C3D0C81760240F91EE72329350CEDC0BFD351828DDC12E5818AFC228B21C90B829C10842AE34BC8F68E182EA2EA7F96531B65F5D8447CB802263120E465017A4CFD37582B67FC728FC088AA3E621364379F5412E534C14A294682F4B5A42D6ACFDD7E94CFDEA96B37C1AC47C76BAFA0CB8C54E9D6F55C57328B0C45BBC966E5FF45F0F66F4FC0D3F57174282074E4BC863CCA88BAABA9794E07BB6FA602F9EE60E0C4BE1D0AEC57CC0A820D322AD906EE707CB47A0E721262305556F0B0545F50624DB4B52280CCC5A708EB01A66E62A0EC937F0350DF57213C90B124BCC6769A12DAE3C4C604A49E709429AC311055312400E90EC725BF0B77D92E38DFCEA954F71A7D83DB403A9F6A90703BFA60BDFE728DDC4DAF3A988F96FFA288A1BCE9EAA528AA3A72ECDF944AB2A09C00F48FE0A3F7FF87D20ED0D773AC812F3879FA2B80128552905FCBA3467F85525D5F06393BFC2CF1F7E1F5104F7E60C2D7FF0C1A50D28A18514D06B939C91470BAA81C7A41E9A2CE3F8AA6789051C5FB879AE674BA92E34B9FBB18F79E3CAD5B7FEB45088B249C8D0F20782DB762153482135F78D43A6A07A586BB710BF4E2736B88BF4ACACED113FC8393E216B7F0260EBBEBB3FEE8261C6261D8CDCEABDBFCA4372F82BF79E96CF5B775D69FD86665548D2CD7C92EB2669551010A49C7A30B25C2D5AC5F00346311EBE8804BDDCD79948A8C42096945DDBC9E90E07FB0BDEAF8B8AB6BB9312912FF90800CEE1530370714348F3A17AFEF498E562EC0A29F500611FE58D034F2E08F26EAF1DF8726AB8BBDF51E6CB42FE19E5745FEA3AB8F7E92ED40125DC7D74A113EA1BF38C585E628493F69BCB81E5279148F3C95FC5F8A9C6A187EBBBA2C8D649F5021DB8C670FB1EA5BFB6772D8E4DB71584DCE290635281E1BF916E1270E46E97D93E5F434B1CC53D04F936C32D1C0097F65057B93B5F2BFA4A1032A540BE98A222834292139B440604E144864757C58FFBEDF6EDF11DDA16D2150CB007DE9C80187082C959B67B44691517DE841126ABEC088B26490B0358081D9D434246CF941B2C9A96CB9EBCBACF2F0A0E37387BA48ADF8485369FB4442466B1FCA803EEF29AC62181A0E1C80D017593A5156EF7F5E5C87FE9346B48B9C3668D65D0AC010552BC8563293AC242E4EBE5CC1A8A1E880113DB59631969D658FACF1A8322E325CD1A03C2C16AD658469835969EB3C6A0207821B3C650F2674318DEB6577CECC330F6455CA22D02928088BA0A457BA0757BA33ED872440DC8AB1BA443A24A8683C9D825C1B06A6B9082355A1DAA01C52C030B03D252D17444976BF048A9BE08C05336C50D7CEE27BFE190F3E89EF0B98D0BB268A9DD5485DCE24242730D48F850759C825BC7893B3006E64BD274CDFD123BFDD666D6DD0F3975D06D35BDA86A202296042EDD10AFBE5A72ABBC60A2E6CF2CEE76E3968A8C0810E762966E67B8F9D2FD2EDA0F54AA640AAF2F2FF6E596EB07BC4355B38A47B4AE560C1B5CF97DA420F9840A5C67393E22BC7F4E3638272D7D2E4ABCAB51B4FC6D7BB64DAAAB0D6D860F64FD72878B7295FD8AD3B7C7B3D7A7B3E3A377DB0415D561ECDDF1D1D36E9B16DFAF2B0775284DB3B26AFADBE387B27CFCFEE4A4A86A2C5EED92759E15D95DF96A9DED4ED0263B21B4BE39393D3DC19BDD8958BC216B45E5F55F5B2A45B1E12E5032874F8DB0C98A5A7722FAE66F58825B2BE68FF84E45041AF522A53702EC402294D3B7C749DA4E079798E083BA82BEA16E5BF394BE9FC58D0B1AAA92D0A72DEED4D289B63EF148B5AEEA53725FD5E64A0C25DBE728946AF85D244F71F86A1A591F73D5A436E4EF32A1EFDABC58A37FB7A4D2CF285F3FA0FCBFEDD0D37F67E995B9FC4C4824579FDCF64D0C22B6C46B323B4522D69C83B34FF62250259A638FB65161F79E68B0FEF6558C6E6C627331E3CE91255A920D1FC3501275DAF75744624F6F8FFF5755F4FBA3ABFF71CB97FED351F5C4FEFBA3D747FFDBB525EFCA12AD1FA275CCBB1D17BDA92179B7CD90A98BD8734B5B552C9FD43B2B62F858DE490DB7240654C2F50E8B1B4668991064CC17A7EE955685C26A9DF9D43A0BACB5D761A751C601437016896075DBC07E3CC924DA5B0F4134F6798ED3F5733DCEC3689D3F91DE48EFF14766D29528D9F44DCB943B70FA9221E859E568FD2BB1B34DD6830BAD28F39EC956B4E82180847D5759EBF4560E3ECA9C95BEAB16572027B6FA6EAB69AE3F4550071CC5381AA61D8ED573ECA0815D63778ED7C90E6D838C24DA3A814E501B59823EBD660D68E521B51DA6B587B866580BC5875C1DAEAB6B87FE22A6ACBA6BA5BA5490DD5B774F9CE5458ED2F5431CFBB966AB7D2EE3ACABFBC2219DB37C409F10DF3561ADDA6C88BA2A8A28C46EB2A244DB3306779E623B43F9268AFC09478CB4821AF70BFEB44CCA3873C4F5234E2DAC12AB26569B811F7181F3CFF806E749B6099B22BA43969A2662770FC22CD3A98C40C89AAD4C76D7A51EB7B5E043A1BBC7E5BA88AA8A05E98DF62E80EB4AD571B1EAB257D06A44CF7D02561B7BEC11289479EC6998A9C9D7C674326D7C6D1A5F6366E0EEA35544DBAF6D89C5B1CA29353F6BA02F39C0C06A89FB02C1774829BA6308408C3298AA607F8463E0A69A5D6772047C7A54223060B7461B631768976C9FE398937508A5186BF2FE9AA5A301D2160C997B41015A54DE960BA95BBDDEB4D4535DE121F696A0EBC0965B4BAA8BB3163B4B2018065D7E7B9FC996F823BE4F8A32E7CC70EFB5024B2CE222F27C9DA5D92E59A36D44A2755CB2186A84DE7B48D77134C92AFB3DCE596AB4B572A5253FA014DD47EAF88656B419A16230CED94D452A8EF55691FA26DA1E4AAC2D9405199B69BCC998EE0D341AFC3DDA22661478A2ED023D05525864F7D9C16DC87C5D987793B2BFC1EB6FE98E62E2C698917FACE411678F93FC9DDC9149B3C471B6BA93BC7C88B3A388A843D4684AA86EDDF5DDD943B2258A32E584ECBE9F1B67D65CE2A73DDAC63231AABEBFD9A2484606038D41882E71DEBDE43BA0B9AFA115677E8F393B68A651BF1DF1039A45BFCE7CDDCCC79D1378CC7F6C799F59502C3FC2B587C98E64EEEE70156C21CA84758372D209DACB4A761642D07D596B9CCD173EE8821D009830258DEDE856551DBF26604E9F2FCE026F2AC4BBBC13F1D24E8CD34221EA937517594371E90545F855B1098A92B28F0DC595C76CB20A9EC42218A35F287EDB9F21439B1F01E7E97E17A66A68D9D37012B341C622E9F27483F20D3B0F790D4F8090DF8855101A70108375864E1020D178A36EF83700F62092DD845B82067E18690192B6E090A0A075B02008D46C59E3723ECEAE744F2F8EE6FDA9F0B90E5E970A3AA78C76DE1A7357E0A67F530C2A801736406FA0C7FF96FBABEA07E9C60D56B013630FD3A69618875A5C34A678288A384A5B8A8452A859B24455EE105B6B859ECE9FF0EEB10C34D8AA4844A457C2457895A3340212CE7FDB279FD11637C1E8C27A1937D1A5433609ACBC92B8A91A0B92036822B5B7253B75A4F73964D64962F9211553B26617D031C6FD2DCCB9CDB51E91C010B2159D21F9C8D7C2DB8E51C6FAB1115BCE1F92B4AB316C947F404F9128D50118ACB5B2BDA5AF71266469F21B9CE958D8FE12850185FB910EA838CF2F72C6CF42807972D83A802E087C60D12E3F5CC1002C5B6243E0224F70BAD93E4367D3C74764CC2E707A5F3EBC3DFEF3B7AF9D8953FE07217C838AE2F72C979E2171843DE8F601ABFD4DBECB3CDB3FBAE3B72936046A2FE7375EA825E5BC50DB941B10B543ACE72714AEDD36E9738A76C9FA8698781E87CB6CE1203668277E4CEE1F4A078F30F6E7783D9B5E077A7C17399FEC297B78AAABFF1157EB28F772BED3960BD9050B360EAED6594A9486CFAB636BE85543D407749D4A70851BA44B267984123262575EC79D505C1B738749A791B17BEBD26353F332F0B873F5F5B8D31573975E98BBF4C29C0489815EB35E2E42B73C2FBF02C91548F4CFCE7D9B8F1A7BE6FDDA392BB467A55FBCE8373998AA067FE8D9603AA46B0512012FD247EA60A1B6C1FB98F71999A4F75E06B348C4CB6C86888CD7D3DEAE63E2F8943CA3A448A37F415E8F6199C24197A64A94C7F1EF739EC6F113B32C9FB71E0BD5A6D8204F63BBBEF67B1ECBC9D97590A86112FD22725FD3E04AA89296DFDD98061DEE77636458C5BF1B43EA186B926C6FA2FACF910D85802992A130BCDE6E2A1BBE7FF79BA4245F73AFE1DE97BE9A7BF42B577AC03E3D6FA31618B7D967DF7EEBFCA4A0EBD221A853AB3EEC0A22253004676739A6D2792FBD648B44D8E3858607EADBBAC3D0DFA6858D0296CA901A4635EC6C8C42A66CD00679E563867A83180099EDCF21685F6F373FA3ED3ECEEAFF47FCBB2F31073FBA54A87E27B25E372F06BF912B4DC11E8F2AA39D3D2C504462F12EC29EEF5012C73FEEB2CC312EA3908AE659859B148328FD23791C72F0D5673AA38FBC76317A7883AFD5463EEF3FDB9241F34E55B4DE2A75E7812B1CCE469437A873BC4D3EE3FC392AB1B3071AF7AA23D7BADBBEC9C95F451592E9F43B02AF35A29467EE279525DE152BD2CFDBC1AA88A74ADB3E89A808BB6E8EA5107B1E6329C696E2180AD23766CB97AA24EB4E9118B5555081AA29F46E619CEA97C93F3DF645B9C2416CFCE71E35E2F75F7AD38703DCC5FFE85AAED2A1C35671951604B36BDAE13EEFAADC35C1B5F7FBD32F5B1F7021A59C7542553AC6C0BC861EE3DA0FCDB6F8E48333E6A8717D843715C4073B338FB31B126B197D81D6A48D4157E98A9BE49F085D732E597CAE395F15F48DDDC695902BA066B3AF601A084CB1635956AFCFA250FA011535AE829C655C153FE37B5CA23C414148FF25291F56640E28B325DAB3EE3E3D8E2C2AF36D506B833114BF8E9C8146CEB07629A1B34BF63BEF4AEC9C2E57C1B207ADE3AAB8DCEE4B9C5EE458356842ADEAE666E09781734F783A38490BBF7220D2F073A0A6BD76E0BAB1B718F95E41E39ECDABF36849BF2E6B4B8674142D3B78F75C24294AD709DAFE1D23AF03028E804F674904061CB3B40A76DC7A0926B21D56DD058CB2697E9EFADC55806682F067954BBCC56B22B301961A0450F561B5075A49267784568534A7F7A7B3EF9CFB67F0E3F60F181544BFEE5ABF211E9D2590F019DC00890187B7505BBCD71110E14163B45E5307E437FE9E0DD8F23E7213CB0F2834B6AA7812FB887FDB2739DEF08F6CDCF5584BA77B1C31D8D6098DD4BA09F08A55950F708DD5951F50D66C557F64597F209C7AB9A461CB07C8BA2B3FBCACABAAFEC8B2FE88FCFC5132C50324DD161F5ED0B4A6C1972BED7699AF934FB67C40AF8EE1F273E2E36FA889560F82AA620E55BBCADE7739CF140F903CB0B01F48F021CBFF97AE37AB1A3D855CDF5CF79370577658F1563F877FED552DC16BEF6B1E5DC914F77AF9C5171FB043999A06EFD3D5A205FE0F18795EA91569F8F9D690690C79A6B3E05F0D87EC9F89AC47D9B2EA89B2C74F61EC9D3F3D66B983C3550F10F95E38E429840188BFCF33307C686531FC5E7004232128A2330DD10B89232F676482ED5DFDFA86F2F9144AC2A064AC828B881406340C2BD3F334C03464BD599E584C90B4BEDBD0A5C4082E34EDEDFDDB10AB3FA2EDFFAE28B27552D5D6B0F76EBD6687FC2DF31B9C89CFD3CDD1C76CCB976C72368D2376C3DD2B20F5C37E5B268FDB644D187B7BFCFAD5AB53A9EF40EA953257D0AED378CAFF229125D8C2D47F5B42233CD2EBAE880840066292AE9347B455B64F280182572A0582FEA4AB4C4C99E3479CD2F940D10F1179E8AA120698A9B7DE9C303032A28B9E4953D9B131DFC5AB0A9CD8DB02A2C8FBEF4E4062AAE52872DF870250C7B3A5D8DAFC81A061DB16A5E611A04279B6C308CD2489F2C5A0A262D6462834E3C83850573912003A45A197BEA41EFA8FCE28302229A2DCAD55419B3982FC23D43792F01BA4768140810DC95E746D264E7CFDC717A20C3A866D84C486561D5129E8AB1D011B952709B25CAE42B9775384B4F916244F03423816389A42CA60AAC3558D37F903B1C2B72E4ADD2300E62CDB3DA2F4F9B6E33E7B94DC73319AA4CECD2B92F6DB0B4249CBB3D598AEF38E8E0E6DBD2320A3D12176C000E4080BF0A061E1209D3AEBE8A0D0553B86B668E6B7CEEEA09FD5F6674CC363FA4D8E290C109F2D8DC98D90F3A7F5034AEF318D057F0BF7DA4020616BE6E8F1095F0C3CB866BD086CCC17761B17F305273FFAF385AC53E60B2B49CC01DFF283AE4D48CFABEA1C47EEB5419368E68B50A13B98A72F4CD82E06EBE4829E2F0AF1082C9E8CA7B703C692B9CFDC3F5F9C4E2C79F1F1FD57C98F25F9D9949237AC0F43656FBDBE7C61927658FA4DABD6979676DB9217F4F2E5D86D4B3B912FC7B6DB48CFABEA1C47EE66BB2D54E8E3DB6D6309DBC56E9B5CD0061D1E2AE5B175F8583276D0E1938B78B9186E144F6F9F8D25711FFB6C52B92F4B946E50BE61FC06DC8A9D1511096085024930C78B4605DCA603C746E5814BB8C5A93EE569D23915DE7E7342085B2D478E4F18E694076AA54232BADBB8F6C8E01AE550ADF632F02867800D1F57E9674230CB9F09C132CFC467C38C5CC58C9C6CA54437FBC11E7D9190A26FB6427662A150EBC2013662D592DCA69B82925DB245F9F88A86AF989F8384A42F44D908CD7A89EAA6BAC45FDC2EB37DBED6DC39B007C9A9D80D6FAED339DEE2121FBDABBC3993951A2AD668233F513821151A38681E7D007CB4295F08B4348F5C5E16B25634A2835AFDD44FFB395D517FF903A00A706BA090ADE2D5D1C088D2BE781A6B36AB791870016507C097B640B287D684E797CF29DA25EB1B62B1DF9E3D24DB0DA1AB3ED6E833F3E71BEC7727C9332529255BAAB18E3D981A6CE4C4E40FC486D86EABC905D1061E04527E2A705EBC4B379779B67FD45C7070C7CB4093CC4F97F31B8E85FAC31786AAAA5151AA1C014F15766E194ED438AAB272E26BBE7C71D8A9DB6523C22AE75878D154361A522A9D130A11C37444EBE0455DC8CF935FA6A80BCB57CAD38A9A22F256E6D555445F8026B01618CD38961E50D735063648E5CD8A360C1D07B51A1953CEF62B9249057DB9B85D690E6E2FF9D5E6A5EB6A73C5975F49E5632978BB75E665E83A731556CF08025DE94FE28D02319EBF1AB61F2209D4B2A357E127AB41F58CE3BC837723724BFBBF7350A814349B89133997E07B0DC3C7D74C2C68289BAF121E5320DEF50C076F346606C6D853E05CDC25E9FD6D7355D00825311FE011C61350124FFCD6859C3AA48718274C096542B7A8A49646E3619487DE840BC2F42FE8F9566A89E609675F4CB89EC97C7F817062F9B71123937F121819EB1FE5D8E3798B5DC0531510160ED5971708989A731B515539270189A6E6316CA0FD2629CFD3327F6E39BBAD77E8D5735657829FAE98CFA36C63C89C2BF8E9938799E3FA865B4D2D5DF6AB7998C124373046FD23604E0894DDC7775662AECFC18A98F93A0AE2C400DF202FD743DE8A55F49442D481EA4C1BCF5C512517E8FB20C075A309582D5DDF00042BA4FC4180A6E9B5C3011BC7E4F480ABF5985E87C9A21C1B52761A34A6BE1A153E0E5A6A7AC058DEA89DCD208D44BF7EC1D0E95B7970F099FA5E640F2020FAB77C2591E6812E2456DFBF7C04C97D744018AA03C54F89A35B30B6B620BB268F24BDF6FB7828324FA031A72EA86F06448FFDE4553336A14B39EAD99F80A76246E74BAECE27B8096B3F7E81B0E91A77509869B99AFA2A7F7D073BE243A2116EF32B2FF37F912F445E8401C4A1090AE97888AF42C6C6D1746F42422B1DE52E071714C6109D8DBDE4A139A6971347019910150CE668403B5BDF790AF19B63D939C14F17192D8883E8583CAFE216D1A7BDA404CEBB93C20DBE48F2A29CA3127D4285BCAAA3A596B8545D5D393E3AEF0222A96F8D2CD70F7887DE1E6F3E650427757425295B011CEB296BAF71A5ABBBCE61A8996632D7DB3B98952AEC93A09A9A54EA0EC95407E7B34AAA864B856AE2FC6D59F4631FF407EAC33E55D17F6D06BB76291AA46E891D5905FF7D928ABC1DE7821371A91E211DAA8CCB6281B3D68D950CB336054459E773CB5481B22DBA46349E9E4CB479AFC9520D7C32540F9BC3DC14EA1550AA847E8448CF1766824B88E0524170694510745B03D401E683AB05B25A70529B7B72CDF577B0A6FAA6B81150EDF24846549B0242AA4E34D3E73DBC4895F0C9504D6C0E8BEA24A72372955216B05A2197858C040713B2B0840CA0D4B83CE64AEBFBFD5255F567A882FA1991916CF5B043265B7D06C992148B21CF3E7C94C73E9B0A2A01F6B19CA9AEE6FD93544BF31DA2DF3CE734515E417A66A5D0332B0B3D730911BC5410BCB420C8DF279679E59241AE991CD6D688A6462987C62AB1AF17B80C26834ACE03424BBEE6669EEB993B91C07CCFA4C2737E97C146FF5717E900FD5F7D87F53F49B2965DF1232AF7B9C698EC326824D7E4B1A894B9F22557C8248295B5E936F61274994B53639F495B739BCF86837ED31A587AB449F0D2A34E3557D16C4B4BF49BEF10F166FBDD8AB262A5C6A4296BB05B9D493753D4755D6B6C2B2193B5EDA3AC5548D7D8418E35D21378556D344D53134DB6ABA43EA6555553A76A2AA2192C45D71EE6C1626B5395226B3318D70B465D256781D712AEFA6ABE50AC54DB04B81ABB55EA4592A2749DA0EDDF318246B1900E55C56531D7F837FCFC33DAEEA106F549503D6DAAB98A0F1815A46BE994FA539A4026819403AA50C86481C7F2016B4C703E19442493C37A3CBFCFC99A4E3DD69A64CD60AB725857F721DB606865C3276BAAAB725857F711816B4F2E555319CD605D976AA1CB27EBD496D3B25731ACB9544D6576035C6379E8ED0D072B8350C3EBF2DD3D6C7E73A9A0B1D867B059FF187790E52CF0DAC875FF583C96D054ACB64CF83C6ED03C3560F3D40C4EF9F10BB3E1AFDC2ABF0536E59972F0EE3950483CA7D0BE24ED7A024C95CE3C405AACA0444A759A7862C577875D57B5DBD97CD80AB887E0BCDAC6889BED6D43FAEF9ACE000E06AAF2DCF7E04E90A3C403AD37849297D896F83D908676E253B692C9A16758126BFFD1D05463E778360E8CF6AD68A6393238C7B6780E56B1DE7F9C5CB6CA80D640F3ED825F8736822B0F1E2855148494E08E80033503BD6011D19947007F425503A0FD76104D072311032D37472CE66FD4C89CC32C4F257138D82E24728BB0BC9146FDB8F3381C4916E8018B90B3913A003A13AD4AF309C14D1703A5028DD6C652E598EE4F562B56E9CFC9353B1711146E9D22F25440D31C949D5F93AAD897706BE4B098010D19771CB6A11D950D939ED6BC9C86A9E7134558C380C6594F391E4D1123F4010DD206F1E3185DF2CD5A1E82C2581A14863A545D40D38655184B1DFC1411D9021A3330FCAA1B34603B246767018D185741286367810DB589B315D074EDF5A386009823B81BC03051401798C349F180E4F7E56B44B6DF341D01DD21AA0AF309E12B0F55FC2368F561152B896F86E26E52DD14295137B2ED3BD26710C0D17BA0216011E7270A02E0BB55F5181092A2A1800F4BA38180267E4D94C683FBCF6C69C5B67270D39BB829E6A643015678F9B1C73AB5D8EA2F07D1EC2EC007047130F8478856B7EB0A1FEB168A650119BAC69817BCCD2B5FDAAB8D5FF6BBA6C1528C0B3B1A811D2084683074832EA043D4CE602F565605EB0FC14D07A209004D36C51CE058E52E5056BC365F266B5E737755D52EC0437E78830AE11CACFE10DCA0DED13BD01C85177877C646910DEB971C6A8CCA6D79607386D3A38DFF6D086780676E1E62FC5470A99F0A567CEE9594DB83F99572610679A17663C76DD6F33BC8D5F955D61F781B3D31F38D05AE48D7CDE612EC56A63EE7E53EF39BD15D3034CDB9F918169B085DEC6E1BC8A7E94C00D545ED7AEE9453239C9F69FDDF820769F61E73853D23E91E78B36FC47C3FA8CE517A7605ED5F1B2FB08266662EAB379AF9597E7F367127A8FD97426AC6CED9293F78A44BEEF5B8613EEBB48BF2E2BA40A54F0EEE12B57B4DA04B2C7D71728D92AE93576D61BE6A3A4471499CA7701D6B074CEF0CD2A23BB47E10E5552ED02821E5F0BAA6715BA8EB0CC8B3A1CCBACCB05D73ED5014D444CDE697C11B1F24E5FEDE3F2B61F94AFF64CDAD5F05E8DA0A394503366D98B707DC964DF57DFAC6B6BECB544D057D9BC98CF20F1F7A56E1170D3EC0F7BA48C2BBD8026F9068BC70F1C68DF07AA9B66CBA8F9334107409A5DEA4D4B88E8ABE3DABDCA68CDDEC86A8A1D1A0539D88DBB24336D7E468075CE13AF8E61196BE9AA59C9CA85DD32B8F2BC524EB2EA24E7C2895CEF74B97F6E6A4BE6DDE7C203FCB2C47F7B87EE0517D7D73F291AE4376B8FE35C74572DF93784368A6B8F221D4136DF35CA577596B5E0A1CB559DAE4EED94F8936A844EFF232B943EB9224AF715154167BF572E8EDF13931E13757E9F5BE7CDC97A4C978F769CB19AFD4758EAEFE372712CF6F9AF777319A40D84C4813F075FA7E9F6C371DDF17685B0873978A04F5C97389C9F75A9625F93FBE7FEE28FD98A596849AEEEB5C09ADF0EE91DE3A2CAED325FA8C7D78FBA9C00B7C8FD674DDF039A9D0AC22621604DFED6FE609BACFD1AE6868F4E5C94F82E1CDEEE9DFFF3F35FA62EBDBB80300 , N'6.2.0-61023')

INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201802150423331_SeedOnly', N'Saina.Data.Migrations.Configuration',  0x1F8B0800000000000400ED7D5B731C37B2E6FB46EC7F60F069F7C41C516C8FE7781CD23921B1499A312D9347DDB677E68501758364ADBBABE8AA6A999C8DFD65FBB03F69FFC20275C525714755356585236476014824901F12895BE6FFFB3FFFF7CD7F3CEDB6479F715E2459FAF6F8F4D5EBE3239CAEB34D92DEBF3DDE9777FFFADDF17FFCFB7FFD2F6FCE37BBA7A39FDB7CDFD07CA4645ABC3D7E28CBC7EF4F4E8AF503DEA1E2D52E59E75991DD95AFD6D9EE046DB293D9EBD77F3D393D3DC184C431A17574F4E6E33E2D931DAE7E909F6759BAC68FE51E6D3F641BBC2D9AEF246559513DFA11ED70F188D6F8EDF11225297A3547257A458A95F8A93C3E7AB74D10E16489B777C747284DB3129584CFEF7F2AF0B2CCB3F47EF9483EA0EDEAF911937C77685BE086FFEFFBECB64D793DA34D39E90BB6A4D6FBA2CC768E044FBF69FAE6442CEED5C3C75DDF91DE3B27BD5C3ED356573DF8F6F8DD7A3DCFD6FB1D4ECB1F30DAE0FCF848ACF5FBB36D4E4BB43DFDEEF1719BACABA4B32CC7AF2A9A092E5E115A1915637AFFAAA5C97C926AFAD351454F20F7A70E53047AF4BF3F1D9DEDB7E53EC76F53BC2F73B4FDD3D1CDFE1329F237FCBCCA7EC5E9DB74BFDDB2AD24ED2469DC07F2E926CF1E715E3E7FC477AAB65F6D8E8F4E78422722A58E8E8E48DD5D5769F9CDECF8E847C21EFAB4C51DCC98AE5D96A4C99738C5392AF1E6069525CE094AAE36B81294C48E50795BF38FFBDD272ABAAEDEBFFC19A8D7400B25DBE7188496CF45897717C95314AE9A1692E18D5B4AF4EF1551169E9CD1BF5B524419106C1E1F7D404F0B9CDE970F6F8FC99FC747847BBC69BF34B47F4A13A20649A132DF1BAB3A7F7ACCF2B26FFD60152DF13A4B37235454A37B8E8B759E3CD6DA68E01A3FA0944C003131FE1E15B8A537BC64C818DF175D35642A7A557F3A4FF73B67D62995967549C5E88BBE2B4BB47E18A5CDEF7654DD8B5D3CCF88C2B618AE3FA2CFC97DA518D54AF68A8C60D2A91FF1B6CA583C248F4DE70A996E81A9ED22CF761FB3AD4C51CE7CBB42F93DA64DC86C4B2CB37DBE7668152B52538B5AA6B8226073809C1D67405BA0EC6DD3D986BC39E96D075B8B82F6D318F604ADE790AC09CA4FA02DD19218CB92582E1C95CA7C71EA5C62E65AA29F6C4E07575D4C65B3112AFB9438AB4781C6598E37E144F6794E9677CFB5E20E2476FEB47E40E93DFEC81A6B0D293B361C01B2CAD1FA572221D83AB42B1BC3AE302F023C66BA76D21A7EAAD34C0F86C9D1B659AD7CC1D6B489B7ED1FD5E29E6D029C439AA215D95CE765A2DA403EE78BDBF9A23865196B3E499CB4DFDDAB9E69AA9EC955CF1455CF9CAB5E2EC09A978BDBE5821346FD45AAB7F90C556B6D3BF43019C868682B98DC5A60159EAB99A0549603DA076D9DABA4DC0EBF7AE66A1B7E366EE72D5228740AAC6793395E273BB43589C73041D1C60B9406EB02B6B2013A5CA975DEA3F4D7667CC2AB3A2643A7D62977AC4652E591749432A3ABB21466AA98939A38191BE63E5B8E59DB0CE698CD71DB6BE29E613083D4C770AEA06981115BC0CCF01E15C9FA2ABDCBF25D95FA8AFD9BA962F2B981E1C5677A108A8FB6174D877F98C6A39CBBEE6DD50D8DB23998A374FD30FC1659CD31ADC5B1ADCB07F409F12D1D8ECBCD868CDCA218BCA29BAC28D1F68CC78E87F4CE50BE898102C20F2397C19AFD0BFEB44CCAE12DA9EB479C2A17C88E5D7391E445F9111738FF8C6F709E649B4063E92AFD4C744B963FD744117BAA10B89FE138B4A09D8BF71999CF50EA7ECAC4EDFB5B53D16D09B40A43B519D0A6DFF2E614B70700E68176B9E18CAE16122DAC34E8947C4A89A00917C6196F45C6363945F3CD689BDAB25D314AE6659C3D6E61BEBB1C7991A55D1FF10705AA3C523F2B330659721C9687B1E4982A26B7E484B9DEE33C40692A0C68C931D50EB4DCF75F0E06293B609F53AF15BD572B432E532647756BA6FB2C4CC6C431AD6F94CB1E6D45C3EF54D19A6C560E61FB2D0ED33334DD99C7928943EDD4DC0D5789B53E05341B98E4E0ED882167B096FE410C74DFB94B89D48107FC54B3967A3459E1151A47009CBDF0CA9972438196AB6472E472DCF8C057223016864799B02ED02ED93E0FBFB7F390A5231C1665BB4794BAAEF81592352B18D5D6AAEB0CACB8F315B48813B58871B567BD66AEFB183EE9A8D36E85052A73D00165900FEFC15CAE8BFB56C9017C36BDA064134A97B80433859D79B75D3B8C5E6EC84FAE919951EA7CDEAD18E0231E6678DDA72FF1477C9F14A41BB98D4DDFED5796D848A700E74459643B0298ED48159E55B2197A5ACAB3CF49BA1E7E665A65BF0F7F4B3ED6014A35597F4029BA1F41CE4D3DA3183B55C386BF3F5A5533FC0E4055CD37A39CFF8D71FCB720EA2C1DC7E6A5A7578DF1F61E6D11A300FC06CC05652B84C022BBCF5EEE4163C04185A7C528DD8CD1DA955EC6D8B06BE403591CFBAF8AC75F0E4730C47EAC188972C780FC9DDC51A1E128174E92BC7C8873328FCA879166D3BAE1D777670FC9964C11A9090AE3182F4BFCB447DB318CC74A68375B3482F9C8C06DF40A97384FF0F0D3FF58664653CFF0E6D958F77A34D68CE7CD9903356702CC0CAF0D1FD1C8D0EE0A79D918FC5DA3811E3AB0954C6E70B0CCF8981D62F9B18C0FDFCB647777785D269F7194B9FD06E58455E8499DEB86D4904FF7FD5E7E795E3917C7A8FE62BAD7209D2F622E02DAC3F057F3C5E48371BEF0198273F97DF09056FFA23E5F6F7ABB7E01477F78B94E982F0403D3F731CE38AF708637523C6FA84ADDDAC29AACB008520571F1897682F3BFB6325F282FAD084992F610D3FDCEC612C5AB1A429DC9C071D57D8758EA135DF9A9DFA8BABD5F0518E0DEB53AD4EDFC8015AE7BE65CB7C612242465F3AFFB087110C3D05B0E33872CA79F43965E7308E06362C03964E5EAD162294E131E6BA0AFD30474DA56FDE4A687F6A3D77C0ECD2C0C6D9F8907B43F4EE31920B4E8CC9B9CFFB4B8544F8B4BC3B428A6C79D16978A6971A99B169721D3A2667A5842D3C352393D2CB5D3838E87CA93828B9305A06AD6F7827DBD642CA71B946F9845A08A1520EB2D9DCA58E6E03C804F08454667E71465B6565CB1AC9244069B6F32436D82B3AF2ED83BC64A12DC0AF68EB18AE01D03ECCD814C0CA0A603B03A00AEFC0C1105A1B16C139081F0452A48761C93C4EC3FCCDDF14C802292F5A64163F90D48AA4D02062099CC7698F059EBB0E9C717E5C26B3CB505471B3FB44276BC0C6803939AAA2134FC0DA2BEAEE16DFB9F0AB393B2496E738F7522487E6EF66B5757AA016AAEA91036629AC4DBD6CE610EA9B814F93E329F1CCBE997CEA69274AB686CD9564E3108564F1380AE603E4BFDC0A60519594D87C6D0EA0DA9C9F53A0375E70B3F8A5132A06E6FAA8C70A1B526348EFDC35636C225D0A6B66E0781DF5AE1D35C8D4854E50EDDF659A1A7F327BC7B2C430935ED798FF2F0AB60573912CD6C0F2AE7BFED93CF684BE0CC3A8CF334D9099D9B3C71BFBDA1D4AA573B748FB5930D9BE3B6D377BD9E85D225850B66729D7E3A07303470479E6DB573A39C599E25C53CF2750E5546E72D9564976C51AEED683E0FD4D5700E79DB02CE167117C5DA005175A8302F7BCDBD2CA6624CC02CBDC9676196199FA9582C3FDA7C9CACB9D3DDE1AF8C740888A5BB44CC6A159C1F7225DD1401BD02CDE9112C30E4856280C65848FE90A45DF58177A908E463917A77CF44CAB03392EC37A8B87923CA4E1547717248F2EC78ED5D4914C682E3474A65F0A5C275BEE1DE29D8D8A85E9AD9DBD891F613F43691D740A8B71AE29D94507A9383BFDDC373853CB8F73720D02FF204A79BEDB3E17DCA9FBF7D6D857747FD4A5B3B4DCD37A8287ECF729D33D1612ABE2ADE55F78F4397DE9779B67FF4DEF09C3FA76897AC6F88AD05AF397EBA9CDFDCD62393D9E46B3FCA5B7C5D8AEBBAA76A07C842955211E5D63CCC67890D362DE202CC62CF535494D07EA89F76243D1B553B127AD36B47C28497766CCA8DA51DEDB4B1CF4075BDA5D48FD75052B44D1F93FB87920F7F7783F35D5214D5AD056F1B846113BEEBD9A7D7A3F45DBAA9FA87BF7EABCC25075451678DAD88789529EA233655A196B82CAEDCA9CF63F48A5AD24B920AF77B82C10A3A9E7262C84EAEA38421E7FC28C365C49A35D68FD5F9AEE931F5086F8C473AB241B97B2C4D9FE594689DADC9C843E5009106D47731BBC7DA0685D9678455659B2E5F998732395FD1AC24128F499D26975AE2A225D93965882948D7BB8AD9CA4BC536F3513CE55A119C5CAD76E690AB42B5B4A30ECEDB630C630A86B2B35D22425763BA388D38DFE51BCC0FBFB4F31A3BAB615EF7ACA67FDDB3F27ADD03BCB71970E05CBABEEE597D7DDD13E5758F7A6D035F25BB5CDCAEF81BF2F5177901537F76BFC106AB06E5CD7C5121ACD44F2AECE7D16174C1E5F4BAE0D24B1700A373F87815970BD553B036C57557E5ABCA185665AC146357A933A4C99C57257EF3F8731FF97E30772C6C25938F699619AF995E283FDA8B5EA6DE21C6956B14A3C15123D4333970047E02421A4D011FA1EAB18344B4F5D60EF4486D8ABD0331DBAD8436660BC19459DE02319608BA3D2F911F6C6C48354D3E3A248EBCB6742122138D10C715D659239A5F90ABC32C6257E45142B89EA75162802ECBE7AD7F882569BC0E3FCAA5FD426BBD60FF9ABF13AEC2A56F977E0B693AD6B7AF2E271016429BDDFD04FE59E50480A6187857E501DE992B3206868A608430905E65EA985CA30A1AC55597EA14D2905E03FB6A0FD4C6883658658FDC5663DBF3B175357207827D457D72C077138FFB0B6C68C61AF40536A9F040E11DA4C9E5D7AD06951FB282EC3CE70DB079C8573139B4797602968E0C8591EDE2A6E669B71EF69BA4245F73AD05F08F1A20CF173929F47B96FFFAEA66BB2790E80AFFE94891E5FCE22F2C28FEEDD55F6D40F1ED5F3FFDE52F9BD7B3EFFEFA6F7F3D7DFDED272F8074CC5DCD3DC0C1951E6DAACF312DF75EE7767EF6EDB7315CB3D73545F1285C0B7E894BC3E9731CCE7B088F521DDDA3EE6BA90E013A683469CEAB411AEF6110DE750F2DE84F95F7B4BE496DB9DBF6EE0C13065B95495AADA87386057B97C806E9AC96C841EBAE362D4C87B154469BE41C34A8F1C57EF57394F1DE8E8E512AFB11FFFE33DAEEF145656E915E1EFEF9D67633748D3ED7F102159068649B5595DF7E49A56C825E7EB52426B79FBD1EDB8EEB920B30CA0D0FC3681093516E142FD048158DE324EB7C8792ED08C7FD39C6E5E0D58C12F393370B87AAE51FC9E388FAB9BA8BAED8E16BD4D66D9B87D9D2E393E43D3C213D480337D7E5BDD56F55FEE5EB5EF00C6A5ADDDB8A39F4A95725A1FA625014525156B873BC4D3EE3FC392AB1B3073A123A72789DEC1051C23739F9ABC2E1F1E977C747CB35A2943D9E85947857AC0822B6835531CEE4D4F6D748D347279E31A691BE6D634C276D6D134C2B151AC1A9A54BBE6D947B3FB50849D23E8398EE7E785BDF3F547175DB6610586ABE4B531D97183ECFD19685CE7594C6D7F96E80F9AEEE5D2393C69D0D3B2F31566496C93F835F5AFFE71E355D164285FA31E4BD10C69E7AAA896DD82AAED28228BA75A518C654979A600ADD90AE33DD326A02D09A6226B5FE9472BA6AD2467587A977509D02EADF61D35BE95DA8A77D233B16921235ACDD68DC0969E71E32580D9CD55920B6688A86A72A39CEFC731D1A1442A0F4752E1A6C2E227D1C693EBAB60C6031C6541259C79BADD42114AF72A82A55B4A38E6B87A905EB420935FB5C466313F8DC41CA47E0D05BF570745EBEE219E135CCE02BD051F6CC2FD09A7445A0538DE226F927422D061D5FA849C4A813F48D0F31D741339B850F98D9ECEB60F9C30C9641A3788B12DAA1FBE15BF4032ABA3554E8B8FD1993690CE5090AD601BF24E5C38A8CCA325BA27D6FD1F892ABD7FAB1D7A5AEDAA65EA184EA1B4AE5ABC6F9C3689C61776D089D5DB2DF395662DA6C7A7C247D1195E65571B9DD9738BDC8B1AC0CFCD7ECED8E75D87ABDA6F2754C8E3F5CEC5FC62E46B8F32F563239224486BC9EC40234421163E3EA50AC77D27BFF75B8E8814043091C0054281B7E00694B8E030B5ACBA460B820824AD709DAFE1DA3A86EED39C2932382E3C607181281B1261A5A1F3BD9783A641E71A9573DC18FF4BA24CE2B95584EEC97788BD7CCC5E9C8FB380483D505ED98C3B0A539F9082499DC475D554809D7D3D97743843C6864308D3EFE805141BA9F3E10AD230286C7D911484E8E04811F1F6D0C90184B1F0B558FE3940DAA34B68F36FBC566F98063C68162E94D0E4E96191F648AE5C782255BEF3898FC887FDB2739DEB08E0D7D27D69656EFB266C88392F7394A3711A33257F426472ECB4C407CE6AEFC58C865EBFD8A5C3D723F108EA3048464E91D0A722B660290DB951F19B955BD5F91AB47EE47B4FE35227029B943C12DE52500B66DF191514BAB9D74E7A93D01A441C72222A3A27728D0A89809C046577E647084DEFFB3E43BC66D5EB6A7A00BBD50BA2AF2389FC9F95A6F8D64138F4D360587CDBF3AFEEA7F635CB30B3C071087DE41ECFD33BC040CBC58A7008EE36EA8E3802FC592A86A1DE4E4AA257E08F8ADBE7882B72B3B2272AB9F93DA12F5C67C13FE7C007430F4270708C38B977B47BEF8583061AA9D1429AB45ABA57EC028CCA180214C8350D1E4B81119F20BCC24D318ED16DC82F79C1966A18A2D8972CED813E5AEECD53184D824AF18323D89F3A7C72C2F15F4EB44BB1A34E1543ACFEDCAD7DB7C9E5B7964B1F192B459A5D789A6FC41BEE478E2232880837888CDB31336F8A5F766A30D7D5A7384484A1CBD48437F1CBBDD26349E9EC219B1D4934E79CC33022FF7E6CEF1A7601AEE9389BBF66A55D1B0EA4B8E0367A7EEECD4D77EC75ABAD5F5DC5ABB5F15175B745FB4C019CC9D795B635C1D46704FFA62FB4CEA60A5CE8BF803A6537D0BBA1CDD915EABEE67BC3D7E2DE181CBBCC23B320F22EADCB82970AA2F704ECF049BACF2156E2EEB0D262B84B45A6C3405FE2C0BB3169B4694F5ED3F41940DD207BB9B790872ACCEAA0B5BB9DCE0EC916A173BD19C65BB47943EAB0423672F7A217E676024CFFE275EF7D94FFFA2CFFF1EA5BF36BDDF95111F745B0285BF3D3C2264E48AA7060FD1DD5BAA9DEDC0F37EFFCC64360FEB224BE9016BC0A86EA7A2D184C45638B570CE38B09B14EED3FA01A5F70E433B2D92A2C4E9DA7A782F126A69AA06B8953C271A7A8737F088C9672D5A6A63DA8AB5BE281F36E2DABDF3C8E2810E380E4114DD31979D349638FF5C3D5F830562D7C9C4E4498AA2BA3F3E1CFCFB5AA6EEE27ABFD2B6877F7ADC548B483BC45FA505CE95B6A33CEEB698A11DA0C3DAD8C5E3A9AFBAC6A965F91E6D51BAC6CB87CA87A4A5D599677749B9C88AC256AAF3A45827A48529BBE6F0D1675290097B7919031F5414878E7AE02EA19A9B779B0D336D18D67775917A706CECD778B4D0876C93DC25F633545D6A99DD953C87DFD81613B9348C7876B382AFF15BFB82629D86C54ACDECD93ECFD965EDBF79C017DAE98EA6716CB75C0F65A6E68C1B0338AF1F715A6D1A5A9AC3DBAC60B30799C2B0FA532E751D81C01E498C0E85BEF2A9C13047C9D67A3BEA4396960F4C762BA3ED5D5164EBA46A5A3FF5737B91CCEF669F93E7E13CDD1C3581344C25FB6DCFFE184A28747CF48174259D15D744BBBC3DFE17A9CD2EF575A744607DEDB62D5FE3EB57AFE48EFE88EF30D57309DAD2452591719296F2114A9292091D6D5DF9130881A7315261F94086CAB8E3414C9963A22CE8E989ABB02232D7F1209C23997AF7CD090353237AE980A602BF65B6D30A1D68C1020AACB679AD5063510B80506E17D0341A7C81A96DB3A5C85B1AE148D4F64D146E46801EE5DD0E73524E086C34DD116532DD49E0A56C9E8D2469B9404029BB21ACFE9120D48D033D7EFA6C2AF078A82981B0023D03C2466E95ADCC222822B8F5E10C8C049C06EDB7F5926C5D9D0B6B2104155081C95A63585401C0AA4D1FCE1833716323E6B65C049CE9FA3E9C95111057ED1CE04D7D02D669DBB454224E5500421C97D70573CA4ADC26C278B0333164AB5D1A460391679241146E46005F73667FDBB5A73AF0578102CC0DC1AEBB0AE03469C2E401C039A2DA0B6FDAB65A29969A4020D0B47D12CCC608086B86881DC0A0CC10BE60C91BE005129F085DBA86DA48B52E1F882D5D8784323186EE6A66EE6EEAA79FD5B63E9C1DD45E2E3694B102FDD699DD569D9F06D3367844334DDF312FC2486B2FCC7C2423A56B86126A606E08696C4617EB0CAE6092E58096951141A6EDF31781B1F9C26ED34BC807E16ABE70D45D22CDA0ED2EB736D7064AA2D1DC6CA658ADED0942A306365F839A395F14A7BA16D2F4588DAB684D36EF088DB1197BF345E0E8171A6D57E7E994439DB02B9E588A329C45C4C36C6A3CCC26C0C3CC0D0FB329F16058197539626142BDFEB15AF838346E6939AF2DCDF3DAD2B5A122CDB1E6B5A5CDBCB634CC6B3EAD1D775E5B1A71BBD4E1D6A781A3E276B9D04A8F24C76A1625359986E69B62A32C97A10A9A6F72489523A8E7E56259A27483F20DE3CC9BF0AFC1065C00460B90D76531A8AC0CC09325343D51A4E763245CE97BFEC09156C5EA6ABD60B56F0D54828732430863F3B9000BA40F295E98B48C2C976DE3A6BAABF433E9D92C7F263D5BE699F8FE9ED9DD551400B78FDDFB42491EE80F89E7981DB34C76C916E5B61881B3837A88CBE9A480E03A46464AE59B4D3D5DF3D922A3A2210A29DCDAE55C349BA47643A799799A0CA0842D7881898D3D9308F58E3373083D77E033C5FC3945BB647D4354F4EDD943B2DD10BAEAD52390195C48F6F95C5794500D006C3435C4DA6ED0B4D56A1FA02F1FBA09A1E9131B4E6E106DFC4120ECA7822CAEDEA59BCB3CDB3F6A766A95451CD1762A76D09BEBB47E567344E3C9D0B07F67A858A38DEC9AE484B0E3C11F80D59F2EE737238014EEA8E9A00A774C147E46406DC5F52DC3891AAD725608A555AE91F00970342E2ED55D6223FFAA742012D55D10C8C168D8ABC68F0174551E6BB419E65E96240497427E9B13152E5C63C6C509D7E8C307081DB915CF4A7874392070D80D7C8898252CA2AA352F44492CDB0895E60F8493D4EF61F58E81A58A59FDA297C903E209808041D7B0143D97BBDED0901B331638E4461F3C3C2EC9EA59737A5127833390EBE945430A40C34A22156BDEE1B8B7D2FBA1DB037C2B43AA1C41F82BFDD1D54A7974254BCC20FC95F2E84ADE208A24FC95F369D52A54F82BE7D32A5595E3BCDC155F4713A97411025492D41733BC38875F805BBE3987AA83B409DB8A31DE9A6BF8B28200532EDE1B738D6CE23035C6CE12E74D3C49EF6F9B0B1846901A4B82FB4C6221179C9A6B848FEDB9E60C86566BEE6CB021140DDD52B2155534D646792F553B39F9053DDF4A0DD4BC3DD016835F5075259C5F5169EB82F6E4DDC787E7932A9B5EB081034329F87D954D6F45E1698C8B2AE5F316BB005355003E3E7CDEBA1E0B29E94F0843539B6D845DD1083E70D4F74D201F63589A9D73BB96B3DBFACC4A6D64AA4A80F6A594D9C9C05456054DD85DE6C3DB123336C46A02ED885CCDC34C5193046370330276AF73EA5DA9C4BBEBFA8A58F75B895D650908BB426617E0AAEB0180DBB37D70B835B6C30629813AD628331B1EBA4293295AB119CDBDA3065BB640E24A0D0C5ABE2EF55530B8A6C3032FD89E09000CCAD0860FAEE0F440AE47A219BA753E2D58BD60DAD05569D4030624CFF99810E46561AF39A7079BE996AC947300C099AFC5CE66870C3B81FF51812748C541DF1D00F896C93FD5EFA3F86C03C0AE22ABC65CC5DC01A38E657F54C8B1E270C01B2D362DE66EEBA8617AC43599947873C65A4B5035A1364C1D26D204E6C7C2992004EB09B52E36A1DF1DEA369CB05F31A373B8C3E5833DEDD45946BA5E2972646BFD4573C80376C808705334DC6A5BBB293A9D03B146B156CF334C861C9459F3D6C7F9450C48DFFEA1D3F4FA4ED78060B3CADEF398464C2FC1B8E3F8AF41648598E6DF81F058FF6B7B2BEFC0B0C8316F8381AA404C1C72C209E460943B5EFA20BA2AC85847D4656F85F94696B08E3E0E55065F2A9A1EB7968DB28110148839E89E9A956CE371161DE7E77530A42C2D49893E241C0D1E33FF443FE3A712885CF353819BE0356D4C6611915540285CAAEEC891A5C97917045A7DBF4D02BA922C158081683D980C241BB774D48F8C4CAC77EF67A0C239E091E9703E70CC6D6C234428DAD787F6B0E04AC58E55611513BDD77E0311DE2DAD4C48F0056B9255E7F2071055EB7BC74043C98A250FAC1F44880FDEE5A481D87C0191A0EEA60C059760C1A54D41C07F074C0B749B62225FBF9700C8D526924938B5BD0011E82C7E0309D6A906448777D6612226789A00094ADE284C7DC4B973003B4B70156120583F8893C9D4CF4F4C852FE73760E1EA2D9409BFEC335000C8EC036403A9E6E5B34CA479B16728BE0207C4CA3C202EC18297E682EC4D5FB06EEE3AB59DD2D7D193EEBC9A6423DF52032424DF0933EAE0EE1A1EAC86997B954665F1BC0571D3DC84B3EBB3268EB4A6CB9A1C6682EDAD19788661EF30D9526A2FF19828F617B08CE64ABD5D045B2BED6E9F8148B3B128536876E76C8AABEC2FE638C596CCB57212922E4CD8CD1F6A82C2AD083B72B39986D44C8E65A920438F2E3484EA93229B3EABF7A7415ADC5980D1F8308F20318F055195CD58A718095C90950831C7D1F6EF188118E53218C9FD0D3F575126214A6D9A91C8078C0AD278AA287F4A13503B0B59CC822C1FB0CE0460D36D01F63E27069B066155BA2DB10FD90683F60E9B6E4BEC2382AD4226D97A14A92C4C7E8BCF8E980AAB4CB20D29B586B7D6EBCB2A28FBBB7BC5E4CF245B9842E6A5B7BC09644D54A5F8C54D2C8120B31FA15CCF03215C8F9872F00ADF14F795DB2FB38FFCDA7500B4EB206DD4B8D4D0EEC9C135743213F7D6F80EB4EB5C38C228DCA716D148C586EAE391F2ED63F634F4BDA70F3FCA10E5DB14DC5D72544CA09F0CA133B9B6A883670A8DD077893A56E6B07DD1C940D9114C0E3DFB7D4655179891A188F868EA4ACFC683210A15DD600E672835441BD0D052AC7644817E62773C833B4B195D0FE82CBB487C5CBB8CB1F89876097B8D9ADE3206DF1B6E64C101E180CEB2881CC7B5491F3B8E157FBF89AAE9227DAC38DB5EF7E820309E19D03FE6B867FC41AE2EF219D31C0BF468E39C0DD8338A785C10762C2277F1D2D6C7EEB2521E3604F5A60F685C7974141C4C0AE8278BA8535CABF471A798460987049A8ED2479A1A4A6B8BA19080BED1464BE29AA08A97C4305F9D73687A41151E69383DCC0546829BAF083021322E8798706BB81C51C24659FB35B90A9204B7568E9F2432CA4550726B23173069D8C1DF86FE51B672666CE5CCBF95B3F15AA99E1A15D16E4476D593A0455BD5D35EB4F94E8C6B03B4541BFA86635915FC86617C6968B52AD6CD704A6A695052EA283822E35A2565D1F0D194D452076C45381C915935B02D5A3A0EB02B46C0064A7EC5440659CF626E4D631D890DAB9E94015DC036DB047F11DA6208FFC2F50B7CE141DB5586802FFA6EF7E82F302C09D057E6F0255C43B4014C9846087726345DA30D59C28E1505319F05A12A5209B428B48A6AC2AFE64C714D2C5A654710E826E07E49F8D88303984023CF22D4093F30F4C14ED86121DE71D10D377D78936151D51E27A9B104B98005052EF881F5C58DE0FC95EDD486D77088B4514E205080115078818931508C3CC2C50753A760EC0EC85036C6F8E0CD5E5D940FD67CE66E45E9EC685D500F4B8281DD23049E3074922E4C85B2658A4015A11DA6882FC1906DAEB605F71710F200E827536004AE219AD0084C035AA1687A4413D160B09E68EE1BAABA00F0CF0FB0CC7BE8F76934EF909F6D6D11E700B5F7260FB455E16A9EE35376366F9289A2F8908D64DC9C43CD547941E73905FCA09B7855121872E66BBC7643D805FC79F3A0E33D7AB388352CBD0407DE4CC95594E96EA55C4F427EAA39D656CAF524C01A5472B039DCE065597F2FC4E89759BC7460E399D9E9BE864B051030F8CBC5E153BED1233034F3BBB911E6676A6B47C2AC1D005C59D65903D6AE83E5AB28313BD7E0B4163CDCB577732B1CCB5A39BAE536E7D8FBDADA135F2BBFB64EE2F25A9B285CAC826B151B77ACC2E2C3E090959B75EAFBE9DAB58CC1FFEAD0DDA576110A69483B7FA2BCEE327A1465071778EF5DA7198D4E4421EA71B69AD51E2A81AEB37467C935CEECD092699C7C1B5ED36D66179610E5213A4DB8506FEE388D6B456D1361E78AB13A1076A7286FE12868877464E3A94FD77590333FB839823B3F2B146829A9F014B70B349B940627730AE6355B93CE1D61DE90AC1E8844EC8EFA2988AE2F649F670AE639AF6721BDC0F93993BBA079DC12A7135A175BAA2E005D70C97C8B4EB88C185693500D83FE294E84EB5DBCDB27F05E97C633146FCC297C4371178ABA9764DA8B5CB04BA721F401E88948BD33AFF158046DACC33E8B00249B77B0B55E8A063DAF603DE2987A06F09BA36E05EF3927BC57785F39436CE598FCB380DB210E2E5D84DD0E3BA72EDC068AFDA3115B372E2079F71723D4510B25D4B901E9D2DE9C2CD70F78879A0F6F4E4896357E2CF7685B3F086B133EA0C747BA84E94B365F8E968F684D15C7BF2E8F8F9E76DBB4787BFC50968FDF9F9C1415E9E2D52E59E75991DD95AFD6D9EE046DB293D9EBD77F3D393D3DD9D5344ED69C6E7B2370DBD5546639BAC7422A5DC96EF0459217E51C95E8132A885CCE363B299BE0F484EFBCAEA7DBCA947E4D64A9B60FA3DAA2F46FC6CFCA2BCAD6ABA65AC0098A40B0EFDA0BD25A9AAF6A38D66D47C9340895E51AD5C7ACD53A4BD520EA87E62CDBEE77A926830865357D7ED1CB9316D31CA8A264FB0C926413ECE92D9F0B32EE2E922788A694E8DEFA79E50C046AFB1C701362E694FE0D31597FB7A776FEF498E5A5D8E0FEAB035F789DA51BA9EBBAAFF6946A94714E47589240B23DED0F28259A4C874938877D0DEF89BAE9F75459CA7C8A43CF36B630D7AFE00B6F1D15312E1D4BCD14B34E4DF55D59A2F583DC56F6BB03B51DBF23CD5114D264AA6F4E0435292AE61349330BF3A5A8F35D67847A9E8F371F40DB4B6EB3014CC1622E68A37128660255B00E35EDE54224B7042378AA29CC17A72289E6930B8D994C63E648A3573DA702292EC58BE24C49517233A1A7F8292945529F64B7043A1A6739DE8844DA6F0E549A272DF5F815A809692EF316EB648B9FBBD4EEB76CF814E1C17E77D0B6395AFF4AC6A83CEFF329EE14A1394B4C73D0B891ACBF89742FF3262A50EBAA1E7F59A85B75D171B0D63DF74DCA2D860936499E34671AA24E5AA91D9BB5831468D442AE5374146BC4CFF13AD9A12D4F5248721869B45D20493EC58FE24C4D12ECCB894696E1558DD3D8629D5FBA0F2F6D69B515DE1512079990E430A7AEB38DB888AB3FB9AC0ED25F21861CADEE9A7D682A10921C38CB51BA7E10386BBE397346E100CC246C9283E5F8803E21705DCE2638F0B8D9100556082BAAFEAB3DA59BAC28A9FF5D1116EC77079D8BF20DD44CF6BB136F9208DA6FF6547EC19F968968C2741FEDE95C3FE2143487B8047B7AD5EEDE475CE0FC33BEC17992092D85D2EDA977AF326A0A48DE8480734C656DC6B786DDF61CA65B79F74E85C257DD9D5765AF15B7BAF498BA922908988372EAC1C852E545C8D9D0F0B430DC4C8BF0299C96903771FBAFEE946630292703BDF5B104B54F0DC609211365EC77CECCFDA0E336EAFDBAD8440D18EC42D2C188CDE4B0C74974BCFB7877F919CAABBA9D2B264A524A7458CE4A1AC1551B5CA05DB27D168CA1E69B83F1F890A5A24D5B7F7230716A07529285D37F76E007EC689F1E0E59184EB5C9A5F1F0E0B6C7D54448F0D8E252951C43F831D6DD747DF111DF2734508A6CCDCBA9F694D972D0EA0D4A77B0ECD7599AED129204D196531D8667520A6AA2FEE23028F3EC7392AE453DD17D75D81FCB7E1724527F996A255EE9BA0F2845F7628FF3294EE7CEB488ACDFB904470E4F01DE9CCE9DAA1233808A93D15895F806A0F28DEBAE0CB829E3B827B320232D95FB99F9ECB8CBD04C09EF49BA04763083CB64FD24CED44F4E6DCDEE33A199D597E1F677269A02E3188BDE56A2AB7918CF5C8931F7FD58CD3D9076E4531CE60EFAB0E78ECC3B2506372CE56407032DC9CB07799F90F9EC32BEA8137D5919B0DF5DCF9AAEEF7A8712F271139B3AD5ECB5C44F7BB495A677E6B3A3346EB648547CEC772FDC0034E5542FCA4B5C3F0F53906E93C79F999A22339090D38C1B6B57FECB99DF269A977837AF81D313B757EF3E49E98BDB1C1D8813969836D9F1C6DD1D5E97C9672C4F0B429283CA4539E938F9C084FDEE75814B797FEB80603B075D423881752EF987B280285448D9A5D27DC1B9F37DC1DA693F4F030AE9A2A722CFCBED37C71B20C0CD0F671AD0F511A7B923CE6D62392C14DF3FFAA05115DD69A00F7B437182BEEC1ACD02FA5021A58882AFCAAE240A2B470A4B00F6CB3F3CECDB9FD29657F3D5F9A0B61920E7E97E071ED572E9AEAA4FBC9DDC7E74A523DE496E3F1ED0A0065DBC068F732800AECFD0B7A2A31E89407159412832B98C788004A40494D902EB0294852EDF104F0FA6C2AFCABD9A1B5EA1E7BE36F884CBA93529C92E7568FBD14523932200BEFACF8EB42A5C9C02D4DA041F7A33153DA7D9877AB713BBACFD36EEA96CACCD8C9BFAADB7B4C7D97FFE82C6E74DFB8C3F74031A765560B303AD2A39867C9A42C0FE249BE04C0FD0F77C8A1F45F13C8B4F72A749042A9B6752A203DAC9B74FD244D77E74B08BD1D3F913DE3D0A2F2998CFCE6D7D8F72F9BC414C73B8469B23C080E8BF3AEC00FDB64F3EA32D6E63EB725B40429A9324F04D9E889B9ECCE783D140BC83FE4035C412F3D045FAE24A3030A544AD24A639003759CBDB0FDDC7C3119FE4E63F548486F80636623492508A52282989134877B82A91A41D01E1AE0497E272F9E24945914B71B0A2EA38CC9C0D0585669E1072620086D0C50547CE67956120A054D25C39C95094521D6E6ED1D36FE1B256FDC9E15D4B2EB9BC683E1D0C1054DED89DC40FF939B7103A5C6CD855D2459EE074B3AD024E0AA78F5C8ADBEA4DA6D67F7539752A8ADFB35C34CABBAF0EB655F1AE3AF712346FF7D59E52E54B5FECF6EEE3E1C0B8F2961F0A6342C407C66031255C486E09C6CDB7B1B70C5C85ABDC08EEE34B48C7727C925B0B3F26F70F6521B7B2FD7E30F03384F5703B3BED69F91CA2EA4A8F293F5925BAAAC318C743B5B768699FA3FB3ACC5CAE54CBEB2CBD41A5F08EBAFF7A30785606F071427245C503C38A72436B317BC44E24153800899348563EA7D350A1E1CE962F250A97CEE7DBF2BED2EA0F733A3D95CA0807E7A50F38A142C341AB3978BF5C28CFE4DB2417C0CB70BDFC0AD72E7D185DCA45D009D5AA9A404236FA555B5C29AC415C36B2252148C9C907235229744FA0540DD18C2C046BA46050332AF102C9CE2A4C236438C7C1C8D92AA08FDB924BA4E8B3F032D3502EBFC4A2D2220CCAE02DF1A86862C242C9CF79B924A7092407DD5771090EE78329E83989F9ECC2DBF3565A24771F0F668C7061BD02470743CBEB71B8A6F498A8620A023A4F4E3D18593671C6428F682815AFFB5F60B958E3434B07BAF3C77C3F1809F1D77163191EEDF5786FBB4349C0305134E514F30493EA7BA1596D73F0190E47C06C68B950E97631ED7C24AB29ACECFBAECCD55CE8742EC54195E698FAB0792FBA88E83F3BD3921F6F71092E937DD359F2769E90E44AB3952A44B44F735B274BDACDC3131FC014F3F9F0C6101BFC31DA58EA5E59848C293511F3D86A13D4638CCDE1A039A38FDEF6A70C1B3EC5E1AE04F569453DD84814F91487BD78FCFBCF68BBC71759BE43255103C2C67C93EC7480B3DDE848B6C9073462FA106CA1167C43C8CB7DB9AAA8F2904BBCEFE57AEF5FB63CBA8F8E8FD7653C329F1D1E9A238854FF75DC3BFAE73B94089EC79B4F2EBD9C635C8A9D5C7F7398BA252F12AE4EA2624C81FF481E790AD5878319C4CA588C4E23188CF06A317C15E50E7BECB63A47F60FD07F773CB8AFCF1F44824292234DC05D75FFD9C53BC036F98CF36720081897E24EF1EC81C6058469B6690E971768F8A255568A610FD8EFE36AC2B62D903613D33C7A4FD26E7C8A0F9F4A113B6BBDB6A0A4FDB884C3D2825102602923ABDB6A43B7A05787A1116BC621EDE5F5562BE6BB2F1AD95941AF4DB2A7F99F7B54498B27D77F75B83E982625F07A87F9EC70264A951B408CFDEEA047D3A2CC49EFD481E2F997116CCAE18DDDEB382FE7057A21E35845E1258C6620841E97E03C12AFC1B7FF52E214A311183C87F6868EEBA7586F79BD116E283F0CBE658748AEEE90E455B2EB0AF902AD4957088BF6E69BCBFB879BE49F085D033EB78424179AF419EE06A6C9251D1AA667B358789ECDFCB10C95FD7271ECEBF34DD923F4112CF02ED685C60FA868D51B4788FDEE321E7EC6F7B8447982A4E1C0A6D853FC25291F56D90E95D912EDC5F9424A7498B72B3B54BC91D87E3CB4D14A198B355E292DFF110B97FE72C7AC8B95A2A68177C97E079262535C561F95B0C0F5079BE232722FB7FB12A71739965E08B229073332987DBAF0A1C110F35D7AA84A0F3334EC813D99C3D0C81760240F91EE72329350CEDC0BFD351828DDC12E5818AFC228B21C90B829C10842AE34BC8F68E182EA2EA7F96531B65F5D8447CB802263120E465017A4CFD37582B67FC728FC088AA3E621364379F5412E534C14A294682F4B5A42D6ACFDD7E94CFDEA96B37C1AC47C76BAFA0CB8C54E9D6F55C57328B0C45BBC966E5FF45F0F66F4FC0D3F57174282074E4BC863CCA88BAABA9794E07BB6FA602F9EE60E0C4BE1D0AEC57CC0A820D322AD906EE707CB47A0E721262305556F0B0545F50624DB4B52280CCC5A708EB01A66E62A0EC937F0350DF57213C90B124BCC6769A12DAE3C4C604A49E709429AC311055312400E90EC725BF0B77D92E38DFCEA954F71A7D83DB403A9F6A90703BFA60BDFE728DDC4DAF3A988F96FFA288A1BCE9EAA528AA3A72ECDF944AB2A09C00F48FE0A3F7FF87D20ED0D773AC812F3879FA2B80128552905FCBA3467F85525D5F06393BFC2CF1F7E1F5104F7E60C2D7FF0C1A50D28A18514D06B939C91470BAA81C7A41E9A2CE3F8AA6789051C5FB879AE674BA92E34B9FBB18F79E3CAD5B7FEB45088B249C8D0F20782DB762153482135F78D43A6A07A586BB710BF4E2736B88BF4ACACED113FC8393E216B7F0260EBBEBB3FEE8261C6261D8CDCEABDBFCA4372F82BF79E96CF5B775D69FD86665548D2CD7C92EB2669551010A49C7A30B25C2D5AC5F00346311EBE8804BDDCD79948A8C42096945DDBC9E90E07FB0BDEAF8B8AB6BB9312912FF90800CEE1530370714348F3A17AFEF498E562EC0A29F500611FE58D034F2E08F26EAF1DF8726AB8BBDF51E6CB42FE19E5745FEA3AB8F7E92ED40125DC7D74A113EA1BF38C585E628493F69BCB81E5279148F3C95FC5F8A9C6A187EBBBA2C8D649F5021DB8C670FB1EA5BFB6772D8E4DB71584DCE290635281E1BF916E1270E46E97D93E5F434B1CC53D04F936C32D1C0097F65057B93B5F2BFA4A1032A540BE98A222834292139B440604E144864757C58FFBEDF6EDF11DDA16D2150CB007DE9C80187082C959B67B44691517DE841126ABEC088B26490B0358081D9D434246CF941B2C9A96CB9EBCBACF2F0A0E37387BA48ADF8485369FB4442466B1FCA803EEF29AC62181A0E1C80D017593A5156EF7F5E5C87FE9346B48B9C3668D65D0AC010552BC8563293AC242E4EBE5CC1A8A1E880113DB59631969D658FACF1A8322E325CD1A03C2C16AD658469835969EB3C6A0207821B3C650F2674318DEB6577CECC330F6455CA22D02928088BA0A457BA0757BA33ED872440DC8AB1BA443A24A8683C9D825C1B06A6B9082355A1DAA01C52C030B03D252D17444976BF048A9BE08C05336C50D7CEE27BFE190F3E89EF0B98D0BB268A9DD5485DCE24242730D48F850759C825BC7893B3006E64BD274CDFD123BFDD666D6DD0F3975D06D35BDA86A202296042EDD10AFBE5A72ABBC60A2E6CF2CEE76E3968A8C0810E762966E67B8F9D2FD2EDA0F54AA640AAF2F2FF6E596EB07BC4355B38A47B4AE560C1B5CF97DA420F9840A5C67393E22BC7F4E3638272D7D2E4ABCAB51B4FC6D7BB64DAAAB0D6D860F64FD72878B7295FD8AD3B7C7B3D7A7B3E3A377DB0415D561ECDDF1D1D36E9B16DFAF2B0775284DB3B26AFADBE387B27CFCFEE4A4A86A2C5EED92759E15D95DF96A9DED4ED0263B21B4BE39393D3DC19BDD8958BC216B45E5F55F5B2A45B1E12E5032874F8DB0C98A5A7722FAE66F58825B2BE68FF84E45041AF522A53702EC402294D3B7C749DA4E079798E083BA82BEA16E5BF394BE9FC58D0B1AAA92D0A72DEED4D289B63EF148B5AEEA53725FD5E64A0C25DBE728946AF85D244F71F86A1A591F73D5A436E4EF32A1EFDABC58A37FB7A4D2CF285F3FA0FCBFEDD0D37F67E995B9FC4C4824579FDCF64D0C22B6C46B323B4522D69C83B34FF62250259A638FB65161F79E68B0FEF6558C6E6C627331E3CE91255A920D1FC3501275DAF75744624F6F8FFF5755F4FBA3ABFF71CB97FED351F5C4FEFBA3D747FFDBB525EFCA12AD1FA275CCBB1D17BDA92179B7CD90A98BD8734B5B552C9FD43B2B62F858DE490DB7240654C2F50E8B1B4668991064CC17A7EE955685C26A9DF9D43A0BACB5D761A751C601437016896075DBC07E3CC924DA5B0F4134F6798ED3F5733DCEC3689D3F91DE48EFF14766D29528D9F44DCB943B70FA9221E859E568FD2BB1B34DD6830BAD28F39EC956B4E82180847D5759EBF4560E3ECA9C95BEAB16572027B6FA6EAB69AE3F4550071CC5381AA61D8ED573ECA0815D63778ED7C90E6D838C24DA3A814E501B59823EBD660D68E521B51DA6B587B866580BC5875C1DAEAB6B87FE22A6ACBA6BA5BA5490DD5B774F9CE5458ED2F5431CFBB966AB7D2EE3ACABFBC2219DB37C409F10DF3561ADDA6C88BA2A8A28C46EB2A244DB3306779E623B43F9268AFC09478CB4821AF70BFEB44CCA3873C4F5234E2DAC12AB26569B811F7181F3CFF806E749B6099B22BA43969A2662770FC22CD3A98C40C89AAD4C76D7A51EB7B5E043A1BBC7E5BA88AA8A05E98DF62E80EB4AD571B1EAB257D06A44CF7D02561B7BEC11289479EC6998A9C9D7C674326D7C6D1A5F6366E0EEA35544DBAF6D89C5B1CA29353F6BA02F39C0C06A89FB02C1774829BA6308408C3298AA607F8463E0A69A5D6772047C7A54223060B7461B631768976C9FE398937508A5186BF2FE9AA5A301D2160C997B41015A54DE960BA95BBDDEB4D4535DE121F696A0EBC0965B4BAA8BB3163B4B2018065D7E7B9FC996F823BE4F8A32E7CC70EFB5024B2CE222F27C9DA5D92E59A36D44A2755CB2186A84DE7B48D77134C92AFB3DCE596AB4B572A5253FA014DD47EAF88656B419A16230CED94D452A8EF55691FA26DA1E4AAC2D9405199B69BCC998EE0D341AFC3DDA22661478A2ED023D05525864F7D9C16DC87C5D987793B2BFC1EB6FE98E62E2C698917FACE411678F93FC9DDC9149B3C471B6BA93BC7C88B3A388A843D4684AA86EDDF5DDD943B2258A32E584ECBE9F1B67D65CE2A73DDAC63231AABEBFD9A2484606038D41882E71DEBDE43BA0B9AFA115677E8F393B68A651BF1DF1039A45BFCE7CDDCCC79D1378CC7F6C799F59502C3FC2B587C98E64EEEE70156C21CA84758372D209DACB4A761642D07D596B9CCD173EE8821D009830258DEDE856551DBF26604E9F2FCE026F2AC4BBBC13F1D24E8CD34221EA937517594371E90545F855B1098A92B28F0DC595C76CB20A9EC42218A35F287EDB9F21439B1F01E7E97E17A66A68D9D37012B341C622E9F27483F20D3B0F790D4F8090DF8855101A70108375864E1020D178A36EF83700F62092DD845B82067E18690192B6E090A0A075B02008D46C59E3723ECEAE744F2F8EE6FDA9F0B90E5E970A3AA78C76DE1A7357E0A67F530C2A801736406FA0C7FF96FBABEA07E9C60D56B013630FD3A69618875A5C34A678288A384A5B8A8452A859B24455EE105B6B859ECE9FF0EEB10C34D8AA4844A457C2457895A3340212CE7FDB279FD11637C1E8C27A1937D1A5433609ACBC92B8A91A0B92036822B5B7253B75A4F73964D64962F9211553B26617D031C6FD2DCCB9CDB51E91C010B2159D21F9C8D7C2DB8E51C6FAB1115BCE1F92B4AB316C947F404F9128D50118ACB5B2BDA5AF71266469F21B9CE958D8FE12850185FB910EA838CF2F72C6CF42807972D83A802E087C60D12E3F5CC1002C5B6243E0224F70BAD93E4367D3C74764CC2E707A5F3EBC3DFEF3B7AF9D8953FE07217C838AE2F72C979E2171843DE8F601ABFD4DBECB3CDB3FBAE3B72936046A2FE7375EA825E5BC50DB941B10B543ACE72714AEDD36E9738A76C9FA8698781E87CB6CE1203668277E4CEE1F4A078F30F6E7783D9B5E077A7C17399FEC297B78AAABFF1157EB28F772BED3960BD9050B360EAED6594A9486CFAB636BE85543D407749D4A70851BA44B267984123262575EC79D505C1B738749A791B17BEBD26353F332F0B873F5F5B8D31573975E98BBF4C29C0489815EB35E2E42B73C2FBF02C91548F4CFCE7D9B8F1A7BE6FDDA392BB467A55FBCE8373998AA067FE8D9603AA46B0512012FD247EA60A1B6C1FB98F71999A4F75E06B348C4CB6C86888CD7D3DEAE63E2F8943CA3A448A37F415E8F6199C24197A64A94C7F1EF739EC6F113B32C9FB71E0BD5A6D8204F63BBBEF67B1ECBC9D97590A86112FD22725FD3E04AA89296DFDD98061DEE77636458C5BF1B43EA186B926C6FA2FACF910D85802992A130BCDE6E2A1BBE7FF79BA4245F73AFE1DE97BE9A7BF42B577AC03E3D6FA31618B7D967DF7EEBFCA4A0EBD221A853AB3EEC0A22253004676739A6D2792FBD648B44D8E3858607EADBBAC3D0DFA6858D0296CA901A4635EC6C8C42A66CD00679E563867A83180099EDCF21685F6F373FA3ED3ECEEAFF47FCBB2F31073FBA54A87E27B25E372F06BF912B4DC11E8F2AA39D3D2C504462F12EC29EEF5012C73FEEB2CC312EA3908AE659859B148328FD23791C72F0D5673AA38FBC76317A7883AFD5463EEF3FDB9241F34E55B4DE2A75E7812B1CCE469437A873BC4D3EE3FC392AB1B3071AF7AA23D7BADBBEC9C95F451592E9F43B02AF35A29467EE279525DE152BD2CFDBC1AA88A74ADB3E89A808BB6E8EA5107B1E6329C696E2180AD23766CB97AA24EB4E9118B5555081AA29F46E619CEA97C93F3DF645B9C2416CFCE71E35E2F75F7AD38703DCC5FFE85AAED2A1C35671951604B36BDAE13EEFAADC35C1B5F7FBD32F5B1F7021A59C7542553AC6C0BC861EE3DA0FCDB6F8E48333E6A8717D843715C4073B338FB31B126B197D81D6A48D4157E98A9BE49F085D732E597CAE395F15F48DDDC695902BA066B3AF601A084CB1635956AFCFA250FA011535AE829C655C153FE37B5CA23C414148FF25291F56640E28B325DAB3EE3E3D8E2C2AF36D506B833114BF8E9C8146CEB07629A1B34BF63BEF4AEC9C2E57C1B207ADE3AAB8DCEE4B9C5EE458356842ADEAE666E09781734F783A38490BBF7220D2F073A0A6BD76E0BAB1B718F95E41E39ECDABF36849BF2E6B4B8674142D3B78F75C24294AD709DAFE1D23AF03028E804F674904061CB3B40A76DC7A0926B21D56DD058CB2697E9EFADC55806682F067954BBCC56B22B301961A0450F561B5075A49267784568534A7F7A7B3EF9CFB67F0E3F60F181544BFEE5ABF211E9D2590F019DC00890187B7505BBCD71110E14163B45E5307E437FE9E0DD8F23E7213CB0F2834B6AA7812FB887FDB2739DEF08F6CDCF5584BA77B1C31D8D6098DD4BA09F08A55950F708DD5951F50D66C557F64597F209C7AB9A461CB07C8BA2B3FBCACABAAFEC8B2FE88FCFC5132C50324DD161F5ED0B4A6C1972BED7699AF934FB67C40AF8EE1F273E2E36FA889560F82AA620E55BBCADE7739CF140F903CB0B01F48F021CBFF97AE37AB1A3D855CDF5CF79370577658F1563F877FED552DC16BEF6B1E5DC914F77AF9C5171FB043999A06EFD3D5A205FE0F18795EA91569F8F9D690690C79A6B3E05F0D87EC9F89AC47D9B2EA89B2C74F61EC9D3F3D66B983C3550F10F95E38E429840188BFCF33307C686531FC5E7004232128A2330DD10B89232F676482ED5DFDFA86F2F9144AC2A064AC828B881406340C2BD3F334C03464BD599E584C90B4BEDBD0A5C4082E34EDEDFDDB10AB3FA2EDFFAE28B27552D5D6B0F76EBD6687FC2DF31B9C89CFD3CDD1C76CCB976C72368D2376C3DD2B20F5C37E5B268FDB644D187B7BFCFAD5AB53A9EF40EA953257D0AED378CAFF229125D8C2D47F5B42233CD2EBAE880840066292AE9347B455B64F280182572A0582FEA4AB4C4C99E3479CD2F940D10F1179E8AA120698A9B7DE9C303032A28B9E4953D9B131DFC5AB0A9CD8DB02A2C8FBEF4E4062AAE52872DF870250C7B3A5D8DAFC81A061DB16A5E611A04279B6C308CD2489F2C5A0A262D6462834E3C83850573912003A45A197BEA41EFA8FCE28302229A2DCAD55419B3982FC23D43792F01BA4768140810DC95E746D264E7CFDC717A20C3A866D84C486561D5129E8AB1D011B952709B25CAE42B9775384B4F916244F03423816389A42CA60AAC3558D37F903B1C2B72E4ADD2300E62CDB3DA2F4F9B6E33E7B94DC73319AA4CECD2B92F6DB0B4249CBB3D598AEF38E8E0E6DBD2320A3D12176C000E4080BF0A061E1209D3AEBE8A0D0553B86B668E6B7CEEEA09FD5F6674CC363FA4D8E290C109F2D8DC98D90F3A7F5034AEF318D057F0BF7DA4020616BE6E8F1095F0C3CB866BD086CCC17761B17F305273FFAF385AC53E60B2B49CC01DFF283AE4D48CFABEA1C47EEB5419368E68B50A13B98A72F4CD82E06EBE4829E2F0AF1082C9E8CA7B703C692B9CFDC3F5F9C4E2C79F1F1FD57C98F25F9D9949237AC0F43656FBDBE7C61927658FA4DABD6979676DB9217F4F2E5D86D4B3B912FC7B6DB48CFABEA1C47EE66BB2D54E8E3DB6D6309DBC56E9B5CD0061D1E2AE5B175F8583276D0E1938B78B9186E144F6F9F8D25711FFB6C52B92F4B946E50BE61FC06DC8A9D1511096085024930C78B4605DCA603C746E5814BB8C5A93EE569D23915DE7E7342085B2D478E4F18E694076AA54232BADBB8F6C8E01AE550ADF632F02867800D1F57E9674230CB9F09C132CFC467C38C5CC58C9C6CA54437FBC11E7D9190A26FB6427662A150EBC2013662D592DCA69B82925DB245F9F88A86AF989F8384A42F44D908CD7A89EAA6BAC45FDC2EB37DBED6DC39B007C9A9D80D6FAED339DEE2121FBDABBC3993951A2AD668233F513821151A38681E7D007CB4295F08B4348F5C5E16B25634A2835AFDD44FFB395D517FF903A00A706BA090ADE2D5D1C088D2BE781A6B36AB791870016507C097B640B287D684E797CF29DA25EB1B62B1DF9E3D24DB0DA1AB3ED6E833F3E71BEC7727C9332529255BAAB18E3D981A6CE4C4E40FC486D86EABC905D1061E04527E2A705EBC4B379779B67FD45C7070C7CB4093CC4F97F31B8E85FAC31786AAAA5151AA1C014F15766E194ED438AAB272E26BBE7C71D8A9DB6523C22AE75878D154361A522A9D130A11C37444EBE0455DC8CF935FA6A80BCB57CAD38A9A22F256E6D555445F8026B01618CD38961E50D735063648E5CD8A360C1D07B51A1953CEF62B9249057DB9B85D690E6E2FF9D5E6A5EB6A73C5975F49E5632978BB75E665E83A731556CF08025DE94FE28D02319EBF1AB61F2209D4B2A357E127AB41F58CE3BC837723724BFBBF7350A814349B89133997E07B0DC3C7D74C2C68289BAF121E5320DEF50C076F346606C6D853E05CDC25E9FD6D7355D00825311FE011C61350124FFCD6859C3AA48718274C096542B7A8A49646E3619487DE840BC2F42FE8F9566A89E609675F4CB89EC97C7F817062F9B71123937F121819EB1FE5D8E3798B5DC0531510160ED5971708989A731B515539270189A6E6316CA0FD2629CFD3327F6E39BBAD77E8D5735657829FAE98CFA36C63C89C2BF8E9938799E3FA865B4D2D5DF6AB7998C124373046FD23604E0894DDC7775662AECFC18A98F93A0AE2C400DF202FD743DE8A55F49442D481EA4C1BCF5C512517E8FB20C075A309582D5DDF00042BA4FC4180A6E9B5C3011BC7E4F480ABF5985E87C9A21C1B52761A34A6BE1A153E0E5A6A7AC058DEA89DCD208D44BF7EC1D0E95B7970F099FA5E640F2020FAB77C2591E6812E2456DFBF7C04C97D744018AA03C54F89A35B30B6B620BB268F24BDF6FB7828324FA031A72EA86F06448FFDE4553336A14B39EAD99F80A76246E74BAECE27B8096B3F7E81B0E91A77509869B99AFA2A7F7D073BE243A2116EF32B2FF37F912F445E8401C4A1090AE97888AF42C6C6D1746F42422B1DE52E071714C6109D8DBDE4A139A6971347019910150CE668403B5BDF790AF19B63D939C14F17192D8883E8583CAFE216D1A7BDA404CEBB93C20DBE48F2A29CA3127D4285BCAAA3A596B8545D5D393E3AEF0222A96F8D2CD70F7887DE1E6F3E650427757425295B011CEB296BAF71A5ABBBCE61A8996632D7DB3B98952AEC93A09A9A54EA0EC95407E7B34AAA864B856AE2FC6D59F4631FF407EAC33E55D17F6D06BB76291AA46E891D5905FF7D928ABC1DE7821371A91E211DAA8CCB6281B3D68D950CB336054459E773CB5481B22DBA46349E9E4CB479AFC9520D7C32540F9BC3DC14EA1550AA847E8448CF1766824B88E0524170694510745B03D401E683AB05B25A70529B7B72CDF577B0A6FAA6B81150EDF24846549B0242AA4E34D3E73DBC4895F0C9504D6C0E8BEA24A72372955216B05A2197858C040713B2B0840CA0D4B83CE64AEBFBFD5255F567A882FA1991916CF5B043265B7D06C992148B21CF3E7C94C73E9B0A2A01F6B19CA9AEE6FD93544BF31DA2DF3CE734515E417A66A5D0332B0B3D730911BC5410BCB420C8DF279679E59241AE991CD6D688A6462987C62AB1AF17B80C26834ACE03424BBEE6669EEB993B91C07CCFA4C2737E97C146FF5717E900FD5F7D87F53F49B2965DF1232AF7B9C698EC326824D7E4B1A894B9F22557C8248295B5E936F61274994B53639F495B739BCF86837ED31A587AB449F0D2A34E3557D16C4B4BF49BEF10F166FBDD8AB262A5C6A4296BB05B9D493753D4755D6B6C2B2193B5EDA3AC5548D7D8418E35D21378556D344D53134DB6ABA43EA6555553A76A2AA2192C45D71EE6C1626B5395226B3318D70B465D256781D712AEFA6ABE50AC54DB04B81ABB55EA4592A2749DA0EDDF318246B1900E55C56531D7F837FCFC33DAEEA106F549503D6DAAB98A0F1815A46BE994FA539A4026819403AA50C86481C7F2016B4C703E19442493C37A3CBFCFC99A4E3DD69A64CD60AB725857F721DB606865C3276BAAAB725857F711816B4F2E555319CD605D976AA1CB27EBD496D3B25731ACB9544D6576035C6379E8ED0D072B8350C3EBF2DD3D6C7E73A9A0B1D867B059FF187790E52CF0DAC875FF583C96D054ACB64CF83C6ED03C3560F3D40C4EF9F10BB3E1AFDC2ABF0536E59972F0EE3950483CA7D0BE24ED7A024C95CE3C405AACA0444A759A7862C577875D57B5DBD97CD80AB887E0BCDAC6889BED6D43FAEF9ACE000E06AAF2DCF7E04E90A3C403AD37849297D896F83D908676E253B692C9A16758126BFFD1D05463E778360E8CF6AD68A6393238C7B6780E56B1DE7F9C5CB6CA80D640F3ED825F8736822B0F1E2855148494E08E80033503BD6011D19947007F425503A0FD76104D072311032D37472CE66FD4C89CC32C4F257138D82E24728BB0BC9146FDB8F3381C4916E8018B90B3913A003A13AD4AF309C14D1703A5028DD6C652E598EE4F562B56E9CFC9353B1711146E9D22F25440D31C949D5F93AAD897706BE4B098010D19771CB6A11D950D939ED6BC9C86A9E7134558C380C6594F391E4D1123F4010DD206F1E3185DF2CD5A1E82C2581A14863A545D40D38655184B1DFC1411D9021A3330FCAA1B34603B246767018D185741286367810DB589B315D074EDF5A386009823B81BC03051401798C349F180E4F7E56B44B6DF341D01DD21AA0AF309E12B0F55FC2368F561152B896F86E26E52DD14295137B2ED3BD26710C0D17BA0216011E7270A02E0BB55F5181092A2A1800F4BA38180267E4D94C683FBCF6C69C5B67270D39BB829E6A643015678F9B1C73AB5D8EA2F07D1EC2EC007047130F8478856B7EB0A1FEB168A650119BAC69817BCCD2B5FDAAB8D5FF6BBA6C1528C0B3B1A811D2084683074832EA043D4CE602F565605EB0FC14D07A209004D36C51CE058E52E5056BC365F266B5E737755D52EC0437E78830AE11CACFE10DCA0DED13BD01C85177877C646910DEB971C6A8CCA6D79607386D3A38DFF6D086780676E1E62FC5470A99F0A567CEE9594DB83F99572610679A17663C76DD6F33BC8D5F955D61F781B3D31F38D05AE48D7CDE612EC56A63EE7E53EF39BD15D3034CDB9F918169B085DEC6E1BC8A7E94C00D545ED7AEE9453239C9F69FDDF820769F61E73853D23E91E78B36FC47C3FA8CE517A7605ED5F1B2FB08266662EAB379AF9597E7F367127A8FD97426AC6CED9293F78A44BEEF5B8613EEBB48BF2E2BA40A54F0EEE12B57B4DA04B2C7D71728D92AE93576D61BE6A3A4471499CA7701D6B074CEF0CD2A23BB47E10E5552ED02821E5F0BAA6715BA8EB0CC8B3A1CCBACCB05D73ED5014D444CDE697C11B1F24E5FEDE3F2B61F94AFF64CDAD5F05E8DA0A394503366D98B707DC964DF57DFAC6B6BECB544D057D9BC98CF20F1F7A56E1170D3EC0F7BA48C2BBD8026F9068BC70F1C68DF07AA9B66CBA8F9334107409A5DEA4D4B88E8ABE3DABDCA68CDDEC86A8A1D1A0539D88DBB24336D7E468075CE13AF8E61196BE9AA59C9CA85DD32B8F2BC524EB2EA24E7C2895CEF74B97F6E6A4BE6DDE7C203FCB2C47F7B87EE0517D7D73F291AE4376B8FE35C74572DF93784368A6B8F221D4136DF35CA577596B5E0A1CB559DAE4EED94F8936A844EFF232B943EB9224AF715154167BF572E8EDF13931E13757E9F5BE7CDC97A4C978F769CB19AFD4758EAEFE372712CF6F9AF777319A40D84C4813F075FA7E9F6C371DDF17685B0873978A04F5C97389C9F75A9625F93FBE7FEE28FD98A596849AEEEB5C09ADF0EE91DE3A2CAED325FA8C7D78FBA9C00B7C8FD674DDF039A9D0AC22621604DFED6FE609BACFD1AE6868F4E5C94F82E1CDEEE9DFFF3F35FA62EBDBB80300 , N'6.2.0-61023')

