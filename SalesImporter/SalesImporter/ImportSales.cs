using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SalesImporter
{
    public class ImportSales
    {
        private readonly ILogger<ImportSales> _logger;
        private readonly IConfiguration _configuration;


        public ImportSales(ILogger<ImportSales> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Function(nameof(ImportSales))]
        public async Task Run([BlobTrigger("incoming/{name}", Connection = "StorageConnection")] Stream stream, string name, FunctionContext ctx)
        {
            var log = ctx.GetLogger<ImportSales>();
            log.LogInformation($"Importing sales data from blob: {name}");
            // Here you would add your logic to process the sales data from the stream.
            // For example, you could read the stream and parse the sales data.
            // Simulate processing time

            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            log.LogInformation($"Processed sales data: {content}");

            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var sales = System.Text.Json.JsonSerializer.Deserialize<List<Sale>>(content, options);

            //2. Import each record into the database (this is just a placeholder, implement your own logic)

            var connectionString =_configuration["SqlConnection"];

            if (!string.IsNullOrEmpty(connectionString) && sales !=null)
            {
                using var db = new SqlConnection(connectionString);
                
                await AddData(sales, db, name);
            }
        }

        private async Task AddData(List<Sale> sales, SqlConnection db,string name)
        {
            db.Open();

            foreach (var sale in sales)
            {
                var cmd = new SqlCommand(
     "INSERT INTO Sales (OrderId, Product, Amount, SaleDate) " +
     "VALUES (@OrderId, @Product, @Amount, @SaleDate)", db);

                cmd.Parameters.AddWithValue("@OrderId", sale.OrderId);
                cmd.Parameters.AddWithValue("@Product", sale.Product);
                cmd.Parameters.AddWithValue("@Amount", sale.Amount);
                cmd.Parameters.AddWithValue("@SaleDate", sale.SaleDate);

                await cmd.ExecuteNonQueryAsync();
            }

            _logger.LogInformation($"Finished importing sales data from blob: {name}");
        }
    }
}
