﻿using Microsoft.Data.SqlClient;

namespace SimpleStocks.Repositories
{
    public class BaseRepository
    {
        private readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
