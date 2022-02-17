using Cysharp.Threading.Tasks;

public sealed class AsyncExceptionThrower
{
    public async UniTask ThrowAsync(bool throwException)
    {
        /*
         FYI: 
         In Custom Nunit 2.0.2 or earlier,
         UniTask.Yield and UniTask.Delay in Assert.ThrowsAsync will cause a hang of Unity editor.
         */
        // await UniTask.Yield();
        // await UniTask.Delay(10);

        if (throwException)
        {
            throw new AsyncException();
        }

        await UniTask.CompletedTask;
    }
}
