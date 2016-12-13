using System;
using System.Reflection;

namespace MyBeerTap.IntegrationTests
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Invokes generic method.
        /// </summary>
        public static TResult InvokeGenericMethod<TResult>(object target, string methodName, Type[] typeArguments, params object[] parameters)
        {

            if (target == null)
                throw new ArgumentNullException("target");
            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentNullException("methodName");

            var resolveMethod = target.GetType().GetMethod(methodName, Type.EmptyTypes);

            try
            {
                return (TResult)
                    resolveMethod
                        .MakeGenericMethod(typeArguments)
                        .Invoke(target, parameters);
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}