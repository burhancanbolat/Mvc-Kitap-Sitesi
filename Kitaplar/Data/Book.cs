using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Kitaplar.Data;

public class Book
{

    public int Id { get; set; }

    [Display(Name = "Kitap Adı")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    [MinLength(4, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
    public required string Name { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Görsel")]
    public string? Image { get; set; }

    [Display(Name = "Fiyat")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public decimal Price { get; set; }

    [Display(Name = "YayınEvi")]
    public string? Editör { get; set; }

    [Display(Name = "Yazar")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public string? Writer { get; set; }

    [Display(Name = "Tür")]
    [Required(ErrorMessage = "{0} alanı boş bırakılamaz!")]
    public int GenreId { get; set; }


    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public virtual Genre? Genre { get; set; }



}
