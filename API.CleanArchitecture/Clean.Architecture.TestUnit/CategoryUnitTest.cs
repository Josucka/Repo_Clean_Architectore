using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Clean.Architecture.TestUnit
{
    public class CategoryUnitTest
    {
        [Fact(DisplayName = "Create Category Test")]
        public void CreateCategory_WithValidationParameters_ResultObjectValidateState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }
        
        [Fact]
        public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Id Invalid");
        }

        [Fact]
        public void CreateCategory_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Category(1, "a");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(" Invalid Name");
        }

        [Fact]
        public void CreateCategory_MissinNameValue_DomainExceptionRequeridName()
        {
            Action action = () => new Category(1, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage(" Invalid Name");
        }

        [Fact]
        public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
        {
            Action action = () => new Category(1, null);
            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}
