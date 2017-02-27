namespace Nop.Core.Domain.Catalog
{
    /// <summary>
    /// Represents a method of inventory management
    /// 我的注释: 在 LocaleStringResource 表中保存的值为 (调用Nop.Services.Extensions ToSelectList会使用localized值, key为Enums.Nop.Core.Domain.Catalog.ManageInventoryMethod.DontManageStock)
    /// Don't track inventory
    /// Track inventory
    /// Track inventory by product attributes
    /// </summary>
    public enum ManageInventoryMethod
    {
        /// <summary>
        /// Don't track inventory for product
        /// </summary>
        DontManageStock = 0,
        /// <summary>
        /// Track inventory for product
        /// </summary>
        ManageStock = 1,
        /// <summary>
        /// Track inventory for product by product attributes
        /// </summary>
        ManageStockByAttributes = 2,
    }
}
