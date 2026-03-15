using Microsoft.Data.SqlClient;
using QuantityMeasurementModelLayer.Entities;
using QuantityMeasurementRepositoryLayer.Interfaces;

namespace QuantityMeasurementRepositoryLayer.Repositories;

public class QuantityMeasurementSqlRepository : IQuantityMeasurementRepositorySql
{
    private readonly string _connectionString = "Server=LAPTOP-MSP0ESL1\\SQLEXPRESS;Database=QuantityMeasurementDB;Trusted_Connection=true;TrustServerCertificate=true;";

    public void Save(QuantityMeasurementEntity entity)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("INSERT INTO QuantityMeasurements (Operation, Operand1, Operand2, Result) VALUES (@Operation, @Operand1, @Operand2, @Result)", connection);
            command.Parameters.AddWithValue("@Operation", entity.Operation);
            command.Parameters.AddWithValue("@Operand1", entity.Operand1);
            command.Parameters.AddWithValue("@Operand2", entity.Operand2);
            command.Parameters.AddWithValue("@Result", entity.Result);
            command.ExecuteNonQuery();
        }
    }

    public List<QuantityMeasurementEntity> GetAll()
    {
        var entities = new List<QuantityMeasurementEntity>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT Operation, Operand1, Operand2, Result FROM QuantityMeasurements", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    entities.Add(new QuantityMeasurementEntity(
                        reader.GetString(0),
                        reader.GetDouble(1),
                        reader.GetDouble(2),
                        reader.GetString(3)
                    ));
                }
            }
        }
        return entities;
    }
}