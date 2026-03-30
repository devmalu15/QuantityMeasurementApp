using QuantityMeasurementModelLayer.DTO;
 
namespace QuantityMeasurementBusinessLayer.Interfaces;
 
public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDTO dto);
    Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
}
