namespace BSG.ADInventory.Entities.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;

    public class InvDocConfig : EntityTypeConfiguration<InvDoc>
    {
        public InvDocConfig()
        {
            this.HasKey(c => c.Id);
            this.Property(c => c.DocNo).HasMaxLength(11).IsRequired();
            this.HasRequired(c => c.InvDocType).WithMany(c => c.InvDocs).HasForeignKey(c => c.InvDocTypeId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.Car).WithMany(c => c.InvDocs).HasForeignKey(c => c.CarId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.PlateCharacter).WithMany(c => c.InvDocs).HasForeignKey(c => c.PlateCharacterId).WillCascadeOnDelete(false);
            this.HasRequired(c => c.Project).WithMany(c => c.InvDocs).HasForeignKey(c => c.ProjectId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.CarType).WithMany(c => c.InvDocs).HasForeignKey(c => c.CarTypeId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.SourceProject).WithMany(c => c.SourceProjectInvDocs).HasForeignKey(c => c.SourceProjectId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.DestProject).WithMany(c => c.DestProjectInvDocs).HasForeignKey(c => c.DestProjectId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.People).WithMany(c => c.InvDocs).HasForeignKey(c => c.PeopleId).WillCascadeOnDelete(false);
            this.HasOptional(c => c.ConfirmUser).WithMany(c => c.ConfirmUsers).HasForeignKey(c => c.ConfirmUserId).WillCascadeOnDelete(false);

            // force operate tor asssign purchasedocId to recived materials
            this.HasOptional(c => c.PurchaseDoc).WithMany(c => c.InvDocs).HasForeignKey(c => c.PurchaseDocId).WillCascadeOnDelete(false);
        }
    }
}