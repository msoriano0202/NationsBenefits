﻿namespace NationsBenefits.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
