using Zenject;

namespace CodeBase.Infrastructure.DiExtension
{
    public static class DiExtension
    {
        public static ConcreteIdArgConditionCopyNonLazyBinder RegisterService<TInterface, TRealisation>(this DiContainer container) where TRealisation : TInterface
        {
            return container.Bind<TInterface>().To<TRealisation>().AsSingle();
        }
    }
}