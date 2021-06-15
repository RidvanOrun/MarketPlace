using MarketPlace.DomainLayer.Entities.Interface;
using MarketPlace.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketPlace.DomainLayer.Entities.Concrete
{
    public class BaseEntity<T> : IBaseEntity
    {
        public T id { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get=>_createDate; set =>_createDate=value; }

        public DateTime? DeleteDate { get; set; }
        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
