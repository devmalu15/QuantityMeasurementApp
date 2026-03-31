namespace OperationService.Models;
public class MeasurementEntity
{
    public int    Id        { get; set; }
    public string Operation { get; set; } = "";
    public double Operand1  { get; set; }
    public double Operand2  { get; set; }
    public string Result    { get; set; } = "";
    public MeasurementEntity() {}
    public MeasurementEntity(string op, double o1, double o2, string result)
    { Operation=op; Operand1=o1; Operand2=o2; Result=result; }
}
