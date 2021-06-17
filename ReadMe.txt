
Projenin �zeti; 
				- ASP.NET CORE ile �r�nlerin ve kategorilerin listenebilece�i, kay�t ekleme, silme, g�ncelleme yap�labilece�i bir Web API ve bu Web API�dan verileri alarak g�rselle�tirme yap�lacak bir web sitesi tasarlanm��t�r.

				- Proje DDD temeliyle yap�lm��t�r. Veri Taban� i�lemleri EntityFramework Core ve Microsoft SQL server ile yap�lm��t�r.

				- "MarketPlace.DomainLayer"(Class Library); Varl�klar olu�turulmu�tur, Generic Repository Design Pattern ve Unit Of Work'�n temelleri at�lm��t�r.

				- "MarketPlace.InfastructureLayer"(Class Library); Db ile ba�lant� amac�yla ApplicationDBContext olu�turulmu�, Varl�klar�n DB ye Mapping i�lemleri yap�lm��t�r. Repository ler ve Unit Of Work g�vdelendirilmi�tir.

				- "MarketPlace.API"(ASP.Net Core Web App); ihtiya�lar do�rultusunda DTO lar olu�turulmu� ve AutoMapper kullanm�n� i�in mapping i�lemi yap�lm��t�r. Services ler olu�turulmu�tur. Product ve Category Controller olu�turulmu�tur.

				- "MarketPlace.Host"(ASP.Net Core Web Application(MVC)); Model folder�na WEP-API'dan talep edilecek varl�klar olu�turuldu. Controllar olu�turuldu. Ve View k�s�mlar� d�zenlendi. NavBarPartial ve LeftBarPartial Eklendi.



1. "MarketPlace" ismiyle Blank Solution a��l�r.

2. "MarketPlace.DomainLayer" ismiyle Class Library (.Core 3.1. version) projesi eklenir.
		DomainLayer : Bu katman, uygulaman�n kalbidir. entities, value objects, domain services and domain events.Varl�klar-entities, de�er nesneleri-value object, etki alan� hizmetleri ve etki alan� olaylar�ndan- domain services and domain events olu�ur. Domain katman�nda i� s�re�lerinin sim�le edilmesine odaklan�l�r.

	Y�klenecek Paketler :	-Microsoft.AspNetCore.Http.Features(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.Extensions.Identity.Stores(5.0.7)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)

	2.1. "Enums" dosyas� a��l�r.
		2.1.1. "Status" Class � eklenir. 
			- Active olan silinen ve g�ncelenen �r�nlerin beliritlmesi amac�yla tutulmu�tur.  

	2.2. "Entities" dosyas� a��l�r. �htiya� duyulan varl�klar olu�turulmaya ba�lan�r.
		2.2.1. Interface dosyas� a��l�r.
			2.2.1.1. "IBaseEntity" Class� olu�turulur.
		2.2.2. Concrete dosyas� a��l�r.		
			2.2.2.1. BaseEntity.cs
				
			2.2.2.2. Product.cs
				"BaseEntity<int>" clas�ndan kal�t�m al�n�r.				
			2.2.2.3. Category.cs
				"BaseEntity<int>" clas�ndan kal�t�m al�n�r.
	
	2.3. "Repository" dosyas� olu�turulur.
		2.3.1. "BaseRepository" klas�r� a��l�r.
			2.3.1.1. "IBaseRepository" interface i olu�turulur ve repositoryler yaz�lmaya ba�lan�r.

				public interface IBaseRepository<T> where T:IBaseEntity
					{
					Task<List<T>> Get(Expression<Func<T, bool>> expression);

					Task<T> FirstOrDefault(Expression<Func<T, bool>> expression);

					Task Add(T entity);

					void Update(T entity);
					void Delete(T entity);
					}

		2.3.2. "EntityRepository" klas�r� a��l�r.
			2.3.2.1. "ICategoryRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Category" tipinde kal�t�m al�n�r.
			2.3.2.2. "IProductRepository" interface i a��l�r.
				"IBaseRepository" interface inden "Product" tipinde kal�t�m al�n�r.

	2.4. "UnitOfWork" dosyas� olu�turulur.
		2.4.1. "IUnitOfWork" Dosyas� Olu�turulur.

			 public interface IUnitOfWork:IAsyncDisposable
				{
				IProductRepository ProductRepository { get; }
				ICategoryRepository CategoryRepository { get; }

				Task Commit(); //Ba�aral� bir i�lememin sonucunda t�m de�i�ikliklerin veri taban�na kaydolmas�n� sa�lar.
				}
		
3. "MarketPlace.InfastructureLayer" ismiyle Class Library (.Core) projesi eklenir.
		InfastructureLayer : Bu katman; teknolojiye �zel kararlara odaklan�l�r ama�tan ziyade implementasyon k�sm� ile ilgilenilir.Bu katmanda domainlerin instancelar� yarat�labilir.Ancak genellikle repositoryler bu katmanda etkile�im i�erisinde olurlar. Veri taban�, mesajla�ma sistemleri, email servisleri gibi d�� servislere eri�ilen katman olacakt�r.

	Y�klenecek Paketler :	-Microsoft.AspNetCore.Identity.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore.SqlServer(5.0.7)
							-Microsoft.EntityFrameworkCore.Tools(5.0.7)

	Referans Proje		:	-"ECommerceApp.DomainLayer"

	3.1. "Mapping" klas�r a��l�r. 
		3.1.1. "Abstract" klas�r� a��l�r.
			3.1.1.1. "BaseMap" class � a��l�r. 

				public abstract class BaseMap<T>:IEntityTypeConfiguration<T> where T : class, IBaseEntity
				{
					public virtual void Configure(EntityTypeBuilder<T> builder) 
					{
						builder.Property(x => x.CreateDate).IsRequired(true);
        
					}
				}
		3.1.2. "Concrete" klas�r� a��l�r.
						
			3.1.2.1. "ProductMap" class � a��l�r.
				"BaseMap" abstract class �ndan "Product" tipinde kal�t�m al�n�r.
			3.1.2.2. "CategoryMap" class � a��l�r.
				"BaseMap" abstract class �ndan "Category" tipinde kal�t�m al�n�r.

	3.2. "Repository" klas�r� a��l�r.
		3.2.1. "BaseRepository" folder� a��l�r
			3.2.1.1. "BaseRepository" abstract class � olu�turulur.
				"IBaseRepository" interface inden T Type olarak kal�t�m al�r. Ve interfacein i�erisinde tan�mlanm�� methodlar g�vdelendirilir.
		3.2.2. "EntityTypeRepositor" folder � a��l�r
			3.2.2.1. "ProductRepository" calas i a��l�r.
				"BaseRepository"den "Product" tipinde ve IProductRepository den kal�t�m al�n�r.
				Icerisine Db ba�lant�s� tan�mlan�r.

				public class ProductRepository : BaseRepository<Product>, IProductRepository
				{
					public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
					{}

				}
			3.2.2.2. "CategoryRepository" interface i a��l�r.
				"BaseRepository"den "Category" tipinde ve ICategoryRepository den kal�t�m al�n�r.
				Icerisine Db ba�lant�s� tan�mlan�r.

	3.3 "UnitOfWork" klas�r� a��l�r.
		3.1. "UnitOfWork" class� a��l�r. UnitOfWork i�in gerekli i�lemler yap�l�r.
			"IUnitOfWork" interface implement edilir. 

	3.4. "Context" folder� a��l�r.
		3.3.1. "ApplicationDbContext.cs" class� olu�turulur. Db ba�lant�s� i�in gerekli i�lemler yap�l�r.
		3.3.2. API katman�nda bulunan Appsettings.Json dosyas�na Db ba�lant� adresi eklenmesi yap�l�r.

4. "MarketPlace.API" ismiyle ASP.Net Core Web App projesi eklenir.
	
	Y�klenecek Paketler :	-Autofac(6.1.0)
							-Autofac.Extensions.DependenceyInjection(7.1.0)
							-AutoMapper(10.1.1)
							-AutoMapper.Extensions.Microsoft.DependencyInjection(8.1.1)
							-Microsoft.AspNetCore.Identity.EntityFrameworkcore(5.0.7)
							-Microsoft.EntityFrameworkCore(5.0.7)
							-Microsoft.EntityFrameworkCore.Design(5.0.7)
							-Microsoft.VisualStudio.Web.CodeGeneration.Design(3.1.5)
													

	Referans Proje		:	-"ECommerceApp.DomainLayer"
							-"ECommerceApp.InfrastructureLayer"

	Not: 4 �nc� maddedeki i�lemler bitmeyi m�teakip 
			Program.cs, -AutoFactContainer i�in 
			Startup.cs, - DbContext i�in
			Appsetting.Json - Db adresi i�in d�zenlenir.

	4.1. "Model" Folder� a��l�r.
		4.1.1. DTOs Fodler� a��l�r. I� odakl� olarak kullan�m ihtiyac�na g�re DTO lar olu�tu�rulur.

	4.2. "Mapper" Folder� a��l�r.
		4.2.1. "Mapping" class � a��l�r. ve DTO lar�n olu�turuldu�u varl�klar� ile maplama i�lemi yap�l�r. AutoMApper Paketinden yararlanmak i�in "Profile" dan kal�t�m al�n�r.

			public Mapping()
			{
				CreateMap<Product, ProductDTO>().ReverseMap();
				CreateMap<ProductDTO, Product>().ReverseMap();

				CreateMap<Category, CategoryDTO>().ReverseMap();
				CreateMap<CategoryDTO, Category>().ReverseMap();
			}

	4.3. "Services" Folder� a��l�r. 
		4.3.1. "Interface" Folder� a��l�r.
			
			4.3.1.1. "ICategoryService" interface i a��l�r ve i�ine "CategoryService" de g�vdelendirilecek olan ve "Category" ile ilgili kullan�lmak istenilen Methodlar tan�mlan�r.

			4.3.1.3. "IProductService" interface i a��l�r ve i�ine "ProductService" de g�vdelendirilecek olan ve "Product" ile ilgili kullan�lmak istenilen Methodlar tan�mlan�r.

		4.3.2. "Concrete" Folder� a��l�r.
			4.3.2.1. "CategoryService" class � a��l�r. "ICategoryService" interface i implement edilir. ve Methodlar g�vdelendirilir. 
			4.3.2.2. "ProductService" class � a��l�r. "IProductService" interface i implement edilir. ve Methodlar g�vdelendirilir. 

	4.4. "IOC" folder� a��l�r. 	Third PArt IOC container kullanmak istedi�im i�in normalde Startup class�nda yap�lmas� gereken IOC container i�lemini burada "AutoFact Container ile ger�ekle�tiricem.
		4.4.1. "AutoFactContainer" class� a��l�r. 
			public class AutoFactContainer:Module
			{
				protected override void Load(ContainerBuilder builder) 
				{
					builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
					builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
					builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();           
				}
			}

	 4.5. Controller folder� a��l�r.
		
		4.5.1. ProductController (API Controller-Empty) eklenir.
				
				IProductService inject edilir ve productservis k�sm�nda uygulayama kazand�r�lan yetekenler kullan�l�r.

				�r�n Listeleme, Ekleme, G�ncelleme ve Silme.
				
		4.5.2.  CategoryController (API Controller-Empty) eklenir.
			
				ICategoryService inject edilir ve categoryservis k�sm�nda uygulayama kazand�r�lan yetekenler kullan�l�r.
				
				Category Listeleme, Ekleme, G�ncelleme ve Silme

5. "MarketPlace.Host" ismiyle ASP.Net Core Web Application(MVC) projesi eklenir.

	5.1. Model.cs i�erisinde Wep-API'dan talep edilecek varl�klar olu�turulur.
		5.1.1. Product.cs
		5.1.2. Category.cs

	5.2. Controllerlar olu�turuldu.
		5.2.1. ProductController.cs
		5.2.2. CategoryController.cs

	5.3. View ler olu�turuldu. Controllerlar da olu�turulan Methodlar�n Viewlar� d�zenlendi.
	
	5.4. NavBarPartial Ve LeftSideBar eklendi.


