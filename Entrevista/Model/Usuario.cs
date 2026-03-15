namespace Entrevista.Model {
    public class Usuario {
        public Guid id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime Data { get; set; } = DateTime.Now; // data hora atual
    }
}
