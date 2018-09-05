// ***********************************************************************
// Solution         : Damon.Core
// Project          : Damon.Domain.User.Interface
// File             : IUserRepository.cs
// ***********************************************************************
// <copyright>
//     Copyright © 2016 - 2018 Kolibre Credit Team. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Threading.Tasks;

namespace Circle.Sword.Domain.Identity.Interface
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task DeleteAsync(string userId);

        Task<User> GetUserAsync(string userId);

        Task<User> GetUserByCellphoneAsync(string cellphone);

        Task<User> GetUserByUserNameAsync(string userName);

        Task<User> GetUserByEmailAsync(string email);
    }
}