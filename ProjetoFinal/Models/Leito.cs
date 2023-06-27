using ProjetoFinal.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinal.Models
{
    public class Leito
    {
        public int Id { get; set; }

        [Display(Name = "Codigo da Sala")]
        public string Codigo { get; set; }
        public StatusLeito Status { get; set; }

        [Required(ErrorMessage = "{0} é requerido")]
        [Display(Name = "Tipo do Leito")]
        public TipoLeito TipoLeito { get; set; }

        [Display(Name = "Registro da Notificação")]
        public int? IdSolicitacao { get; set; }
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
