using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_StokTakipOtomasyonu.MyModel
{
    public class MyMarkalar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MyMarkalar()
        {
            this.Urunler = new HashSet<MyUrunler>();
        }

        public int ID { get; set; }
        [Required(ErrorMessage ="Kategori alanı boş geçilemez.")]
        public int KategoriID { get; set; }
        [Required(ErrorMessage = "Marka alanı boş geçilemez.")]
        [Display(Name ="Açıklama")]
        public string Marka { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş geçilemez.")]
        public string Aciklama { get; set; }

        public virtual MyKategoriler Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyUrunler> Urunler { get; set; }
    }
}
