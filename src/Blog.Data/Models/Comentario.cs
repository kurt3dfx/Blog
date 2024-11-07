using System.ComponentModel.DataAnnotations;

namespace Blog.Data.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Id_post { get; set; }

        [Required]
        public string? Id_usuario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres ")]
        public string? Descricao { get; set; }

        [Required]
        public DateTime DataComentario { get; set; }

        public string? Autor { get; set; }

        public string? Id_autor { get; set; }
    }
}
