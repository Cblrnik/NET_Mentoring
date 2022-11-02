using System;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            if (userId < 0)
                throw new ArgumentOutOfRangeException("Invalid userId", new Exception());

            var user = _userDao.GetUser(userId);
            if (user == null)
                throw new ArgumentNullException("User not found", new Exception());

            var tasks = user.Tasks;
            foreach (var t in tasks)
            {
                if (string.Equals(task.Description, t.Description, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("The task already exists", new Exception());
            }

            tasks.Add(task);
        }
    }
}