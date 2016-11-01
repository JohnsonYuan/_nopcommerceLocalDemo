﻿using System.Data.Entity.ModelConfiguration;
using Nop.Core.Domain.Affiliates;

namespace Nop.Data.Mapping.AffiliateMap
{
    public partial class AffiliateMap : NopEntityTypeConfiguration<Affiliate>
    {
        public AffiliateMap()
        {
            this.ToTable("Affiliate");
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Address).WithMany().HasForeignKey(x => x.AddressId).WillCascadeOnDelete();
        }
    }
}