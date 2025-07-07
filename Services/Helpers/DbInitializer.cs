using BusinessObjects.Models;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class DbInitializer
    {
        private readonly AbsoluteCinemaContext _context;
        private readonly IHashPasswordService _hasher;

        public DbInitializer(AbsoluteCinemaContext context, IHashPasswordService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public void Seed()
        {
            // Apply pending migrations
            _context.Database.Migrate();
            // Test password seeding 
            string test = "test123";
            string hashedPassword = _hasher.HashPassword(test);
            bool verifyPassword = _hasher.VerifyPassword(test, hashedPassword);
            Console.WriteLine($"Hashed password for '{test}': {hashedPassword}");

        }
    }
}
