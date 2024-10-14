
namespace BSG.ADInventory.Data.Hooks
{
    public interface IHook
    {
        void HookObject(object entity, HookEntityMetadata metadata);
    }
}
