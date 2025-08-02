using Npgsql;

namespace Discount.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger>();
                try
                {
                    logger.LogInformation("Discount Db Migration Started.");
                    ApplyMigration(config);
                    logger.LogInformation("Discount Db Migration Completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
            return host;
        }

        private static void ApplyMigration(IConfiguration config)
        {
            var retry = 5;
            while (retry > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var cmd = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                    ProductName VARCHAR(500) NOT NULL,
                                                    Description TEXT,
                                                    Amount INT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
                    cmd.ExecuteNonQuery();
                    // Exit loop if successful
                    break;
                }
                catch(Exception ex)
                {
                    retry--;
                    if(retry == 0)
                    {
                        throw;
                    }
                }
            }
            
        }
    }
}
