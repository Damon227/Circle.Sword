// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.User
// File             : UserRepository.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Threading.Tasks;
using Circle.Sword.Domain.Identity.Interface;

namespace Circle.Sword.Domain.Identity
{
    public class UserRepository : IUserRepository
    {
        public async Task CreateAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByCellphoneAsync(string cellphone)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}