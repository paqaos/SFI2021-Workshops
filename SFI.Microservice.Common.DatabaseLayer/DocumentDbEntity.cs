using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Microservice.Common.DatabaseLayer
{
    public abstract class DocumentDbEntity<T> : DbEntity
    {
        public string DataType => typeof(T).Name;

        public string Version => "0.0.1";

        public new string Id { get; set; }

        public int RelatedDocumentId { get; set; }
    }
}
