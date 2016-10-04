using System.ComponentModel;
using System.Runtime.CompilerServices;
using PropertyChanged;
using SQLite;

namespace WineTracker.Models
{
    [ImplementPropertyChanged]
    [Table("BottleOccasions")]
    public class BottleOccasions 
    {
        #region Properties
        [NotNull]
        [MaxLength(10)]
        public string Code { get; set; }

        [NotNull]
        [MaxLength(50)]
        public string Occasion { get; set; }
  
        #endregion
    }
}