using System.Buffers;
using System.Threading.Tasks.Dataflow;
using System;
using Xunit;
using FluentAssertions;
using CleanArch.Domain.Entities;

namespace CleanArch.DomainTests
{
    public class ProductTest
    {
        [Fact]
        public void CreateProduct_WithValidParameters()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        public void CreateProduct_NegativeValue()
        {
            Action action = () => new Product(-1, "Product name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id");
        }
    }
}