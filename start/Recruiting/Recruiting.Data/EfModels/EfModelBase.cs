using System;

namespace Recruiting.Data.EfModels
{
    public abstract class EfModelBase
    {
        public virtual int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
