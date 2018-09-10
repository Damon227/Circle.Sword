// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.User
// File             : UserRepository.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System;
using System.Data;
using System.Threading.Tasks;
using Circle.Sword.Domain.Identity.Interface;
using Circle.Sword.Infrastructure.DapperExtensions;
using Damon.Domain.Foundation;
using Microsoft.Extensions.Options;

namespace Circle.Sword.Domain.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly DataOptions _dataOptions;

        public UserRepository(IOptions<DataOptions> dataOptions)
        {
            _dataOptions = dataOptions?.Value ?? throw new ArgumentNullException(nameof(dataOptions));
        }

        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await GetConnection(_dataOptions).InsertAsync(user);
        }

        public async Task DeleteAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            await GetConnection(_dataOptions).DeleteAsync<User>(userId);
        }

        public async Task<User> GetUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await GetConnection(_dataOptions).QueryFirstOrDefaultAsync<User>($"UserId='{userId}'");
        }

        public async Task<User> GetUserByCellphoneAsync(string cellphone)
        {
            if (string.IsNullOrWhiteSpace(cellphone))
            {
                throw new ArgumentNullException(nameof(cellphone));
            }

            return await GetConnection(_dataOptions).QueryFirstOrDefaultAsync<User>($"Cellphone='{cellphone}'");
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return await GetConnection(_dataOptions).QueryFirstOrDefaultAsync<User>($"UserName='{userName}'");
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await GetConnection(_dataOptions).QueryFirstOrDefaultAsync<User>($"Email='{email}'");
        }

        private static IDbConnection GetConnection(DataOptions dataOptions)
        {
            return DbConnectionExtensions.GetConnection(dataOptions);
        }
    }
}