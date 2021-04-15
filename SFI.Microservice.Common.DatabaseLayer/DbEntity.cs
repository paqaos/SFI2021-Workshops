using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SFI.Microservice.Common.DatabaseLayer
{
    public abstract class DbEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
