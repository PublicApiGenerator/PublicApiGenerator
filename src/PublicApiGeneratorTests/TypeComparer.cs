using PublicApiGenerator;

namespace PublicApiGeneratorTests;

public class TypeComparer
{
    [Theory]
    [InlineData(OrderMode.FullName)]
    [InlineData(OrderMode.NamespaceThenFullName)]
    public void Should_convert_between_enum_and_underlying_type_comparer(OrderMode orderMode)
    {
        var options = new ApiGeneratorOptions
        {
            OrderBy = orderMode
        };

        Assert.Equal(orderMode, options.OrderBy);
    }
}
