using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aspcore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Aspcore.Tests.Provides
{
    public class TestContextProvider : ITestContextProvider
    {
        private DatnekContext _datnekContext;

        public async Task<DatnekContext> GetDatabaseContext()
        {

            var options = new DbContextOptionsBuilder<DatnekContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _datnekContext = new DatnekContext(options);
            _datnekContext.Database.EnsureCreated();
            return await Task.Run(() => _datnekContext);
        }
    }
}
