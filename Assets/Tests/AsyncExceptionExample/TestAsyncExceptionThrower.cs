using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Cysharp.Threading.Tasks;

public class TestAsyncExceptionThrower
{
    AsyncExceptionThrower _target;

    [SetUp]
    public void SetUp()
    {
        _target = new AsyncExceptionThrower();
    }

    [UnityTest]
    public IEnumerator NUnit1x_Throws() => UniTask.ToCoroutine(async () =>
    {
        Exception actual = null;
        try
        {
            await _target.ThrowAsync(true);
            Assert.Fail();
        }
        catch (Exception e)
        {
            actual = e;
        }

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual, Is.TypeOf<AsyncException>());
    });

    [UnityTest]
    public IEnumerator NUnit1x_DoesNotThrows() => UniTask.ToCoroutine(async () =>
    {
        Exception actual = null;
        try
        {
            await _target.ThrowAsync(false);
        }
        catch (Exception e)
        {
            actual = e;
        }

        Assert.That(actual, Is.Null);
    });

    [UnityTest]
    public IEnumerator NUnit2x_ThrowsAsync() => UniTask.ToCoroutine(async () =>
    {
        Assert.ThrowsAsync<AsyncException>(async () => await _target.ThrowAsync(true));
        await UniTask.CompletedTask;
    });

    [UnityTest]
    public IEnumerator NUnit2x_DoesNotThrowAsync() => UniTask.ToCoroutine(async () =>
    {
        Assert.DoesNotThrowAsync(async () => await _target.ThrowAsync(false));
        await UniTask.CompletedTask;
    });

    [UnityTest]
    public IEnumerator NUnit2x_That_ThrowsTypeOf() => UniTask.ToCoroutine(async () =>
    {
        Assert.That(async () => await _target.ThrowAsync(true), Throws.TypeOf<AsyncException>());
        await UniTask.CompletedTask;
    });

    [UnityTest]
    public IEnumerator NUnit2x_That_ThrowsNothing() => UniTask.ToCoroutine(async () =>
    {
        Assert.That(async () => await _target.ThrowAsync(false), Throws.Nothing);
        await UniTask.CompletedTask;
    });
}
