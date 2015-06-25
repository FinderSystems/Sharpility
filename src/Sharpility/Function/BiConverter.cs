
namespace Sharpility.Function
{
    public delegate TOutput BiConverter<in TInput1, in TInput2, out TOutput>(TInput1 input1, TInput2 input2);
}
