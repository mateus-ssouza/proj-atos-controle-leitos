namespace ProjetoFinal.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }

        public Endereco() { }

        public Endereco(int id, string estado, string cidade, string bairro, string cep, string numero, Paciente paciente)
        {
            Id = id;
            Estado = estado;
            Cidade = cidade;
            Bairro = bairro;
            Cep = cep;
            Numero = numero;
            Paciente = paciente;
        }
    }
}
