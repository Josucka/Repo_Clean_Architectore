using Clean.Architecture.Domain.Entities;
using Clean.Architecture.Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Clean.Architecture.TestUnit
{
    public class ProductUnitTest
    {
        [Fact]
        public void CreateProduct_WithValidateParameters_ResultExceptionValidateState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Id Invalid");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "PN", "Product Description", 9.99m, 99, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Name");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, 
                "Product Image e muitos caracteres lkajiaungliowiernltkwhbiuaysbfasopirnusbdvkjllklw.l .t,ownjiuaybsdybkvjh slnriekjguyshfbksjd j,KJ LKJDBQKYBQVKN SLNDKJALKJSBKJEKB LWQNLERKT NLQEKRJLAkajdsifnoaiunldfak sjd fasdjk fkadskjfln  kjso kigSKDNFKjSDFFLJK SB KU , XKF SLKLDF QL GKNL");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Image");
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "P N", "Product Description", 9.99m, 99, null);
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_InvalidPriceValue_DomainExceptio()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Price");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value, "Product Image");
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Stock");
        }

    }
}
