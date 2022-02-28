using System.ComponentModel.DataAnnotations;

namespace toDo.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; } 
        [Display(Name ="Tarefa")]
        
        [Required]
        public string Name { get; set; }
        [Display(Name="Entregar em")]
        public DateTime EndDate { get; set; }
        [Display(Name="Realizado")]
        public bool Status { get; set; }
    }
}
