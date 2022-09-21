namespace CursoAcessoDados.Models
{
    public class CarrerItem
    {
        public Guid Id { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public Course Course { get; set; }
    }
}