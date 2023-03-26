using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Kitaplar.Data;

public class Genre
{
    public int Id { get; set; }

    [Display(Name = "Tür Adı")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [MinLength(3, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
    public required string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();

}
