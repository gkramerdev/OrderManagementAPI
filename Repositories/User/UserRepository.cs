﻿using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Infra;
using OrderManagementAPI.Models;

namespace OrderManagementAPI.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;


        }

        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }


       
    }
}
