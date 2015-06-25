
namespace Sharpility.Function
{
    /// <summary>
    /// Consumes input.
    /// </summary>
    /// <typeparam name="TInput">Type of input</typeparam>
    /// <param name="input">consumed input</param>
    public delegate void Consumer<in TInput>(TInput input);
}
