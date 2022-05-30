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
        [Trait("Categoria", "ValidPam")]
        public void CreateProduct_WithValidParameters()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        [Trait("Categoria", "NegativeId")]
        public void CreateProduct_NegativeValue()
        {
            Action action = () => new Product(-1, "Product name", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid Id");
        }
        [Fact]
        [Trait("Categoria", "ShortName")]
        public void CreateProduct_ShortNameValue()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "Product image");
            action.Should()
                .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Too short");
        }
        [Fact]
        [Trait("Categoria", "NullDescription")]
        public void CreateProductNullDescription()
        {
            Action action = () => new Product(1, "Product name", null, 9.99m, 99, "Image product");
            action.Should()
               .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid description. Description is required");   
        }
        [Fact]
        [Trait("Categoria", "EmptyDescription")]
        public void CreateProductEmptyDescription()
        {
            Action action = () => new Product(1, "Product name", "De", 9.99m, 99, "Image product");
            action.Should()
               .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid description. Too short");   
        }
        [Fact]
        [Trait("Categoria", "NullValue")]
        public void CreateProductNullValue()
        {
            Action action = () => new Product(1, "Product name", "Description product", -9.9m, 99, "Image product");
            action.Should()
               .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid price value."); 
        }
        [Fact]
        [Trait("Categoria", "nullstock")]
        public void CreateProductNullStock()
        {
            Action action = () => new Product(1, "Product name", "Description product", 9.99m, -99, "Image product");
            action.Should()
               .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid stock value.");
        }
        [Fact]
        [Trait("Categoria", "NullImage")]
        public void CreateProductNullImage()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
            action.Should()
               .NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact]
        [Trait("Categoria", "NullReferenceImage")]
        public void CreateProductNullReferenceImage()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
            action.Should()
               .NotThrow<NullReferenceException>();
        }
        [Fact]
        [Trait("Categoria", "LongImage")]
        public void CreateProductLongImage()
        {
            Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "This is a loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong imageeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
            action.Should()
               .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
               .WithMessage("Invalid name. Too long");
        }
    }
}