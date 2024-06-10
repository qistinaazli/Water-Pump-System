namespace PumpDashboard.Service
{
    public interface IDBHelper
    {
        /// <summary>
        /// Get data list from database using query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Cmd"></param>
        /// <returns></returns>
        public Task<List<T>> Fetch<T>(string Cmd);
        public Task<bool> Update(string Cmd);
        public string GetString();
    }
}
