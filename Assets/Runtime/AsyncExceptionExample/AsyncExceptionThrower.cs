using Cysharp.Threading.Tasks;

public sealed class AsyncExceptionThrower
{
    public async UniTask ThrowAsync(bool throwException)
    {
        await UniTask.Yield();

        if (throwException)
        {
            throw new AsyncException();
        }
    }
}
