using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace HistoryService.Models;
 
[Table("QuantityMeasurements")]
public class MeasurementEntity
{
    [Key] public int    Id        { get; set; }
    public string Operation { get; set; } = "";
    public double Operand1  { get; set; }
    public double Operand2  { get; set; }
    public string Result    { get; set; } = "";
    public MeasurementEntity() {}
}
