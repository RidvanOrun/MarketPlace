
Projenin Özeti; 
				- ASP.NET CORE ile ürünlerin ve kategorilerin listenebileceði, kayýt ekleme, silme, güncelleme yapýlabileceði bir Web API ve bu Web API’dan verileri alarak görselleþtirme yapýlacak bir web sitesi tasarlanmýþtýr.

				- Proje DDD temeliyle yapýlmýþtýr. Veri Tabaný iþlemleri EntityFramework Core ve Microsoft SQL server ile yapýlmýþtýr.

				- "MarketPlace.DomainLayer"(Class Library); Varlýklar oluþturulmuþtur, Generic Repository Design Pattern ve Unit Of Work'ün temelleri atýlmýþtýr.

				- "MarketPlace.InfastructureLayer"(Class Library); Db ile baðlantý amacýyla ApplicationDBContext oluþturulmuþ, Varlýklarýn DB ye Mapping iþlemleri yapýlmýþtýr. Repository ler ve Unit Of Work gövdelendirilmiþtir.

				- "MarketPlace.API"(ASP.Net Core Web App); ihtiyaçlar doðrultusunda DTO lar oluþturulmuþ ve AutoMapper kullanmýný için mapping iþlemi yapýlmýþtýr. Services ler oluþturulmuþtur. Product ve Category Controller oluþturulmuþtur.

				- "MarketPlace.Host"(ASP.Net Core Web Application(MVC)); Model folderýna WEP-API'dan talep edilecek varlýklar oluþturuldu. Controllar oluþturuldu. Ve View kýsýmlarý düzenlendi. NavBarPartial ve LeftBarPartial Eklendi.



1. "MarketPlace" ismiyle Blank Solution açýlýr.

2. "MarketPlace.DomainLayer" ismiyle Class Library (.Core 3.1. version) projesi eklenir.
		DomainLayer : Bu katman, uygulamanýn kalbidir. entities, value objects, domain services and domain events.Varlýklar-entities, deðer nesneleri-value object, etki alaný hizmetleri ve etki alaný olaylarýndan- domain services and domain events oluþur. Domain katmanýnda iþ süreçlerinin simüle edilmesine odaklanýlýr.

	Yüklenecek Paketler :	-Microsoft.AspNetCore.Http.Features(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.Extensions.Identity.Stores(5.0.7)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)

	2.1. "Enums" dosyasý açýlýr.
		2.1.1. "Status" Class ý eklenir. 
			- Active olan silinen ve güncelenen ürünlerin beliritlmesi amacýyla tutulmuþtur.  

	2.2. "Entities" dosyasý açýlýr. Ýhtiyaç duyulan varlýklar oluþturulmaya baþlanýr.
		2.2.1. Interface dosyasý açýlýr.
			2.2.1.1. "IBaseEntity" Classý oluþturulur.
		2.2.2. Concrete dosyasý açýlýr.		
			2.2.2.1. BaseEntity.cs
				
			2.2.2.2. Product.cs
				"BaseEntity<int>" clasýndan kalýtým alýnýr.				
			2.2.2.3. Category.cs
				"BaseEntity<int>" clasýndan kalýtým alýnýr.
	
	2.3. "Repository" dosyasý oluþturulur.
		2.3.1. "BaseRepository" klasörü açýlýr.
			2.3.1.1. "IBaseRepository" interface i oluþturulur ve repositoryler yazýlmaya baþlanýr.

				public interface IBaseRepository<T> where T:IBaseEntity
					{
					Task<List<T>> Get(Expression<Func<T, bool>> expression);

					Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);

					Task Add(T entity);

					void Update(T entity);
					void Delete(T entity);
					}

		2.3.2. "EntityRepository" klasörü açýlýr.
			2.3.2.1. "ICategoryRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Category" tipinde kalýtým alýnýr.
			2.3.2.2. "IProductRepository" interface i açýlýr.
				"IBaseRepository" interface inden "Product" tipinde kalýtým alýnýr.

	2.4. "UnitOfWork" dosyasý oluþturulur.
		2.4.1. "IUnitOfWork" Dosyasý Oluþturulur.

			 public interface IUnitOfWork:IAsyncDisposable
				{
				IProductRepository ProductRepository { get; }
				ICategoryRepository CategoryRepository { get; }

				Task Commit(); //Baþaralý bir iþlememin sonucunda tüm deðiþikliklerin veri tabanýna kaydolmasýný saðlar.
				}
		
3. "MarketPlace.InfastructureLayer" ismiyle Class Library (.Core) projesi eklenir.
		InfastructureLayer : Bu katman; teknolojiye özel kararlara odaklanýlýr amaçtan ziyade implementasyon kýsmý ile ilgilenilir.Bu katmanda domainlerin instancelarý yaratýlabilir.Ancak genellikle repositoryler bu katmanda etkileþim içerisinde olurlar. Veri tabaný, mesajlaþma sistemleri, email servisleri gibi dýþ servislere eriþilen katman olacaktýr.

	Yüklenecek Paketler :	-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.7)
							-Microsoft.EntityFrameworkCore.Tools(5.0.7)

	Referans Proje		:	-"ECommerceApp.DomainLayer"

	3.1. "Mapping" klasör açýlýr. 
		3.1.1. "Abstract" klasörü açýlýr.
			3.1.1.1. "BaseMap" class ý açýlýr. 

				public abstract class BaseMap<T>:IEntityTypeConfiguration<T> where T : class, IBaseEntity
				{
					public virtual void Configure(EntityTypeBuilder<T> builder) 
					{
						builder.Property(x => x.CreateDate).IsRequired(true);
        
					}
				}
		3.1.2. "Concrete" klasörü açýlýr.
						
			3.1.2.1. "ProductMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "Product" tipinde kalýtým alýnýr.
			3.1.2.2. "CategoryMap" class ý açýlýr.
				"BaseMap" abstract class ýndan "Category" tipinde kalýtým alýnýr.

	3.2. "Repository" klasörü açýlýr.
		3.2.1. "BaseRepository" folderý açýlýr
			3.2.1.1. "BaseRepository" abstract class ý oluþturulur.
				"IBaseRepository" interface inden T Type olarak kalýtým alýr. Ve interfacein içerisinde tanýmlanmýþ methodlar gövdelendirilir.
		3.2.2. "EntityTypeRepositor" folder ý açýlýr
			3.2.2.1. "ProductRepository" calas i açýlýr.
				"BaseRepository"den "Product" tipinde ve IProductRepository den kalýtým alýnýr.
				Icerisine Db baðlantýsý tanýmlanýr.

				public class ProductRepository : BaseRepository<Product>, IProductRepository
				{
					public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
					{}

				}
			3.2.2.2. "CategoryRepository" interface i açýlýr.
				"BaseRepository"den "Category" tipinde ve ICategoryRepository den kalýtým alýnýr.
				Icerisine Db baðlantýsý tanýmlanýr.

	3.3 "UnitOfWork" klasörü açýlýr.
		3.1. "UnitOfWork" classý açýlýr. UnitOfWork için gerekli iþlemler yapýlýr.
			"IUnitOfWork" interface implement edilir. 

	3.4. "Context" folderý açýlýr.
		3.3.1. "ApplicationDbContext.cs" classý oluþturulur. Db baðlantýsý için gerekli iþlemler yapýlýr.
		3.3.2. API katmanýnda bulunan Appsettings.Json dosyasýna Db baðlantý adresi eklenmesi yapýlýr.

4. "MarketPlace.API" ismiyle ASP.Net Core Web App projesi eklenir.
	
	Yüklenecek Paketler :	-Autofac(6.1.0)
							-Autofac.Extensions.DependenceyInjection(7.1.0)
							-AutoMapper(10.1.1)
							-AutoMapper.Extensions.Microsoft.DependencyInjection(8.1.1)
							-Microsoft.AspNetCore.Identity.EntityFrameworkcore(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore.Design(5.0.7)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)
													

	Referans Proje		:	-"ECommerceApp.DomainLayer"
							-"ECommerceApp.InfrastructureLayer"

	Not: 4 üncü maddedeki iþlemler bitmeyi müteakip 
			Program.cs, -AutoFactContainer için 
			Startup.cs, - DbContext için
			Appsetting.Json - Db adresi için düzenlenir.

	4.1. "Model" Folderý açýlýr.
		4.1.1. DTOs Fodlerý açýlýr. Iþ odaklý olarak kullaným ihtiyacýna göre DTO lar oluþtuýrulur.

	4.2. "Mapper" Folderý açýlýr.
		4.2.1. "Mapping" class ý açýlýr. ve DTO larýn oluþturulduðu varlýklarý ile maplama iþlemi yapýlýr. AutoMApper Paketinden yararlanmak için "Profile" dan kalýtým alýnýr.

			public Mapping()
			{
				CreateMap<Product, ProductDTO>().ReverseMap();
				CreateMap<ProductDTO, Product>().ReverseMap();

				CreateMap<Category, CategoryDTO>().ReverseMap();
				CreateMap<CategoryDTO, Category>().ReverseMap();
			}

	4.3. "Services" Folderý açýlýr. 
		4.3.1. "Interface" Folderý açýlýr.
			
			4.3.1.1. "ICategoryService" interface i açýlýr ve içine "CategoryService" de gövdelendirilecek olan ve "Category" ile ilgili kullanýlmak istenilen Methodlar tanýmlanýr.

			4.3.1.3. "IProductService" interface i açýlýr ve içine "ProductService" de gövdelendirilecek olan ve "Product" ile ilgili kullanýlmak istenilen Methodlar tanýmlanýr.

		4.3.2. "Concrete" Folderý açýlýr.
			4.3.2.1. "CategoryService" class ý açýlýr. "ICategoryService" interface i implement edilir. ve Methodlar gövdelendirilir. 
			4.3.2.2. "ProductService" class ý açýlýr. "IProductService" interface i implement edilir. ve Methodlar gövdelendirilir. 

	4.4. "IOC" folderý açýlýr. 	Third PArt IOC container kullanmak istediðim için normalde Startup classýnda yapýlmasý gereken IOC container iþlemini burada "AutoFact Container ile gerçekleþtiricem.
		4.4.1. "AutoFactContainer" classý açýlýr. 
			public class AutoFactContainer:Module
			{
				protected override void Load(ContainerBuilder builder) 
				{
					builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
					builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
					builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();           
				}
			}

	 4.5. Controller folderý açýlýr.
		
		4.5.1. ProductController (API Controller-Empty) eklenir.
				
				IProductService inject edilir ve productservis kýsmýnda uygulayama kazandýrýlan yetekenler kullanýlýr.

				Ürün Listeleme, Ekleme, Güncelleme ve Silme.
				
		4.5.2.  CategoryController (API Controller-Empty) eklenir.
			
				ICategoryService inject edilir ve categoryservis kýsmýnda uygulayama kazandýrýlan yetekenler kullanýlýr.
				
				Category Listeleme, Ekleme, Güncelleme ve Silme

5. "MarketPlace.Host" ismiyle ASP.Net Core Web Application(MVC) projesi eklenir.

	5.1. Model.cs içerisinde Wep-API'dan talep edilecek varlýklar oluþturulur.
		5.1.1. Product.cs
		5.1.2. Category.cs

	5.2. Controllerlar oluþturuldu.
		5.2.1. ProductController.cs
		5.2.2. CategoryController.cs

	5.3. View ler oluþturuldu. Controllerlar da oluþturulan Methodlarýn Viewlarý düzenlendi.
	
	5.4. NavBarPartial Ve LeftSideBar eklendi.


