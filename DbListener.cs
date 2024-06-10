using MudBlazor.Services;
using Npgsql;

namespace PumpDashboard.Service
{
    public delegate void DbEventHandler(object sender, EventArgs e);
    public class DataEventArgs : EventArgs
    {
        public string Data { get; }

        public DataEventArgs(string data)
        {
            Data = data;
        }
    }
    public class DbListener
    {
        public event EventHandler<DataEventArgs> DbEventHandler;

        private string _connectionString = "Server=192.168.0.178; Port=7000; Database=PumpDB; User ID=postgres; Password=mplex1234;";

        protected virtual void DbListen(string data)
        {
            DbEventHandler?.Invoke(this, new DataEventArgs(data));
        }
        public async void BackgroundService()
        {
            CancellationToken cancellationToken = new();

            try
            {
                using (var conn = new NpgsqlConnection(_connectionString))
                {
                    await conn.OpenAsync();

                    conn.Notification += async (o, e) =>
                    {
                        if (e.Channel == "pump_list_update")
                        {
                            DbListen("pump_list_update");
                        }

                        switch (e.Channel)
                        {
                            case "pump_list_update":
                                DbListen("pump_list_update");
                                break;

                            case "machine_status_update":
                                DbListen("machine_status_update");
                                break;
						}
                    };

                    using (var cmd = new NpgsqlCommand("LISTEN pump_list_update; ", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }

					using (var cmd = new NpgsqlCommand("LISTEN machine_status_update; ", conn))
					{
						await cmd.ExecuteNonQueryAsync();
					}


					while (!cancellationToken.IsCancellationRequested)
                    {
                        conn.Wait();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
