using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.API.Integracao.DTOs
{
    public class {{_Entity}}DTO
    {
        [Key]
        [SwaggerSchema(Description = "Chave primária de {{_Entity}} no ERP, esta chave deve identificar de forma única o registro.")]
        [Required(ErrorMessage = "Chave{{_Entity}} é obrigatório")]
        [MaxLength(60)]
        public string Chave{{_Entity}} { get; set; }
    }
}
