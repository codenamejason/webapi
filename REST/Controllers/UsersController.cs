
namespace REST.Controllers
{
    using REST.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private static IList<User> Users { get; set; }
        static UsersController()
        {
            UsersController.CreateTestUsers();
        }
        private static void CreateTestUsers()
        {
            UsersController.Users = new List<User>() {
                new User("TU","Tran", 20,"Male"),
                new User("TU 1","Tran", 20,"Male")
            };
        }
        [Route("")]
        [HttpGet()]
        public IList<User> GetUsers()
        {
            return UsersController.Users;
        }

        [HttpGet]
        [Route("{userId}")]
        public User GetUser(Guid userId)
        {
            return UsersController.Users.FirstOrDefault(user => user.Id == userId);
        }

        [HttpPost]
        [Route("")]
        public User CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            UsersController.Users.Add(user);
            return user;
        }
        [HttpPost]
        [Route("{userId}")]
        public void UpdateUser(Guid userId, User user)
        {
            Models.User currentUser = UsersController.Users.FirstOrDefault(item => item.Id == userId);
            currentUser.FirstName = user.FirstName;
            currentUser.LastName = user.LastName;
            currentUser.Age = user.Age;
            currentUser.Gender = user.Gender;
        }

        [HttpDelete]
        [Route("{userId}")]
        public User DeleteUser(Guid userId)
        {
            Models.User deletedUser = UsersController.Users.FirstOrDefault(item => item.Id == userId);
            UsersController.Users.Remove(deletedUser);
            return deletedUser;

        }
        [HttpDelete]
        [Route("")]
        public IList<User> DeleteUsers(IList<Guid> userIds)
        {
            IList<Models.User> deletedUsers = UsersController.Users.Where(item => userIds.Contains(item.Id)).ToList();
            foreach (Models.User item in deletedUsers)
            {
                UsersController.Users.Remove(item);
            }
            return deletedUsers;
        }
    }
}
