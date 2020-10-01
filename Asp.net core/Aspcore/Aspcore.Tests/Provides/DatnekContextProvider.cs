using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aspcore.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Aspcore.Tests.Provides
{
    public class DatnekContextProvider
    {

        public static async Task<DatnekContext> GetDatabaseContext()
        {

            var options = new DbContextOptionsBuilder<DatnekContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var phonebookContext = new DatnekContext(options);

            return await Task.Run(() => phonebookContext);
        }

    }
}
