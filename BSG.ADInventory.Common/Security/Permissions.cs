namespace BSG.ADInventory.Common.Security
{
    using BSG.ADInventory.Resources;
    using Zcf.Security;

    /// <summary>
    ///   Contains the atomic permissions of the application.
    /// </summary>
    public class Permissions
    {
        #region Management
        public static readonly PermissionItem SystemManagement = new PermissionItem("SystemManagement","مدیریت سیستم", 1, "حوزه راهبری سیستم","تمامی بخش‌ها و آیتم‌های نرم‌افزار بدون هیچ گونه محدودیتی برای کاربر در دسترس خواهد بود .");        
        public static readonly PermissionItem BaseDataManagement = new PermissionItem("BaseDataManagement", "مدیریت اطلاعات پایه", 1, "حوزه راهبری سیستم", "مدیریت کلیه اطلاعات پایه‌ای سیستم در اختیار کاربر خواهد بود. اعم از حذف، ویراش و افزودن اطلاعات.");        
        public static readonly PermissionItem SettingManagement = new PermissionItem("SettingManagement", "مدیریت تنظیمات سیستم", 1, "حوزه راهبری سیستم", "کاربر قادر خواهد بود تنظیمات پایه ای سیستم را ویرایش نماید.");
        public static readonly PermissionItem DatabaseManagement = new PermissionItem("DatabaseManagement", "مدیریت پایگاه داده", 1, "حوزه راهبری سیستم", "کاربر قادر خواهد بود عملیات مربوط به پایگاه داده سیستم را انجام دهد.");
        
        #endregion

        #region security
        public static readonly PermissionItem UserManagement = new PermissionItem("UserManagement", "مدیریت کاربران", 2, "حوزه امنیت", "کاربر توانایی ایجاد/ویرایش/حذف  کاربر و اعطای نقش/حذف نقش به آن را خواهد داشت.");
        public static readonly PermissionItem RoleManagement = new PermissionItem("RoleManagement", "مدیریت  نقش ها", 2, "حوزه امنیت", "کاربر توانایی ایجاد/ویرایش/حذف نقش و اعمال محدودیت های دسترسی به هر نقش را خواهد داشت.");
        #endregion security

        #region Base Data

        //public static readonly PermissionItem GeoLocations = new PermissionItem("GeoLocations", "مکان‌های جغرافیایی", 3,"اطلاعات پایه نرم‌افزار","کاربر امکان ایجاد/ویرایش/حذف اطلاعات مربوط به کشورها، استان‌ها و شهرستان‌ها را خواهد داشت.");        
        //public static readonly PermissionItem BranchManagement = new PermissionItem("ProjectManagement", "مدیریت پروژه‌ها", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش و حذف پروژه‌ها واطلاعات آنها خواهد بود.");
        //public static readonly PermissionItem ContractorManagement = new PermissionItem("InventoryManagement", "مدیریت انبارها", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف انبارها و همچنین مدیریت اطلاعات مربوط به آتها خواهد بود.");
        //public static readonly PermissionItem MatGroupManagement = new PermissionItem("MatGroupManagement", "گروه‌بندی کالا", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف گروهای کالا در دسته بندی های متنوع و اطلاعات فنی مربوط به هر یک خواهد بود. ");
        //public static readonly PermissionItem BrandManagement = new PermissionItem("BrandManagement", "برند کالا و تجهیزات", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف برند یا نام تجاری مربطو به کالا و تجهیزات خواهد بود.");
        //public static readonly PermissionItem MatUnitManagement = new PermissionItem("MatUnitManagement", "واحد شمارش کالا و تجهیزات", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف واحد‌های شمارشی کالا خواهد بود.");        
        //public static readonly PermissionItem CarTypeManagement = new PermissionItem("CarTypeManagement", "انواع خودرو", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف انواع خودروها خواهد بود.");        
        //public static readonly PermissionItem MaterialManagement = new PermissionItem("MaterialManagement", "مدیریت کالاها", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف انواع کالا ، تجهیزات و مواد مصرفی خواهد بود.");
        //public static readonly PermissionItem DeviceManagement = new PermissionItem("DeviceManagement", "دارایی و تجهیزات", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف انواع دارایی و تجهیزات موجود در سازمان خواهد بود.");        
        //public static readonly PermissionItem OrgCarManagement = new PermissionItem("OrgCarManagement", "خودروهای سازمانی", 3, "اطلاعات پایه نرم‌افزار", "کاربر قادر به ایجاد/ویرایش/حذف خودروهای سازمانی ، خودروها بوده و اطلاعات کلیه خودرو های سازمانی را خواهد داشت.");
        
        #endregion

        #region People Management
        //public static readonly PermissionItem EmployeeManagement = new PermissionItem("EmployeeManagement", "مدیریت اشخاص", 4,"مدیریت اشخاص","کاربر قادر به ایجاد/ویرایش/حذف اطلاعات مربوط به پرسنل و اشخاص مرتبط با سازمان می‌باشد.");
        
        #endregion People management

        
        #region Stock In/Out
        #endregion

        #region Inventory
        #endregion Inventory




    }
}