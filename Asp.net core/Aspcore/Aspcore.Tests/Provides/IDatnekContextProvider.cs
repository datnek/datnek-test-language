﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aspcore.Migrations;

namespace Aspcore.Tests.Provides
{
    public interface IDatnekContextProvider
    {
        Task<DatnekContext> GetDatabaseContext();
    }
}
