﻿using Microsoft.EntityFrameworkCore;
using SGHServer.Application.Interfaces;
using SGHServer.Domain;

namespace SGHServer.Persistence;

public class DataStore : DbContext, IDataStore
{
    public DbSet<User> Users { get; set; }
    
    public DataStore(DbContextOptions<DataStore> options) 
        : base(options) { }
    
}