using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGastosResidenciais.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedAt { get; private set; }


        public void SoftDelete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }

    // escolhi utilizar a  entidade base  para centralizar propriedades comuns as entidades, facilitando a manutenção e evitando repetição de código.
}