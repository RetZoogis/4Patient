using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FourPatient.WebAPI.Controllers;
using Xunit;
using FourPatient.Domain;
using FourPatient.Domain.Tables;
using FourPatient.DataAccess.Entities;
using FourPatient.DataAccess;
using FourPatient.WebAPI.Models;
using Moq;
using Microsoft.Extensions.Options;
using System.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XUnitTests
{
    public class UnitTest
    {
        private readonly DbContextOptions<_4PatientContext> options;

        private _4PatientContext context;

        public UnitTest()
        {
            //IServiceCollection services = new ServiceCollection();

            //services.AddScoped<IHospital, HospitalRepo>();
            //services.AddScoped<IPatient, PatientRepo>();
            //services.AddScoped<IReview, ReviewRepo>();
            //services.AddScoped<IAccommodation, AccommodationRepo>();
            //services.AddScoped<ICleanliness, CleanlinessRepo>();
            //services.AddScoped<ICovid, CovidRepo>();
            //services.AddScoped<INursing, NursingRepo>();

            // services.AddDbContext<_4PatientContext>(x =>
            //     x.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName + "/FourPatient.WebAPI/")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DbConnection");

            options = new DbContextOptionsBuilder<_4PatientContext>()
                .UseSqlServer(connectionString)
                .Options;

            context = new _4PatientContext(options);
        }
            /*
            // create some mock products to play with
            IList<Product> products = new List<Product>
                {
                    new Product { ProductId = 1, Name = "C# Unleashed",
                        Description = "Short description here", Price = 49.99 },
                    new Product { ProductId = 2, Name = "ASP.Net Unleashed",
                        Description = "Short description here", Price = 59.99 },
                    new Product { ProductId = 3, Name = "Silverlight Unleashed",
                        Description = "Short description here", Price = 29.99 }
                };

            // Mock the Products Repository using Moq
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            // Return all the products
            mockProductRepository.Setup(mr => mr.FindAll()).Returns(products);

            // return a product by Id
            mockProductRepository.Setup(mr => mr.FindById(
                It.IsAny<int>())).Returns((int i) => products.Where(
                x => x.ProductId == i).Single());

            // return a product by Name
            mockProductRepository.Setup(mr => mr.FindByName(
                It.IsAny<string>())).Returns((string s) => products.Where(
                x => x.Name == s).Single());

            // Allows us to test saving a product
            mockProductRepository.Setup(mr => mr.Save(It.IsAny<Product>())).Returns(
                (Product target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.ProductId.Equals(default(int)))
                    {
                        target.DateCreated = now;
                        target.DateModified = now;
                        target.ProductId = products.Count() + 1;
                        products.Add(target);
                    }
                    else
                    {
                        var original = products.Where(
                            q => q.ProductId == target.ProductId).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.Name = target.Name;
                        original.Price = target.Price;
                        original.Description = target.Description;
                        original.DateModified = now;
                    }

                    return true;
                });

            // Complete the setup of our Mock Product Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Our Mock Products Repository for use in testing
        /// </summary>
        public readonly IProductRepository MockProductsRepository;

        /// <summary>
        /// Can we return a product By Id?
        /// </summary>
        [TestMethod]
        public void CanReturnProductById()
        {
            // Try finding a product by id
            Product testProduct = this.MockProductsRepository.FindById(2);

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual("ASP.Net Unleashed", testProduct.Name); // Verify it is the right product
        }*/
        /*
        [Fact]
        public void Test0()
        {
            // Arrange
            var testTable = new FourPatient.Domain.Tables.Patient();
            var testEntity = new FourPatient.DataAccess.Entities.Patient();

            var context = new Mock<_4PatientContext>();
            var dbSetMock = new Mock<DbSet<FourPatient.DataAccess.Entities.Patient>>();
            context.Setup(x => x.Set<FourPatient.DataAccess.Entities.Patient>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<FourPatient.DataAccess.Entities.Patient>())).Returns(testEntity);

            // Act
            var repository = new PatientRepo(context.Object);
            repository.Create(testTable);

            //Assert
            context.Verify(x => x.Set<FourPatient.DataAccess.Entities.Patient>());
            dbSetMock.Verify(x => x.Add(It.Is<FourPatient.DataAccess.Entities.Patient>(y => y == testEntity)));

        }
        */
        [Fact]
        public void Test1()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DbConnection");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName + "/FourPatient.WebAPI/")
                .AddJsonFile("appsettings.Development.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DbConnection");

            DbContextOptions<_4PatientContext> options = new DbContextOptionsBuilder<_4PatientContext>()
                .UseSqlServer(connectionString)
                .Options;

            using (var context = new _4PatientContext(options))
            {
                PatientRepo P = new PatientRepo(context);
                string result = P.Get(1).FirstName;
                Assert.Equal("Jorge", result);
            }
        }

        [Fact]
        public void Test2()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);

            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName + "/FourPatient.WebAPI/")
            //    .AddJsonFile("appsettings.Development.json")
            //    .Build();

            //string connectionString = configuration.GetConnectionString("DbConnection");

            //DbContextOptions<_4PatientContext> options = new DbContextOptionsBuilder<_4PatientContext>()
            //    .UseSqlServer(connectionString)
            //    .Options;

            //_4PatientContext context = new _4PatientContext(options);

            HospitalRepo H = new HospitalRepo(context);
            string result = H.Get(1).Name;
            Assert.Equal("Blue Hospital", result);
        }

        [Fact]
        public void Test3()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            PatientRepo P = new PatientRepo(context);
            HospitalRepo H = new HospitalRepo(context);
            ReviewRepo R = new ReviewRepo(context, P, H);
            int result = R.Get(3).PatientId;
            Assert.Equal(3, result);
        }

        [Fact]
        public void Test4()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            PatientRepo P = new PatientRepo(context);
            HospitalRepo H = new HospitalRepo(context);
            ReviewRepo R = new ReviewRepo(context, P, H);
            AccommodationRepo A = new AccommodationRepo(context, R);
            int? result = A.Get(4).Room;
            Assert.Equal(5, result);
        }
        [Fact]
        public void Test5()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            PatientRepo P = new PatientRepo(context);
            HospitalRepo H = new HospitalRepo(context);
            ReviewRepo R = new ReviewRepo(context, P, H);
            CleanlinessRepo C = new CleanlinessRepo(context,R);
            int? result = C.Get(1).Bathroom;
            Assert.Equal(4, result);
        }
        [Fact]
        public void Test6()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            PatientRepo P = new PatientRepo(context);
            HospitalRepo H = new HospitalRepo(context);
            ReviewRepo R = new ReviewRepo(context, P, H);
            CovidRepo C = new CovidRepo(context, R);
            int? result = C.Get(2).Safety;
            Assert.Equal(2, result);
        }
        [Fact]
        public void Test7()
        {
            //DbContextOptionsBuilder<_4PatientContext> optionsBuilder = new DbContextOptionsBuilder<_4PatientContext>();
            //optionsBuilder.UseInMemoryDatabase("DB");
            //_4PatientContext context = new _4PatientContext(optionsBuilder.Options);
            PatientRepo P = new PatientRepo(context);
            HospitalRepo H = new HospitalRepo(context);
            ReviewRepo R = new ReviewRepo(context, P, H);
            NursingRepo N = new NursingRepo(context, R);
            int? result = N.Get(1).WaitTimes;
            Assert.Equal(4, result);
        }

    }
}
