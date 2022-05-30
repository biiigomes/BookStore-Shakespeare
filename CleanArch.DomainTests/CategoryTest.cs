using System.Buffers;
using System.Threading.Tasks.Dataflow;
using System;
using Xunit;
using FluentAssertions;
using CleanArch.Domain.Entities;

namespace CleanArch.DomainTests
{
    public class CategoryTest
    {
        [Fact(DisplayName = "Create category with valid state")]
        public void CreateCategoryWithValidParameters()
        {
            //Arrange
            Action action = () => new Category(1, "CategoryName");
            //Act
            action.Should()
            //Assert
              .NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
        }
        [Fact(DisplayName = "Invalid category with valid state")]
        public void CreateCategoryWithNegativeId()
        {
            //Arrange
            Action action = () => new Category(-1, "CategoryName");
            //Act
            action.Should()
            //Assert
              .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
              .WithMessage("Invalid Id");
        }
        [Fact]
        public void CreateCategory_ShortName()
        {
            Action action = () => new Category(1, "Ca");
            action.Should()
                .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Too short");
        }
        [Fact]
        public void CreateCategory_MissingName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name. Name is required");
        }
    }
}
