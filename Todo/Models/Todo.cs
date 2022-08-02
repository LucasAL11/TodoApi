using System.ComponentModel.DataAnnotations;


namespace Todo.Models
{
    public class TodoModels
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}