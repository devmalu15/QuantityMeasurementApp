namespace OperationService.Models;
public class ConvertRequest
{
    public QuantityDTO Input      { get; set; } = new();
    public string      TargetUnit { get; set; } = "";
}
