﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UserDomain;
using UserDomain.Entities;
using UserDomain.Interface;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        { _context = context; }

        public async Task<int> GetByEmailAsync(string email)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();

                await connection.OpenAsync();

                // Create command to check for table
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Users'";

                // Execute scalar query
                var tableExists = await command.ExecuteScalarAsync();


                var response = await _context.Users.CountAsync(x => x.Email == email);
                return response;
            }
            catch (Exception ex)
            {
                // Log the actual database error
                Console.WriteLine($"Database error in GetByEmailAsync: {ex.ToString()}");

                throw; // Re-throw to preserve stack trace
            }
        }


        public async Task<User> GetLoginAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw new DatabaseUnavailableException("DataBase not connected");
            }
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task<int> UpdateDetailsAsync(User user)
        {
            if (user == null)
            {
                throw new BadRequestException("Bad Request");
            }

            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (dbUser is null)
                    throw new NotFoundException("Record Not existed");
                dbUser.Name = user.Name;
                dbUser.Email = user.Email;
                _context.Update(dbUser);
                return await _context.SaveChangesAsync();
          
            }
          
            catch (DbUpdateException ex)
            {
                throw new InternalServerException("Something wrong at backend side");
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Something Wrong");
            }
        }
    }
}
