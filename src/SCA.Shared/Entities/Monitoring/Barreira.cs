namespace SCA.Shared.Entities.Monitoring
{
    public class Barreira
    {
        public int Id { get; set; }
        public Regiao Regiao { get; set; }
        public int RegiaoId { get; set; }
        public string Descricao { get; set; }
    }
}
