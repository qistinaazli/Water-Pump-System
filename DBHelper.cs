using Dapper;
using Npgsql;

namespace PumpDashboard.Service
{
    public class DBHelper : IDBHelper
    {
        private string _connectionString = "Server=192.168.0.178; Port=7000; Database=PumpDB; User ID=postgres; Password=mplex1234;";
        public async Task<List<T>> Fetch<T>(string Cmd)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    return (await conn.QueryAsync<T>(Cmd)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetString()
        {
            return _connectionString;
        }

        public async Task<bool> Update(string Cmd)
        {
            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync(Cmd);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
