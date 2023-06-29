using ProjetoFinal.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Leito
    {
        public int Id { get; set; }

        [Display(Name = "Código da Sala")]
        [Required(ErrorMessage = "{0} é requerido")]
        [StringLength(6, MinimumLength = 3,
            ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        public StatusLeito Status { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        [Display(Name = "Tipo do Leito")]
        public TipoLeito TipoLeito { get; set; }

        public Solicitacao? Solicitacao { get; set; }

        public Leito() {}

        public Leito(int id, string codigo, StatusLeito status, TipoLeito tipoLeito, Solicitacao? solicitacao)
        {
            Id = id;
            Codigo = codigo;
            Status = status;
            TipoLeito = tipoLeito;
            Solicitacao = solicitacao;
        }
    }
}
