﻿using System;
using System.Collections.Generic;
using System.Text;
using Pasat.Models;

namespace Pasat.Logic
{
    static class ReactiveFactory
    {
        public static event EventHandler UserListChanged;
        public static event EventHandler<User> UserChanged;

        public static void ChangeUser(User user)
        {
            UserChanged?.Invoke(null, user);
        }

        public static void ChangeUserList()
        {
            UserListChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
