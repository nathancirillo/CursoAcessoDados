namespace CursoAcessoDados.Models
{
    public class Carrer
    {
        public Guid Id { get; set; }   
        public string Title { get; set; }
        public IList<CarrerItem> Items { get; set; }  

        public Carrer()
        {
            Items = new List<CarrerItem>();
        }
    }
}