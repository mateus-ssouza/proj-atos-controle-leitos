namespace ProjetoFinal.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public Paciente()
        {
        }

        public Paciente(int id, string nome, DateTime dataNascimento, string cpf, string email, string telefone, Endereco endereco)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            Email = email;
            Telefone = telefone;
            Endereco = endereco;
        }
    }
}
