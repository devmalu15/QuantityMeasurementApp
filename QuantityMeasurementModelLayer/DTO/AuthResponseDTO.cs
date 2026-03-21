namespace QuantityMeasurementModelLayer.DTO;
public class AuthResponseDTO
{
    public string Token   { get; set; }
    public string Email   { get; set; }
    public DateTime Expiry { get; set; }
}
