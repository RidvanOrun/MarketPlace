using MarketPlace.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.DomainLayer.Entities.Interface
{
    public interface IBaseEntity
    {
        DateTime CreateDate { get; set; }
        DateTime? DeleteDate { get; set; }
        Status Status { get; set; }

    }
}
