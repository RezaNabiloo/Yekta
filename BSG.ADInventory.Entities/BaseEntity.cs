namespace BSG.ADInventory.Entities
{
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Zcf.Core.Models;

    public abstract class BaseEntity : ICreateTime, ICreateUser, ILastUpdateTime, ILastUpdateUser
    {
        [Display(ResourceType = typeof(Resource), Name = "Id")]
        public long Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CreateUser")]
        public Guid? CreateUserId { get; set; }

        public virtual User CreateUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "LastUpdateUser")]
        public Guid? LastUpdateUserId { get; set; }

        public virtual User LastUpdateUser { get; set; }
    }
}
