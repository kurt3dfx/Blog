using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public string? Id_usuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ")]
        public string? Descricao { get; set; }

        public DateTime DataPost { get; set; }

        public string? Autor { get; set; }
    }
}
