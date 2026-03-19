 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace QuantityMeasurementModelLayer.Entities
{
    [Table("QuantityMeasurements")]   // maps this class to the QuantityMeasurements table
    public class QuantityMeasurementEntity
    {
        [Key]                                                    // primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    // auto-increment
        public int Id { get; set; }
 
        [Required]
        [MaxLength(20)]
        public string Operation { get; set; }
 
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
 
        [Required]
        [MaxLength(50)]
        public string Result { get; set; }
 
        // Required for Redis JSON deserialization — do not remove
        public QuantityMeasurementEntity() { }
 
        public QuantityMeasurementEntity(string operation, double op1, double op2, string result)
        {
            Operation = operation;
            Operand1  = op1;
            Operand2  = op2;
            Result    = result;
        }
    }
}
