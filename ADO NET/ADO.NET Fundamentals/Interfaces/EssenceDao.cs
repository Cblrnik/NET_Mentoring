﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.NET_Fundamentals.Interfaces
{
    public abstract class EssenceDao<T>
    {
        protected string connectionString = @"Data Source=EPBYMINW1935;Initial Catalog=Fundamentals;Integrated Security=True";

        public abstract void Create(T entity);

        public abstract T Read(int id);

        public abstract List<T> ReadAll();

        public abstract T Update(T entity);

        public abstract void Delete(T entity);
    }
}
